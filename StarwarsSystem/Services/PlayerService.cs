namespace ConsoleApp3;

public class PlayerService : IPlayerService
{
    private readonly IRepository<Player> _playerRepository;
    
    public PlayerService(IRepository<Player> playerRepository)
    {
        _playerRepository = playerRepository;
    }
    
    public Player CreatePlayer(string playerName, string characterName, AccountType accountType)
    {
        var player = new Player
        {
            Name = playerName,
            CharacterName = characterName,
            AccountType = accountType,
            Power = 150
        };
        _playerRepository.Add(player); 
        return player; 
    }
    
    public Player GetPlayerById(int playerId)
    {
        return _playerRepository.GetById(playerId);
    }
    
    public List<Player> GetAllPlayers()
    {
        return _playerRepository.GetAll().ToList();
    }
    
    public void UpdatePlayer(Player player)
    {
        _playerRepository.Update(player);
    }
    
    public void DeletePlayer(int playerId)
    {
        _playerRepository.Delete(playerId);
    }

    public void UpdatePlayerPower(int playerId, int power)
    {
        var player = _playerRepository.GetById(playerId);
        if (player != null)
        {
            player.Power = power;
            _playerRepository.Update(player);
        }

    }
}