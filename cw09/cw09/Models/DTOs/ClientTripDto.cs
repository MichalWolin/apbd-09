using System.ComponentModel.DataAnnotations;

namespace cw09.Models.DTOs;

public class ClientTripDto
{
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]

}