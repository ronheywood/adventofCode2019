using System.Collections.Generic;

namespace Christmas.Day6
{
    public interface IOrbitingBody
    {
        string Name { get; set; }
        int OrbitDepth { get; }
        IOrbitingBody Parent { get; set; }
        void AddPlanet(IOrbitingBody planet);
        IEnumerable<IOrbitingBody> Orbits();
    }
}