using System.Data.Common;
using apbdTest2b.Data;
using apbdTest2b.DTOs;
using apbdTest2b.Models;
using Microsoft.EntityFrameworkCore;

namespace apbdTest2b.Services;

public class CustomerService
{
    private readonly DatabaseContext _context;

    public CustomerService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<CustomerPurchasesDto?> GetCustomerPurchasesAsync(int customerId)
    {
        var customer = await _context.Customers
            .Include(c => c.Purchases)
            .ThenInclude(p => p.Ticket)
            .ThenInclude(t => t.Concert)
            .FirstOrDefaultAsync(c => c.Id == customerId);
        
        if(customer == null)
            return null;

        return new CustomerPurchasesDto
        {
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            PhoneNumber = customer.PhoneNumber,
            Purchases = customer.Purchases.Select(p => new PurchaseDto
            {
                Date = p.Date,
                Price = p.Price,
                Ticket = new TicketDto
                {
                    Serial = p.Ticket.Serial,
                    SeatNumber = p.Ticket.SeatNumber
                },
                Concert = new ConcertDto
                {
                    Name = p.Ticket.Concert.Name,
                    Date = p.Ticket.Concert.Date
                }
            }).ToList()
        };
    }

    public async Task<bool> AddCustomerWithPurchasesAsync(AddCustomerDto addCustomerDto)
    {
        var existingCustomer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == addCustomerDto.Customer.Id);
        if(existingCustomer != null)
            return false;

        var purchasesByConcert = addCustomerDto.Purchases.GroupBy(p => p.ConcertName);
        foreach (var group in purchasesByConcert)
        {
            if(group.Count() > 5)
                return false;
        }
        
        var concertNames = addCustomerDto.Purchases.Select(p => p.ConcertName).Distinct().ToList();
        var existingConcerts = await _context.Concerts
            .Where(c => concertNames.Contains(c.Name))
            .ToListAsync();

        if (existingConcerts.Count != concertNames.Count)
            return false;

        var customer = new Customer
        {
            Id = addCustomerDto.Customer.Id,
            FirstName = addCustomerDto.Customer.FirstName,
            LastName = addCustomerDto.Customer.LastName,
            PhoneNumber = addCustomerDto.Customer.PhoneNumber,
        };
        
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();

        foreach (var purchaseRequest in addCustomerDto.Purchases)
        {
            var concert = existingConcerts.First(c => c.Name == purchaseRequest.ConcertName);
            
            var ticketSerial = $"TK{DateTime.Now.Ticks % 10000}/S{purchaseRequest.SeatNumber}/{DateTime.Now.Millisecond}";

            var ticket = new Ticket
            {
                Serial = ticketSerial,
                SeatNumber = purchaseRequest.SeatNumber,
                ConcertId = concert.Id,
            };
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();
            
            var purchase = new Purchase
            {
                Date = DateTime.Now,
                Price = purchaseRequest.Price,
                CustomerId = customer.Id,
                TicketId = ticket.Id
            };
                
            _context.Purchases.Add(purchase);
        }
            
        await _context.SaveChangesAsync();
        return true;
    }
}