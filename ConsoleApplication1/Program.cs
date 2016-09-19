using Lesson13;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace lesson13
{
    class Program
    {
        static void Main(string[] args)
        {
            Weather w = new Weather();
            w.GetCurrentWeather("Chicago");
            w.GetForeCustWeather("Chicago");          
        }

        void CreateService()
        {
            ServiceHost host = new ServiceHost(typeof(Weather));
            BasicHttpBinding binding = new BasicHttpBinding();
            host.AddServiceEndpoint(typeof(IWeather), binding, "http://localhost:8585/WeatherService");
            host.Open();
        }

        void ShareMetedata()
        {
            ServiceMetadataBehavior mdb = new ServiceMetadataBehavior();
            mdb.HttpGetUrl = new Uri("http://localhost:8585/Weather");
            mdb.HttpsGetEnabled = true;
        }

    }
}
