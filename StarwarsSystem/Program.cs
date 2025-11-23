using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApp3;
using ConsoleApp3.Commands; 

public class Program
{
    private static Dictionary<string, ICommands> _commands;

    public static void Main(string[] args)
    {
        IRepository<Player> playerRepository = new PlayerRepository();
        IRepository<Equipment> equipmentRepository = new EquipmentRepository();
        IRepository<Ability> abilityRepository = new AbilityRepository();
        IRepository<Planet> planetRepository = new PlanetRepository();
        IPlayerService playerService = new PlayerService(playerRepository);
        
        IBattleService battleService = new BattleService(
            playerRepository, 
            equipmentRepository, 
            abilityRepository, 
            planetRepository
        );
        
        _commands = new Dictionary<string, ICommands>
        {
            { "add_player", new AddDataCommand(playerService, equipmentRepository) },
            { "display_players", new DisplayDataCommand(playerService) },
            { "battle", new BattleCommand(battleService) }, 
            { "delete_player", new DeletePlayerCommand(playerService) },
            { "help", new HelpCommand() },
            { "exit", new ExitCommand() }
        };
        
        InitializeData(playerRepository, equipmentRepository, abilityRepository, planetRepository);
        Console.WriteLine("=============================================");
        Console.WriteLine("Ласкаво просимо у Всесвіт Star Wars .");
        Console.WriteLine("Введіть 'help' для перегляду списку команд.");
        Console.WriteLine("=============================================");

        while (true)
        {
            Console.Write("\n> "); 
            string input = Console.ReadLine()?.Trim().ToLower();

            if (string.IsNullOrEmpty(input))
            {
                continue;
            }

            if (_commands.ContainsKey(input))
            {
                try
                {
                    _commands[input].Execute();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"!! Виникла помилка при виконанні команди: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Невідома команда. Введіть 'help' для списку.");
            }
        }
    }
    private static void InitializeData(
        IRepository<Player> pRepo, 
        IRepository<Equipment> eRepo, 
        IRepository<Ability> aRepo,
        IRepository<Planet> plRepo)
    {
        
        pRepo.Add(new Player { Id = 1, Name = "DefaultPlayer1", CharacterName = "Luke Skywalker", Health = 100, Power = 100, PowerShield = 50, AccountType = AccountType.RebelAlliance });
        pRepo.Add(new Player { Id = 2, Name = "DefaultPlayer2", CharacterName = "Darth Vader", Health = 150, Power = 120, PowerShield = 70, AccountType = AccountType.GalacticEmpire });
        
        plRepo.Add(new Planet { PlanetId = 1, PlanetName = "Tatooine", PowerBuff = 5, ArmourBuff = 0 });
        plRepo.Add(new Planet { PlanetId = 2, PlanetName = "Hoth", PowerBuff = 0, ArmourBuff = 10 });
        plRepo.Add(new Planet { PlanetId = 3, PlanetName = "Dagobah", PowerBuff = 15, ArmourBuff = 5 });
        
        eRepo.Add(new Equipment { EquipmentId = 1, PlayerId = 1, Name = "Lightsaber (Blue)", Strength = 50, Armour = 10 });
        eRepo.Add(new Equipment { EquipmentId = 2, PlayerId = 2, Name = "Lightsaber (Red)", Strength = 60, Armour = 20 });


        eRepo.Add(new Equipment { Name = "Blaster Pistol", Strength = 45, Armour = 5 }); // ID = 3
        eRepo.Add(new Equipment { Name = "Basic Armor", Strength = 0, Armour = 45 });    // ID = 4
        eRepo.Add(new Equipment { Name = "Vibroknife", Strength = 40, Armour = 10 });    // ID = 5
        
        aRepo.Add(new Ability { AbilityId = 1, PlayerId = 1, Name = "Force Push", Combo = 20 }); // 20% chance
        aRepo.Add(new Ability { AbilityId = 2, PlayerId = 2, Name = "Force Choke", Combo = 30 }); // 30% chance
    }
}