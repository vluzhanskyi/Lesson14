using Lesson13;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using System;
using System.Globalization;

namespace lesson13
{
    internal class Weather : IWeather
    {
        private const string AppId = "0949ad752887b4deeaf01429a455e7ed";

        public XmlDocument  GetCurrentWeather(string city)
        {
            string url = string.Format("http://api.openweathermap.org/data/2.5/weather?q={0}&APPID={1}&mode=xml&units=metric", city, AppId);
            XmlDocument doc = new XmlDocument();
            doc.Load(url);

            return doc;       
        }

        public WeatherForcust GetForeCustWeather(string city)
        {
            string url = string.Format("http://api.openweathermap.org/data/2.5/forecast?q={0}&APPID={1}&mode=xml&units=metric", city, AppId);
            XmlDocument doc = new XmlDocument();
            doc.Load(url);
            var result = SerializeWeatherData(doc);
           doc.Save("ForeCust.xml");
            return result;
        }

        private WeatherForcust SerializeWeatherData(XmlDocument data)
        {
            WeatherForcust result = new WeatherForcust();
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
                result.Data.From = Convert.ToDateTime(data.Attribute("from").Value);
                result.Data.To = Convert.ToDateTime(data.Attribute("to").Value);
                result.Temperature = float.Parse(temperature.Value, CultureInfo.InvariantCulture.NumberFormat);
                result.CloudsValue = clouds.Value;
                result.WindSpeed = float.Parse(windSpeed.Value, CultureInfo.InvariantCulture.NumberFormat);
                result.Humidity = float.Parse(humidity.Value, CultureInfo.InvariantCulture.NumberFormat);
                result.Pressure = float.Parse(pressure.Value, CultureInfo.InvariantCulture.NumberFormat);
            }
            if (precipitation == null || !precipitation.Attributes().Any()) return result;
            result.Precipitation.Value = float.Parse(precipitation.Attribute("value").Value);
            result.Precipitation.Type = precipitation.Attribute("type").Value;
            return result;
        }
    }
}
