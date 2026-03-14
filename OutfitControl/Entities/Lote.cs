using OutfitControl.Entities.Enum;

namespace OutfitControl.Entities;

public class Lote : BaseEntity
{
    public DateOnly Data { get; set; }
    public Peca Peca { get; set; }
    public int Quantidade { get; set; }
}