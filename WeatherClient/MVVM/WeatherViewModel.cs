using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using WeatherService;

namespace WeatherClient.MVVM
{
    class WeatherViewModel : DependencyObject
    {
        public static ClientWeatherData data;

        public string CityName
        {
            get { return (string)GetValue(CityNameProperty); }
            set { SetValue(CityNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CityNameProperty =
            DependencyProperty.Register("CityName", typeof(string), typeof(WeatherViewModel), new PropertyMetadata("", CityName_Changed));

        private static void CityName_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            data.City = (string)e.NewValue;
        }

        public List<WeatherData> CurrentWeather
        {
            get { return (List<WeatherData>)GetValue(CurrentWeatherProperty); }
            set { SetValue(CurrentWeatherProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentWeatherProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentWeatherProperty =
            DependencyProperty.Register("CurrentWeather", typeof(List<WeatherData>), typeof(WeatherViewModel), new PropertyMetadata(null));

        public List<WeatherData> WeatherForecust
        {
            get { return (List<WeatherData>)GetValue(WeatherForecustProperty); }
            set { SetValue(WeatherForecustProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WeatherForecustProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WeatherForecustProperty =
            DependencyProperty.Register("WeatherForecust", typeof(List<WeatherData>), typeof(WeatherViewModel), new PropertyMetadata(null));

        public WeatherViewModel()
        {
            data = new ClientWeatherData();
            UpdateResults();
        }
        private void UpdateResults()
        {
            CurrentWeather = data.CurrenWeather;
            WeatherForecust = data.WeatherForeCust;
        }

        private ICommand _getButton;

       public ICommand GetButton
        {
            get
            {
                return _getButton ?? (_getButton = new RelayCommand(() =>
                {
                    data.GetWeather();
                    UpdateResults();
                } ));
            }
        }
    }
}
