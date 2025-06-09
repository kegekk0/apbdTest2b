using apbdTest2b.DTOs;
using apbdTest2b.Services;
using Microsoft.AspNetCore.Mvc;

namespace apbdTest2b.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly CustomerService _customerService;

    [HttpGet("{customerId}/purchases")]
    public async Task<IActionResult> GetCustomerPurchases(int customerId)
    {
        var result = await _customerService.GetCustomerPurchasesAsync(customerId);
            
        if (result == null)
            return NotFound("Customer not found");
                
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddCustomerWithPurchases([FromBody] AddCustomerDto addCustomerDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
                
        var success = await _customerService.AddCustomerWithPurchasesAsync(addCustomerDto);
            
        if (!success)
            return BadRequest("Failed to add customer");
                
        return Ok("Customer and purchases added");
    }
}