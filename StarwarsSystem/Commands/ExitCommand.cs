using System;
namespace ConsoleApp3.Commands
{
    internal class ExitCommand : ICommands
    {
        public string Name { get; } = "exit";

        public void Execute()
        {
            Console.WriteLine("Бувай, джедаю! Нехай прибуде з тобою Сила.");
            Environment.Exit(0);
        }
    }
}