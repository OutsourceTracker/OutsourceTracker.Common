using System.ComponentModel.DataAnnotations;

namespace OutsourceTracker.Authentication;

public class LoginModel
{
    [Required, EmailAddress(ErrorMessage = "Please enter a valid email address")]
    public string Email { get; set; } = string.Empty;

    [Required, MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
    public string Password { get; set; } = string.Empty;

    public bool RememberMe { get; set; } = false;
}
