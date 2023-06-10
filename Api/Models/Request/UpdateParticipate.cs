using Api.Mappings;
using BusinessObjects;

namespace Api.Models;

public class UpdateParticipate : IMapTo<ParticipatingProject>
{
    public int? CompanyProjectID { get; set; }
    public int? EmployeeID { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public ProjectRole? ProjectRole { get; set; }
}
