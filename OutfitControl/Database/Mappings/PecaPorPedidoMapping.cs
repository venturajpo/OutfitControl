using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutfitControl.Entities;

namespace OutfitControl.Database.Mappings;

public class PecaPorPedidoMapping : IEntityTypeConfiguration<PecaPorPedido>
{
    public void Configure(EntityTypeBuilder<PecaPorPedido> builder)
    {
        builder.ToTable("peca_x_pedido");
        
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Id)
            .HasColumnName("id_peca_x_pedido")
            .HasColumnType("int")
            .ValueGeneratedOnAdd()
            .IsRequired();
        
        builder.Property(p => p.Quantidade)
            .HasColumnName("quantidade")
            .HasColumnType("int")
            .IsRequired();

        builder.HasOne(p => p.Peca)
            .WithMany()
            .HasForeignKey("id_peca")
            .IsRequired();
        
        builder.HasOne(p => p.Pedido)
            .WithMany(p => p.Pecas)
            .HasForeignKey("id_pedido")
            .IsRequired();
    }
}