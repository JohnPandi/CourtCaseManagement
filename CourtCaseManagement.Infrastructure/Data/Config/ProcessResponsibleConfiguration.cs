using CourtCaseManagement.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourtCaseManagement.Infrastructure.Data.Config
{
    public class ProcessResponsibleConfiguration : IEntityTypeConfiguration<ProcessResponsibleEntity>
    {
        public void Configure(EntityTypeBuilder<ProcessResponsibleEntity> builder)
        {
            builder.ToTable("process_responsible");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id")
                .IsRequired();

            builder.Property(x => x.ProcessId)
                .HasColumnName("process_id");

            builder.Property(x => x.ResponsibleId)
                .HasColumnName("responsible_id");

            builder.HasOne(processResponsible => processResponsible.Responsible)
                .WithMany(process => process.ProcessResponsible);

            builder.HasOne(processResponsible => processResponsible.Process)
                .WithMany(process => process.ProcessResponsible);
        }
    }
}