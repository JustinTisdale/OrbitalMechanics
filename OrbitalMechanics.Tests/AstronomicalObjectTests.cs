using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OrbitalMechanics.Models;
using OrbitalMechanics.Utility;

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

            double velocityAtPeriapsis = earth.CalculateVelocityAtAltitude(earth.Orbit.Periapsis);
            Console.WriteLine(string.Format("{0:N} m/s", velocityAtPeriapsis));

            //TODO: Finish writing this test
            Assert.AreNotEqual(0.0, velocityAtPeriapsis);

            //Should equal approximately 30,300 m/s for Earth at perihelion
            Assert.AreEqual(303, Math.Round((velocityAtPeriapsis/100)));
        }

        [Test]
        public void CalculateEarthOrbitalPeriod()
        {
            AstronomicalObject earth = this.GetSunEarthMoonObject();

            double orbitalPeriodEarth = earth.CalculateOrbitalPeriod();
            Console.WriteLine(string.Format("{0:N} seconds", orbitalPeriodEarth));
            Console.WriteLine(string.Format("{0:N} days", Conversions.SecondsToDays(orbitalPeriodEarth)));

            Assert.AreEqual(365.0, Math.Round(Conversions.SecondsToDays(orbitalPeriodEarth)));
        }

        [Test]
        public void CalculateSemiMajorAxisForDesiredOrbitalPeriod()
        {
            // I want to launch a geosynchronous satellite. 
            // What semi-major axis do I need to accomplish an orbital period around Earth of exactly 24 hours?

            AstronomicalObject earth = this.GetSunEarthMoonObject();

            AstronomicalObject satellite = new AstronomicalObject(earth);
            satellite.Name = "Geosync Satellite";
            satellite.Mass = 3000.0;

            double desiredSemiMajorAxis = satellite.CalculateSemiMajorAxisForDesiredOrbitalPeriod(Conversions.HoursToSeconds(24));

            // Semi-major axis of a geosynchronous orbit is 42,164 km according to https://en.wikipedia.org/wiki/Geosynchronous_orbit
            Assert.AreEqual(42164000.0, Math.Round(desiredSemiMajorAxis));
        }
    }
}
