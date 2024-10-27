using System.ComponentModel.DataAnnotations;

namespace Backend.API;

public class Planta
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public string Nome { get; set; }

    // Relantionship
    public virtual ICollection<Caminhao> Caminhoes { get; set; }
}
