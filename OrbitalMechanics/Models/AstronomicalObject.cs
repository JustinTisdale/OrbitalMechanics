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
                throw new InvalidOperationException("Orbital data missing.");
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
            // Where GM is the standard gravitational parameter of the primary,
            // r = altitude at desired point of calculation
            // a = semi-major axis

            return Math.Sqrt(this.Primary.StandardGravitationalParameter*((2.0/altitude) - (1.0/this.Orbit.SemiMajorAxis)));
        }

        /// <summary>
        /// Calculate the orbital period for this object
        /// </summary>
        /// <returns></returns>
        public double CalculateOrbitalPeriod()
        {
            if (this.Orbit == null || this.Primary == null)
            {
                throw new InvalidOperationException("Orbital data missing.");
            }

            // T = 2 * pi * sqrt(a^3 / GM }
            // Where a = semi-major axis
            // GM = standard gravitational parameter of the primary

            return 2.0 * Math.PI * Math.Sqrt(Math.Pow(this.Orbit.SemiMajorAxis, 3.0) / this.Primary.StandardGravitationalParameter);
        }

        /// <summary>
        /// Calculate a semi-major axis for an orbit with the specified orbital period (in seconds)
        /// </summary>
        /// <param name="targetOrbitalPeriod">The desired orbital period (in seconds)</param>
        /// <returns>The semi-major axis for the orbit needed to achieve the desired orbital period</returns>
        public double CalculateSemiMajorAxisForDesiredOrbitalPeriod(double targetOrbitalPeriod)
        {
            if (this.Primary == null)
            {
                throw new InvalidOperationException("Primary orbital data missing.");
            }

            //a = cubed root of (GM * T^2) / (4 * pi^2)
            // Where
            // a = semi-major axis
            // GM = standard gravitational parameter of the primary
            // T = time in seconds

            return Math.Pow(((this.Primary.StandardGravitationalParameter * Math.Pow(targetOrbitalPeriod, 2.0)) / (4.0 * Math.Pow(Math.PI, 2.0)) ), (1.0 / 3.0));
        }
    }
}