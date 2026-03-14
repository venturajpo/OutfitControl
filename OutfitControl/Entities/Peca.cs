using OutfitControl.Entities.Enum;

namespace OutfitControl.Entities;

public class Peca : BaseEntity
{
    public TipoPeca Tipo { get; set; }
    public string Tamanho { get; set; }
}