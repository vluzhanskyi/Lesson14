using Lesson13;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Xml.Linq;
using System.Linq;
using System;
using System.Globalization;

namespace lesson13
{
    class Weather : IWeather
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
            WeatherForcust result = new WeatherForcust();
            string url = string.Format("http://api.openweathermap.org/data/2.5/forecast?q={0}&APPID={1}&mode=xml&units=metric", city, AppId);
            XmlDocument doc = new XmlDocument();
            doc.Load(url);
            result = SerializeWeatherData(doc);
           // doc.Save("ForeCust.xml");
            return result;
        }

        private WeatherForcust SerializeWeatherData(XmlDocument data)
        {
            WeatherForcust result = new WeatherForcust();
            XDocument doc = XDocument.Parse(data.OuterXml);
            ForeCast forecastItem = new ForeCast();
            result.CityName = doc.Root.Elements().Select(x => x.Element("name")).ElementAt(0).Value;
            XElement XmlForecast = doc.Root.Elements().Select(x => x.Element("time")).ElementAt(4);
            var test = doc.Root.Elements().Where(e => e.Element("time") != null);
            foreach (var node in test.Elements())
            {
                result.Forecast.Add(GetForecastFromXML(node));
            }
            return result;
        }
        private ForeCast GetForecastFromXML(XElement data)
        {
            ForeCast result = new ForeCast();
            var temperature = data.Element("temperature").Attribute("value");
            var clouds = data.Element("clouds").Attribute("value");
            var windSpeed = data.Element("windSpeed").Attribute("mps");
            var humidity = data.Element("humidity").Attribute("value");
            var precipitation = data.Element("precipitation");
            var pressure = data.Element("pressure").Attribute("value");
            result.data.From = Convert.ToDateTime(data.Attribute("from").Value);
            result.data.To = Convert.ToDateTime(data.Attribute("to").Value);
            result.Temperature = float.Parse(temperature.Value, CultureInfo.InvariantCulture.NumberFormat);
            result.CloudsValue = clouds.Value;
            result.WindSpeed = float.Parse(windSpeed.Value, CultureInfo.InvariantCulture.NumberFormat);
            result.Humidity = float.Parse(humidity.Value, CultureInfo.InvariantCulture.NumberFormat);
            result.Pressure = float.Parse(pressure.Value, CultureInfo.InvariantCulture.NumberFormat);
            if (precipitation.Value != string.Empty)
            {
                result.Precipitation.value = float.Parse(precipitation.Attribute("value").Value, CultureInfo.InvariantCulture.NumberFormat);
                result.Precipitation.type = precipitation.Attribute("type").Value;
            }
            return result;
        }
    }
}
