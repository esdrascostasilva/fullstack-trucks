namespace Backend.API;

public class Planta
{
    public int Id { get; set; }
    public string Nome { get; set; }

    // Relantionship
    public virtual ICollection<Caminhao> Caminhoes { get; set; }
}
