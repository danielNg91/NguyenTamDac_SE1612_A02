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
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CompanyProjectID { get; set; }

    public string ProjectName { get; set; }

    public string ProjectDescription { get; set; }

    public DateTime EstimatedStartDate { get; set; }

    public DateTime ExpectedEndDate { get; set; }

    public virtual ICollection<ParticipatingProject> ParticipatingProjects { get; set; }
}
