using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WeatherClient.ForecustWeatherServiceReference;
using WeatherService;

namespace WeatherClient 
{
    public class ViewModel
    {
        public string CityName { set; get; }
        public WeatherForecust ResultForecust { set; get; }
        public static List<WeatherData> CurrenWeather { get; private set; }
        public static List<WeatherData> WeatherForeCust { get; private set; }

        private readonly MemoryCache _cache;
        public ViewModel()
        {
            _cache = new MemoryCache("test", new NameValueCollection());
        }

        private List<WeatherData> CollectWeatherData(ForeCast[] forecust)
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
            if (_cache.Contains(CityName))
            {
                var cacheItem = (WeatherForecust)_cache.GetCacheItem(CityName, null).Value;

                if (cacheItem.CurrentWeather[0].Data.From.AddHours(3) > DateTime.Now)
                {
                    // var currentResult = _weatherService.GetCurrentWeather(CityName.Text);
                    // var foreCustRes = _weatherService.GetForeCustWeather(CityName.Text);
                    ResultForecust = cacheItem;
                }
            }
            else
            {
                var weatherService = new WeatherServiceClient();
                ResultForecust = weatherService.GetWeather(CityName); ;
                //var item = new CacheItem(CityName.Text, ResultForecust);
                var policy = new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.MaxValue };
                _cache.Add(CityName, ResultForecust, policy);               
            }
            CurrenWeather = CollectWeatherData(ResultForecust.CurrentWeather);
            WeatherForeCust = CollectWeatherData(ResultForecust.Forecast);
        }

      //  public event TextChangedEventArgs CityNameChanged;
    }
}
