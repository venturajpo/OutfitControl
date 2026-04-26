namespace OutfitControl.Entities;

public class Estoque : BaseEntity
{
    public Peca Peca { get; set; }
    public int Quantidade  { get; set; }
}