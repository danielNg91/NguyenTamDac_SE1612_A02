using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessObjects;

[PrimaryKey(nameof(CompanyProjectID), nameof(EmployeeID))]
public class ParticipatingProject
{
    [Required]
    [Column(Order = 0), Key, ForeignKey(nameof(CompanyProject))]
    public int CompanyProjectID { get; set; }
    [JsonIgnore]
    public virtual CompanyProject CompanyProject { get; set; }


    [Required]
    [Column(Order = 1), Key, ForeignKey(nameof(Employee))]
    public int EmployeeID { get; set; }
    public virtual Employee Employee { get; set; }


    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    [Required]
    public ProjectRole ProjectRole { get; set; }


}

public enum ProjectRole
{
    ProjectManager = 1,
    ProjectMember = 2
}