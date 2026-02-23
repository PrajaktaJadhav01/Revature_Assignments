using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FakeItEasy;
using MYAPP;

namespace MYAPP.Tests
{
    [TestClass]
    public class MyCalcAppTests
    {
        [TestMethod]
        public void MyCalcAppTest()
        {
            // Arrange
            var fakeAverage = A.Fake<IAverageCalculation>();

            A.CallTo(() => fakeAverage.GetValues())
                .Returns(new int[] { 40, 50, 60 });

            var app = new MyCalcApp(fakeAverage);

            // Act
            var result = app.CalculateAverageHO();

            // Assert
            Assert.AreEqual(50.0, result);
        }
    }
}