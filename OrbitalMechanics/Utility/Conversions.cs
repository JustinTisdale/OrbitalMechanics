using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrbitalMechanics.Utility
{
    public static class Conversions
    {
        public static double SecondsToDays(double secondsToConvert)
        {
            // Unit conversion, seconds -> minutes -> hours => days
            return secondsToConvert/60.0/60.0/24.0;
        }
    }
}
