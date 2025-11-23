namespace ConsoleApp3;

public class Equipment
{
    public int EquipmentId { get; set; }
    public string Name { get; set; }
    public int PlayerId { get; set; } // Owner
    public int Strength { get; set; }
    public int Armour { get; set; }
}