using System.ComponentModel.DataAnnotations;

namespace OutsourceTracker.Authentication;

public class LoginModel
{
    [Required(ErrorMessage = "Email address is required")]
    [EmailAddress(ErrorMessage = "You must provide a valid email address")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [MinLength(6, ErrorMessage = "The password must be at least 6 characters long")]
    public string Password { get; set; }

    public bool RememberMe { get; set; }
}
