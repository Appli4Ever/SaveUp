using System.ComponentModel.DataAnnotations;

namespace SaveUp.Models.ViewModels;

public class EntrieViewModel
{
    public int Id { get; set; }

    [Required]
    public DateTime Created { get; set; }

    [MaxLength(50)]
    [Required]
    public string Description { get; set; }

    [Required]
    public double Amount { get; set; }
}