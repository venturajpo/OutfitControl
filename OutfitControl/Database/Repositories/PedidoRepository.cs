using Microsoft.EntityFrameworkCore;
using OutfitControl.Entities;
using OutfitControl.Entities.Enum;

namespace OutfitControl.Database.Repositories;

public class PedidoRepository(ApplicationDbContext context) : RepositoryBase<Pedido>(context)
{
    public IEnumerable<Pedido> GetPedidosNovos()
    {
        return GetAll().Where(p => p.Status == StatusPedido.Novo);
    }

    public IEnumerable<Pedido> GetAllComDetalhes()
    {
        return DbSet
            .Include(p => p.Funcionario)
            .Include(p => p.Pecas)
                .ThenInclude(pp => pp.Peca)
            .OrderByDescending(p => p.Data)
            .ToList();
    }
}