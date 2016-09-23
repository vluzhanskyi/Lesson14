using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace WeatherService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WeatherService" in both code and config file together.
    public class WeatherService : IWeatherService
    {
        private const string AppId = "0949ad752887b4deeaf01429a455e7ed";

        public WeatherForecust GetWeather(string city)
        {
            var temp = GetCurrentWeather(city);
            var result = new WeatherForecust
            {
                CurrentWeather = temp.Forecast,
                CityName = temp.CityName,
                Forecast = GetForeCustWeather(city).Forecast
            };
            return result;
        }

        public ForcustItem GetCurrentWeather(string city)
        {
            string url =
                string.Format("http://api.openweathermap.org/data/2.5/weather?q={0}&APPID={1}&mode=xml&units=metric",
                    city, AppId);
            XmlDocument doc = new XmlDocument();
            doc.Load(url);
            var result = GetCurrentWeatherfromXml(doc);
            // doc.Save("current.xml");
            return result;
        }

        public ForcustItem GetForeCustWeather(string city)
        {
            string url =
                string.Format("http://api.openweathermap.org/data/2.5/forecast?q={0}&APPID={1}&mode=xml&units=metric",
                    city, AppId);
            var doc = new XmlDocument();
            doc.Load(url);
            var result = SerializeWeatherData(doc);
           // doc.Save("ForeCust.xml");
            return result;
        }

        private ForcustItem GetCurrentWeatherfromXml(XmlNode data)
        {
            ForcustItem result = new ForcustItem();
            XDocument doc = XDocument.Parse(data.OuterXml);

            if (doc.Root == null) return result;
            //=================================/ Get Data from XmlDocument  /=============================================
            var city = doc.Root.Elements().Where(x => x.Name == "city").Attributes("name");
            var sTemperature = doc.Root.Elements().Where(x => x.Name == "temperature").Attributes("value").ElementAt(0).Value;
            var sHumidity = doc.Root.Elements().Where(x => x.Name == "humidity").Attributes("value").ElementAt(0).Value;
            var sPressure = doc.Root.Elements().Where(x => x.Name == "pressure").Attributes("value").ElementAt(0).Value;
            var sWindSpeed = doc.Root.Elements().Where(x => x.Name == "wind").Elements().Where(e => e.Name == "speed");
            var sClouds = doc.Root.Elements().Where(x => x.Name == "clouds").Attributes("name").ElementAt(0).Value;
            var sPrecipitationType = doc.Root.Elements().Where(x => x.Name == "precipitation").Attributes("mode").ElementAt(0).Value;
            float precipitationValue = 0;
            if (doc.Root.Elements().Where(x => x.Name == "precipitation").Attributes("value").Any())
            {
                var sPrecipitationValue = doc.Root.Elements().Where(x => x.Name == "precipitation").Attributes("value").ElementAt(0).Value;
                precipitationValue = float.Parse(sPrecipitationValue, CultureInfo.InvariantCulture.NumberFormat);
            }
            var sDate = doc.Root.Elements().Where(x => x.Name == "lastupdate").Attributes("value").ElementAt(0).Value;
            //=================================/ Convert Data  /==========================================================
            var temperature = float.Parse(sTemperature, CultureInfo.InvariantCulture.NumberFormat);
            var humidity = float.Parse(sHumidity, CultureInfo.InvariantCulture.NumberFormat);
            var pressure = float.Parse(sPressure, CultureInfo.InvariantCulture.NumberFormat);
            var windSpeed = float.Parse(sWindSpeed.Attributes("value").ElementAt(0).Value, CultureInfo.InvariantCulture.NumberFormat);
            var clouds = sClouds;
            var precipitationType = sPrecipitationType;
                  
            var date = Convert.ToDateTime(sDate);
            //=================================/ Add Data to ForcustItem object  /======================================
            result.CityName = city.ElementAt(0).Value;
            var temData = new Data()
            {
                From = date,
                To = date
            };

            var temPrecipitation = new Precipitation()
            {
                Type = precipitationType,
                Value = precipitationValue
            };

            var forecust = new ForeCast()
            {
                CloudsValue = clouds,
                Temperature = temperature,
                WindSpeed = windSpeed,
                Pressure = pressure,
                Humidity = humidity,
                Data = temData,
                Precipitation = temPrecipitation
            };
            result.Forecast.Add(forecust);

         return result;
        }

        private ForcustItem SerializeWeatherData(XmlNode data)
        {
            ForcustItem result = new ForcustItem();
            XDocument doc = XDocument.Parse(data.OuterXml);
            if (doc.Root == null) return result;
            result.CityName = doc.Root.Elements().Select(x => x.Element("name")).ElementAt(0).Value;
            var test = doc.Root.Elements().Where(e => e.Element("time") != null);
            foreach (var node in test.Elements())
            {
                result.Forecast.Add(GetForecastFromXML(node));
            }
            return result;
        }

        private ForeCast GetForecastFromXML(XElement data)
        {
            var result = new ForeCast();
            var xElement = data.Element("temperature");
            if (xElement == null) return result;
            var temperature = xElement.Attribute("value");
            var element = data.Element("clouds");
            if (element == null) return result;
            var clouds = element.Attribute("value");
            var o = data.Element("windSpeed");
            if (o == null) return result;
            var windSpeed = o.Attribute("mps");
            var xElement1 = data.Element("humidity");
            if (xElement1 == null) return result;
            var humidity = xElement1.Attribute("value");
            var precipitation = data.Element("precipitation");
            var element1 = data.Element("pressure");
            if (element1 != null)
            {
                var pressure = element1.Attribute("value");
                result.Data.From = Convert.ToDateTime(data.Attribute("from").Value, CultureInfo.InvariantCulture);
                result.Data.To = Convert.ToDateTime(data.Attribute("to").Value, CultureInfo.InvariantCulture);
                result.Temperature = float.Parse(temperature.Value, CultureInfo.InvariantCulture.NumberFormat);
                result.CloudsValue = clouds.Value;
                result.WindSpeed = float.Parse(windSpeed.Value, CultureInfo.InvariantCulture.NumberFormat);
                result.Humidity = float.Parse(humidity.Value, CultureInfo.InvariantCulture.NumberFormat);
                result.Pressure = float.Parse(pressure.Value, CultureInfo.InvariantCulture.NumberFormat);
            }
            if (precipitation == null || !precipitation.Attributes().Any()) return result;
            result.Precipitation.Value = float.Parse(precipitation.Attribute("value").Value, CultureInfo.InvariantCulture.NumberFormat);
            result.Precipitation.Type = precipitation.Attribute("type").Value;
            return result;
        }
    }
}

