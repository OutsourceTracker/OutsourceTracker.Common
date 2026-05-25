using System.ComponentModel.DataAnnotations;

namespace OutsourceTracker.Authentication;

public class RegisterModel
{
    [Required(ErrorMessage = "First name is required")]
    [MinLength(2, ErrorMessage = "First name must be at least 2 characters")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last name is required")]
    [MinLength(2, ErrorMessage = "Last name must be at least 2 characters")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "AlphaCode is required")]
    [RegularExpression(@"^[A-Z]{4}\d{0,2}$",
        ErrorMessage = "AlphaCode must be 4 uppercase letters, optionally followed by 1-2 numbers (e.g. KESS12)")]
    [MaxLength(6)]
    public string AlphaCode { get; set; } = string.Empty;

    [Required(ErrorMessage = "Workday ID is required")]
    [RegularExpression(@"^\d+$", ErrorMessage = "Workday ID must contain only numbers")]
    [MaxLength(10)]
    public string WorkdayId { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please confirm your password")]
    [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set; } = string.Empty;
}
