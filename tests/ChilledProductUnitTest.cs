using Xunit;
using web.Warehouse;

namespace tests;

public class ChilledProductUnitTest
{
    [Fact]
    public void ChilledProductTest1()
    {
        var result = Warehouse.Instance.Alloc(new AllocateCellParameters
        {
            ProductId = "milk",
            Quantity = 2
        });

        Assert.NotNull(result.Cell);
        Assert.Equal(result.Cell, "7,7");

        var result2 = Warehouse.Instance.Alloc(new AllocateCellParameters
        {
            ProductId = "milk",
            Quantity = 2
        });

        Assert.NotNull(result2.Cell);
        Assert.Equal(result2.Cell, "7,7");
    }
    
    [Fact]
    public void ChilledProductTest2()
    {
        var result = Warehouse.Instance.Alloc(new AllocateCellParameters
        {
            ProductId = "milk",
            Quantity = 10
        });

        Assert.NotNull(result.Cell);
        Assert.Equal(result.Cell, "7,7");

        var result2 = Warehouse.Instance.Alloc(new AllocateCellParameters
        {
            ProductId = "milk",
            Quantity = 2
        });

        Assert.NotNull(result2.Cell);
        Assert.Equal("7,8", result2.Cell);
    }

    [Fact]
    public void ChilledProductTest3()
    {
        var result = Warehouse.Instance.Alloc(new AllocateCellParameters
        {
            ProductId = "milk",
            Quantity = 2
        });

        Assert.NotNull(result.Cell);
        Assert.Equal( "7,7", result.Cell);

        var result2 = Warehouse.Instance.Alloc(new AllocateCellParameters
        {
            ProductId = "yogurt",
            Quantity = 2
        });

        Assert.NotNull(result2.Cell);
        Assert.Equal( "7,8", result2.Cell);

        var result3 = Warehouse.Instance.Alloc(new AllocateCellParameters
        {
            ProductId = "yogurt",
            Quantity = 2
        });

        Assert.NotNull(result3.Cell);
        Assert.Equal( "7,8", result3.Cell);
    }
}
