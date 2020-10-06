using CourtCaseManagement.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourtCaseManagement.Infrastructure.Data.Config
{
    public class SituationConfiguration : IEntityTypeConfiguration<SituationEntity>
    {
        public void Configure(EntityTypeBuilder<SituationEntity> builder)
        {
            builder.ToTable("situation");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id")
                .IsRequired();

            builder.Property(x => x.Name)
                .HasColumnName("name");

            builder.Property(x => x.Finished)
                .HasColumnName("finished");

            builder.HasMany(p => p.Processes)
                .WithOne(s => s.Situation);
        }
    }
}