using System.ComponentModel.DataAnnotations;

namespace PC_Designer.Shared;

public class Sockets
{
    [Key]
    public int SocketId { get; set; }

    [Required(ErrorMessage = "Name can't be empty")]
    public string Name { get; set; } = null!;

}
