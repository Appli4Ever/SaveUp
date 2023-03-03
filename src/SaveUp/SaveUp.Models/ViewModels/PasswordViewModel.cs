using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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