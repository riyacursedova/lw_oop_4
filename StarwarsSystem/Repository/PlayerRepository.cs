using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp3
{
    public class PlayerRepository : IRepository<Player>
    {
        private List<Player> _players = new List<Player>();
        
        public void Add(Player entity)
        {
            if (entity.Id == 0)
            {
                int maxId = _players.Any() ? _players.Max(p => p.Id) : 0;
                entity.Id = maxId + 1;
            }
            _players.Add(entity);
        }

        public Player GetById(int id)
        {
            return _players.FirstOrDefault(p => p.Id == id);
        }
        
        public IEnumerable<Player> GetAll()
        {
            return _players;
        }
        
        public void Update(Player entity)
        {
            var existingPlayer = GetById(entity.Id);
            if (existingPlayer != null)
            {
                existingPlayer.Name = entity.Name;
                existingPlayer.CharacterName = entity.CharacterName;
                existingPlayer.Health = entity.Health;
                existingPlayer.Power = entity.Power;
                existingPlayer.PowerShield = entity.PowerShield;
                existingPlayer.AccountType = entity.AccountType;
            }
        }
        
        public void Delete(int id)
        {
            var player = GetById(id);
            if (player != null)
                _players.Remove(player);
        }
    }
}