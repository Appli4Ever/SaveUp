using System.ComponentModel.DataAnnotations;

namespace SaveUp.Web.API.Entities;

public class Entrie : ITenantEntity
{
    public int Id { get; set; }

    public DateTime Created { get; set; }

    [MaxLength(50)]
    public string Description { get; set; }

    public double Amount { get; set; }

    [Required]
    public int TenantId { get; set; }
}