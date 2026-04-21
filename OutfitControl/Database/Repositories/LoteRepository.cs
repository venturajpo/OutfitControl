using Microsoft.EntityFrameworkCore;
using OutfitControl.Entities;

namespace OutfitControl.Database.Repositories;

public class LoteRepository(ApplicationDbContext context) : RepositoryBase<Lote>(context)
{
    public IEnumerable<Lote> GetAllComDetalhes()
    {
        return DbSet
            .Include(l => l.Peca)
            .ToList();
    }
}
