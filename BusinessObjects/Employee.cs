using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessObjects;
public class Employee
{
    [Required]
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int EmployeeID { get; set; }

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

    public virtual Department Department { get; set; }

    [Required]
    public string EmailAddress { get; set; }

    [Required]
    public string Password { get; set; }

    [JsonIgnore]
    public virtual ICollection<ParticipatingProject> ParticipatingProjects { get; set; }

}

public enum Status
{
    Inactive,
    Active
}
