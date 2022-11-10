namespace CubeTool
{
    public class BoosterCalculatorSettings
    {
        public int TotalBoosters { get; set; }
        public int TotalCardsPerBooster { get; set; }
        public Booster TotalCardsBooster { get; set; } = new();
    }
}