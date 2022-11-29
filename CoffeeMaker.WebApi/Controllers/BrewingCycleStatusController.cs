using Microsoft.AspNetCore.Mvc;
using CoffeeMaker.Adapters;
using CoffeeMaker.Adapters.StateMachines;
using CoffeeMaker.WebApi.Services;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CoffeeMaker.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class BrewingCycleStatusController : ControllerBase
{
    private readonly ILogger<BrewingCycleStatusController> _logger;
    private readonly CoffeeMakerStateMachine _stateMachine;
    private JsonSerializerOptions options;

    public BrewingCycleStatusController(ILogger<BrewingCycleStatusController> logger, BrewButton brewButton, CoffeeMakerStateMachine stateMachine, BrewingCycleService brewingCycleService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _stateMachine = stateMachine ?? throw new ArgumentNullException(nameof(stateMachine));
        options = new JsonSerializerOptions
        {
            Converters = { new JsonStringEnumConverter() }
        };
    }

    [HttpGet(Name = "GetBrewingCycleStatus")]
    public async Task<ActionResult<string>> Get()
    {
        return Ok(JsonSerializer.Serialize(_stateMachine, options));
    }
}
