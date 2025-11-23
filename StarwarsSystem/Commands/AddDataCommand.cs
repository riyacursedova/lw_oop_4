using System;
using System.Linq; 

namespace ConsoleApp3.Commands
{
    public class AddDataCommand : ICommands
    {
        private readonly IPlayerService _playerService;
        private readonly IRepository<Equipment> _equipmentRepo; 

        public string Name { get; } = "add_player";
        public AddDataCommand(IPlayerService playerService, IRepository<Equipment> equipmentRepo)
        {
            _playerService = playerService;
            _equipmentRepo = equipmentRepo;
        }
        public void Execute()
        {
            try
            {
                Console.WriteLine("=== Додавання нового гравця ===");
                
                Console.Write("Введіть ім'я гравця (ваше ім'я): ");
                string playerName = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(playerName))
                {
                    Console.WriteLine("Ім'я не може бути пустим. Створення скасовано.");
                    return;
                }

                Console.Write("Введіть ім'я персонажа (напр., 'Obi-Wan Kenobi'): ");
                string characterName = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(characterName))
                {
                    Console.WriteLine("Ім'я персонажа не може бути пустим. Створення скасовано.");
                    return;
                }

                Console.WriteLine("Оберіть фракцію:");
                Console.WriteLine("  1. JediOrder");
                Console.WriteLine("  2. GalacticEmpire");
                Console.WriteLine("  3. RebelAlliance");
                Console.Write("Ваш вибір (1-3): ");
                
                AccountType accountType;
                switch (Console.ReadLine())
                {
                    case "1": accountType = AccountType.JediOrder; break;
                    case "2": accountType = AccountType.GalacticEmpire; break;
                    case "3": accountType = AccountType.RebelAlliance; break;
                    default: 
                        Console.WriteLine("Невірний вибір. Встановлено RebelAlliance.");
                        accountType = AccountType.RebelAlliance; 
                        break;
                }
                
                Player newPlayer = _playerService.CreatePlayer(playerName, characterName, accountType);
                
                Console.WriteLine($"\nУспішно створено гравця: {newPlayer.CharacterName} (ID: {newPlayer.Id})");
                
                var availableItems = _equipmentRepo.GetAll().Where(e => e.PlayerId == 0).ToList();

                if (!availableItems.Any())
                {
                    Console.WriteLine("Вільних предметів для видачі не знайдено.");
                    return;
                }

                Console.WriteLine("\n--- Виберіть стартовий предмет ---");
                Console.WriteLine("  0 - Нічого (пропустити)");
                foreach (var item in availableItems)
                {
                    Console.WriteLine($"  {item.EquipmentId} - {item.Name} (Сила: {item.Strength}, Броня: {item.Armour})");
                }

                Console.Write("Ваш вибір ID предмета: ");
                if (int.TryParse(Console.ReadLine(), out int itemId) && itemId > 0)
                {
                    var selectedItem = availableItems.FirstOrDefault(i => i.EquipmentId == itemId);
                    
                    if (selectedItem != null)
                    {
                        selectedItem.PlayerId = newPlayer.Id;
                        _equipmentRepo.Update(selectedItem);
                        
                        Console.WriteLine($"Предмет '{selectedItem.Name}' успішно виданий гравцю {newPlayer.CharacterName}.");
                    }
                    else
                    {
                        Console.WriteLine("Предмет з таким ID не знайдено серед доступних.");
                    }
                }
                else
                {
                    Console.WriteLine("Вибір пропущено.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка при створенні гравця: {ex.Message}");
            }
        }
    }
}