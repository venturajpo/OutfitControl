using OutfitControl.Entities.Enum;

namespace OutfitControl.Entities;

public class Lote
{
    public DateTime Data { get; set; }
    public Peca Peca { get; set; }
    public int Quantidade { get; set; }
}