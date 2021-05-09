using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using RedisInto.Extensions;

namespace RedisInto.Data
{
    public class WeatherForecastService
    {
        public IDistributedCache Cache { get; }

        public WeatherForecastService(IDistributedCache cache)
        {
            Cache = cache;
        }

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };        

        public async Task<WeatherForecast[]> GetForecastAsync(DateTime startDate)
        {
            var rng = new Random();

            //Since the abosulte expiration time by default is set to 1 Minute thus after each minute this key will expire
            //Thus after every 1 Minute we will see "Loading..."
            var key = "MyKey_" + DateTime.Now.ToString("yyyyMMdd_hhmm");

            if(await Cache.GetRecordAsync<WeatherForecast[]>(key) == null)
            {
                //Simulating DB fetch time
                await Task.Delay(1500);

                //Logic to return weather forecasts array
                WeatherForecast[] weatherForecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = startDate.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                }).ToArray();

                await Cache.SetRecordAsync<WeatherForecast[]>(key, weatherForecasts);
            }

            //Cache is set, return the value
            return await Cache.GetRecordAsync<WeatherForecast[]>(key);
        }
    }
}
