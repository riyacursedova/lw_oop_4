using System;
namespace ConsoleApp3.Commands
{
    internal class HelpCommand : ICommands
    {
        public string Name { get; } = "help";

        public void Execute()
        {
            Console.WriteLine("--- Доступні команди ---");
            Console.WriteLine("  help            - Показати цей список команд");
            Console.WriteLine("  add_player      - Додати нового гравця");
            Console.WriteLine("  display_players - Відобразити список всіх гравців");
            Console.WriteLine("  delete_player   - Видалити гравця за ID");
            Console.WriteLine("  battle          - Запустити симуляцію битви (ID1, ID2, PlanetID)");
            Console.WriteLine("  exit            - Вийти з програми");
        }
    }
}