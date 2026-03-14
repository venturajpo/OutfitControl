using OutfitControl.Entities.Enum;

namespace OutfitControl.Entities;

public class Retirada : BaseEntity
{
    public Pedido Pedido { get; set; }
    public DateOnly Data { get; set; }
}