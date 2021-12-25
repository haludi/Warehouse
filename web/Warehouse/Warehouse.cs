namespace web.Warehouse;

public class Warehouse
{
    public static Warehouse Instance = new Warehouse((10, 10));

// Chilled: milk, yogurt, cheese, insulin
// Hazardous: bleach, stain removal, insulin
// Others: bread, pasta, salt, bamba, apple


    private (int columnCound, int rowCount) _size;
    private Dictionary<string, HashSet<string>> ProductRestrictionPerProduct = new Dictionary<string, HashSet<string>>
    {
        {"milk", new HashSet<string>(StringComparer.OrdinalIgnoreCase) {"Chilled"}},
        {"yogurt", new HashSet<string>(StringComparer.OrdinalIgnoreCase) {"Chilled"}},
        {"cheese", new HashSet<string>(StringComparer.OrdinalIgnoreCase) {"Chilled"}},
        {"insulin", new HashSet<string>(StringComparer.OrdinalIgnoreCase) {"Chilled", "Hazardous"}},
        {"bleach", new HashSet<string>(StringComparer.OrdinalIgnoreCase) {"Hazardous"}},
        {"stain", new HashSet<string>(StringComparer.OrdinalIgnoreCase) {"Hazardous"}},
        {"removal", new HashSet<string>(StringComparer.OrdinalIgnoreCase) {"Hazardous"}},
        {"bread", new HashSet<string>(StringComparer.OrdinalIgnoreCase)},
        {"pasta", new HashSet<string>(StringComparer.OrdinalIgnoreCase)},
        {"salt", new HashSet<string>(StringComparer.OrdinalIgnoreCase)},
        {"bamba", new HashSet<string>(StringComparer.OrdinalIgnoreCase)},
        {"apple", new HashSet<string>(StringComparer.OrdinalIgnoreCase)},
    }; 

    private Cell[] _cells;
    private List<(List<Func<string, bool>>, Cell[])> indexes = new List<(List<Func<string, bool>>, Cell[])>();

    public Warehouse((int columnCound, int rowCount) size)
    {
        _size = size;
        _cells = new Cell[size.rowCount * size.columnCound];
        for (int row = 0; row < size.rowCount; row++)
        {
            for (int column = 0; column < size.columnCound; column++)
            {
                _cells[row * _size.columnCound +  column] = new Cell(row, column, 10);
            }
        }

        DefineCellPropretiesForColumnt("hazardous", 9);
        for (int i = 7; i <= 9; i++)
        {
            DefineCellPropretiesForColumnt("cooling", i, 7, 9);
        }
    }

    private Cell GetCell(int row, int column)
    {
        return _cells[row * _size.columnCound + column];
    }

    private void DefineCellPropretiesForColumnt(string prop, int column, int start = 0, int? end = null)
    {
        if(column < 0 || column >= _size.columnCound)
            throw new Exception($"Column out of bondary {column}, Column count {_size.columnCound}");

        if(start < 0 || start > _size.rowCount -1)
            throw new Exception($"start out of bondary {start}, Row count {_size.rowCount}");

        if(end.HasValue)
        {
            if(end < 0 || end > _size.rowCount -1)
                throw new Exception($"End out of bondary {end}, Row count {_size.rowCount}");
            
            if(end < start)
                throw new Exception($"end({end}) < start({end})");
        }

        var localEnd = end.HasValue? end.Value: _size.rowCount - 1;
            
        for (int row = start; row < localEnd; row++)
        {
            var cell = GetCell(row, column);
            cell.Properties.Add(prop);
        }
    }

    private bool DefaultRestriction(string product)
    {
        if(ProductRestrictionPerProduct.TryGetValue(product, out var productTags) == false)
            throw new Exception("The product doesn't exist");

        return productTags.Any() == false;
    }
    
    public AllocateCellResult Alloc(AllocateCellParameters request)
    {
        foreach (var cell in GetAvailableCells(request.ProductId))
        {
            if(cell.Capacity - cell.Count >= request.Quantity)
            {
                cell.Count += request.Quantity;
                cell.ProductType = request.ProductId;
                return new AllocateCellResult
                {
                    Cell = $"{cell.Location.Colomn},{cell.Location.Row}"
                };
            }
        }
        return new AllocateCellResult();
    }

    private IEnumerable<Cell> GetAvailableCells(string productType)
    {
        if(ProductRestrictionPerProduct.TryGetValue(productType, out var restrictions) == false)
            throw new Exception($"Unknown product {productType}");

        var list = _cells.Where(c =>
        {
            if(c.Count == c.Capacity)
                return false;
            if(c.ProductType != productType)
                return false;

            return true;
        });

        return list.Concat(_cells.Where(c => 
        {
            if(c.Count != 0)
                return false;

            if(c.Properties.Count() != restrictions.Count())
                return false;

            foreach (var item in restrictions)
            {
                if(c.Properties.Contains(item) == false)
                    return false;
            }
            return true;
        }));
    }
}

public class AllocateCellParameters
{
    public string ProductId { get; set; }
    public int Quantity { get; set; }
}

public class AllocateCellResult
{
    public string Cell { get; set; }
}

public class Cell
{
    public Cell(int row, int column, int capacity)
    {
        Location = new CellLocation(row, column);
        Capacity = capacity;
    }

    public CellLocation Location {get;}
    public int Capacity {get;}
    public HashSet<string> Properties {get;} = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
 
    public int Count { get; set; }
    public string ProductType {get; set; }
}

public class CellLocation
{
    public CellLocation(int row, int column)
    {
        Row = row;
        Colomn = column;
    }
    public int Row { get; set; }
    public int Colomn { get; set; }
}