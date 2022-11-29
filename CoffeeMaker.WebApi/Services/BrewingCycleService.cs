using CoffeeMaker.Adapters;
using CoffeeMaker.Adapters.StateMachines;
using CoffeeMaker.Hardware.Api;

namespace CoffeeMaker.WebApi.Services;

public class BrewingCycleService
{
    private readonly CoffeeMakerStateMachine _stateMachine;
    private readonly Boiler _boiler;
    private readonly BrewButton _brewButton;
    private readonly Warmer _warmer;

    public BrewingCycleService(CoffeeMakerStateMachine stateMachine, Boiler boiler, BrewButton brewButton, Warmer warmer)
    {
        _stateMachine = stateMachine ?? throw new ArgumentNullException(nameof(stateMachine));
        _boiler = boiler ?? throw new ArgumentNullException(nameof(boiler));
        _brewButton = brewButton ?? throw new ArgumentNullException(nameof(brewButton));
        _warmer = warmer ?? throw new ArgumentNullException(nameof(warmer));
    }

    public void StartBrewingCycle()
    {
        // Place empty Pot on warmer plate
        _stateMachine.WarmerPlateStatus = WarmerPlateStatus.POT_EMPTY;
        
        // Indicator light is off during brewing cycle
        // _stateMachine.IndicatorState = IndicatorState.OFF;
        _brewButton.SetIndicatorState(IndicatorState.OFF);
        _stateMachine.UpdateBrewingCycleStep("1. Place empty Pot on warmer plate and indicator light is off during brewing cycle");
        
        // 2. Water has been filled into boiler and boiler elements start heating water
        _stateMachine.BoilerStatus = BoilerStatus.NOT_EMPTY;

        // Turn heating in boiler on
        // _stateMachine.BoilerState = BoilerState.ON;
        _boiler.SetBoilerState(BoilerState.ON);
        _stateMachine.UpdateBrewingCycleStep("2. Water has been filled into boiler and boiler elements start heating water");

        // 3. pressure Relief Valve is closed and Coffee starts to spray over the coffee filter (if valve is open the steam will just spray into environment instead of over the filter, this should happen in case the can is remove)
        // _stateMachine.ReliefValveState = ReliefValveState.CLOSED;
        _boiler.SetReliefValveState(ReliefValveState.CLOSED);
        // TODO: If Pot is removed the reliefValveState should be set to Open such that no water gets sprayed onto coffee grounds (needs to be an on-going check all time)
        // and brewing is on hold until Pot is returned on warmer plate
        _stateMachine.UpdateBrewingCycleStep("3. pressure Relief Valve is closed and Coffee starts to spray over the coffee filter (if valve is open the steam will just spray into environment instead of over the filter, this should happen in case the can is remove)");

        // 4. sensors detect that Pot now contains coffee and heating element/warmer for Pot is turned on
        _stateMachine.WarmerPlateStatus = WarmerPlateStatus.POT_NOT_EMPTY;
        // _stateMachine.WarmerState = WarmerState.ON;
        _warmer.SetWarmerState(WarmerState.ON);
        _stateMachine.UpdateBrewingCycleStep("4. sensors detect that Pot now contains coffee and heating element/warmer for Pot is turned on");

        // 5. water in boiler is empty and boilerbutton is switched off
        _stateMachine.BoilerStatus = BoilerStatus.EMPTY;
        // _stateMachine.BoilerState = BoilerState.OFF;
        _boiler.SetBoilerState(BoilerState.OFF);
        _stateMachine.UpdateBrewingCycleStep("5. water in boiler is empty and boilerbutton is switched off");

        // 6. brewing has finished and so indicator light is turned on
        // _stateMachine.IndicatorState = IndicatorState.ON;
        _brewButton.SetIndicatorState(IndicatorState.ON);
        _stateMachine.UpdateBrewingCycleStep("6. brewing has finished and so indicator light is turned on");

        // 7. heating element of Pot still detects that Pot is non-empty and is on
        _stateMachine.UpdateBrewingCycleStep("7. heating element of Pot still detects that Pot is non-empty and is on");

        // 8. Pot gets removed and heating element/warmer detects that no Pot is placed on it and switches off
        _stateMachine.WarmerPlateStatus = WarmerPlateStatus.WARMER_EMPTY;
        // _stateMachine.WarmerState = WarmerState.OFF;
        _warmer.SetWarmerState(WarmerState.OFF);
        _stateMachine.UpdateBrewingCycleStep("8. Pot gets removed and heating element/warmer detects that no Pot is placed on it and switches off");
    }
}
