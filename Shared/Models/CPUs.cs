using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PC_Designer.Shared;

public class CPUs
{
    [Key]
    public int CpuId { get; set; }

    [ForeignKey("CpuVendors")]
    public int CpuVendorId { get; set; }
    public CpuVendors CpuVendor { get; set; } = null!;

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public int MaxWattage { get; set; }

    public bool IntegratedGraphics { get; set; }

    public string ImageUrl { get; set; } = null!;

}
