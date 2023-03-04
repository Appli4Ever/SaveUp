using System.ComponentModel.DataAnnotations;

namespace SaveUp.Models.ViewModels;

public class PasswordViewModel
{
    [Required]
    [MaxLength(50)]
    public string Password { get; set; }

    [Required]
    [MaxLength(50)]
    public string VerifiyPassword { get; set; }
}