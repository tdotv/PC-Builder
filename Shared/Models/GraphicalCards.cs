using System.ComponentModel.DataAnnotations;

namespace PC_Designer.Shared;

public class GraphicalCards
{
    [Key]
    public int GraphicalCardId { get; set; }

    public string GraphicalCardName { get; set; } = null!;

    public decimal Price { get; set; }

    public int MaxWattage { get; set; }

    public string ImageUrl { get; set; } = null!;

}
