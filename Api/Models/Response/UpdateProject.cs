using Api.Mappings;
using BusinessObjects;
using System.ComponentModel.DataAnnotations;

namespace Api.Models;

public class UpdateProject : IMapTo<CompanyProject>
{
    public string? ProjectName { get; set; }
    public string? ProjectDescription { get; set; }
    public DateTime? EstimatedStartDate { get; set; }
    public DateTime? ExpectedEndDate { get; set; }
}
