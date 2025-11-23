using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp3
{
    public class AbilityRepository : IRepository<Ability>
    {
        private List<Ability> _abilities = new List<Ability>();
        private int _nextId = 1; 
        
        public void Add(Ability item)
        {
            if (item.AbilityId == 0)
            {
                item.AbilityId = _nextId++;
            }
            
            _abilities.Add(item);
        }
        
        public Ability GetById(int id)
        {
            return _abilities.FirstOrDefault(a => a.AbilityId == id);
        }
        
        public IEnumerable<Ability> GetAll()
        {
            return _abilities;
        }
        
        public void Update(Ability item)
        {
            var existingAbility = GetById(item.AbilityId);
            if (existingAbility != null)
            {
                existingAbility.Name = item.Name;
                existingAbility.PlayerId = item.PlayerId;
                existingAbility.Combo = item.Combo;
            }
        }
        
        public void Delete(int id)
        {
            var abilityToRemove = GetById(id);
            if (abilityToRemove != null)
            {
                _abilities.Remove(abilityToRemove);
            }
        }
        
        public IEnumerable<Ability> GetByPlayerId(int playerId)
        {
            return _abilities.Where(a => a.PlayerId == playerId).ToList();
        }
    }
}