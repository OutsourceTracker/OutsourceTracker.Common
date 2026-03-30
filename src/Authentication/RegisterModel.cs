using System.ComponentModel.DataAnnotations;

namespace OutsourceTracker.Authentication;

public class RegisterModel
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "First name is required")]
    [MinLength(2, ErrorMessage = "First name must be at least 2 characters")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last name is required")]
    [MinLength(2, ErrorMessage = "Last name must be at least 2 characters")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "ALPHA Code is required")]
    [RegularExpression(@"[A-Za-z0-9]{5,6}", ErrorMessage = "ALPHA Codes are formatted 'ABCDE1'")]
    public string AlphaCode { get; set; } = string.Empty;

    [Required(ErrorMessage = "Workday Id is required")]
    [RegularExpression(@"\d{5,8}", ErrorMessage = "Workday Id is anywhere from 5 to 8 digits long")]
    public string WorkdayId { get; set; } = string.Empty;
}
