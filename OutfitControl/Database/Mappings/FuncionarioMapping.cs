using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutfitControl.Entities;

namespace OutfitControl.Database.Mappings;

public class FuncionarioMapping : IEntityTypeConfiguration<Funcionario>
{
    public void Configure(EntityTypeBuilder<Funcionario> builder)
    {
        builder.ToTable("funcionario");
        
        builder.HasKey(f => f.Id);
        
        builder.Property(f => f.Id)
            .HasColumnName("id_funcionario")
            .HasColumnType("int")
            .IsRequired()
            .ValueGeneratedOnAdd();
        
        builder.Property(f => f.NomeCompleto)
            .HasColumnName("nome_completo")
            .HasColumnType("varchar")
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(f => f.Matricula)
            .HasColumnName("matricula")
            .HasColumnType("char")
            .HasMaxLength(7)
            .IsRequired();
            
        builder.HasIndex(p => p.Matricula).IsUnique();
    }
}