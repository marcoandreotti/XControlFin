using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using xControlFin.Domain.Entities;

namespace xControlFin.Infrastructure.Data.Mappings;

public class FinancialReleaseMapping : IEntityTypeConfiguration<FinancialReleaseEntity>
{
    public void Configure(EntityTypeBuilder<FinancialReleaseEntity> builder)
    {
        builder.ToTable("FinancialReleases");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Value).HasColumnType("decimal(18,2)");
        builder.Property(x => x.Historic).IsRequired();

        builder.Property(x => x.PaymentDate)
               .HasColumnType("timestamp");

        builder.Property(x => x.CompensationDate)
               .HasColumnType("timestamp");
    }
}

public class FinancialPlanningMapping : IEntityTypeConfiguration<FinancialPlanningEntity>
{
    public void Configure(EntityTypeBuilder<FinancialPlanningEntity> builder)
    {
        builder.ToTable("FinancialPlannings");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Value).HasColumnType("decimal(18,2)");
        builder.Property(x => x.Historic).IsRequired();

        builder.Property(x => x.StartDate)
               .HasColumnType("timestamp");

        builder.Property(x => x.LastStartDate)
       .HasColumnType("timestamp");

        builder.Property(x => x.EndDate)
       .HasColumnType("timestamp");
    }
}