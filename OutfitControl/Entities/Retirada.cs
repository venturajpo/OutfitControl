using OutfitControl.Entities.Enum;

namespace OutfitControl.Entities;

public class Retirada
{
    public int Id { get; set; }
    public Pedido Pedido { get; set; }
    public DateTime Data { get; set; }
}