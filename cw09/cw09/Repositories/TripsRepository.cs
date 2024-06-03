using cw09.Data;
using cw09.Models;
using cw09.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace cw09.Repositories;

public class TripsRepository : ITripsRepository
{
    private readonly ApbdContext _context;

    public TripsRepository(ApbdContext context)
    {
        _context = context;
    }

    public async Task<GetTripsDto> GetTrips(string? query, int? pageNum, int? pageSize)
    {
        var tripsCount = await _context.Trips.Where(e => query == null || e.Name.Contains(query)).CountAsync();
        var pages = (int) Math.Ceiling(tripsCount / (double)pageSize.Value);

        var trips = await _context.Trips.Select(e => new TripsDto()
            {
                Name = e.Name,
                Description = e.Description,
                DateFrom = e.DateFrom,
                DateTo = e.DateTo,
                MaxPeople = e.MaxPeople,
                Countries = e.IdCountries.Select(c => new CountryDto()
                {
                    Name = c.Name
                }),
                Clients = e.ClientTrips.Select(c => new ClientDto()
                {
                    FirstName = c.IdClientNavigation.FirstName,
                    LastName = c.IdClientNavigation.LastName
                })
            })
            .ToListAsync();

        return new GetTripsDto()
        {
            PageNum = pageNum.Value,
            Pages = pages,
            PageSize = pageSize.Value,
            Trips = trips
        };
    }

    public async Task<bool> DoesClientHaveTrip(int id)
    {
        var trips = await _context.ClientTrips.Where(e => e.IdClient == id).ToListAsync();

        return trips.Count > 0;
    }

    public Task DeleteClient(int id)
    {
        _context.Clients.Remove(new Client()
        {
            IdClient = id
        });

        return _context.SaveChangesAsync();
    }
}