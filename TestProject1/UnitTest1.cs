using Microsoft.VisualStudio.TestTools.UnitTesting;
using zd3;

namespace TestProject1
{
    [TestClass]
    public class RoadWorkTests
    {
        [TestMethod]
        public void RoadWork_CalculateQuality_ReturnsCorrectValue()
        {
            // Arrange
            var work = new RoadWork(2.0, 3.0, 4.0, "Test Work", System.DateTime.Now);

            // Act
            double quality = work.CalculateQuality();

            // Assert
            Assert.AreEqual(2.0 * 3.0 * 4.0 / 1000, quality);
        }

        [TestMethod]
        public void RoadWork_ToString_ReturnsFormattedString()
        {
            // Arrange
            var work = new RoadWork(2.0, 3.0, 4.0, "Test Work", new System.DateTime(2023, 1, 1));

            // Act
            string result = work.ToString();

            // Assert
            StringAssert.Contains(result, "Test Work");
            StringAssert.Contains(result, "Ш: 2м");
            StringAssert.Contains(result, "Д: 3м");
            StringAssert.Contains(result, "Вес: 4кг/м²");
            StringAssert.Contains(result, "Дата: 01.01.2023");
        }
    }

    [TestClass]
    public class EnhancedRoadWorkTests
    {
        [TestMethod]
        public void EnhancedRoadWork_CalculateQuality_WithCoefficient5_ReturnsBaseQualityMultipliedBy1_1()
        {
            // Arrange
            var work = new EnhancedRoadWork(2.0, 3.0, 4.0, "Test Work", System.DateTime.Now, 5, "Contractor", false);
            double baseQuality = 2.0 * 3.0 * 4.0 / 1000;

            // Act
            double quality = work.CalculateQuality();

            // Assert
            Assert.AreEqual(baseQuality * 1.1, quality);
        }

        [TestMethod]
        public void EnhancedRoadWork_CalculateQuality_WithCoefficient4_ReturnsBaseQualityMultipliedBy1_6()
        {
            // Arrange
            var work = new EnhancedRoadWork(2.0, 3.0, 4.0, "Test Work", System.DateTime.Now, 4, "Contractor", false);
            double baseQuality = 2.0 * 3.0 * 4.0 / 1000;

            // Act
            double quality = work.CalculateQuality();

            // Assert
            Assert.AreEqual(baseQuality * 1.6, quality);
        }

        [TestMethod]
        public void EnhancedRoadWork_ToString_ContainsEnhancedInfo()
        {
            // Arrange
            var work = new EnhancedRoadWork(2.0, 3.0, 4.0, "Test Work", System.DateTime.Now, 5, "Contractor", false);

            // Act
            string result = work.ToString();

            // Assert
            StringAssert.Contains(result, "[Улучшенная:");
            StringAssert.Contains(result, "P=5");
        }
    }

    [TestClass]
    public class RoadWorkManagerTests
    {
        [TestMethod]
        public void RoadWorkManager_AddWork_AddsWorkToCollection()
        {
            // Arrange
            var manager = new RoadWorkManager();
            var work = new RoadWork(1.0, 1.0, 1.0, "Test", System.DateTime.Now);

            // Act
            manager.AddWork(work);
            var stats = manager.GetStatistics();

            // Assert
            Assert.AreEqual(1, stats.totalWorks);
        }

        [TestMethod]
        public void RoadWorkManager_RemoveWork_RemovesWorkFromCollection()
        {
            // Arrange
            var manager = new RoadWorkManager();
            var work = new RoadWork(1.0, 1.0, 1.0, "Test", System.DateTime.Now);
            manager.AddWork(work);

            // Act
            bool result = manager.RemoveWork(work);
            var stats = manager.GetStatistics();

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(0, stats.totalWorks);
        }

        [TestMethod]
        public void RoadWorkManager_GetStatistics_ReturnsCorrectValues()
        {
            // Arrange
            var manager = new RoadWorkManager();
            manager.AddWork(new RoadWork(1.0, 1.0, 1.0, "Test1", System.DateTime.Now));
            manager.AddWork(new EnhancedRoadWork(2.0, 2.0, 2.0, "Test2", System.DateTime.Now, 5, "", false));

            // Act
            var stats = manager.GetStatistics();

            // Assert
            Assert.AreEqual(2, stats.totalWorks);
            Assert.AreEqual(1, stats.enhancedWorks);
            Assert.IsTrue(stats.avgQuality > 0);
            Assert.IsTrue(stats.maxQuality > 0);
            Assert.IsTrue(stats.minQuality > 0);
            Assert.IsTrue(stats.totalQuality > 0);
        }

        [TestMethod]
        public void RoadWorkManager_SearchWorks_ReturnsCorrectResults()
        {
            // Arrange
            var manager = new RoadWorkManager();
            manager.AddWork(new RoadWork(1.0, 1.0, 1.0, "Road construction", System.DateTime.Now));
            manager.AddWork(new RoadWork(1.0, 1.0, 1.0, "Bridge repair", System.DateTime.Now));

            // Act
            var results1 = manager.SearchWorks("road");
            var results2 = manager.SearchWorks("");
            var results3 = manager.SearchWorks("nonexistent");

            // Assert
            Assert.AreEqual(1, results1.Count);
            Assert.AreEqual(2, results2.Count);
            Assert.AreEqual(0, results3.Count);
        }
    }
}
