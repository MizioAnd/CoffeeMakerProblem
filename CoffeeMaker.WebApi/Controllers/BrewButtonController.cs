using Microsoft.AspNetCore.Mvc;
using CoffeeMaker.Hardware.Api;
using CoffeeMaker.Adapters;
using CoffeeMaker.Adapters.StateMachines;

namespace CoffeeMaker.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class BrewButtonController : ControllerBase
{
    private readonly ILogger<BrewButtonController> _logger;
    private readonly BrewButton _brewButton;
    private readonly CoffeeMakerStateMachine _stateMachine;

    public BrewButtonController(ILogger<BrewButtonController> logger, BrewButton brewButton, CoffeeMakerStateMachine stateMachine)
    {
        _logger = logger;
        _brewButton = brewButton;
        _stateMachine = stateMachine;
    }

    [HttpGet(Name = "GetBrewButton")]
    public string Get()
    {
        return _brewButton.GetBrewButtonStatus().ToString();
    }

    [HttpPost(Name = "PostBrewButton")]
    public string Post()
    {
        if (_brewButton.GetBrewButtonStatus() == BrewButtonStatus.NOT_PUSHED)
            _stateMachine.BrewButton = BrewButtonStatus.PUSHED;
        else
            _stateMachine.BrewButton = BrewButtonStatus.NOT_PUSHED;

        return  _stateMachine.BrewButton.ToString();
    }
}