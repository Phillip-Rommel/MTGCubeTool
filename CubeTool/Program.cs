using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CubeTool
{
    class Program
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

        private static Random _random = new Random();

        static void Main(string[] args)
        {
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

            var boosters = CalculateBoosters();
            PrintBoosters(boosters);
            PrintToCsv(boosters);
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

            string filePath = "/Users/PhillipRommel/Workspace/TestFolder/cube.csv";

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
                    $"Lands:{booster.Land}");

                summedBoosters += booster.GetTotalCards();
            }

            Console.WriteLine($"Total Cards in boosters: {summedBoosters}");
        }

        static Booster[] CalculateBoosters()
        {
            var sumBooster = new Booster
            {
                Black = _totalBlackCards,
                Blue = _totalBlueCards,
                Colorless = _totalColorlessCards,
                Green = _totalGreenCards,
                Land = _totalLandCards,
                MultiColor = _totalMultiColorCards,
                Red = _totalRedCards,
                White = _totalWhiteCards

            };

            Booster[] boosters = new Booster[_totalBoosters];

            FloatBooster avgBooster = new FloatBooster
            {
                Black = (double)_totalBlackCards / _totalBoosters,
                Blue = (double)_totalBlueCards / _totalBoosters,
                Green = (double)_totalGreenCards / _totalBoosters,
                Colorless = (double)_totalColorlessCards / _totalBoosters,
                Land = (double)_totalLandCards / _totalBoosters,
                MultiColor = (double)_totalMultiColorCards / _totalBoosters,
                Red = (double)_totalRedCards / _totalBoosters,
                White = (double)_totalWhiteCards / _totalBoosters
            };

            for (int i = 0; i < boosters.Length; i++)
            {
                var booster = new Booster();

                var initialCards = Convert.ToInt32(Math.Floor(avgBooster.Black));
                booster.Black = initialCards;
                sumBooster.Black -= initialCards;

                initialCards = Convert.ToInt32(Math.Floor(avgBooster.Blue));
                booster.Blue = initialCards;
                sumBooster.Blue -= initialCards;

                initialCards = Convert.ToInt32(Math.Floor(avgBooster.Green));
                booster.Green = initialCards;
                sumBooster.Green -= initialCards;

                initialCards = Convert.ToInt32(Math.Floor(avgBooster.Red));
                booster.Red = initialCards;
                sumBooster.Red -= initialCards;

                initialCards = Convert.ToInt32(Math.Floor(avgBooster.White));
                booster.White = initialCards;
                sumBooster.White -= initialCards;

                initialCards = Convert.ToInt32(Math.Floor(avgBooster.MultiColor));
                booster.MultiColor = initialCards;
                sumBooster.MultiColor -= initialCards;

                initialCards = Convert.ToInt32(Math.Floor(avgBooster.Colorless));
                booster.Colorless = initialCards;
                sumBooster.Colorless -= initialCards;

                initialCards = Convert.ToInt32(Math.Floor(avgBooster.Land));
                booster.Land = initialCards;
                sumBooster.Land -= initialCards;

                boosters[i] = booster;
            }

            int calcRounds = 0;

            while (sumBooster.GetTotalCards() > 0)
            {
                calcRounds++;

                Console.WriteLine($"Starting calculation Round: {calcRounds}");

                for (int i = 0; i < boosters.Length; i++)
                {
                    var booster = boosters[i];

                    var possibleCardGroups = GetPossibleCardGroupsFromBooster(booster, avgBooster, sumBooster);

                    if (!possibleCardGroups.Any())
                    {
                        continue;
                    }

                    var cardGroup = GetRandomGroup(possibleCardGroups.ToArray());

                    switch (cardGroup)
                    {
                        case CardGroup.Black:
                            {
                                booster.Black++;
                                sumBooster.Black--;
                                break;
                            }
                        case CardGroup.Blue:
                            {
                                booster.Blue++;
                                sumBooster.Blue--;
                                break;
                            }
                        case CardGroup.Colorless:
                            {
                                booster.Colorless++;
                                sumBooster.Colorless--;
                                break;
                            }
                        case CardGroup.Green:
                            {
                                booster.Green++;
                                sumBooster.Green--;
                                break;
                            }
                        case CardGroup.Land:
                            {
                                booster.Land++;
                                sumBooster.Land--;
                                break;
                            }
                        case CardGroup.MultiColor:
                            {
                                booster.MultiColor++;
                                sumBooster.MultiColor--;
                                break;
                            }
                        case CardGroup.Red:
                            {
                                booster.Red++;
                                sumBooster.Red--;
                                break;
                            }
                        case CardGroup.White:
                            {
                                booster.White++;
                                sumBooster.White--;
                                break;
                            }
                        default:
                            break;
                    }

                    boosters[i] = booster;

                }
            }

            return boosters;
        }

        private static CardGroup GetRandomGroup(CardGroup[] cardGroups)
        {
            return cardGroups[_random.Next(cardGroups.Length)];
        }

        private static List<CardGroup> GetPossibleCardGroupsFromBooster(Booster booster, FloatBooster avgBooster, Booster sumBooster)
        {
            List<CardGroup> cardGroups = new List<CardGroup>();

            if (booster.Black < avgBooster.Black && sumBooster.Black > 0)
            {
                cardGroups.Add(CardGroup.Black);
            }

            if (booster.Blue < avgBooster.Blue && sumBooster.Blue > 0)
            {
                cardGroups.Add(CardGroup.Blue);
            }

            if (booster.Colorless < avgBooster.Colorless && sumBooster.Colorless > 0)
            {
                cardGroups.Add(CardGroup.Colorless);
            }

            if (booster.Green < avgBooster.Green && sumBooster.Green > 0)
            {
                cardGroups.Add(CardGroup.Green);
            }

            if (booster.Land < avgBooster.Land && sumBooster.Land > 0)
            {
                cardGroups.Add(CardGroup.Land);
            }

            if (booster.MultiColor < avgBooster.MultiColor && sumBooster.MultiColor > 0)
            {
                cardGroups.Add(CardGroup.MultiColor);
            }

            if (booster.Red < avgBooster.Red && sumBooster.Red > 0)
            {
                cardGroups.Add(CardGroup.Red);
            }

            if (booster.White < avgBooster.White && sumBooster.White > 0)
            {
                cardGroups.Add(CardGroup.White);
            }


            return cardGroups;
        }
    }
}
