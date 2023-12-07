using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PC_Designer.Shared;

public class PcConfigurations
{
    [Key]
    public int PcConfigurationId { get; set; }

    [ForeignKey("MotherBoards")]
    public int MotherBoardId { get; set; }
    public MotherBoards MotherBoard { get; set; } = null!;

    [ForeignKey("CPUs")]
    public int CpuId { get; set; }
    public CPUs Cpu { get; set; } = null!;

    [ForeignKey("GraphicalCards")]
    public int GraphicalCardId { get; set; }
    public GraphicalCards GraphicalCard { get; set; } = null!;

    [ForeignKey("ComputerCases")]
    public int CaseId { get; set; }
    public ComputerCases Case { get; set; } = null!;

    public string? Name { get; set; }

    public int TotalWattage { get; set; }

    public DateTime CreateOn { get; set; } = DateTime.UtcNow;

}
