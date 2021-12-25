using Microsoft.AspNetCore.Mvc;
using web.Warehouse;

namespace web.Controllers;

[ApiController]
[Route("api")]
public class WarehouseController : ControllerBase
{
    private readonly ILogger<WarehouseController> _logger;

    public WarehouseController(ILogger<WarehouseController> logger)
    {
        _logger = logger;
    }

    [Route("allocateCell")]
    [HttpPost]
    public async Task<AllocateCellRespond> AllocateCell([FromBody]AllocateCellRequest allocateCellRequest)
    {
        var result = Warehouse.Warehouse.Instance.Alloc(new AllocateCellParameters
        {
            ProductId = allocateCellRequest.ProductId,
            Quantity = allocateCellRequest.Quantity

        });
        return new AllocateCellRespond
        {
            Cell = result.Cell,
            FoundCell = result.Cell != null
        };
    }
}

public class AllocateCellRequest
{
    public string ProductId { get; set; }
    public int Quantity { get; set; }
}

public class AllocateCellRespond
{
    public bool FoundCell { get; set; }
    public string Cell { get; set; }
}
