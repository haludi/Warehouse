using Xunit;
using web.Warehouse;

namespace tests;

public class HazardousProductUnitTest
{
    [Fact]
    public void HazardousProductTest1()
    {
        var result = Warehouse.Instance.Alloc(new AllocateCellParameters
        {
            ProductId = "bleach",
            Quantity = 2
        });

        Assert.NotNull(result.Cell);
        Assert.Equal(result.Cell, "9,0");

        var result2 = Warehouse.Instance.Alloc(new AllocateCellParameters
        {
            ProductId = "bleach",
            Quantity = 2
        });

        Assert.NotNull(result2.Cell);
        Assert.Equal(result2.Cell, "9,0");
    }
    
    [Fact]
    public void HazardousProductTest2()
    {
        var result = Warehouse.Instance.Alloc(new AllocateCellParameters
        {
            ProductId = "bleach",
            Quantity = 10
        });

        Assert.NotNull(result.Cell);
        Assert.Equal(result.Cell, "9,0");

        var result2 = Warehouse.Instance.Alloc(new AllocateCellParameters
        {
            ProductId = "bleach",
            Quantity = 2
        });

        Assert.NotNull(result2.Cell);
        Assert.Equal("9,1", result2.Cell);
    }

    [Fact]
    public void HazardousProductTest3()
    {
        var result = Warehouse.Instance.Alloc(new AllocateCellParameters
        {
            ProductId = "bleach",
            Quantity = 2
        });

        Assert.NotNull(result.Cell);
        Assert.Equal( "9,0", result.Cell);

        var result2 = Warehouse.Instance.Alloc(new AllocateCellParameters
        {
            ProductId = "stain",
            Quantity = 2
        });

        Assert.NotNull(result2.Cell);
        Assert.Equal( "9,1", result2.Cell);

        var result3 = Warehouse.Instance.Alloc(new AllocateCellParameters
        {
            ProductId = "stain",
            Quantity = 2
        });

        Assert.NotNull(result3.Cell);
        Assert.Equal( "9,1", result3.Cell);
    }
}