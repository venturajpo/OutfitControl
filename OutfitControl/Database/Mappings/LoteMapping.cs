using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutfitControl.Entities;

namespace OutfitControl.Database.Mappings;

public class LoteMapping : IEntityTypeConfiguration<Lote>
{
    public void Configure(EntityTypeBuilder<Lote> builder)
    {
        builder.ToTable("lote");
        
        builder.HasKey(l => l.Id);
        
        builder.Property(l => l.Id)
            .HasColumnName("id_lote")
            .HasColumnType("int")
            .IsRequired()
            .ValueGeneratedOnAdd();
        
        builder.Property(l => l.Data)
            .HasColumnName("data")
            .HasColumnType("date")
            .IsRequired();
        
        builder.Property(l => l.Quantidade)
            .HasColumnName("quantidade")
            .HasColumnType("int")
            .IsRequired();
        
        builder.HasOne(l => l.Peca)
            .WithMany()
            .HasForeignKey("id_peca")
            .IsRequired();
    }
}