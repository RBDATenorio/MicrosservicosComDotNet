using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NSE.Identidade.API.Models;

namespace NSE.Identidade.API.Data
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.UserName)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(u => u.Senha)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(u => u.DataCadastro)
                .IsRequired()
                .HasColumnType("date");

            builder.ToTable("Usuarios");

        }
    }
}
