using System.ComponentModel.DataAnnotations;

namespace apbdTest2b.Models;

public class Concert
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    
    [Required]
    public DateTime Date { get; set; }

    public List<Ticket> Tickets { get; set; } = new List<Ticket>();
}