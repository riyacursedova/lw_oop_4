using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp3
{
    public class EquipmentRepository : IRepository<Equipment>
    {
        private List<Equipment> _equipment = new List<Equipment>();
        
        public void Add(Equipment item)
        {
            if (item.EquipmentId == 0)
            {
                int maxId = _equipment.Any() ? _equipment.Max(e => e.EquipmentId) : 0;
                item.EquipmentId = maxId + 1;
            }
            _equipment.Add(item);
        }
        
        public Equipment GetById(int id)
        {
            return _equipment.FirstOrDefault(e => e.EquipmentId == id);
        }
        
        public IEnumerable<Equipment> GetAll()
        {
            return _equipment;
        }
        
        public void Update(Equipment item)
        {
            var existingEquipment = GetById(item.EquipmentId);
            if (existingEquipment != null)
            {
                existingEquipment.Name = item.Name;
                existingEquipment.PlayerId = item.PlayerId;
                existingEquipment.Strength = item.Strength;
                existingEquipment.Armour = item.Armour;
            }
        }
        
        public void Delete(int id)
        {
            var equipmentToRemove = GetById(id);
            if (equipmentToRemove != null)
            {
                _equipment.Remove(equipmentToRemove);
            }
        }
        
        public IEnumerable<Equipment> GetByPlayerId(int playerId)
        {
            return _equipment.Where(e => e.PlayerId == playerId).ToList();
        }
    }
}