using System.ComponentModel.DataAnnotations;

namespace APICatalogo.DTO.User;

public class RegisterModel
{
    [Required(ErrorMessage = "User name is required")]
    public string? Username { get; set; }

    [EmailAddress]
    [Required(ErrorMessage = "Email is Required")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; set; }
}