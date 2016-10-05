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
        string City { set; get; }
        ViewModel Model = new ViewModel();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {   
            City = CityName.Text;
        }

        private void GetWeatherButton_Click(object sender, RoutedEventArgs e)
        {
            if (City != string.Empty)
            {
                Model.GetWeather(City);
                WeatherForeCustDataGrid.ItemsSource = Model.WeatherForeCust;
                CurrenWeatherDataGrid.ItemsSource = Model.CurrenWeather;
            }
        }
    }
}
