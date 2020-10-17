using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CalculateShortestPath.Tests
{
    [TestClass]
    public class DistanceTests
    {
        private Distance Distance;

        [TestInitialize]
        public void TestSetup()
        {
            Distance = new Distance();
        }

        [TestMethod]
        public void GetDistance_Start12AndTarget19_ReturnsCorrectDistance()
        {
            AssertDistance(12, 19, 4);
        }

        [TestMethod]
        public void GetDistance_Start12AndTarget20_ReturnsCorrectDistance()
        {
            AssertDistance(12, 20, 4);
        }

        [TestMethod]
        public void GetDistance_Start12AndTarget21_ReturnsCorrectDistance()
        {
            AssertDistance(12, 21, 4);
        }

        [TestMethod]
        public void GetDistance_Start28AndTarget32_ReturnsCorrectDistance()
        {
            AssertDistance(28, 32, 4);
        }

        [TestMethod]
        public void GetDistance_Start39AndTarget4_ReturnsCorrectDistance()
        {
            AssertDistance(39, 4, 5);
        }

        [TestMethod]
        public void GetDistance_Start41AndTarget6_ReturnsCorrectDistance()
        {
            AssertDistance(41, 6, 5);
        }

        [TestMethod]
        public void GetDistance_Start36AndTarget43_ReturnsCorrectDistance()
        {
            AssertDistance(36, 43, 5);
        }

        [TestMethod]
        public void GetDistance_Start11AndTarget5_ReturnsCorrectDistance()
        {
            AssertDistance(11, 5, 3);
        }

        [TestMethod]
        public void GetDistance_Start5AndTarget8_ReturnsCorrectDistance()
        {
            AssertDistance(5, 8, 3);
        }

        [TestMethod]
        public void GetDistance_Start24AndTarget15_ReturnsCorrectDistance()
        {
            AssertDistance(24, 15, 5);
        }

        [TestMethod]
        public void GetDistance_Start33AndTarget42_ReturnsCorrectDistance()
        {
            AssertDistance(33, 42, 7);
        }

        [TestMethod]
        public void GetDistance_Start33AndTarget11_ReturnsCorrectDistance()
        {
            AssertDistance(33, 11, 5);
        }

        [TestMethod]
        public void GetDistance_Start45AndTarget20_ReturnsCorrectDistance()
        {
            AssertDistance(45, 20, 6);
        }

        [TestMethod]
        public void GetDistance_Start20AndTarget45_ReturnsCorrectDistance()
        {
            AssertDistance(20, 45, 6);
        }

        [TestMethod]
        public void GetDistance_Start42AndTarget15_ReturnsCorrectDistance()
        {
            AssertDistance(42, 15, 6);
        }

        [TestMethod]
        public void GetDistance_Start30AndTarget41_ReturnsCorrectDistance()
        {
            AssertDistance(30, 41, 7);
        }

        [TestMethod]
        public void GetDistance_Start10AndTarget7_ReturnsCorrectDistance()
        {
            AssertDistance(10, 7, 2);
        }

        [TestMethod]
        public void GetDistance_Start7AndTarget4_ReturnsCorrectDistance()
        {
            AssertDistance(7, 4, 2);
        }

        public void AssertDistance(int start, int target, int expected)
        {
            Distance.GenerateGrid();
            var path = Distance.GetDistance(start, target);
            Assert.AreEqual(path.Count, expected);
        }
    }
}
