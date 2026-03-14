using OutfitControl.Entities.Enum;

namespace OutfitControl.Entities;

public class Peca
{
    public int Id { get; set; }
    public TipoPeca Tipo { get; set; }
    public string Tamanho { get; set; }
}