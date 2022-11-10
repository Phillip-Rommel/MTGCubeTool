using FluentAssertions;

namespace CubeTool.Tests
{
    public class BoosterCalculatorTest
    {
        private BoosterCalculator _boosterCalculator;

        [SetUp]
        public void Setup()
        {
            _boosterCalculator = new();
        }

        [Test]
        public void CalculateBoosters_NormalScenario_EveryBoosterShouldContainExpectedNumberOfCards()
        {
            // arrange

            int cardsPerBooster = 16;

            BoosterCalculatorSettings settings = new()
            {
                TotalBoosters = 84,
                TotalCardsPerBooster = cardsPerBooster,
                TotalCardsBooster = new Booster
                {
                    Black = 132,
                    Blue = 195,
                    Colorless = 201,
                    Green = 125,
                    Land = 84,
                    MultiColor = 203,
                    Red = 201,
                    White = 203
                }
            };

            // act
            var result = _boosterCalculator.CalculateBoosters(settings);

            // assert
            result.Should().AllSatisfy(x =>
                {
                    x.GetTotalCards().Should().Be(cardsPerBooster);
                });
        }

        [Test]
        public void CalculateBoosters_NormalScenarioII_EveryBoosterShouldContainExpectedNumberOfCards()
        {
            // arrange

            int cardsPerBooster = 15;

            BoosterCalculatorSettings settings = new()
            {
                TotalBoosters = 78,
                TotalCardsPerBooster = cardsPerBooster,
                TotalCardsBooster = new Booster
                {
                    Black = 100,
                    Blue = 100,
                    Colorless = 100,
                    Green = 100,
                    Land = 211,
                    MultiColor = 204,
                    Red = 175,
                    White = 180
                }
            };

            // act
            var result = _boosterCalculator.CalculateBoosters(settings);

            // assert
            result.Should().AllSatisfy(x =>
            {
                x.GetTotalCards().Should().Be(cardsPerBooster);
            });
        }
    }
}