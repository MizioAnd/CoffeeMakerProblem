using CoffeeMaker.Adapters.StateMachines;
using CoffeeMaker.Hardware.Api;

namespace CoffeeMaker.Adapters;

public class Boiler : ICoffeeMaker
{
    private readonly CoffeeMakerStateMachine _stateMachine;

    public Boiler(CoffeeMakerStateMachine stateMachine)
    {
        _stateMachine = stateMachine ?? throw new ArgumentNullException(nameof(stateMachine));
    }

    public BoilerStatus GetBoilerStatus()
    {
        return _stateMachine.BoilerStatus;
    }

    public void SetReliefValveState(ReliefValveState state)
    {
        _stateMachine.ReliefValveState = state;
    }

    public void SetBoilerState(BoilerState state)
    {
        _stateMachine.BoilerState = state;
    }

    // Below follows interface members not implemented in this adapter

    public BrewButtonStatus GetBrewButtonStatus()
    {
        throw new NotImplementedException();
    }

    public WarmerPlateStatus GetWarmerPlateStatus()
    {
        throw new NotImplementedException();
    }

    public void SetIndicatorState(IndicatorState state)
    {
        throw new NotImplementedException();
    }


    public void SetWarmerState(WarmerState state)
    {
        throw new NotImplementedException();
    }

}
