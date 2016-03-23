using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrbitalMechanics.Models
{
    /// <summary>
    /// Data about an orbit. 
    /// </summary>
    /// <remarks>
    /// All units of distance are in Meters and mass in kilograms
    /// </remarks>
    public class Orbit
    {
        public double Apoapsis { get; set; }

        public double Periapsis { get; set; }

        public double Eccentricity { get; set; }

        public double Inclination { get; set; }

        /// <summary>
        /// Semi Major Axis = the average of the orbit's apoapsis and periapsis.
        /// In the case of a circular orbit, this will be exactly equal to the Apoapsis and Periapsis altitudes.
        /// </summary>
        public double SemiMajorAxis
        {
            get { return (this.Apoapsis + this.Periapsis)/2.0; }
        }
    }
}
