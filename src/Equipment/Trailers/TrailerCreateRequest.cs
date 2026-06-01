using System.ComponentModel.DataAnnotations;

namespace OutsourceTracker.Equipment.Trailers;

public class TrailerCreateRequest
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "Prefix is required.")]
    [MinLength(2, ErrorMessage = "Prefix connot be less than 2 characters.")]
    [MaxLength(4, ErrorMessage = "Prefix cannot exceed 4 characters.")]
    public string Prefix { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required.")]
    [MinLength(2, ErrorMessage = "Name connot be less than 2 characters.")]
    [MaxLength(10, ErrorMessage = "Name connot exceed 10 characters.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Type is required.")]
    public TrailerType Type { get; set; } = TrailerType.Van;

    public Guid? AccountId { get; set; }
}
