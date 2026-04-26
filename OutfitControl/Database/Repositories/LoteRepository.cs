using Microsoft.EntityFrameworkCore;
using OutfitControl.Entities;

namespace OutfitControl.Database.Repositories;

public class LoteRepository(ApplicationDbContext context) : RepositoryBase<Lote>(context)
{
}
