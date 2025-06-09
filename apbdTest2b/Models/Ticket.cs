using System.ComponentModel.DataAnnotations;

namespace apbdTest2b.Models;

public class Ticket
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Serial { get; set; }
    
    [Required]
    public int SeatNumber { get; set; }
    
    public int ConcertId { get; set; }
    public Concert Concert { get; set; }
    public List<Purchase> Purchases { get; set; } = new List<Purchase>();
}