using Api.Mappings;
using BusinessObjects;
using System.ComponentModel.DataAnnotations;

namespace Api.Models;

public class CreateEmpl : IMapTo<Employee>
{
    [Required]
    public string FullName { get; set; }

    [Required]
    public string Skills { get; set; }

    [Required]
    public string Telephone { get; set; }

    [Required]
    public string Address { get; set; }

    [Required]
    public Status Status { get; set; }

    [Required]
    public int DepartmentID { get; set; }

    [Required, EmailAddress]
    public string EmailAddress { get; set; }

    [Required]
    public string Password { get; set; }
}
