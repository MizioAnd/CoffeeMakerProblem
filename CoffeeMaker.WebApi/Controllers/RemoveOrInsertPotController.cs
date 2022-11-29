using Microsoft.AspNetCore.Mvc;
using CoffeeMaker.Adapters;
using CoffeeMaker.Hardware.Api;
using CoffeeMaker.Adapters.StateMachines;

namespace CoffeeMaker.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class RemoveOrInsertPotController : ControllerBase
{
    private readonly ILogger<RemoveOrInsertPotController> _logger;
    private readonly Warmer _warmer;
    private readonly CoffeeMakerStateMachine _stateMachine;

    public RemoveOrInsertPotController(ILogger<RemoveOrInsertPotController> logger, Warmer warmer, CoffeeMakerStateMachine stateMachine)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _warmer = warmer ?? throw new ArgumentNullException(nameof(warmer));
        _stateMachine = stateMachine ?? throw new ArgumentNullException(nameof(stateMachine));
    }

    [HttpGet(Name = "GetRemoveOrInsertPot")]
    public async Task<ActionResult<string>> Get()
    {
        return  Ok(_warmer.GetWarmerPlateStatus().ToString());
    }

    [HttpPost(Name = "PostRemoveOrInsertPot")]
    public async Task<ActionResult<string>> Post()
    {
        if (_warmer.GetWarmerPlateStatus() is WarmerPlateStatus.POT_EMPTY || _warmer.GetWarmerPlateStatus() is WarmerPlateStatus.POT_NOT_EMPTY)
            _stateMachine.WarmerPlateStatus = WarmerPlateStatus.WARMER_EMPTY;
        else if (_stateMachine.PotStatus == PotStatus.POT_EMPTY)
            _stateMachine.WarmerPlateStatus = WarmerPlateStatus.POT_EMPTY;
        else if (_stateMachine.PotStatus == PotStatus.POT_NOT_EMPTY)
            _stateMachine.WarmerPlateStatus = WarmerPlateStatus.POT_NOT_EMPTY;

        return  Ok(_stateMachine.WarmerPlateStatus.ToString());
    }
}
