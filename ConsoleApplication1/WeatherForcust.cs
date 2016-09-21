﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Lesson13
{
    [DataContract]
   public class WeatherForcust
    {
        [DataMember]
        public string CityName { set; get; }
        [DataMember]
        public List<ForeCast> Forecast = new List<ForeCast>();
    }

    public class ForeCast
    {
        public Precipitation Precipitation = new Precipitation();
        public Data Data = new Data();
        public string CloudsValue { set; get; }
        public float Temperature { set; get; }
        public float WindSpeed { set; get; }
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
        public float Value { set; get; }
        public string Type { set; get; }
    }
}
