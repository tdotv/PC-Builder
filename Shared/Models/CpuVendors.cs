using System.ComponentModel.DataAnnotations;

namespace PC_Designer.Shared;

public class CpuVendors
{
    [Key]
    public int CpuVendorId { get; set; }

    public string Name { get; set; } = null!;
}
