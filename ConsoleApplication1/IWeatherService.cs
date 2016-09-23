using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Xml;

namespace WeatherService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWeatherService" in both code and config file together.
    [ServiceContract]
    public interface IWeatherService
    {
        [OperationContract]
        WeatherForecust GetWeather(string city);
        [OperationContract]
        ForcustItem GetCurrentWeather(string city);
        [OperationContract]
        ForcustItem GetForeCustWeather(string city);

    }
}
