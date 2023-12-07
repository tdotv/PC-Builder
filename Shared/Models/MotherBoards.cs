using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PC_Designer.Shared;

public class MotherBoards
{
    [Key]
    public int MotherBoardId { get; set; }

    [ForeignKey("RAMs")]
    public int RamId { get; set; }
    public RAMs Ram { get; set; } = null!;

    [ForeignKey("Sockets")]
    public int SocketId { get; set; }
    public Sockets Socket { get; set; } = null!;

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public string ImageUrl { get; set; } = null!;

}
