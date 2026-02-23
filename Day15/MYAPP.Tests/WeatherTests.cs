using Microsoft.VisualStudio.TestTools.UnitTesting;
using MYAPP;
using System.Linq;

namespace MYAPP.Tests
{
    [TestClass]
    public class WeatherTests
    {
        [TestMethod]
        public void WeatherTemperature_Count_Test()
        {
            IWeatherService service = new MockWeatherService();

            var temps = service.GetTemperature("Pune");

            Assert.AreEqual(5, temps.Count());
        }

        [TestMethod]
        public void WeatherTemperature_FirstValue_Test()
        {
            IWeatherService service = new MockWeatherService();

            var temps = service.GetTemperature("Pune");

            Assert.AreEqual(20, temps.First());
        }
    }
}
