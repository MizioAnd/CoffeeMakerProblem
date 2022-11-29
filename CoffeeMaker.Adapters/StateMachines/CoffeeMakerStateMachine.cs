using CoffeeMaker.Hardware.Api;

namespace CoffeeMaker.Adapters.StateMachines;

public class CoffeeMakerStateMachine
{
    // Assume the following
    // 1. filter in filter holder and filled with coffee grounds and that is slided into its receptacle
    // 2. water has already been poured into the water strainer
    // 3. we only need to press the brew button in order to start the brewing cycle

    private string brewingCycleStep = "";
    public string BrewingCycleStep { get => brewingCycleStep; set => brewingCycleStep = value; }

    private BoilerStatus boilerStatus = BoilerStatus.EMPTY;
    public BoilerStatus BoilerStatus { get => boilerStatus; set => boilerStatus = value; }

    private BoilerState boilerState = BoilerState.OFF;
    public BoilerState BoilerState { get => boilerState; set => boilerState = value; }

    private ReliefValveState reliefValveState = ReliefValveState.OPEN;
    public ReliefValveState ReliefValveState { get => reliefValveState; set => reliefValveState = value; }

    private WarmerState warmerState = WarmerState.OFF;
    public WarmerState WarmerState { get => warmerState; set => warmerState = value; }

    private WarmerPlateStatus warmerPlateStatus = WarmerPlateStatus.WARMER_EMPTY;
    public WarmerPlateStatus WarmerPlateStatus
    {
        get => warmerPlateStatus;
        set
        {
            warmerPlateStatus = value;
            if (warmerPlateStatus == WarmerPlateStatus.POT_EMPTY)
                potStatus = PotStatus.POT_EMPTY;
            else if (warmerPlateStatus == WarmerPlateStatus.POT_NOT_EMPTY)
                potStatus = PotStatus.POT_NOT_EMPTY;
        }
    }

    private PotStatus potStatus = PotStatus.POT_EMPTY;
    public PotStatus PotStatus { get => potStatus; set => potStatus = value; }
    
    private IndicatorState indicatorState = IndicatorState.ON;
    public IndicatorState IndicatorState { get => indicatorState; set => indicatorState = value; }

    private BrewButtonStatus brewButton = BrewButtonStatus.NOT_PUSHED;
    public BrewButtonStatus BrewButton
    {
        get => brewButton;
        set
        {
            brewButton = value;
        }
    }

    public void UpdateBrewingCycleStep(string msg)
    {
        brewingCycleStep = msg;
        // Pause every time we update the brewing step such that progress can be followed using request to BrewingCycleStatus end point
        Thread.Sleep(5000);
    }
}

public enum PotStatus
{
    POT_EMPTY, 
    POT_NOT_EMPTY
};
