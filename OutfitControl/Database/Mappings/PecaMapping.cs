using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutfitControl.Entities;
using OutfitControl.Entities.Enum;

namespace OutfitControl.Database.Mappings;

public class PecaMapping : IEntityTypeConfiguration<Peca>
{
    public void Configure(EntityTypeBuilder<Peca> builder)
    {
        builder.ToTable("peca");
        
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Id)
            .HasColumnName("id_peca")
            .HasColumnType("int")
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Property(p => p.Tipo)
            .HasColumnName("tipopeca")
            .HasColumnType("tipopeca ENUM('Camiseta', 'Calca', 'Calcado')")
            .HasConversion(
                code => code.ToString(),
                db => (TipoPeca)Enum.Parse(typeof(TipoPeca), db))
            .IsRequired();
        
        builder.Property(p => p.Tamanho)
            .HasColumnName("tamanhopeca")
            .HasColumnType("varchar")
            .HasMaxLength(4)
            .IsRequired();
            
        builder.HasIndex(p => new { p.Tipo, p.Tamanho }).IsUnique();
    }
}