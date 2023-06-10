using Api.Mappings;
using BusinessObjects;

namespace Api.Models;

public class UpdateParticipant : IMapTo<ParticipatingProject>
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public ProjectRole? ProjectRole { get; set; }
}
