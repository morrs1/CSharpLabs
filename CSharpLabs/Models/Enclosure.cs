public class Enclosure
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Area { get; set; }

    public int? AnimalId { get; set; } 
    public Animal? Animal { get; set; }
}