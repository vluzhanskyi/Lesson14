using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherService;

namespace WeatherClient
{
    public delegate void WeatherReceived();
    public delegate void GetWeatherClicked(string cityName);

    public  class Commands
    {       
        public event EventHandler<WeatherReceivedEventArgs> WeatherReceivedEvent;
        public event EventHandler<GetWeatherClickedEventArgs> GetWeatherClickedEvent;
        
        public Commands(string City)
        {
            ViewModel model = new ViewModel();
            if (model.WeatherForeCust != null)
            {
                OnRaiseGetWeatherClicked(new GetWeatherClickedEventArgs(City));
            }
            model.GetWeather(City);
            OnRaiseWeatherReceived(new WeatherReceivedEventArgs(model.CurrenWeather, model.WeatherForeCust));            
        }

        protected virtual void OnRaiseGetWeatherClicked(GetWeatherClickedEventArgs e)
        {
            GetWeatherClickedEvent?.Invoke(this, e);
        }
        protected virtual void OnRaiseWeatherReceived(WeatherReceivedEventArgs e)
        {
            WeatherReceivedEvent?.Invoke(this, e);
        }

    }
    public class GetWeatherClickedEventArgs
    {
        
        public string CityNameValue { get; set; }
        public GetWeatherClickedEventArgs(string cityName)
        {
            CityNameValue = cityName;
        }
    }
    public class WeatherReceivedEventArgs
    {
        public List<WeatherData> CurrentValue { get; set; }
        public List<WeatherData> ForecustValue { get; set; }
        public WeatherReceivedEventArgs(List<WeatherData> current, List<WeatherData> forecust)
        {
            CurrentValue = current;
            ForecustValue = forecust;
        }
    }


}
