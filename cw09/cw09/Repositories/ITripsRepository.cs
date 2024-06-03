using cw09.Models.DTOs;

namespace cw09.Repositories;

public interface ITripsRepository
{
    Task<GetTripsDto> GetTrips(string? query, int? pageNum, int? pageSize);
}