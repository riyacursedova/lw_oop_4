using System;
using System.Linq;
using System.Text; 

namespace ConsoleApp3
{
    public class BattleService : IBattleService
    {
        private readonly IRepository<Player> _playerRepo;
        private readonly IRepository<Equipment> _equipmentRepo;
        private readonly IRepository<Ability> _abilityRepo;
        private readonly IRepository<Planet> _planetRepo;
        private readonly Random _random = new Random(); // Random proc generator
        
        public BattleService(
            IRepository<Player> playerRepo, 
            IRepository<Equipment> equipmentRepo, 
            IRepository<Ability> abilityRepo, 
            IRepository<Planet> planetRepo)
        {
            _playerRepo = playerRepo;
            _equipmentRepo = equipmentRepo;
            _abilityRepo = abilityRepo;
            _planetRepo = planetRepo;
        }
        
        public string SimulateBattle(int player1Id, int player2Id, int planetId)
        {
            var player1 = _playerRepo.GetById(player1Id);
            var player2 = _playerRepo.GetById(player2Id);
            var planet = _planetRepo.GetById(planetId);
            
            if (player1 == null || player2 == null)
                return "Помилка: Один з гравців не знайдений.";
            if (planet == null)
                return "Помилка: Планета не знайдена.";
            
            var log = new StringBuilder();
            log.AppendLine($"--- Битва на планеті {planet.PlanetName} ---");
            log.AppendLine($" {player1.CharacterName} vs {player2.CharacterName} ");
            log.AppendLine("---------------------------------------");
            
            int player1Damage = CalculateTotalDamage(player1, planet, log);
            int player2Damage = CalculateTotalDamage(player2, planet, log);
            log.AppendLine("---------------------------------------");
            
            int player1Defense = CalculateTotalDefense(player1, planet);
            int player2Defense = CalculateTotalDefense(player2, planet);
            
            log.AppendLine($"{player1.CharacterName} має {player1Defense} захисту.");
            log.AppendLine($"{player2.CharacterName} має {player2Defense} захисту.");
            log.AppendLine("---------------------------------------");
            
            int finalDamageToP2 = Math.Max(0, player1Damage - player2Defense);
            int finalDamageToP1 = Math.Max(0, player2Damage - player1Defense);

            log.AppendLine($"{player1.CharacterName} наносить {finalDamageToP2} фінального урону.");
            log.AppendLine($"{player2.CharacterName} наносить {finalDamageToP1} фінального урону.");
            log.AppendLine("---------------------------------------");
            if (finalDamageToP2 > finalDamageToP1)
                log.AppendLine($" {player1.CharacterName} перемагає! ");
            else if (finalDamageToP1 > finalDamageToP2)
                log.AppendLine($" {player2.CharacterName} перемагає! ");
            else
                log.AppendLine(" Нічия! Сили рівні! ");
            
            return log.ToString();
        }
        
        private int CalculateTotalDamage(Player player, Planet planet, StringBuilder log)
        {
            int totalDamage = player.Power;
            
            totalDamage += planet.PowerBuff;
            
            var playerEquipment = _equipmentRepo.GetAll().Where(e => e.PlayerId == player.Id);
            totalDamage += playerEquipment.Sum(e => e.Strength);
            
            var playerAbilities = _abilityRepo.GetAll().Where(a => a.PlayerId == player.Id);
            foreach (var ability in playerAbilities)
            {
                if (_random.Next(1, 101) <= ability.Combo)
                {
                    int bonusDamage = _random.Next(20, 51); // Proc bonus damage between 20 and 50
                    log.AppendLine($" {player.CharacterName} активує '{ability.Name}' (+{bonusDamage} урону)!");
                    totalDamage += bonusDamage;
                }
            }
            
            log.AppendLine($"-> Розрахунковий урон для {player.CharacterName}: {totalDamage}");
            return totalDamage;
        }
        
        private int CalculateTotalDefense(Player player, Planet planet)
        {
            int totalDefense = player.PowerShield;
            
            totalDefense += planet.ArmourBuff;
            
            var playerEquipment = _equipmentRepo.GetAll().Where(e => e.PlayerId == player.Id);
            totalDefense += playerEquipment.Sum(e => e.Armour);

            return totalDefense;
        }
    }
}