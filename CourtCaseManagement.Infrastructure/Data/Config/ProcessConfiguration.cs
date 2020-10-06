using CourtCaseManagement.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourtCaseManagement.Infrastructure.Data.Config
{
    public class ProcessConfiguration : IEntityTypeConfiguration<ProcessEntity>
    {
        public void Configure(EntityTypeBuilder<ProcessEntity> builder)
        {
            builder.ToTable("process");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id")
                .IsRequired();

            builder.Property(x => x.Version)
                .HasColumnName("version");

            builder.Property(x => x.UpdateDate)
                .HasColumnName("update_date");

            builder.Property(x => x.UpdateUserName)
                .HasColumnName("update_user_name");

            builder.Property(x => x.Description)
                .HasColumnName("description");

            builder.Property(x => x.JusticeSecret)
                .HasColumnName("justice_secret");

            builder.Property(x => x.DistributionDate)
                .HasColumnName("distribution_date");

            builder.Property(x => x.ClientPhysicalFolder)
                .HasColumnName("client_physical_folder");

            builder.Property(x => x.UnifiedProcessNumber)
                .HasColumnName("unified_process_number");

            builder.Property(x => x.SituationId)
                .HasColumnName("situation_id");

            builder.Property(x => x.LinkedProcessId)
                .HasColumnName("process_id");

            builder.HasOne(process => process.ProcessFather)
                .WithMany(process => process.Children)
                .HasForeignKey(process => process.LinkedProcessId);

            builder.HasMany(process => process.ProcessResponsible)
                .WithOne(processResponsible => processResponsible.Process);

            builder.HasOne(process => process.Situation)
                .WithMany(situation => situation.Processes);
        }
    }
}