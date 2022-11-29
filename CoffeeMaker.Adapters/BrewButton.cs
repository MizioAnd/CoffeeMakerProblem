using CoffeeMaker.Adapters.StateMachines;
using CoffeeMaker.Hardware.Api;

namespace CoffeeMaker.Adapters;

public class BrewButton : ICoffeeMaker
{
    private readonly CoffeeMakerStateMachine _stateMachine;

    public BrewButton(CoffeeMakerStateMachine stateMachine)
    {
        _stateMachine = stateMachine ?? throw new ArgumentNullException(nameof(stateMachine));
    }

    public BrewButtonStatus GetBrewButtonStatus()
    {
        return _stateMachine.BrewButton;
    }

    public void SetIndicatorState(IndicatorState state)
    {
        _stateMachine.IndicatorState = state;
    }

    // Below follows interface members not implemented in this adapter

    public BoilerStatus GetBoilerStatus()
    {
        throw new NotImplementedException();
    }

    public WarmerPlateStatus GetWarmerPlateStatus()
    {
        throw new NotImplementedException();
    }

    public void SetBoilerState(BoilerState state)
    {
        throw new NotImplementedException();
    }


    public void SetReliefValveState(ReliefValveState state)
    {
        throw new NotImplementedException();
    }

    public void SetWarmerState(WarmerState state)
    {
        throw new NotImplementedException();
    }
}
