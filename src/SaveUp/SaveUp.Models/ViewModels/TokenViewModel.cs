using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaveUp.Models.Enum;

namespace SaveUp.Models.ViewModels;

public class TokenViewModel
{
    public string? Token { get; set; }

    public LoginStatus LoginStatus{ get; set; }
}