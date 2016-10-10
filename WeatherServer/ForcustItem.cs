using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WeatherService
{
    [DataContract]
   public class WeatherForecust
    {
        [DataMember]
        public string CityName { set; get; }
        [DataMember]
        public List<ForeCast> Forecast = new List<ForeCast>();
        [DataMember]
        public List<ForeCast> CurrentWeather = new List<ForeCast>();
    }
     [DataContract]
    public class ForcustItem
    {
         [DataMember]
        public string CityName { set; get; }
         [DataMember]
        public List<ForeCast> Forecast = new List<ForeCast>();
    }
    
}
