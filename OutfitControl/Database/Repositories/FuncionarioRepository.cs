using OutfitControl.Entities;

namespace OutfitControl.Database.Repositories;

public class FuncionarioRepository(ApplicationDbContext context) : RepositoryBase<Funcionario>(context)
{
    public Funcionario? GetByMatricula(string matricula)
    {
        return Context.Funcionarios.FirstOrDefault(f => f.Matricula == matricula);
    }
}