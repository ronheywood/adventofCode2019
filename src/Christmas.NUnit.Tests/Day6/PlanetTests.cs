using System.Collections.Generic;
using System.Linq;
using Christmas.Day6;
using NUnit.Framework;

namespace Christmas.NUnit.Tests.Day6
{
    [TestFixture]
    public class PlanetTests
    {
        [Test]
        public void Should_have_name()
        {
            var planet = new OrbitingBody(){ Name = "Test"};
            Assert.That(planet.ToString(), Is.EqualTo("Planet Test"));
        }
        [Test]
        public void Should_have_list_of_orbiting_bodies()
        {
            var planet = new OrbitingBody(){ Name = "Test"};
            Assert.That(planet.OrbitingBodies, Is.InstanceOf<IEnumerable<IOrbitingBody>>());
            planet.AddPlanet(new OrbitingBody() {Name = "moon"});
            Assert.That(planet.OrbitingBodies.Count, Is.EqualTo(1));
            Assert.That(planet.OrbitingBodies.First(p => p.Name == "moon"),Is.InstanceOf<IOrbitingBody>());
        }
        [Test]
        public void Should_have_list_of_orbits_back_to_centre()
        {
            var sun = new OrbitingBody() {Name = "Sun"};
            var earth = new OrbitingBody() {Name = "Earth"};
            var moon = new OrbitingBody() {Name = "Moon"};
            sun.AddPlanet(earth);
            earth.AddPlanet(moon);
            Assert.That(moon.Orbits, Is.InstanceOf<IEnumerable<IOrbitingBody>>());
        }
        [Test]
        public void Should_calculate_orbit_depth()
        {
            var sun = new OrbitingBody() {Name = "Sun"};
            var earth = new OrbitingBody() {Name = "Earth"};
            var moon = new OrbitingBody() {Name = "Moon"};
            sun.AddPlanet(earth);
            earth.AddPlanet(moon);
            Assert.That(moon.OrbitDepth, Is.EqualTo(2));
        }
    }
}