using cw09.Models.DTOs;

namespace cw09.Repositories;

public interface ITripsRepository
{
    Task<GetTripsDto> GetTrips(string? query, int? pageNum, int? pageSize);
    Task<bool> DoesClientHaveTrip(int id);
    Task DeleteClient(int id);
    Task<bool> DoesClientAlreadyExist(string pesel);
    Task<bool> IsClientAlreadyAssignedToTrip(string pesel, int idTrip);
    Task<bool> DoesTripExist(int idTrip);
    Task AssignClientToTrip(ClientTripDto clientTripDto);
}