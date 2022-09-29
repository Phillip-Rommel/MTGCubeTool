using System;
namespace MTGCubeTool.Domain.Models
{
    public class Booster
    {
        public int White { get; set; }
        public int Blue { get; set; }
        public int Red { get; set; }
        public int Green { get; set; }
        public int Black { get; set; }
        public int MultiColor { get; set; }
        public int Land { get; set; }
        public int Colorless { get; set; }

        public int GetTotalCards()
        {
            return Black + Blue + Green + Red + White + MultiColor + Colorless + Land;
        }
    }
}
