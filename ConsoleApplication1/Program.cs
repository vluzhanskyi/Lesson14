using WeatherService;
using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace WeatherService
{
    class Program
    {
        static void Main(string[] args)
        {
            //var w = new WeatherService();
            //var FcResult = w.GetForeCustWeather("Kiev");
            //var CurrentResult = w.GetCurrentWeather("Kiev");
            CreateService();
            Console.ReadLine();
        }

        private static void CreateService()
        {
            ServiceHost host = new ServiceHost(typeof(WeatherService));
            BasicHttpBinding binding = new BasicHttpBinding();
            binding.CloseTimeout = TimeSpan.MaxValue;
            ServiceMetadataBehavior mdb = new ServiceMetadataBehavior
            {
                HttpGetUrl = new Uri("http://localhost:8585/Weather"),
                HttpGetEnabled = true
                
            };
           
            var debug = host.Description.Behaviors.Find<ServiceDebugBehavior>();
           debug.IncludeExceptionDetailInFaults = true;
           
           host.Description.Behaviors.Add(mdb);

           
            host.AddServiceEndpoint(typeof(IWeatherService), binding, "http://localhost:8585/Weather");
           
            host.Open();
        }
    }
}
