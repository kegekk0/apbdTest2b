using System.ComponentModel.DataAnnotations;

namespace apbdTest2b.Models;

public class Purchase
{
    public int Id { get; set; }
    
    [Required]
    public DateTime Date { get; set; }
    
    [Required]
    public decimal Price { get; set; }
    
    [Required]
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    
    [Required]
    public int TicketId { get; set; }
    public Ticket Ticket { get; set; }
}