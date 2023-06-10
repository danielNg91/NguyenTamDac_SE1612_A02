using Api.Mappings;
using BusinessObjects;
using System.ComponentModel.DataAnnotations;

namespace Api.Models;

public class CreateProject: IMapTo<CompanyProject>
{

    [Required]
    public string ProjectName { get; set; }

    [Required]
    public string ProjectDescription { get; set; }

    [Required]
    public DateTime EstimatedStartDate { get; set; }

    [Required]
    public DateTime ExpectedEndDate { get; set; }
}
