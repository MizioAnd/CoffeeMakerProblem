using CoffeeMaker.Hardware.Api;

namespace CoffeeMaker.Adapters.StateMachines;

public class CoffeeMakerStateMachine
{
    private BrewButtonStatus brewButton = BrewButtonStatus.NOT_PUSHED;

    // Assume the following
    // 1. filter in filter holder and filled with coffee grounds and that is slided into its receptacle
    // 2. water has already been poured into the water strainer
    // 3. we only need to press the brew button in order to start the brewing cycle

    public WarmerState WarmerState { get; set; } = WarmerState.OFF;
    public WarmerPlateStatus WarmerPlateStatus { get; set; } = WarmerPlateStatus.WARMER_EMPTY;
    public PotStatus PotStatus { get; set; } = PotStatus.POT_EMPTY;
    public BrewButtonStatus BrewButton 
    { 
        get => brewButton; 
        set 
        {
            brewButton = value;
            if (value == BrewButtonStatus.PUSHED)
                StartBrewingCycle();
        } 
    }
    public void StartBrewingCycle()
    {
        // TODO: maybe subscribe to field BrewButton
        throw new NotImplementedException();
    }
}

public enum PotStatus
{
    POT_EMPTY, 
    POT_NOT_EMPTY
};
