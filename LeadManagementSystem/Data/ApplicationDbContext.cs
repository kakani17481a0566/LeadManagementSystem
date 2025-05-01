using Microsoft.EntityFrameworkCore;
using LeadManagementSystem.Models;
using LeadManagementSystem.ViewModel.Lead;

namespace LeadManagementSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<LeadEntity> leads { get; set; }
        public DbSet<Source> sources { get; set; }
        public DbSet<Branch> branches { get; set; }
        public DbSet<School> schools { get; set; }
        public DbSet<LeadType> lead_types { get; set; }
        public DbSet<Status> statuses { get; set; }
        public DbSet<LeadList> lead_lists { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<RolesModel> Roles { get; set; }

        public DbSet<SalesPerson> SalesPersons { get; set; }

        public DbSet<LeadCountByStatusViewModel> LeadCountByStatusViewModels { get; set; }

        public DbSet<CallLog> CallLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SalesPerson>(entity =>
            {
                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(10)
                    .IsFixedLength();
            });
        }





    }
}
