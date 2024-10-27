namespace Backend.API;

public class Modelo
{
    public int Id { get; set; }
    public string Nome { get; set; }

    // Relantionship
    public virtual ICollection<Caminhao> Caminhoes { get; set; }
}
