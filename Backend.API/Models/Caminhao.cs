using System.ComponentModel.DataAnnotations;

namespace Backend.API;

public class Caminhao
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O campo Ano de Fabricação é obrigatório")]
    public int AnoFabricacao { get; set; }

    [Required(ErrorMessage = "O campo Código de Chassi é obrigatório")]
    public string CodigoChassi { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public string Cor { get; set; }

    // FK
    [Required]
    public int ModeloId { get; set; }

    [Required]
    public int PlantaId { get; set; }

    // Relantionship
    public Modelo Modelo { get; set; }
    public Planta Planta { get; set; }

}
