using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp3
{
    public class PlanetRepository : IRepository<Planet>
    {
        private List<Planet> _planets = new List<Planet>();
        
        public void Add(Planet item)
        {
            if (item.PlanetId == 0)
            {
                int maxId = _planets.Any() ? _planets.Max(p => p.PlanetId) : 0;
                item.PlanetId = maxId + 1;
            }
            _planets.Add(item);
        }
        
        public Planet GetById(int id)
        {
            return _planets.FirstOrDefault(p => p.PlanetId == id);
        }
        
        public IEnumerable<Planet> GetAll()
        {
            return _planets;
        }
        
        public void Update(Planet item)
        {
            var existingPlanet = GetById(item.PlanetId);
            if (existingPlanet != null)
            {
                existingPlanet.PlanetName = item.PlanetName;
                existingPlanet.PowerBuff = item.PowerBuff;
                existingPlanet.ArmourBuff = item.ArmourBuff;
            }
        }
        
        public void Delete(int id)
        {
            var planetToRemove = GetById(id);
            if (planetToRemove != null)
            {
                _planets.Remove(planetToRemove);
            }
        }
    }
}