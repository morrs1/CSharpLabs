using CSharpLabs.Models;

public class Animal
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Species { get; set; }
    public int Age { get; set; }

    // Делаем связи необязательными
    public int? PersonId { get; set; }  
    public Person? Person { get; set; }

    public int? EnclosureId { get; set; }  
    public Enclosure? Enclosure { get; set; }
}