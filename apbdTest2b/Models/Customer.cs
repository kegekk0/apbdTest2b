using System.ComponentModel.DataAnnotations;

namespace apbdTest2b.Models;

public class Customer
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string LastName { get; set; }
    
    [MaxLength(20)]
    public string? PhoneNumber { get; set; }
    
    public List<Purchase> Purchases { get; set; } = new List<Purchase>();
}