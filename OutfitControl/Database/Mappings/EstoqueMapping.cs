using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutfitControl.Entities;

namespace OutfitControl.Database.Mappings;

public class EstoqueMapping : IEntityTypeConfiguration<Estoque>
{
    public void Configure(EntityTypeBuilder<Estoque> builder)
    {
        builder.ToView("estoque");
        
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasColumnName("id_peca")
            .HasColumnType("int");
        
        builder.Property(e => e.Quantidade)
            .HasColumnName("quantidade")
            .HasColumnType("int");

        builder.HasOne(e => e.Peca)
            .WithOne()
            .HasForeignKey<Estoque>(e => e.Id);
    }
}