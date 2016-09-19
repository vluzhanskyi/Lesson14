using System;
using System.Collections.Generic;
using System.Linq;
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

    }
}
