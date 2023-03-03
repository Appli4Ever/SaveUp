using System.ComponentModel.DataAnnotations;

namespace SaveUp.Web.API.Entities;

public class User
{
    public int Id { get; set; }

    [MaxLength(50)]
    public string Username  { get; set; }

    [MaxLength(255)]
    public string Password { get; set; }

    public bool LoginBlocked { get; set; }

    public int LoginTries { get; set; }
}