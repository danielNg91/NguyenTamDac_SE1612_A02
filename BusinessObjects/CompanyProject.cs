using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects;
public class CompanyProject
{
    [Required]
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CompanyProjectID { get; set; }

    [Required]
    public string ProjectName { get; set; }

    [Required]
    public string ProjectDescription { get; set; }

    [Required]
    public DateTime EstimatedStartDate { get; set; }

    [Required]
    public DateTime ExpectedEndDate { get; set; }

    public virtual ICollection<ParticipatingProject> ParticipatingProjects { get; set; }
}
