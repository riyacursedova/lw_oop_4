using System;

namespace ConsoleApp3.Commands
{
    public class BattleCommand : ICommands
    {
        private readonly IBattleService _battleService;
        
        public string Name { get; } = "battle";
        
        public BattleCommand(IBattleService battleService)
        {
            _battleService = battleService;
        }
        
        public void Execute()
        {
            Console.WriteLine("===  Симуляція Битви  ===");
            try
            {
                Console.Write("Введіть ID Гравця 1 (напр., 1 для Luke): ");
                int player1Id = int.Parse(Console.ReadLine());

                Console.Write("Введіть ID Гравця 2 (напр., 2 для Vader): ");
                int player2Id = int.Parse(Console.ReadLine());

                Console.Write("Введіть ID Планети (напр., 1, 2 або 3): ");
                int planetId = int.Parse(Console.ReadLine());
                
                string resultLog = _battleService.SimulateBattle(player1Id, player2Id, planetId);
                
                Console.WriteLine("\n--- Результат Битви ---");
                Console.WriteLine(resultLog);
            }
            catch (FormatException)
            {
                Console.WriteLine("Помилка: Ви ввели не число. Будь ласка, вводьте тільки ID.");
            }
        }
    }
}