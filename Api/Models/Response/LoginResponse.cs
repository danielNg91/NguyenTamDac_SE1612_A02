using Api.Mappings;
using Api.Utils;
using BusinessObjects;

namespace Api.Models.Response;

public class LoginResponse : IMapFrom<Employee>
{
    public int EmployeeId { get; set; }
    public string EmailAddress { get; set; }
    public string Role { get; set; }
}
