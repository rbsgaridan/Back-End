
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using YamangTao.Model;
using YamangTao.Model.Auth;
using YamangTao.Model.DocumentTracking;
using YamangTao.Model.LND;
using YamangTao.Model.OrgStructure;
using YamangTao.Model.PM;
using YamangTao.Model.PM.Template;
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


        // //IPCR
        // public DbSet<Ipcr> Ipcrs { get; set; } // Actual IPCR of Faculty
        // public DbSet<Rating> Ratings { get; set; } // Actual IPCR of Faculty
        // public DbSet<RatingMatrix> RatingMatrix { get; set; } // Actual IPCR of Faculty
        // public DbSet<Kpi> KPIs { get; set; } // Actual IPCR of Faculty
        public DbSet<KpiType> KpiTypes { get; set; }

        // Templates
        public DbSet<IpcrTemplate> IpcrTemplates { get; set; }
        public DbSet<KpiTemplate> KpiTemplates { get; set; }
        public DbSet<RatingMatrixTemplate> RatingMatrixTemplates { get; set; }
        public DbSet<RatingTemplate> RatingTemplates { get; set; }
        
        public DbSet<DocumentPath> DocumentPaths { get; set; }

        //LND
        public DbSet<Activity> Activities { get; set; }
        public DbSet<ActivityType> ActivityTypes { get; set; }
        public DbSet<CertificateType> CertificateTypes { get; set; }
        public DbSet<Certificate> Certificate { get; set; }

        // PDS
        public DbSet<PersonalDataSheet> PersonalDataSheets { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Eligibility> Eligibities { get; set; }
        public DbSet<Identification> Identifications { get; set; }
        public DbSet<CharacterReference> References { get; set; }
        public DbSet<Child> Children { get; set; }
        public DbSet<EducationalBackground> EducationalBackgrounds { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<Recognition> Recognitions { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<TrainingAttended> TrainingsAttented { get; set; }
        public DbSet<VoluntaryWork> VoluntaryWorks { get; set; }
        public DbSet<WorkExperience> WorkExperiences { get; set; }

        // protected override void OnConfiguring(DbContextOptionsBuilder options)
        // {
        //    options.UseSqlServer()
        //     options.UseSqlServer(connectionString, conf =>
        //     {
        //         conf.UseHierarchyId();
        //     });
        // }
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
            builder.Entity<Employee>().Property(e => e.Suffix).HasMaxLength(5);
            builder.Entity<Employee>().Property(e => e.Sex).HasMaxLength(15);
            builder.Entity<Employee>().Property(e => e.Telephone).HasMaxLength(50);
            builder.Entity<Employee>().Property(e => e.MobileNumber).HasMaxLength(50);
            builder.Entity<Employee>().Property(e => e.FullName).HasMaxLength(150);

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
            // builder.Entity<RatingMatrix>(matrix => {
                
            //     matrix.Property(m => m.Dimension).HasMaxLength(15);
            //     matrix.Property(m => m.MeansOfVerification).HasMaxLength(150);
            // });

            // // Rating
            // builder.Entity<Rating>(rating => {
            //     rating.HasKey("RatingMatrixId", "Rate");
            //     rating.HasOne(r => r.Matrix)
            //     .WithMany(m => m.Ratings)
            //     .HasForeignKey(r => r.RatingMatrixId)
            //     .OnDelete(DeleteBehavior.Cascade);

            //     rating.Property(r => r.Description).HasMaxLength(100);
            // });
            
            // // KPI
            // builder.Entity<Kpi>(k => {
            //     k.HasMany(p => p.RatingMatrices).WithOne(m => m.Kpi).HasForeignKey(m => m.KpiId).OnDelete(DeleteBehavior.Cascade);
            //     k.HasMany(p => p.Kpis).WithOne(k => k.ParentKpi).HasForeignKey(p => p.ParentKpiId);
            //     k.Property(p => p.SuccessIndicator).HasMaxLength(256);
            //     k.Property(p => p.ActualAccomplishment).HasMaxLength(256);
            //     k.Property(p => p.OrderNumber).HasMaxLength(10);
            //     k.Property(p => p.Code).HasMaxLength(20);
            // });

            // KpiType
            builder.Entity<KpiType>(k => {
                k.Property(p => p.Description).HasMaxLength(30);
            });

            // IPCR
            // builder.Entity<Ipcr>(ipcr => {
            //     ipcr.HasOne(e => e.Ratee)
            //         .WithMany(r => r.IPCRs)
            //         .HasForeignKey(i => i.EmployeeId);
                
            //     ipcr.HasOne(i => i.Position)
            //         .WithMany(p => p.Ipcrs)
            //         .HasForeignKey(i => i.JobPositionId);
                
            //     ipcr.HasOne(i => i.Unit)
            //         .WithMany(p => p.IpcrsUnderThisUnit)
            //         .HasForeignKey(i => i.OrgUnitId);
                
            //     ipcr.HasOne(i => i.CompiledBy)
            //         .WithMany(p => p.CompiledIpcrs)
            //         .HasForeignKey(i => i.CompiledById);
                
            //     ipcr.HasOne(i => i.ReviewedBy)
            //         .WithMany(p => p.ReviewedIpcrs)
            //         .HasForeignKey(i => i.ReviewedById);
                
            //     ipcr.HasOne(i => i.ApprovedBy)
            //         .WithMany(p => p.ApprovedIpcrs)
            //         .HasForeignKey(i => i.ApprovedById);
                
            //     ipcr.HasMany(i => i.KPIs)
            //         .WithOne(p => p.Ipcr)
            //         .HasForeignKey(i => i.IpcrId);

            //     ipcr.Property(i => i.EmployeeId).HasMaxLength(30);
            //     ipcr.Property(i => i.ReviewedById).HasMaxLength(30);
            //     ipcr.Property(i => i.CompiledById).HasMaxLength(30);
            //     ipcr.Property(i => i.ApprovedById).HasMaxLength(30);
            //     ipcr.Property(i => i.ApprovedById).HasMaxLength(30);
            //     ipcr.Property(i => i.EmployeeIdLocation).HasMaxLength(30);
            //     ipcr.Property(i => i.Status).HasMaxLength(50);
            // });

            // IPCR Templates
            builder.Entity<IpcrTemplate>(ipcrTemplate => {
                ipcrTemplate.HasKey(p => p.Id);
                // ipcrTemplate.HasMany(p => p.Kpis)
                //     .WithOne(p => p.IpcrTemplateParent)
                //     .HasForeignKey(p => p.IpcrTemplateId)
                //     .OnDelete(DeleteBehavior.Cascade);
                ipcrTemplate.Property(p => p.Description).HasMaxLength(50);
            });
            // KPI templates
            builder.Entity<KpiTemplate>(kpiTemplate => {
                kpiTemplate.HasKey(p => p.Id);
                kpiTemplate.HasIndex(p => p.Path);

                // kpiTemplate.HasOne(p => p.IpcrTemplateParent)
                //             .WithMany(p => p.Kpis)
                //             .HasForeignKey(p => p.IpcrTemplateId);
                
                kpiTemplate.HasMany(p => p.Kpis)
                            .WithOne(p => p.ParentKpi)
                            .HasForeignKey(p => p.ParentKpiId)
                            .IsRequired(false)
                            .OnDelete(DeleteBehavior.NoAction);
                
                kpiTemplate.HasMany(p => p.RatingMatrixTemplates)
                    .WithOne(p => p.Kpi)
                    .HasForeignKey(p => p.KpiId)
                    .OnDelete(DeleteBehavior.Cascade);
                

                
                kpiTemplate.Property(p => p.SuccessIndicator).HasMaxLength(500);
                kpiTemplate.Property(p => p.Path).HasMaxLength(100);
                kpiTemplate.Property(p => p.TaskId).HasMaxLength(50);
                
            });

            // Rating Matrix Template
            builder.Entity<RatingMatrixTemplate>(r => {
                r.HasKey(p => p.Id);
                r.HasMany(p => p.Ratings)
                    .WithOne(p => p.Matrix)
                    .HasForeignKey(p => p.RatingMatrixId)
                    .OnDelete(DeleteBehavior.Cascade);
                r.HasOne(p => p.Kpi)
                    .WithMany(p => p.RatingMatrixTemplates)
                    .HasForeignKey(p => p.KpiId);
                
                r.Property(p => p.Dimension).HasMaxLength(15);
                r.Property(p => p.MeansOfVerification).HasMaxLength(150);
            });

            // RatingTemplates
            builder.Entity<RatingTemplate>(r => {
                r.HasKey(p => p.Id);
                r.HasOne(p => p.Matrix)
                    .WithMany(p => p.Ratings)
                    .HasForeignKey(p => p.RatingMatrixId);
                r.Property(p => p.Description).HasMaxLength(150);
            });

            // Personal Data Sheets
            builder.Entity<PersonalDataSheet>(pds => {
                pds.HasMany(p => p.Addresses)
                    .WithOne(a => a.Pds)
                    .HasForeignKey(a => a.PdsId)
                    .OnDelete(DeleteBehavior.Cascade);
                    
                pds.HasMany(p => p.Eligibilities)
                    .WithOne(a => a.Pds)
                    .HasForeignKey(a => a.PdsId)
                    .OnDelete(DeleteBehavior.Cascade);
                    
                pds.HasMany(p => p.IdCards)
                    .WithOne(a => a.Pds)
                    .HasForeignKey(a => a.PdsId)
                    .OnDelete(DeleteBehavior.Cascade);

                pds.HasMany(p => p.Children)
                    .WithOne(a => a.Pds)
                    .HasForeignKey(a => a.PdsId)
                    .OnDelete(DeleteBehavior.Cascade);

                pds.HasMany(p => p.EducationalBackgrounds)
                    .WithOne(a => a.Pds)
                    .HasForeignKey(a => a.PdsId);

                pds.HasMany(p => p.Memberships)
                    .WithOne(a => a.Pds)
                    .HasForeignKey(a => a.PdsId)
                    .OnDelete(DeleteBehavior.Cascade);

                pds.HasMany(p => p.Recognitions)
                    .WithOne(a => a.Pds)
                    .HasForeignKey(a => a.PdsId)
                    .OnDelete(DeleteBehavior.Cascade);

                pds.HasMany(p => p.Skills)
                    .WithOne(a => a.Pds)
                    .HasForeignKey(a => a.PdsId)
                    .OnDelete(DeleteBehavior.Cascade);

                pds.HasMany(p => p.TrainingsAttended)
                    .WithOne(a => a.Pds)
                    .HasForeignKey(a => a.PdsId)
                    .OnDelete(DeleteBehavior.Cascade);

                pds.HasMany(p => p.VoluntaryWorks)
                    .WithOne(a => a.Pds)
                    .HasForeignKey(a => a.PdsId)
                    .OnDelete(DeleteBehavior.Cascade);

                pds.HasMany(p => p.WorkExperiences)
                    .WithOne(a => a.Pds)
                    .HasForeignKey(a => a.PdsId)
                    .OnDelete(DeleteBehavior.Cascade);
                

                
                pds.Property(p => p.EmployeeId).HasMaxLength(30);
                pds.Property(p => p.BirthPlace).HasMaxLength(100);
                pds.Property(p => p.SpouseSurname).HasMaxLength(50);
                pds.Property(p => p.SpouseMiddle).HasMaxLength(50);
                pds.Property(p => p.SpouseFirstname).HasMaxLength(50);
                pds.Property(p => p.SpouseSuffix).HasMaxLength(5);
                pds.Property(p => p.SpouseOccupation).HasMaxLength(30);
                pds.Property(p => p.SpouseEmployer).HasMaxLength(100);
                pds.Property(p => p.SpouseEmployerAddress).HasMaxLength(100);
                pds.Property(p => p.SpouseEmployerTelNumber).HasMaxLength(30);
                pds.Property(p => p.FatherSurname).HasMaxLength(50);
                pds.Property(p => p.FatherMiddle).HasMaxLength(50);
                pds.Property(p => p.FatherFirstname).HasMaxLength(50);
                pds.Property(p => p.FatherSuffix).HasMaxLength(5);
                pds.Property(p => p.MotherMaidenName).HasMaxLength(150);
                pds.Property(p => p.MotherSurname).HasMaxLength(50);
                pds.Property(p => p.MotherFirstname).HasMaxLength(50);
                pds.Property(p => p.MotherMiddle).HasMaxLength(50);
                pds.Property(p => p.MotherSuffix).HasMaxLength(50);
                pds.Property(p => p.BirthPlace).HasMaxLength(150);
                pds.Property(p => p.CivilStatus).HasMaxLength(30);
                pds.Property(p => p.OtherCivilStatus).HasMaxLength(50);
                pds.Property(p => p.Height).HasMaxLength(10);
                pds.Property(p => p.Weight).HasMaxLength(10);
                pds.Property(p => p.BloodType).HasMaxLength(5);
                pds.Property(p => p.GsisNumber).HasMaxLength(50);
                pds.Property(p => p.HdmfNumber).HasMaxLength(50);
                pds.Property(p => p.PhilHealthNumber).HasMaxLength(50);
                pds.Property(p => p.SssNumber).HasMaxLength(50);
                pds.Property(p => p.TinNumber).HasMaxLength(50);
                pds.Property(p => p.AgencyNumber).HasMaxLength(50);
                pds.Property(p => p.Citizenship).HasMaxLength(20);
                pds.Property(p => p.DualCitizenType).HasMaxLength(50);
                pds.Property(p => p.DualCitizenCountry).HasMaxLength(50);
                pds.Property(p => p.AgencyNumber).HasMaxLength(50);
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

           

            // Address
            builder.Entity<Address>(e => {
                e.HasOne(p => p.Pds)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(el => el.PdsId);
                e.Property(p => p.EmployeeId).HasMaxLength(30);
                e.Property(p => p.Description).HasMaxLength(30);
                e.Property(p => p.Block).HasMaxLength(50);
                e.Property(p => p.Street).HasMaxLength(50);
                e.Property(p => p.Purok).HasMaxLength(50);
                e.Property(p => p.Barangay).HasMaxLength(50);
                e.Property(p => p.Municipality).HasMaxLength(50);
                e.Property(p => p.Province).HasMaxLength(50);
                e.Property(p => p.Region).HasMaxLength(50);
                e.Property(p => p.ZipCode).HasMaxLength(10);
            });

            //Character Reference
            builder.Entity<CharacterReference>(c => {
                c.HasOne(p => p.Pds)
                    .WithMany(p => p.References)
                    .HasForeignKey(cp => cp.PdsId);
                c.Property(p => p.EmployeeId).HasMaxLength(30);
                c.Property(p => p.Name).HasMaxLength(100);
                c.Property(p => p.Address).HasMaxLength(100);
                c.Property(p => p.Mobile).HasMaxLength(100);
            });

            //Child
            builder.Entity<Child>(c => {
                c.HasOne(p => p.Pds)
                    .WithMany(p => p.Children)
                    .HasForeignKey(cp => cp.PdsId);
                c.Property(p => p.EmployeeId).HasMaxLength(30);
                c.Property(p => p.Lastname).HasMaxLength(50);
                c.Property(p => p.Firstname).HasMaxLength(50);
                c.Property(p => p.Middle).HasMaxLength(50);
                c.Property(p => p.Suffix).HasMaxLength(10);
            });

            //Educational Background
            builder.Entity<EducationalBackground>(c => {
                c.HasOne(p => p.Pds)
                    .WithMany(p => p.EducationalBackgrounds)
                    .HasForeignKey(cp => cp.PdsId);
                c.Property(p => p.EmployeeId).HasMaxLength(30);
                c.Property(p => p.OrderNumber).HasMaxLength(5);
                c.Property(p => p.Level).HasMaxLength(30);
                c.Property(p => p.School).HasMaxLength(150);
                c.Property(p => p.Course).HasMaxLength(150);
                c.Property(p => p.HighestLevel).HasMaxLength(15);
                c.Property(p => p.YearGraduated).HasMaxLength(10);
                c.Property(p => p.Honors).HasMaxLength(30);
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
            
            // Identification
            builder.Entity<Identification>(e => {
                e.HasOne(p => p.Pds)
                    .WithMany(p => p.IdCards)
                    .HasForeignKey(el => el.PdsId);
                e.Property(p => p.EmployeeId).HasMaxLength(30);
                e.Property(p => p.IdType).HasMaxLength(30);
                e.Property(p => p.Control).HasMaxLength(30);
                e.Property(p => p.IssuanceDatePlace).HasMaxLength(60);
            });
            
            // Membership
            builder.Entity<Membership>(e => {
                e.HasOne(p => p.Pds)
                    .WithMany(p => p.Memberships)
                    .HasForeignKey(el => el.PdsId);
                e.Property(p => p.EmployeeId).HasMaxLength(30);
                e.Property(p => p.Organization).HasMaxLength(100);
                e.Property(p => p.CertNumber).HasMaxLength(15);
                
            });
            
            // Recognition
            builder.Entity<Recognition>(e => {
                e.HasOne(p => p.Pds)
                    .WithMany(p => p.Recognitions)
                    .HasForeignKey(el => el.PdsId);
                e.Property(p => p.EmployeeId).HasMaxLength(30);
                e.Property(p => p.Description).HasMaxLength(100);
                e.Property(p => p.Year).HasMaxLength(10);
                e.Property(p => p.CertNumber).HasMaxLength(15);
                
            });

            // Skill
            builder.Entity<Skill>(e => {
                e.HasOne(p => p.Pds)
                    .WithMany(p => p.Skills)
                    .HasForeignKey(el => el.PdsId);
                e.Property(p => p.EmployeeId).HasMaxLength(30);
                e.Property(p => p.Description).HasMaxLength(100);
            });
            

            // Trainings Attended
            builder.Entity<TrainingAttended>(e => {
                e.HasOne(p => p.Pds)
                    .WithMany(p => p.TrainingsAttended)
                    .HasForeignKey(el => el.PdsId);
                e.Property(p => p.EmployeeId).HasMaxLength(30);
                e.Property(p => p.LndType).HasMaxLength(30);
                e.Property(p => p.Sponsor).HasMaxLength(100);
                e.Property(p => p.TrainingCode).HasMaxLength(15);
                e.Property(p => p.CertNumber).HasMaxLength(15);
            });

            // Voluntary Wrk
            builder.Entity<VoluntaryWork>(e => {
                e.HasOne(p => p.Pds)
                    .WithMany(p => p.VoluntaryWorks)
                    .HasForeignKey(el => el.PdsId);
                e.Property(p => p.EmployeeId).HasMaxLength(30);
                e.Property(p => p.Address).HasMaxLength(150);
                e.Property(p => p.Position).HasMaxLength(150);
            });

            // Work Experience
            builder.Entity<WorkExperience>(e => {
                e.HasOne(p => p.Pds)
                    .WithMany(p => p.WorkExperiences)
                    .HasForeignKey(el => el.PdsId);
                e.Property(p => p.EmployeeId).HasMaxLength(30);
                e.Property(p => p.Position).HasMaxLength(100);
                e.Property(p => p.Company).HasMaxLength(150);
                e.Property(p => p.SalaryGrade).HasMaxLength(10);
                e.Property(p => p.AppointmentStatus).HasMaxLength(30);
            });



            // Lnd
            builder.Entity<Activity>(a => {
                a.HasOne(p => p.ActivityType)
                    .WithMany(at => at.Activities)
                    .HasForeignKey(at => at.ActivityTypeId);
                a.Property(p => p.Id).HasMaxLength(30);
            });

            builder.Entity<ActivityType>(a => {
                a.Property(p => p.Description).HasMaxLength(100);
            });

            builder.Entity<CertificateType>(a => {
                a.Property(p => p.Name).HasMaxLength(60);
                a.Property(p => p.Id).HasMaxLength(30);
            });

            builder.Entity<Certificate>(a => {
                a.HasOne(p => p.CertificateType)
                    .WithMany(at => at.Certificates)
                    .HasForeignKey(at => at.CertificateTypeId);
                    
                a.HasOne(p => p.TheActivity)
                    .WithMany(a => a.Certificates)
                    .HasForeignKey(p => p.ActivityId)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.Cascade);
                
                a.Property(p => p.ActivityId).IsRequired(false);
                a.Property(p => p.Id).HasMaxLength(30);
                a.Property(p => p.Role).HasMaxLength(50);
                a.Property(p => p.Sex).HasMaxLength(10);
                a.Property(p => p.Suffix).HasMaxLength(10);
                a.Property(p => p.ActivityId).HasMaxLength(30);
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