using System.ComponentModel.DataAnnotations;

namespace PC_Designer.Shared;

public class RAMs
{
    [Key]
    public int RamId { get; set; }

    public string Name { get; set; } = null!;

    public string Type { get; set; } = null!;

    public int Capacity { get; set; }

    public int ClockFrequency { get; set; }

    public string ImageUrl { get; set; } = null!;

}
