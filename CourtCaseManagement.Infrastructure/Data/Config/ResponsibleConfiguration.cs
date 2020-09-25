using CourtCaseManagement.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourtCaseManagement.Infrastructure.Data.Config
{
    public class ResponsibleConfiguration : IEntityTypeConfiguration<ResponsibleEntity>
    {
        public void Configure(EntityTypeBuilder<ResponsibleEntity> builder)
        {
            builder.ToTable("responsible");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id")
                .IsRequired();

            builder.Property(x => x.Cpf)
                .HasColumnName("cpf");

            builder.Property(x => x.Mail)
                .HasColumnName("mail");

            builder.Property(x => x.Name)
                .HasColumnName("name");

            builder.Property(x => x.Photograph)
                .HasColumnName("photograph");

            builder.Property(x => x.ProcessId)
                .HasColumnName("process_id");

            builder
                .HasOne(p => p.Process)
                .WithMany(s => s.Responsibles);
        }
    }
}