using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Produtos.Domain.Models;

namespace Produtos.Infra.Data.EF.Mappings
{
    public class ImagemMap : IEntityTypeConfiguration<Imagem>
    {
        public void Configure(EntityTypeBuilder<Imagem> builder)
        {
            builder.ToTable("Imagem");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Nome)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .HasOne(img => img.Produto)
                .WithMany(p => p.Imagens)
                .HasForeignKey(img => img.ProdutoId);
        }
    }
}