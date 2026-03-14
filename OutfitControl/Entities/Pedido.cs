using OutfitControl.Entities.Enum;

namespace OutfitControl.Entities;

public class Pedido : BaseEntity
{
    public Funcionario Funcionario { get; set; }
    public DateOnly Data  { get; set; }
    public StatusPedido Status { get; set; }
    
    public ICollection<PecaPorPedido> Pecas { get; set; }
}