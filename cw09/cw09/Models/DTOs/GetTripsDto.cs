namespace cw09.Models.DTOs;

public class GetTripsDto
{
    public int PageNum { get; set; }
    public int PageSize { get; set; }
    public int Pages { get; set; }
    public IEnumerable<TripsDto> Trips { get; set; }
}