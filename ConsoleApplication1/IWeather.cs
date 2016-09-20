using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.ServiceModel;

namespace Lesson13
{
    [ServiceContract]
    interface IWeather
    {
        [OperationContract]
        XmlDocument GetCurrentWeather(string city);
        [OperationContract]
        WeatherForcust GetForeCustWeather(string city);

    }
}
