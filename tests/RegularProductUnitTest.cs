using Xunit;
using web.Warehouse;

namespace tests;

public class RegularProductUnitTest
{
    [Fact]
    public void RegularProductTest1()
    {
        var result = Warehouse.Instance.Alloc(new AllocateCellParameters
        {
            ProductId = "bread",
            Quantity = 2
        });

        Assert.NotNull(result.Cell);
        Assert.Equal(result.Cell, "0,0");

        var result2 = Warehouse.Instance.Alloc(new AllocateCellParameters
        {
            ProductId = "bread",
            Quantity = 2
        });

        Assert.NotNull(result2.Cell);
        Assert.Equal(result2.Cell, "0,0");
    }
    
    [Fact]
    public void RegularProductTest2()
    {
        var result = Warehouse.Instance.Alloc(new AllocateCellParameters
        {
            ProductId = "bread",
            Quantity = 10
        });

        Assert.NotNull(result.Cell);
        Assert.Equal(result.Cell, "0,0");

        var result2 = Warehouse.Instance.Alloc(new AllocateCellParameters
        {
            ProductId = "bread",
            Quantity = 2
        });

        Assert.NotNull(result2.Cell);
        Assert.Equal("1,0", result2.Cell);
    }

    [Fact]
    public void RegularProductTest3()
    {
        var result = Warehouse.Instance.Alloc(new AllocateCellParameters
        {
            ProductId = "bread",
            Quantity = 2
        });

        Assert.NotNull(result.Cell);
        Assert.Equal( "0,0", result.Cell);

        var result2 = Warehouse.Instance.Alloc(new AllocateCellParameters
        {
            ProductId = "pasta",
            Quantity = 2
        });

        Assert.NotNull(result2.Cell);
        Assert.Equal( "1,0", result2.Cell);

        var result3 = Warehouse.Instance.Alloc(new AllocateCellParameters
        {
            ProductId = "pasta",
            Quantity = 2
        });

        Assert.NotNull(result3.Cell);
        Assert.Equal( "1,0", result3.Cell);
    }
    
    [Fact]
    public void RegularProductTest4()
    {
        var result = Warehouse.Instance.Alloc(new AllocateCellParameters
        {
            ProductId = "bread",
            Quantity = 11
        });
        Assert.Null(result.Cell);
    }
    
    [Fact]
    public void RegularProductTest5()
    {
        for (int i = 0; i < 84; i++)
        {
            Assert.NotNull(Warehouse.Instance.Alloc(new AllocateCellParameters
            {
                ProductId = "bread",
                Quantity = 10
            }));
        }

        var result = Warehouse.Instance.Alloc(new AllocateCellParameters
        {
            ProductId = "bread",
            Quantity = 10
        });
        Assert.Null(result.Cell);
    }
}
