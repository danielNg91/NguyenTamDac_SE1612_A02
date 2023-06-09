using BusinessObjects;

namespace WebClient.Models;


public class UpdateEmp
{
    public string? FullName { get; set; }
    public string? Skills { get; set; }
    public string? Telephone { get; set; }
    public string? Address { get; set; }
    public Status? Status { get; set; }
    public int? DepartmentID { get; set; }
    public string? Password { get; set; }
}
