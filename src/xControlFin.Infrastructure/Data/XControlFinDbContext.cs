using Microsoft.EntityFrameworkCore;
using xControlFin.Domain.Entities;

namespace xControlFin.Infrastructure.Data;

public class XControlFinDbContext : DbContext
{
    public XControlFinDbContext(DbContextOptions<XControlFinDbContext> options) : base(options)
    {
    }

    public DbSet<UserEntity> Users { get; set; }
    public DbSet<CostCenterEntity> CostCenters { get; set; }
    public DbSet<FinancialInstitutionEntity> FinancialInstitutions { get; set; }
    public DbSet<UserFinancialInstitutionEntity> UserFinancialInstitutions { get; set; }
    public DbSet<FinancialReleaseEntity> FinancialReleases { get; set; }
    public DbSet<FinancialPlanningEntity> FinancialPlannings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(XControlFinDbContext).Assembly);
    }
}