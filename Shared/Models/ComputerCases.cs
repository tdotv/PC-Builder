using System.ComponentModel.DataAnnotations;

namespace PC_Designer.Shared;

public class ComputerCases
{
    [Key]
    public int CaseId { get; set; }

    public string CaseName { get; set; } = null!;

    public decimal Price { get; set; }

    public string ImageUrl { get; set; } = null!;

}
