using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using WeatherClient.ForecustWeatherServiceReference;
using WeatherService;

namespace WeatherClient
{
    public class ClientWeatherData
    {
        private WeatherForecust ResultForecust { get; set; }
        public string City { set; get; }
        public List<WeatherData> CurrenWeather { get; private set; }
        public List<WeatherData> WeatherForeCust { get; private set; }

        private static MemoryCache _cache;
        public ClientWeatherData()
        {
            CurrenWeather = new List<WeatherData>();
            WeatherForeCust = new List<WeatherData>();
            _cache = new MemoryCache("WeatherCache", new NameValueCollection());
        }

        private static List<WeatherData> CollectWeatherData(ForeCast[] forecust)
        {
            var result = new List<WeatherData>();
            foreach (var item in forecust)
            {
                WeatherData data = new WeatherData();
                data.CloudsValue = item.CloudsValue;
                data.DataFrom = item.Data.From;
                data.DataTo = item.Data.To;
                data.Humidity = item.Humidity;
                data.PrecipitationType = item.Precipitation.Type;
                data.PrecipitationValue = item.Precipitation.Value;
                data.Pressure = item.Pressure;
                data.WindSpeed = item.WindSpeed;
                data.Temperature = item.Temperature;
                result.Add(data);
            }

            return result;
        }

        public void GetWeather()
        {
            if (!_cache.Contains(City))
            {
                var weatherService = new WeatherServiceClient();
                ResultForecust = weatherService.GetWeather(City);
                var policy = new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.MaxValue };
                _cache.Add(City, ResultForecust, policy);
            }
            else
            {
                var cacheItem = (WeatherForecust)_cache.GetCacheItem(City, null).Value;

                if (cacheItem.CurrentWeather[0].Data.From.AddHours(3) > DateTime.Now)
                {
                    ResultForecust = cacheItem;
                }
            }
            CurrenWeather = CollectWeatherData(ResultForecust.CurrentWeather);
            WeatherForeCust = CollectWeatherData(ResultForecust.Forecast);
        }
    }
}
