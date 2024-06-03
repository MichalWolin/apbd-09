using cw09.Data;
using cw09.Models;
using cw09.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cw09.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TripsController : ControllerBase
{
    private readonly ITripsRepository _tripsRepository;

    public TripsController(ITripsRepository tripsRepository)
    {
        _tripsRepository = tripsRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetTrips(string? query, int? pageNum, int? pageSize)
    {
        pageSize ??= 10;
        pageNum ??= 1;

        if (pageSize <= 0)
            return BadRequest("Page size must be greater than 0");

        if (pageNum <= 0)
            return BadRequest("Page number must be greater than 0");

        return Ok(await _tripsRepository.GetTrips(query, pageNum, pageSize));
    }
}