using System;
using System.Collections.Generic;

namespace MYAPP
{
    public class WeatherProgram
    {
        public static void RunWeatherTest()
        {
            IWeatherService weatherService = new MockWeatherService();

            Console.WriteLine("Temperatures:");

            foreach (var temp in weatherService.GetTemperature("Pune"))
            {
                Console.WriteLine(temp);
            }

            Console.WriteLine("Weather Test Passed");
        }
    }
}