using OutfitControl.Entities.Enum;

namespace OutfitControl.Entities;

public class PecaPorPedido : BaseEntity
{
    public Peca Peca { get; set; }
    public Pedido Pedido { get; set; }
    public int Quantidade { get; set; }
}