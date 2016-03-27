using System;

namespace OrbitalMechanics.Models
{
    public class AstronomicalObject
    {
        public AstronomicalObject(AstronomicalObject primary = null)
        {
            this.Orbit = primary != null ? new Orbit() : null;
            this.Primary = primary;
        }
        /// <summary>
        /// A friendly name for this object
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The object around which this object orbits
        /// </summary>
        public AstronomicalObject Primary { get; set; }

        /// <summary>
        /// Describes the orbit of this object
        /// </summary>
        public Orbit Orbit { get; set; }

        /// <summary>
        /// The mass of this object, in kilograms.
        /// </summary>
        public double Mass { get; set; }

        private double? standardGravitationalParameterField = null;

        /// <summary>
        /// Mathematical notation for the Standard Gravitational Parameter of this object
        /// This value can be manually set in cases where GM is known with greater accuracy than can be calculated
        /// </summary>
        /// <see cref="StandardGravitationalParameter"/>
        public double Mu
        {
            get { return this.StandardGravitationalParameter; }
            set { this.standardGravitationalParameterField = value; }
        }
        
        /// <summary>
        /// The Standard Gravitational Parameter (GM) describes how much acceleration will be applied by gravity to an object orbiting this object.
        /// This value can be manually set in cases where GM is known with greater accuracy than can be calculated
        /// </summary>
        /// <see cref="Mu"/>
        public double StandardGravitationalParameter
        {
            get
            {
                return this.standardGravitationalParameterField.HasValue ?
                    this.standardGravitationalParameterField.Value : this.Mass*AstrodynamicConstants.GravitationalConstant;
            }
            set { this.standardGravitationalParameterField = value; }
        }

        /// <summary>
        /// Calculate the valocity of an object at a specific altitude inside its orbit.
        /// </summary>
        /// <param name="altitude">The altitude for which the velocity is desired</param>
        /// <returns>The object's velocity in meters per second.</returns>
        public double CalculateVelocityAtAltitude(double altitude)
        {
            if (this.Orbit == null || this.Primary == null)
            {
                return 0.0;
            }

            if (this.Orbit.SemiMajorAxis == 0.0)
            {
                throw new InvalidOperationException("Semi-major axis is zero; this calculation would result in dividing by zero.");
            }

            if (altitude == 0.0)
            {
                throw new InvalidOperationException("Altitude is zero; this calculation would result in dividing by zero.");
            }

            //V^2 = GM*(2/r - 1/a)
            // Where GM is the standard gravitational parameter,
            // r = altitude at desired point of calculation
            // a = semi-major axis

            return Math.Sqrt(this.Primary.StandardGravitationalParameter*((2.0/altitude) - (1.0/this.Orbit.SemiMajorAxis)));
        }
    }
}