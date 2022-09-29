using FluentAssertions;

namespace MTGCubeTool.Infrastructure.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var myBool = false;
            myBool.Should().BeTrue();
        }
    }
}