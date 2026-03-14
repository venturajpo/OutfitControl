using Microsoft.EntityFrameworkCore;
using OutfitControl.Database.Mappings;
using OutfitControl.Entities;

namespace OutfitControl.Database;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Funcionario> Funcionarios { get; set; }
    public DbSet<Lote> Lotes { get; set; }
    public DbSet<Peca> Pecas { get; set; }
    public DbSet<PecaPorPedido> PecasPorPedidos { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<Retirada> Retiradas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new FuncionarioMapping());
        modelBuilder.ApplyConfiguration(new LoteMapping());
        modelBuilder.ApplyConfiguration(new PecaMapping());
        modelBuilder.ApplyConfiguration(new PecaPorPedidoMapping());
        modelBuilder.ApplyConfiguration(new PedidoMapping());
        modelBuilder.ApplyConfiguration(new RetiradaMapping());
        
        base.OnModelCreating(modelBuilder);
    }
    
    
}