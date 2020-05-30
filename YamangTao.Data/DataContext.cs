using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using YamangTao.Model;
using YamangTao.Model.Auth;
using YamangTao.Model.OrgStructure;
using YamangTao.Model.PM;
using YamangTao.Model.RSP;
using YamangTao.Model.RSP.Pds;

namespace YamangTao.Data
{
    public class DataContext : IdentityDbContext<User, Role, string, 
        IdentityUserClaim<string>, UserRole, IdentityUserLogin<string>, 
        IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options) {}
        // using Microsoft.EntityFrameworkCore;
        
        public DbSet<Employee> Employees { get; set; }
        public DbSet<BranchCampus> BranchCampuses { get; set; }
        public DbSet<OrgUnit> OrgUnits { get; set; }
        public DbSet<JobPosition> JobPositions { get; set; }
        // public DbSet<Ipcr> IpcrTemplates { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<RatingMatrix> RatingMatrix { get; set; }
        public DbSet<Kpi> KPIs { get; set; }
        public DbSet<KpiType> KpiTypes { get; set; }
        public DbSet<Ipcr> Ipcrs { get; set; }
        public DbSet<PersonalDataSheet> PersonalDataSheets { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Eligibility> Eligibities { get; set; }
        public DbSet<Identification> Identifications { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<UserRole>(userRole => {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId});

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();
                
                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            // Employee
            builder.Entity<Employee>(emp => {
                emp.HasOne(e => e.CurrentUnit)
                    .WithMany(o => o.Employees)
                    .HasForeignKey(e => e.OrgUnitId)
                    .OnDelete(DeleteBehavior.NoAction);
            });
            builder.Entity<Employee>().Property(e => e.Id).HasMaxLength(30);
            builder.Entity<Employee>().Property(e => e.EmployeeGroup).HasMaxLength(30);
            builder.Entity<Employee>().Property(e => e.CurrentStatus).HasMaxLength(30);
            builder.Entity<Employee>().Property(e => e.Lastname).HasMaxLength(50);
            builder.Entity<Employee>().Property(e => e.Firstname).HasMaxLength(50);
            builder.Entity<Employee>().Property(e => e.MiddleName).HasMaxLength(50);
            builder.Entity<Employee>().Property(e => e.MI).HasMaxLength(3);
            builder.Entity<Employee>().Property(e => e.Suffix).HasMaxLength(3);
            builder.Entity<Employee>().Property(e => e.Sex).HasMaxLength(15);
            builder.Entity<Employee>().Property(e => e.Telephone).HasMaxLength(50);
            builder.Entity<Employee>().Property(e => e.MobileNumber).HasMaxLength(50);

            // Branch Campus
            builder.Entity<BranchCampus>().Property(b => b.Campus).HasMaxLength(100);
            builder.Entity<BranchCampus>().Property(b => b.Address).HasMaxLength(200);

            // Job Positions
            builder.Entity<JobPosition>().Property(b => b.Name).HasMaxLength(100);
            builder.Entity<JobPosition>().Property(b => b.SalaryGrade).HasMaxLength(5);
            
            //OrgUnits
            builder.Entity<OrgUnit>()
                .HasOne(p => p.ParentUnit)
                .WithMany(c => c.OrgUnitChildren)
                .HasForeignKey(o => o.ParentUnitId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<OrgUnit>()
                .HasOne(p => p.CurrentHead)
                .WithMany(e => e.HeadedUnits)
                .HasForeignKey(e => e.CurrentHeadId);
            builder.Entity<OrgUnit>().Property(o => o.UnitName).HasMaxLength(200);
            builder.Entity<OrgUnit>().Property(o => o.Code).HasMaxLength(10);
            builder.Entity<OrgUnit>().Property(o => o.UnitType).HasMaxLength(30);
            builder.Entity<OrgUnit>().Property(o => o.NameOfHead).HasMaxLength(100);
            builder.Entity<OrgUnit>().Property(o => o.Location).HasMaxLength(150);
            builder.Entity<OrgUnit>().Property(o => o.CurrentHeadId).HasMaxLength(30);
            
            // Rating Matrix
            builder.Entity<RatingMatrix>(matrix => {
                
                matrix.Property(m => m.Dimension).HasMaxLength(15);
                matrix.Property(m => m.MeansOfVerification).HasMaxLength(150);
            });

            // Rating
            builder.Entity<Rating>(rating => {
                rating.HasKey("RatingMatrixId", "Rate");
                rating.HasOne(r => r.Matrix)
                .WithMany(m => m.Ratings)
                .HasForeignKey(r => r.RatingMatrixId)
                .OnDelete(DeleteBehavior.Cascade);

                rating.Property(r => r.Description).HasMaxLength(100);
            });
            
            // KPI
            builder.Entity<Kpi>(k => {
                k.HasMany(p => p.RatingMatrices).WithOne(m => m.Kpi).HasForeignKey(m => m.KpiId).OnDelete(DeleteBehavior.Cascade);
                k.HasMany(p => p.Kpis).WithOne(k => k.ParentKpi).HasForeignKey(p => p.ParentKpiId);
                k.Property(p => p.SuccessIndicator).HasMaxLength(256);
                k.Property(p => p.ActualAccomplishment).HasMaxLength(256);
                k.Property(p => p.OrderNumber).HasMaxLength(10);
                k.Property(p => p.Code).HasMaxLength(20);
            });

            // KpiType
            builder.Entity<KpiType>(k => {
                k.Property(p => p.Description).HasMaxLength(30);
            });

            // IPCR
            builder.Entity<Ipcr>(ipcr => {
                ipcr.HasOne(e => e.Ratee)
                    .WithMany(r => r.IPCRs)
                    .HasForeignKey(i => i.EmployeeId);
                
                ipcr.HasOne(i => i.Position)
                    .WithMany(p => p.Ipcrs)
                    .HasForeignKey(i => i.JobPositionId);
                
                ipcr.HasOne(i => i.Unit)
                    .WithMany(p => p.IpcrsUnderThisUnit)
                    .HasForeignKey(i => i.OrgUnitId);
                
                ipcr.HasOne(i => i.CompiledBy)
                    .WithMany(p => p.CompiledIpcrs)
                    .HasForeignKey(i => i.CompiledById);
                
                ipcr.HasOne(i => i.ReviewedBy)
                    .WithMany(p => p.ReviewedIpcrs)
                    .HasForeignKey(i => i.ReviewedById);
                
                ipcr.HasOne(i => i.ApprovedBy)
                    .WithMany(p => p.ApprovedIpcrs)
                    .HasForeignKey(i => i.ApprovedById);
                
                ipcr.HasMany(i => i.KPIs)
                    .WithOne(p => p.Ipcr)
                    .HasForeignKey(i => i.IpcrId);

                ipcr.Property(i => i.EmployeeId).HasMaxLength(30);
                ipcr.Property(i => i.ReviewedById).HasMaxLength(30);
                ipcr.Property(i => i.CompiledById).HasMaxLength(30);
                ipcr.Property(i => i.ApprovedById).HasMaxLength(30);
                ipcr.Property(i => i.ApprovedById).HasMaxLength(30);

            });

            // Personal Data Sheets
            builder.Entity<PersonalDataSheet>(pds => {
                pds.HasMany(p => p.Addresses)
                    .WithOne(a => a.Pds)
                    .HasForeignKey(a => a.PdsId);
                pds.HasMany(p => p.Eligibilities)
                    .WithOne(a => a.Pds)
                    .HasForeignKey(a => a.PdsId);
                pds.HasMany(p => p.IdCards)
                    .WithOne(a => a.Pds)
                    .HasForeignKey(a => a.PdsId);
                pds.Property(p => p.EmployeeId).HasMaxLength(30);
                pds.Property(p => p.CivilStatus).HasMaxLength(30);
                pds.Property(p => p.Height).HasMaxLength(10);
                pds.Property(p => p.Weight).HasMaxLength(10);
                pds.Property(p => p.BloodType).HasMaxLength(5);
                pds.Property(p => p.ConsanguinityFouthDetails).HasMaxLength(50);
                pds.Property(p => p.AdministrativeOffenseDetails).HasMaxLength(50);
                pds.Property(p => p.CriminalChargeStatus).HasMaxLength(50);
                pds.Property(p => p.ConvictedDetails).HasMaxLength(50);
                pds.Property(p => p.SeparatedFromServiceDetails).HasMaxLength(50);
                pds.Property(p => p.ElectionCandidateDetails).HasMaxLength(50);
                pds.Property(p => p.ResignedForElectionDetails).HasMaxLength(50);
                pds.Property(p => p.ImmigrantDetails).HasMaxLength(50);
                pds.Property(p => p.IpMemberDetails).HasMaxLength(50);
                pds.Property(p => p.PwdMemberDetails).HasMaxLength(50);
                pds.Property(p => p.SoloParentId).HasMaxLength(30);
                pds.Property(p => p.GovIdType).HasMaxLength(30);
                pds.Property(p => p.GovIdDatePlaceIssued).HasMaxLength(75);
            });

            // Eligibility
            builder.Entity<Eligibility>(e => {
                e.HasOne(p => p.Pds)
                    .WithMany(p => p.Eligibilities)
                    .HasForeignKey(el => el.PdsId);
                e.Property(p => p.EmployeeId).HasMaxLength(30);
                e.Property(p => p.Description).HasMaxLength(100);
                e.Property(p => p.Rating).HasMaxLength(10);
                e.Property(p => p.ExamPlace).HasMaxLength(100);
                e.Property(p => p.LicenseNumber).HasMaxLength(30);
            });

            // Address
            builder.Entity<Address>(e => {
                e.HasOne(p => p.Pds)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(el => el.PdsId);
                e.Property(p => p.EmployeeId).HasMaxLength(30);
                e.Property(p => p.Description).HasMaxLength(30);
                e.Property(p => p.Block).HasMaxLength(10);
                e.Property(p => p.Street).HasMaxLength(100);
                e.Property(p => p.Purok).HasMaxLength(30);
                e.Property(p => p.Barangay).HasMaxLength(30);
                e.Property(p => p.Municipality).HasMaxLength(30);
                e.Property(p => p.Province).HasMaxLength(30);
            });
            
            // Identification
            builder.Entity<Identification>(e => {
                e.HasOne(p => p.Pds)
                    .WithMany(p => p.IdCards)
                    .HasForeignKey(el => el.PdsId);
                e.Property(p => p.EmployeeId).HasMaxLength(30);
                e.Property(p => p.IDType).HasMaxLength(30);
                e.Property(p => p.Control).HasMaxLength(10);
            });
            

            // builder.Entity<Ipcr>(ipcr => { Residential Address
            //     ipcr.HasOne(i => i.Ratee)
            //     .WithMany(e => e.IPCRs)
            //     .HasForeignKey(p => p.EmployeeId);

            //     ipcr.HasOne(i => i.CompiledBy).WithMany(e => e.CompiledIpcrs);
            //     ipcr.HasOne(i => i.ReviewedBy).WithMany(e => e.ReviewedIpcrs);
            //     ipcr.HasOne(i => i.ApprovedBy).WithMany(e => e.ApprovedIpcrs);
            // });
            // builder.Entity<Like>()
            //     .HasKey(k => new {k.LikeeId, k.LikerId});
            
            // builder.Entity<Like>()
            //     .HasOne(u => u.Likee)
            //     .WithMany(u => u.Likers)
            //     .HasForeignKey(u => u.LikeeId)
            //     .OnDelete(DeleteBehavior.Restrict);

            // builder.Entity<Like>()
            //     .HasOne(u => u.Liker)
            //     .WithMany(u => u.Likees)
            //     .HasForeignKey(u => u.LikerId)
            //     .OnDelete(DeleteBehavior.Restrict);
            
            //Configure Messages entity relation ship
            // builder.Entity<Message>()
            //     .HasOne(u => u.Sender)
            //     .WithMany(m => m.MessagesSent)
            //     .OnDelete(DeleteBehavior.Restrict);

            // builder.Entity<Message>()
            //     .HasOne(u => u.Recipient)
            //     .WithMany(m => m.MessagesReceived)
            //     .OnDelete(DeleteBehavior.Restrict);

            //Global Query Filters
            // builder.Entity<Photo>().HasQueryFilter(p => p.IsApproved);
        }
        
    }
}