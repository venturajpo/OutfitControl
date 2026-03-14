using OutfitControl.Entities.Enum;

namespace OutfitControl.Entities;

public class Pedido
{
    public int Id { get; set; }
    public Funcionario Funcionario { get; set; }
    public DateTime Data  { get; set; }
    public StatusPedido Status { get; set; }
    
}