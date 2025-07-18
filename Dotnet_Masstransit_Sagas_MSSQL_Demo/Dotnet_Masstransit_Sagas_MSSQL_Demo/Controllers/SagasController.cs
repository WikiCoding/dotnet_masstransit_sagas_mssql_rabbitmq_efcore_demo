using Dotnet_Masstransit_Sagas_MSSQL_Demo.Producer;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet_Masstransit_Sagas_MSSQL_Demo.Controllers;

[ApiController]
[Route("[controller]")]
public class SagasController : ControllerBase
{
    private readonly ILogger<SagasController> _logger;
    private readonly ProducerService _producerService;

    public SagasController(ILogger<SagasController> logger, ProducerService producerService)
    {
        _logger = logger;
        _producerService = producerService;
    }

    [HttpGet]
    public async Task<IActionResult> Start()
    {
        await _producerService.ExecuteAsync();
        return Ok();
    }
}
