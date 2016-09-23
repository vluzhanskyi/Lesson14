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
        private readonly IWeatherService _weatherService;
        private readonly MemoryCache _cache;
        public MainWindow()
        {
            InitializeComponent();
            _weatherService = new WeatherServiceClient();
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
                var weather = _weatherService.GetWeather(CityName.Text);
                var item = new CacheItem(CityName.Text, weather);
                var policy = new CacheItemPolicy {AbsoluteExpiration = DateTimeOffset.MaxValue};
                _cache.Add(item.Key, item, policy);
            }
            else
            {
                var currentResult = _cache.Where(x => x.Key == CityName.Text);
                
                
            }
            

        }

        private void PopulateResult(WeatherForecust weather)
        {
            var dataGridTemperatureColumn = CurrenWeatherDataGrid.Columns;
            DataGridViewCell cell = new DataGridViewTextBoxCell();
            
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
