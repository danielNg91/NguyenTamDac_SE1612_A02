using BusinessObjects;
using WebClient.Mappings;

namespace WebClient.Models;

public class UpdateProject : IMapFrom<CompanyProject>
{
    public string? ProjectName { get; set; }
    public string? ProjectDescription { get; set; }
    public DateTime? EstimatedStartDate { get; set; }
    public DateTime? ExpectedEndDate { get; set; }
}
