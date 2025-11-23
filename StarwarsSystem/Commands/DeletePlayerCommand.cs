using System;

namespace ConsoleApp3.Commands
{
    public class DeletePlayerCommand : ICommands
    {
        private readonly IPlayerService _playerService;
        public string Name { get; } = "delete_player";

        public DeletePlayerCommand(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        public void Execute()
        {
            Console.WriteLine("=== Видалення гравця ===");
            try
            {
                Console.Write("Введіть ID гравця, якого хочете видалити: ");
                
                if (int.TryParse(Console.ReadLine(), out int playerId))
                {
                    _playerService.DeletePlayer(playerId);
                    Console.WriteLine($"Гравець з ID {playerId} успішно видалений.");
                    Console.WriteLine("(Можете перевірити командою 'display_players')");
                }
                else
                {
                    Console.WriteLine("Помилка: Ви ввели не число.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"!! Виникла помилка: {ex.Message}");
            }
        }
    }
}