using System;
using System.Linq;

namespace ConsoleApp3.Commands
{
    public class DisplayDataCommand : ICommands
    {
        private readonly IPlayerService _playerService;
        public string Name { get; } = "display_players";
        public DisplayDataCommand(IPlayerService playerService)
        {
            _playerService = playerService;
        }
        public void Execute()
        {
            Console.WriteLine("=== Відображення даних гравців ===");
            
            var players = _playerService.GetAllPlayers(); 
            
            if (players == null || !players.Any())
            {
                Console.WriteLine("Гравців не знайдено.");
                return;
            }
            
            foreach (var player in players)
            {
                Console.WriteLine($"ID: {player.Id}, Гравець: {player.Name}, Персонаж: {player.CharacterName}, Сила: {player.Power}");
            }
        }
    }
}