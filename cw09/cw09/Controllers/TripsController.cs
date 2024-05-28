using cw09.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cw09.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TripsController : ControllerBase
{
    private readonly ApbdContext _context;
    
    public TripsController(ApbdContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetTrips()
    {
        var trips = await _context.Trips.Select(e => new
            {
                Name = e.Name,
                Countries = e.IdCountries.Select(c => new
                {
                    Name = c.Name
                })
            })
            .ToListAsync();

        var tripsIcnlude = await _context.Trips
            .Include(e => e.IdCountries)
            .ToListAsync();

        return Ok(trips);
    }
}