namespace ConsoleApp3;

public interface IPlayerService
{
    Player CreatePlayer(string playerName, string characterName, AccountType accountType);
    Player GetPlayerById(int playerId);
    List<Player> GetAllPlayers();
    void UpdatePlayer(Player player);
    void DeletePlayer(int playerId);
    void UpdatePlayerPower(int playerId, int power);
}