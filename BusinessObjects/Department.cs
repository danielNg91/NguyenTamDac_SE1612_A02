using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects;
public class Department
{
    [Required]
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int DepartmentID { get; set; }

    [Required]
    public string DepartmentName { get; set; }

    [Required]
    public string DepartmentDescription { get; set; }

    public virtual ICollection<Employee> Employees { get; set; }
}
