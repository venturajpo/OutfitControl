using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutfitControl.Entities;
using OutfitControl.Entities.Enum;

namespace OutfitControl.Database.Mappings;

public class PedidoMapping : IEntityTypeConfiguration<Pedido>
{
    public void Configure(EntityTypeBuilder<Pedido> builder)
    {
        builder.ToTable("pedido");
        
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Id)
            .HasColumnName("id_pedido")
            .HasColumnType("int")
            .IsRequired()
            .ValueGeneratedOnAdd();
        
        builder.Property(p => p.Data)
            .HasColumnName("data")
            .HasColumnType("date")
            .IsRequired();

        builder.Property(p => p.Status)
            .HasColumnName("status")
            .HasColumnType("status ENUM('Novo', 'EmProcesso', 'AguardandoRetirada', 'Finalizado')")
            .HasConversion(
                code => code.ToString(),
                db => (StatusPedido)Enum.Parse(typeof(StatusPedido), db))
            .IsRequired();
        
        builder.HasOne(p => p.Funcionario)
            .WithMany()
            .HasForeignKey("id_funcionario")
            .IsRequired();
    }
}