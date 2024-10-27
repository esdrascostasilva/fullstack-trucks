namespace Backend.API;

public class Caminhao
{
    public int Id { get; set; }
    public int AnoFabricacao { get; set; }
    public string CodigoChassi { get; set; }
    public string Cor { get; set; }

    // FK
    public int ModeloId { get; set; }
    public int PlantaId { get; set; }

    // Relantionship
    public Modelo Modelo { get; set; }
    public Planta Planta { get; set; }

}
