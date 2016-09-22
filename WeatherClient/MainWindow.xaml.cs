using System.Collections.Specialized;
using System.Runtime.Caching;
using System.Windows;
using System.Windows.Controls;
using WeatherClient.ForecustWeatherServiceReference;

namespace WeatherClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
           

            
        }

        private void GetWeatherButton_Click(object sender, RoutedEventArgs e)
        {          
            IWeatherService weatherService = new WeatherServiceClient();
            var currentResult = weatherService.GetCurrentWeather(CityName.Text);
            var foreCustRes = weatherService.GetForeCustWeather(CityName.Text);
            var cache = new MemoryCache(CityName.Text, new NameValueCollection());
            var item = new CacheItem("current", currentResult);
            var item2 = new CacheItem("forecast", foreCustRes);
            var policy = new CacheItemPolicy();
            cache.Add(item, policy);
            cache.Add(item2, policy);
        }
    }
}
