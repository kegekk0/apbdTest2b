﻿namespace apbdTest2b.DTOs;

public class CustomerPurchasesDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public List<PurchaseDto> Purchases { get; set; } = new List<PurchaseDto>();
}
    
public class PurchaseDto
{
    public DateTime Date { get; set; }
    public decimal Price { get; set; }
    public TicketDto Ticket { get; set; }
    public ConcertDto Concert { get; set; }
}
    
public class TicketDto
{
    public string Serial { get; set; }
    public int SeatNumber { get; set; }
}
    
public class ConcertDto
{
    public string Name { get; set; }
    public DateTime Date { get; set; }
}