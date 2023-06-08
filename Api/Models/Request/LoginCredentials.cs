using System.ComponentModel.DataAnnotations;

namespace Api.Models.Request;

public class LoginCredentials
{
    [Required]
    public string EmailAddress { get; set; }

    [Required]
    public string Password { get; set; }
}
