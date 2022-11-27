using CoffeeMaker.Adapters.StateMachines;
using CoffeeMaker.Hardware.Api;

namespace CoffeeMaker.Adapters;

public class Warmer : ICoffeeMaker
{
    private readonly CoffeeMakerStateMachine _stateMachine;

    public Warmer(CoffeeMakerStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public WarmerPlateStatus GetWarmerPlateStatus()
    {
        return _stateMachine.WarmerPlateStatus;
    }

    public void SetWarmerState(WarmerState state)
    {   
        _stateMachine.WarmerState = state;
    }

    // Below follows interface members not implemented in this adapter  

    public BrewButtonStatus GetBrewButtonStatus()
    {
        throw new NotImplementedException();
    }

    public BoilerStatus GetBoilerStatus()
    {
        throw new NotImplementedException();
    }

    public void SetBoilerState(BoilerState state)
    {
        throw new NotImplementedException();
    }

    public void SetIndicatorState(IndicatorState state)
    {
        throw new NotImplementedException();
    }

    public void SetReliefValveState(ReliefValveState state)
    {
        throw new NotImplementedException();
    }

}
