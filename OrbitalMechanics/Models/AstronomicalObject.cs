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

        public double CalculateVelocityAtPeriapsis()
        {
            if (this.Orbit == null || this.Primary == null)
            {
                return 0.0;
            }



            return 0;
        }
    }
}