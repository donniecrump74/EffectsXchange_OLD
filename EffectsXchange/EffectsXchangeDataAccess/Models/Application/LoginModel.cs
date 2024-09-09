using System.ComponentModel.DataAnnotations;

namespace EffectsXchangeData.Models.Application;


public class LoginModel {
#pragma warning disable CS8618
    [Required(ErrorMessage = "Email is required")]
    [DataType(DataType.EmailAddress)]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string EmailAddress { get; set; }

    [Required]
    [StringLength(30, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long.")]
    public string Password { get; set; }
#pragma warning restore CS8618
}
