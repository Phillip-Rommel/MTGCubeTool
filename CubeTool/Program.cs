using CubeTool.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CubeTool
{
    internal class Program
    {
        private static int _totalCubeCards { get; set; }
        private static int _cardsPerBooster { get; set; }
        private static int _totalBoosters { get; set; }
        private static int _totalWhiteCards { get; set; }
        private static int _totalBlackCards { get; set; }
        private static int _totalBlueCards { get; set; }
        private static int _totalRedCards { get; set; }
        private static int _totalGreenCards { get; set; }
        private static int _totalMultiColorCards { get; set; }
        private static int _totalLandCards { get; set; }
        private static int _totalColorlessCards { get; set; }

        private static Random _random = new();

        private static IBoosterCalculator _boosterCalculator;

        private static void Main(string[] args)
        {
            _boosterCalculator = new BoosterCalculator();

            Console.WriteLine("Welcome to CubeTool");

            Console.WriteLine("Enter the number of cards per booster:");
            _cardsPerBooster = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter the total number of boosters:");
            _totalBoosters = Convert.ToInt32(Console.ReadLine());

            _totalCubeCards = _totalBoosters * _cardsPerBooster;

            Console.WriteLine("Enter the total number of black cards:");
            _totalBlackCards = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter the total number of blue cards:");
            _totalBlueCards = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter the total number of green cards:");
            _totalGreenCards = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter the total number of red cards:");
            _totalRedCards = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter the total number of white cards:");
            _totalWhiteCards = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter the total number of multicolor cards:");
            _totalMultiColorCards = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter the total number of coloreless cards:");
            _totalColorlessCards = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter the total number of land cards:");
            _totalLandCards = Convert.ToInt32(Console.ReadLine());

            if (CalculateTotalEnteredCards() != _totalCubeCards)
            {
                Console.WriteLine("Please check your entered numbers. They seem not to fit to the toal amount of cards.");
                return;
            }

            Console.WriteLine("Here is your cube:");
            Console.WriteLine($"Total cards:{_totalCubeCards}");
            Console.WriteLine($"Cards per booster:{_cardsPerBooster}");
            Console.WriteLine($"Total boosters:{_totalBoosters}");
            Console.WriteLine($"Total black cards:{_totalBlackCards}");
            Console.WriteLine($"Total blue cards:{_totalBlueCards}");
            Console.WriteLine($"Total green cards:{_totalGreenCards}");
            Console.WriteLine($"Total red cards:{_totalRedCards}");
            Console.WriteLine($"Total white cards:{_totalWhiteCards}");
            Console.WriteLine($"Total multicolor cards:{_totalMultiColorCards}");
            Console.WriteLine($"Total colorless cards:{_totalColorlessCards}");
            Console.WriteLine($"Total land cards:{_totalLandCards}");

            var boosterCalculatorSettings = new BoosterCalculatorSettings()
            {
                TotalBoosters = _totalBoosters,
                TotalCardsPerBooster = _cardsPerBooster,
                TotalCardsBooster = new Booster()
                {
                    Black = _totalBlackCards,
                    Blue = _totalBlueCards,
                    Colorless = _totalColorlessCards,
                    Green = _totalGreenCards,
                    Land = _totalLandCards,
                    MultiColor = _totalMultiColorCards,
                    Red = _totalRedCards,
                    White = _totalWhiteCards
                }
            };

            var boosters = _boosterCalculator.CalculateBoosters(boosterCalculatorSettings);
            PrintBoosters(boosters);
            PrintToCsv(boosters);
        }

        private static int CalculateTotalEnteredCards()
        {
            return
            _totalBlackCards +
            _totalBlueCards +
            _totalColorlessCards +
            _totalGreenCards +
            _totalLandCards +
            _totalMultiColorCards +
            _totalRedCards +
            _totalWhiteCards;
        }

        private static void PrintToCsv(Booster[] boosters)
        {
            var stringWriter = new StringWriter();

            stringWriter.WriteLine("Booster;Black;Blue;Green;Red;White;Multicolored;Colorless;Lands");

            for (int i = 1; i < boosters.Length + 1; i++)
            {
                var booster = boosters[i - 1];
                stringWriter.WriteLine($"{i};{booster.Black};{booster.Blue};{booster.Green};{booster.Red};{booster.White};{booster.MultiColor};{booster.Colorless};{booster.Land}");
            }

            string filePath = @"C:\CubeTool\cube.csv";

            Console.WriteLine(filePath);

            File.WriteAllText(filePath, stringWriter.ToString());
        }

        private static void PrintBoosters(Booster[] boosters)
        {
            int summedBoosters = 0;

            for (int i = 0; i < boosters.Length; i++)
            {
                var booster = boosters[i];

                Console.WriteLine($"Booster Number {i}: " +
                    $"Black:{booster.Black} " +
                    $"Blue:{booster.Blue} " +
                    $"Green:{booster.Green} " +
                    $"Red:{booster.Red} " +
                    $"White:{booster.White} " +
                    $"Colorless:{booster.Colorless} " +
                    $"Multicolor:{booster.MultiColor} " +
                    $"Lands:{booster.Land}" +
                    $"Total: {booster.GetTotalCards()}");

                summedBoosters += booster.GetTotalCards();
            }

            Console.WriteLine($"Total Cards in boosters: {summedBoosters}");
        }
    }
}