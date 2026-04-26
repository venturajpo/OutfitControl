using Microsoft.EntityFrameworkCore;
using OutfitControl.Entities;

namespace OutfitControl.Database.Repositories;

public class EstoqueRepository(ApplicationDbContext context) : RepositoryBase<Estoque>(context)
{
    public override IQueryable<Estoque> GetAll()
    {
        return base.GetAll().Include(e => e.Peca);
    }
}