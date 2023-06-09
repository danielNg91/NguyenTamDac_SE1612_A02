using Api.Mappings;
using BusinessObjects;
using System.ComponentModel.DataAnnotations;

namespace Api.Models;

public class UpdateEmp : IMapTo<Employee>
{
    public string? FullName { get; set; }
    public string? Skills { get; set; }
    public string? Telephone { get; set; }
    public string? Address { get; set; }
    public Status? Status { get; set; }
    public int? DepartmentID { get; set; }
    public string? Password { get; set; }
}
