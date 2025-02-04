using Microsoft.AspNetCore.Mvc;

namespace CustomerService.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomersController : ControllerBase
{
    private static readonly string[] _summaries = ["Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"];

    [HttpGet]
    //[Authorize]
    public IEnumerable<Customer> Get ()
    {
        return Enumerable.Range(1, 5).Select(index => new Customer
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = _summaries[Random.Shared.Next(_summaries.Length)]
        })
        .ToArray();
    }

    [HttpGet("getSingle")]
    //[Authorize(Roles = "Admin")]
    public Customer GetSingle ()
    {
        return new Customer
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(5)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = _summaries[Random.Shared.Next(_summaries.Length)]
        };
    }
}