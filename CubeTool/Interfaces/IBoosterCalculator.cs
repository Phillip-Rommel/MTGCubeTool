namespace CubeTool.Interfaces
{
    public interface IBoosterCalculator
    {
        Booster[] CalculateBoosters(BoosterCalculatorSettings settings);
    }
}