using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.Caching;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using WeatherClient.ForecustWeatherServiceReference;
using WeatherService;

namespace WeatherClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MemoryCache _cache;
        public WeatherForecust ResultForecust { set; get; }

        public MainWindow()
        {
            InitializeComponent();
            _cache = new MemoryCache("test", new NameValueCollection());
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
           

            
        }

        private void GetWeatherButton_Click(object sender, RoutedEventArgs e)
        {
            if (!_cache.Contains(CityName.Text))
            {
               // var currentResult = _weatherService.GetCurrentWeather(CityName.Text);
               // var foreCustRes = _weatherService.GetForeCustWeather(CityName.Text);
                var weatherService = new WeatherServiceClient();
                ResultForecust = weatherService.GetWeather(CityName.Text); ;
                //var item = new CacheItem(CityName.Text, ResultForecust);
                var policy = new CacheItemPolicy {AbsoluteExpiration = DateTimeOffset.MaxValue};
                _cache.Add(CityName.Text, ResultForecust, policy);               
            }
            else
            {
                var item = _cache.GetCacheItem(CityName.Text);
                if (item == null) return;
                var cacheItem = item.Value;
                    ResultForecust = (WeatherForecust) cacheItem;
            }
            CurrenWeatherDataGrid.ItemsSource = CollectWeatherData(ResultForecust.CurrentWeather);
            WeatherForeCustDataGrid.ItemsSource = CollectWeatherData(ResultForecust.Forecast);
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

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void CurrenWeatherDataGrid_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {

        }
    }
}
