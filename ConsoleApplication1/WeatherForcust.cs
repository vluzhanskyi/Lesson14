using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson13
{
   public class WeatherForcust
    {
        public string CityName { set; get; }
        public List<ForeCast> Forecast = new List<ForeCast>();
    }

    public class ForeCast
    {
        public string CloudsValue { set; get; }
        public Data data = new Data();
        public float Temperature { set; get; }
        public float WindSpeed { set; get; }
        public Precipitation Precipitation { set; get; }
        public float Pressure { set; get; }
        public float Humidity { set; get; }
    }
    public class Data
    {
        public DateTime From { set; get; }
        public DateTime To { set; get; }
    }
    public class Precipitation
    {
        public float value { set; get; }
        public string type { set; get; }
    }
}
