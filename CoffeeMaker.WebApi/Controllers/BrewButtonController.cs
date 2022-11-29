using Microsoft.AspNetCore.Mvc;
using CoffeeMaker.Hardware.Api;
using CoffeeMaker.Adapters;
using CoffeeMaker.Adapters.StateMachines;
using CoffeeMaker.WebApi.Services;

namespace CoffeeMaker.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class BrewButtonController : ControllerBase
{
    private readonly ILogger<BrewButtonController> _logger;
    private readonly BrewButton _brewButton;
    private readonly CoffeeMakerStateMachine _stateMachine;
    private readonly BrewingCycleService _brewingCycleService;

    public BrewButtonController(ILogger<BrewButtonController> logger, BrewButton brewButton, CoffeeMakerStateMachine stateMachine, BrewingCycleService brewingCycleService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _brewButton = brewButton ?? throw new ArgumentNullException(nameof(brewButton));
        _stateMachine = stateMachine ?? throw new ArgumentNullException(nameof(stateMachine));
        _brewingCycleService = brewingCycleService ?? throw new ArgumentNullException(nameof(brewingCycleService));
    }

    [HttpGet(Name = "GetBrewButton")]
    public async Task<ActionResult<string>> Get()
    {
        return Ok(_brewButton.GetBrewButtonStatus().ToString());
    }

    [HttpPost(Name = "PostBrewButton")]
    public async Task<ActionResult<string>> Post()
    {
        if (_brewButton.GetBrewButtonStatus() == BrewButtonStatus.NOT_PUSHED)
        {
            _stateMachine.BrewButton = BrewButtonStatus.PUSHED;
            _brewingCycleService.StartBrewingCycle();
        }
        else
        {
            _stateMachine.BrewButton = BrewButtonStatus.NOT_PUSHED;
            // TODO: clear values written to _stateMachine instance
        }

        return  Ok(_stateMachine.BrewButton.ToString());
    }
}
