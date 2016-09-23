using System;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.Caching;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using WeatherClient.ForecustWeatherServiceReference;

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
          //  WeatherForeCustDataGrid.ItemsSource = ResultForecust.CurrentWeather;
            WeatherForeCustDataGrid.ItemsSource = ResultForecust.Forecast;
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CurrenWeatherDataGrid_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {

        }
    }
}
