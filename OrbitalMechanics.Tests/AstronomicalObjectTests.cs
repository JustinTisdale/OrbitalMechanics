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
        [Test]
        public void CalculateEarthPeriapsisVelocity()
        {
            AstronomicalObject sun = new AstronomicalObject();

            sun.Name = "Sun";
            sun.Mass = 1.98855E30;
            sun.StandardGravitationalParameter = 1.32712440018E20;

            AstronomicalObject earth = new AstronomicalObject(sun);

            earth.Name = "Earth";
            earth.Mass = 5.97237E24; //5.97237×1024 kg

            earth.Orbit.Apoapsis = 152100000000;
            earth.Orbit.Periapsis = 147095000000;
            earth.Orbit.Eccentricity = 0.0167086;
            earth.Orbit.Inclination = 7.155;

            Console.WriteLine(earth.Orbit.SemiMajorAxis);

            double velocityAtPeriapsis = earth.CalculateVelocityAtPeriapsis();

            //TODO: Finish writing this test
            Assert.AreNotEqual(0.0, velocityAtPeriapsis);
        }
    }
}
