using System.Collections.Generic;

namespace Christmas.Day6
{
    public class OrbitingBody : IOrbitingBody
    {
        public int OrbitDepth => Parent?.OrbitDepth+1 ?? 0;

        public OrbitingBody()
        {
        }
        public OrbitingBody(IOrbitingBody parent)
        {
            Parent = parent;
        }
        public IOrbitingBody Parent { get; set; }

        public IList<IOrbitingBody> OrbitingBodies { get; } = new List<IOrbitingBody>();

        public string Name { get; set; }

        public override string ToString()
        {
            return $"Planet {Name}";
        }

        public void AddPlanet(IOrbitingBody planet)
        {
            planet.Parent = this;
            OrbitingBodies.Add(planet);
        }

        public IEnumerable<IOrbitingBody> Orbits()
        {
            var orbits = new List<IOrbitingBody>();
            var directOrbit = Parent;

            while (directOrbit != null)
            {
                orbits.Add(directOrbit);
                directOrbit = directOrbit.Parent;
            }

            return orbits;
        }
    }
}