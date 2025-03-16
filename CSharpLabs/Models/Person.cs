using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSharpLabs.Models;

public class Person
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Имя обязательно")]
    [StringLength(50, ErrorMessage = "Максимум 50 символов")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Фамилия обязательна")]
    [StringLength(50, ErrorMessage = "Максимум 50 символов")]
    public string LastName { get; set; }

    [EmailAddress(ErrorMessage = "Некорректный email")]
    public string Email { get; set; }
    
    public ICollection<Animal> Animals { get; set; } = new List<Animal>();
}