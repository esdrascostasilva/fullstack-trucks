using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Backend.API.Models;

public class Modelo
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public string Nome { get; set; }

    // Relantionship
    [JsonIgnore]
    public virtual ICollection<Caminhao> Caminhoes { get; set; }
}
