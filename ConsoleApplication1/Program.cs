using Lesson13;
using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace lesson13
{
    class Program
    {
        static void Main(string[] args)
        {
            Weather w = new Weather();
          //  w.GetCurrentWeather("Chicago");
            var result = w.GetForeCustWeather("Kiev");
            Console.ReadLine();
        }

       static void CreateService()
        {
            ServiceHost host = new ServiceHost(typeof(Weather), new Uri("http://localhost:8585/"));
            BasicHttpBinding binding = new BasicHttpBinding();
            ServiceMetadataBehavior mdb = new ServiceMetadataBehavior
            {
                HttpGetUrl = new Uri("http://localhost:8585/Weather"),
                HttpsGetEnabled = true
            };
            host.AddServiceEndpoint(typeof(IWeather), binding, "http://localhost:8585/Weather");
            host.Open();
        }
    }
}
