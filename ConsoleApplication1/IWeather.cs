using System.Xml;
using System.ServiceModel;

namespace Lesson13
{
    [ServiceContract]
    internal interface IWeather
    {
        [OperationContract]
        XmlDocument GetCurrentWeather(string city);
        [OperationContract]
        WeatherForcust GetForeCustWeather(string city);

    }
}
