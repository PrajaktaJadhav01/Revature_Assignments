using System;
using System.Collections.Generic;

namespace MYAPP
{
    public interface IWeatherService
    {
        IEnumerable<double> GetTemperature(string city);
    }

    public class MockWeatherService : IWeatherService
    {
        public IEnumerable<double> GetTemperature(string city)
        {
            yield return 20;
            yield return 21;
            yield return 22;
            yield return 23;
            yield return 24;
        }
    }

    public class WeatherProgram
    {
        public static void Run()
        {
            IWeatherService weatherService = new MockWeatherService();

            foreach (var temp in weatherService.GetTemperature("Pune"))
            {
                Console.WriteLine(temp);
            }

            Console.WriteLine("Weather Test Passed");
        }
    }
}