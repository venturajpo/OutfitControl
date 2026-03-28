using OutfitControl.Entities;

namespace OutfitControl.Database.Repositories;

public class PecaPorPedidoRepository(ApplicationDbContext context) : RepositoryBase<PecaPorPedido>(context)
{
    public IEnumerable<PecaPorPedido> GetByPedido(int pedidoId)
    {
        return GetAll().Where(pp => pp.Pedido.Id == pedidoId);
    }
    
}