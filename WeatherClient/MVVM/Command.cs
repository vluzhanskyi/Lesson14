using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using WeatherService;

namespace WeatherClient
{
    public class Command : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private bool _canExecute;
        private ClientWeatherData data;
        public Command(string CityName)
        {
            data = new ClientWeatherData();
            data.City = CityName;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            data.GetWeather();
        }

        public void OnCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }
    }
}
