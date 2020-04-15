using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using YamangTao.Model;
using YamangTao.Model.Auth;

namespace YamangTao.Data
{
    public class DataContext : IdentityDbContext<User, Role, string, 
        IdentityUserClaim<string>, UserRole, IdentityUserLogin<string>, 
        IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options) {}
        // using Microsoft.EntityFrameworkCore;
        
        public DbSet<Employee> Employees { get; set; }

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