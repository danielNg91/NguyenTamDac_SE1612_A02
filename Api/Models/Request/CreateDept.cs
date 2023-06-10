using Api.Mappings;
using BusinessObjects;
using System.ComponentModel.DataAnnotations;

namespace Api.Models;

public class CreateDept : IMapTo<Department>
{
    [Required]
    public string DepartmentName { get; set; }

    [Required]
    public string DepartmentDescription { get; set; }
}
