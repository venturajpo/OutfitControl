using OutfitControl.Entities;
using OutfitControl.Entities.Enum;

namespace OutfitControl.Database.Repositories;

public class PecaRepository(ApplicationDbContext context) : RepositoryBase<Peca>(context)
{
    public Peca GetOrAddByTipoETamanho(TipoPeca tipoPeca, string tamanho)
    {
        var retorno = GetAll().FirstOrDefault(p => p.Tipo == tipoPeca &&  p.Tamanho == tamanho);
        
        if (retorno is null)
            retorno = Add(new Peca{Tipo = tipoPeca, Tamanho = tamanho});
        
        return retorno;
    }
}