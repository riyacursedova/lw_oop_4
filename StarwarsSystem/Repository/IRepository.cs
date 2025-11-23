namespace ConsoleApp3;

public interface IRepository<T>
{
    void Add(T item); // Create: player, planet, rating, planet
    T GetById(int id); // Search by id
    IEnumerable<T> GetAll(); // Get all items
    void Update(T item); // Update item
    void Delete(int id); // Delete item by id
}