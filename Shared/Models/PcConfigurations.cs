using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PC_Designer.Shared;

public class PcConfigurations
{
    [Key]
    public int PcConfigurationId { get; set; }

    [ForeignKey("MotherBoards")]
    public int MotherBoardId { get; set; }
    public MotherBoards? MotherBoard { get; set; }

    [ForeignKey("CPUs")]
    public int CpuId { get; set; }
    public CPUs? Cpu { get; set; }

    [ForeignKey("GraphicalCards")]
    public int GraphicalCardId { get; set; }
    public GraphicalCards? GraphicalCard { get; set; }

    [ForeignKey("ComputerCases")]
    public int CaseId { get; set; }
    public ComputerCases? Case { get; set; }

    public string? Name { get; set; }

    public string? About { get; set; }

    public decimal Cost { get; set; }

    public int TotalWattage { get; set; }

    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

    public bool Status { get; set; }

    public int UserId { get; set; }
    public User? user { get; set; }

}
