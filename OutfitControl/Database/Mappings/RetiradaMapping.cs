using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutfitControl.Entities;

namespace OutfitControl.Database.Mappings;

public class RetiradaMapping : IEntityTypeConfiguration<Retirada>
{
    public void Configure(EntityTypeBuilder<Retirada> builder)
    {
        builder.ToTable("retirada");
        
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Id)
            .HasColumnName("id_retirada")
            .HasColumnType("int")
            .IsRequired()
            .ValueGeneratedOnAdd();
        
        builder.Property(p => p.Data)
            .HasColumnName("data")
            .HasColumnType("date")
            .IsRequired();
        
        builder.HasOne(p => p.Pedido)
            .WithMany()
            .HasForeignKey("id_pedido")
            .IsRequired();
    }
}