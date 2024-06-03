using cw09.Data;
using cw09.Models;
using cw09.Models.DTOs;
using cw09.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cw09.Controllers;

[ApiController]
public class TripsController : ControllerBase
{
    private readonly ITripsRepository _tripsRepository;

    public TripsController(ITripsRepository tripsRepository)
    {
        _tripsRepository = tripsRepository;
    }

    [HttpGet("api/trips")]
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

    [HttpDelete("api/clients/{id}")]
    public async Task<IActionResult> DeleteClient(int id)
    {
        if (await _tripsRepository.DoesClientHaveTrip(id))
            return BadRequest("Client has existing trips!");

        return Ok(_tripsRepository.DeleteClient(id));
    }

    [HttpPost("api/trips/{idTrip}/clients")]
    public async Task<IActionResult> AddClientToTrip(ClientTripDto clientTripDto)
    {
        if (await _tripsRepository.DoesClientAlreadyExist(clientTripDto.Pesel))
            return BadRequest("Client already exists!");

        if (await _tripsRepository.IsClientAlreadyAssignedToTrip(clientTripDto.Pesel, clientTripDto.IdTrip))
            return BadRequest("Client is already assigned to this trip!");

        if (!await _tripsRepository.DoesTripExist(clientTripDto.IdTrip))
            return BadRequest("Trip does not exist!");

        await _tripsRepository.AssignClientToTrip(clientTripDto);

        return Ok(clientTripDto);
    }
}