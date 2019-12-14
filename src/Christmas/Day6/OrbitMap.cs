using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Christmas.Day6
{
    public class OrbitMap
    {
        private IList<IOrbitingBody> _planets;

        public OrbitMap(TextReader sr)
        {
            _planets = new List<IOrbitingBody>();
            while (sr.Peek() > 0)
            {
                var line = sr.ReadLine()?.Split(')') ?? new string[2];
                Map(line[0], line[1]);
            }
        }

        private void Map(string orbitsAround, string planetName)
        {
            var orbitingBody = _planets.SingleOrDefault(x => x.Name == orbitsAround);
            var existingChild = _planets.SingleOrDefault(x => x.Name == planetName);

            if (orbitingBody == null)
            {
                orbitingBody = new OrbitingBody(){ Name = orbitsAround };
                _planets.Add(orbitingBody);
            }

            if (existingChild == null)
            {
                existingChild = new OrbitingBody(orbitingBody){ Name = planetName};
                _planets.Add(existingChild);
            }
            else if (existingChild.Parent == null)
            {
                existingChild.Parent = orbitingBody;
            }

            orbitingBody.AddPlanet(existingChild);
        }

        public int TotalOrbits()
        {
            return _planets.Select(p => p.OrbitDepth).Sum();
        }

        public int GetTransferCount(string planetNameOrigin, string planetNameDestination)
        {
            var origin = _planets.First(p => p.Name == planetNameOrigin);
            var destination = _planets.First(p => p.Name == planetNameDestination);
            
            var commonAncestors = origin.Orbits().Where(y => destination.Orbits().Any(s => s == y)).ToArray();
            var lastCommonAncestor = commonAncestors.SingleOrDefault(t => t.OrbitDepth == (commonAncestors.Max(m => m.OrbitDepth))) ?? throw new Exception("there is no route to the destination");

            return (
                origin.OrbitDepth - lastCommonAncestor.OrbitDepth
                + destination.OrbitDepth - lastCommonAncestor.OrbitDepth
            );
        }
    }
}