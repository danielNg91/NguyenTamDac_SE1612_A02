using System.ComponentModel.DataAnnotations;

namespace WebClient.Models;

public class LoginCredentials
{
    [Required, EmailAddress]
    public string EmailAddress { get; set; }

    [Required]
    public string Password { get; set; }
}
