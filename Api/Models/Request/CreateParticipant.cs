using Api.Mappings;
using BusinessObjects;
using System.ComponentModel.DataAnnotations;

namespace Api.Models;

public class CreateParticipant : IMapTo<ParticipatingProject>
{

    [Required]
    public int EmployeeID { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    [Required]
    public ProjectRole ProjectRole { get; set; }
}
