using OutfitControl.Entities;
using OutfitControl.Entities.Enum;

namespace OutfitControl.Database.Repositories;

public class PedidoRepository(ApplicationDbContext context) : RepositoryBase<Pedido>(context)
{
    public IEnumerable<Pedido> GetPedidosNovos()
    {
        return GetAll().Where(p => p.Status == StatusPedido.Novo);
    }
}