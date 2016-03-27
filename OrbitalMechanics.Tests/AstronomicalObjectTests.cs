using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OrbitalMechanics.Models;

namespace OrbitalMechanics.Tests
{
    [TestFixture]
    public class AstronomicalObjectTests
    {
        /// <summary>
        /// Generates an sun-earth-moon system for use in the orbital calculation tests
        /// </summary>
        /// <returns></returns>
        private AstronomicalObject GetSunEarthMoonObject()
        {
            AstronomicalObject sun = new AstronomicalObject();

            // Orbital parameters and mass taken from https://en.wikipedia.org/wiki/Sun
            sun.Name = "Sun";
            sun.Mass = 1.98855E30;

            // GM for Sol taken from https://en.wikipedia.org/wiki/Standard_gravitational_parameter
            sun.StandardGravitationalParameter = 1.32712440018E20;

            AstronomicalObject earth = new AstronomicalObject(sun);

            // Orbital parameters and mass taken from https://en.wikipedia.org/wiki/Earth
            earth.Name = "Earth";
            earth.Mass = 5.97237E24; //5.97237×1024 kg
            earth.Orbit.Apoapsis = 152100000000;
            earth.Orbit.Periapsis = 147095000000;
            earth.Orbit.Eccentricity = 0.0167086;
            earth.Orbit.Inclination = 7.155;

            // GM for Earth taken from https://en.wikipedia.org/wiki/Standard_gravitational_parameter
            earth.StandardGravitationalParameter = 3.986004418E14;

            AstronomicalObject moon = new AstronomicalObject(earth);

            // Orbital parameters and mass taken from https://en.wikipedia.org/wiki/Moon
            moon.Name = "Moon";
            moon.Orbit.Periapsis = 362600000;
            moon.Orbit.Apoapsis = 405400000;
            moon.Orbit.Eccentricity = 0.0549;
            moon.Orbit.Inclination = 5.145;

            // GM for Moon taken from https://en.wikipedia.org/wiki/Standard_gravitational_parameter
            moon.StandardGravitationalParameter = 4.9048695E12;

            return earth;
        }

        [Test]
        public void CalculateEarthPeriapsisVelocity()
        {
            AstronomicalObject earth = this.GetSunEarthMoonObject();

            Console.WriteLine(earth.Orbit.SemiMajorAxis);

            double velocityAtPeriapsis = earth.CalculateVelocityAtAltitude(earth.Orbit.Periapsis);
            Console.WriteLine(string.Format("{0:N} m/s", velocityAtPeriapsis));

            //TODO: Finish writing this test
            Assert.AreNotEqual(0.0, velocityAtPeriapsis);

            //Should equal approximately 30,300 m/s for Earth at perihelion
            Assert.AreEqual(303, Math.Round((velocityAtPeriapsis/100)));
        }
    }
}
