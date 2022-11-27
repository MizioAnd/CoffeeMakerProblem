using CoffeeMaker.Hardware.Api;

namespace CoffeeMaker.Adapters;

public class Boiler : ICoffeeMaker
{
    public BoilerStatus GetBoilerStatus()
    {
        throw new NotImplementedException();
    }

    public BrewButtonStatus GetBrewButtonStatus()
    {
        throw new NotImplementedException();
    }

    public void SetReliefValveState(ReliefValveState state)
    {
        throw new NotImplementedException();
    }

    public void SetBoilerState(BoilerState state)
    {
        throw new NotImplementedException();
    }

    // Below follows interface members not implemented in this adapter

    public WarmerPlateStatus GetWarmerPlateStatus()
    {
        // Boilerplate code
        throw new NotImplementedException();
    }

    public void SetIndicatorState(IndicatorState state)
    {
        // Boilerplate code
        throw new NotImplementedException();
    }


    public void SetWarmerState(WarmerState state)
    {
        // Boilerplate code
        throw new NotImplementedException();
    }

}
