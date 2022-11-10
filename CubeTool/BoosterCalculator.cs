using CubeTool.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CubeTool
{
    public class BoosterCalculator : IBoosterCalculator
    {
        private Random _random = new();

        public Booster[] CalculateBoosters(BoosterCalculatorSettings settings)
        {
            var sumBooster = new Booster
            {
                Black = settings.TotalCardsBooster.Black,
                Blue = settings.TotalCardsBooster.Blue,
                Colorless = settings.TotalCardsBooster.Colorless,
                Green = settings.TotalCardsBooster.Green,
                Land = settings.TotalCardsBooster.Land,
                MultiColor = settings.TotalCardsBooster.MultiColor,
                Red = settings.TotalCardsBooster.Red,
                White = settings.TotalCardsBooster.White
            };

            Booster[] boosters = new Booster[settings.TotalBoosters];

            FloatBooster avgBooster = new()
            {
                Black = (double)settings.TotalCardsBooster.Black / settings.TotalBoosters,
                Blue = (double)settings.TotalCardsBooster.Blue / settings.TotalBoosters,
                Green = (double)settings.TotalCardsBooster.Green / settings.TotalBoosters,
                Colorless = (double)settings.TotalCardsBooster.Colorless / settings.TotalBoosters,
                Land = (double)settings.TotalCardsBooster.Land / settings.TotalBoosters,
                MultiColor = (double)settings.TotalCardsBooster.MultiColor / settings.TotalBoosters,
                Red = (double)settings.TotalCardsBooster.Red / settings.TotalBoosters,
                White = (double)settings.TotalCardsBooster.White / settings.TotalBoosters
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

                    var possibleCardGroups = GetPossibleCardGroupsFromBooster(booster, avgBooster, sumBooster, settings.TotalCardsPerBooster);

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

        private static List<CardGroup> GetPossibleCardGroupsFromBooster(Booster booster, FloatBooster avgBooster, Booster sumBooster, int cardsPerBooster)
        {
            List<CardGroup> cardGroups = new();

            bool isBoosterFull = booster.GetTotalCards() == cardsPerBooster;

            if (isBoosterFull)
            {
                return cardGroups;
            }

            if ((booster.Black < avgBooster.Black || !isBoosterFull) && sumBooster.Black > 0)
            {
                cardGroups.Add(CardGroup.Black);
            }

            if ((booster.Blue < avgBooster.Blue || !isBoosterFull) && sumBooster.Blue > 0)
            {
                cardGroups.Add(CardGroup.Blue);
            }

            if ((booster.Colorless < avgBooster.Colorless || !isBoosterFull) && sumBooster.Colorless > 0)
            {
                cardGroups.Add(CardGroup.Colorless);
            }

            if ((booster.Green < avgBooster.Green || !isBoosterFull) && sumBooster.Green > 0)
            {
                cardGroups.Add(CardGroup.Green);
            }

            if ((booster.Land < avgBooster.Land || !isBoosterFull) && sumBooster.Land > 0)
            {
                cardGroups.Add(CardGroup.Land);
            }

            if ((booster.MultiColor < avgBooster.MultiColor || !isBoosterFull) && sumBooster.MultiColor > 0)
            {
                cardGroups.Add(CardGroup.MultiColor);
            }

            if ((booster.Red < avgBooster.Red || !isBoosterFull) && sumBooster.Red > 0)
            {
                cardGroups.Add(CardGroup.Red);
            }

            if ((booster.White < avgBooster.White || !isBoosterFull) && sumBooster.White > 0)
            {
                cardGroups.Add(CardGroup.White);
            }

            return cardGroups;
        }

        private CardGroup GetRandomGroup(CardGroup[] cardGroups)
        {
            return cardGroups[_random.Next(cardGroups.Length)];
        }
    }
}