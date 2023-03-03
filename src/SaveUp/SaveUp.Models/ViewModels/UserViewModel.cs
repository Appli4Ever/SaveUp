using System.ComponentModel.DataAnnotations;

namespace SaveUp.Models.ViewModels;

public class UserViewModel
{
    [Required]
    [MaxLength(50)]
    public string Username { get; set; }

    [Required]
    [MaxLength(50)]
    public string Password { get; set; }
}