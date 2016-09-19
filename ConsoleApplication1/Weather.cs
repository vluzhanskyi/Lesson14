using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace lesson13
{
    class Weather
    {
        private const string AppId = "0949ad752887b4deeaf01429a455e7ed";
        public XmlDocument  GetCurrentWeather(string city)
        {
            string url = string.Format("http://api.openweathermap.org/data/2.5/weather?q={0}&APPID={1}&mode=xml&units=metric", city, AppId);
            XmlDocument doc = new XmlDocument();
            doc.Load(url);
            doc.Save("Current.xml");
            return doc;       
        }

        public XmlDocument GetForeCustWeather(string city)
        {
            string url = string.Format("http://api.openweathermap.org/data/2.5/forecast?q={0}&APPID={1}&mode=xml&units=metric", city, AppId);
            XmlDocument doc = new XmlDocument();
            doc.Load(url);
            doc.Save("ForeCust.xml");
            return doc;
        }
    }
}
