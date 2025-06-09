using System.ComponentModel.DataAnnotations;

namespace apbdTest2b.DTOs;

public class AddCustomerDto
{
    [Required]
    public CustomerDto Customer { get; set; }
    
    [Required]
    public List<PurchaseRequestDto> Purchases { get; set; }
}

public class CustomerDto
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
}

public class PurchaseRequestDto
{
    [Required] public int SeatNumber { get; set; }

    [Required] public string ConcertName { get; set; }

    [Required]
    [Range(0.01, double.MaxValue)]
    public decimal Price { get; set; }
}    