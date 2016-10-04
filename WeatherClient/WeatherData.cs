using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherService
{
    public struct WeatherData
    {
        public DateTime DataFrom { set; get; }
        public DateTime DataTo { set; get; }
        public string CloudsValue { set; get; }
        public string PrecipitationType { set; get; }
        public float PrecipitationValue { set; get; }
        public float Temperature { set; get; }
        public float WindSpeed { set; get; }
        public float Pressure { set; get; }
        public float Humidity { set; get; }
    }
            
}
