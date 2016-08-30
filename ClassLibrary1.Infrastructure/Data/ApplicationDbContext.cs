using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using ClassLibrary1.Infrastructure.Security;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace ClassLibrary1.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            var context = new ApplicationDbContext();

            context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            return context;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>().MapToStoredProcedures();
            //modelBuilder.Entity<IdentityRole>().MapToStoredProcedures();
            modelBuilder.Entity<PreviousPassword>().MapToStoredProcedures();

            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<ApplicationUser>().Ignore(u => u.Logins);
            //modelBuilder.Ignore<IdentityUserLogin>();
        }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    // How to remove dbo.AspNetUserClaims and dbo.AspNetUserLogins tables
        //    // http://stackoverflow.com/questions/28948309/how-to-remove-dbo-aspnetuserclaims-and-dbo-aspnetuserlogins-tables-identityuser
        //    // NOTE: that OnModelCreating override should not invoke base.OnModelCreating method.
        //    // Needed to ensure subclasses share the same table
        //    var user = modelBuilder.Entity<ApplicationUser>()
        //        .ToTable("AspNetUsers");
        //    user.HasMany(u => u.Roles).WithRequired().HasForeignKey(ur => ur.UserId);
        //    user.Ignore(u => u.Claims);
        //    user.Ignore(u => u.Logins);
        //    user.Property(u => u.UserName)
        //        .IsRequired()
        //        .HasMaxLength(256)
        //        .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("UserNameIndex") { IsUnique = true }));

        //    // CONSIDER: u.Email is Required if set on options?
        //    user.Property(u => u.Email).HasMaxLength(256);

        //    modelBuilder.Entity<IdentityUserRole>()
        //        .HasKey(r => new { r.UserId, r.RoleId })
        //        .ToTable("AspNetUserRoles");

        //    var role = modelBuilder.Entity<IdentityRole>()
        //        .ToTable("AspNetRoles");
        //    role.Property(r => r.Name)
        //        .IsRequired()
        //        .HasMaxLength(256)
        //        .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("RoleNameIndex") { IsUnique = true }));
        //    role.HasMany(r => r.Users).WithRequired().HasForeignKey(ur => ur.RoleId);

        //    modelBuilder.Ignore<IdentityUserLogin>();
        //    modelBuilder.Ignore<IdentityUserClaim>();
        //}



        // From: https://www.captechconsulting.com/blogs/Customizing-the-ASPNET-Identity-Data-Model-with-the-Entity-Framework-Fluent-API--Part-1
        //protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.HasDefaultSchema("NAM");

        //    modelBuilder.Entity().Map(c =>
        //    {
        //        c.ToTable("UserLogin");
        //        c.Properties(p => new
        //        {
        //            p.UserId,
        //            p.LoginProvider,
        //            p.ProviderKey
        //        });
        //    }).HasKey(p => new { p.LoginProvider, p.ProviderKey, p.UserId });

        //    // Mapping for ApiRole
        //    modelBuilder.Entity().Map(c =>
        //    {
        //        c.ToTable("Role");
        //        c.Property(p => p.Id).HasColumnName("RoleId");
        //        c.Properties(p => new
        //        {
        //            p.Name
        //        });
        //    }).HasKey(p => p.Id);
        //    modelBuilder.Entity().HasMany(c => c.Users).WithRequired().HasForeignKey(c => c.RoleId);

        //    modelBuilder.Entity().Map(c =>
        //    {
        //        c.ToTable("User");
        //        c.Property(p => p.Id).HasColumnName("UserId");
        //        c.Properties(p => new
        //        {
        //            p.AccessFailedCount,
        //            p.Email,
        //            p.EmailConfirmed,
        //            p.PasswordHash,
        //            p.PhoneNumber,
        //            p.PhoneNumberConfirmed,
        //            p.TwoFactorEnabled,
        //            p.SecurityStamp,
        //            p.LockoutEnabled,
        //            p.LockoutEndDateUtc,
        //            p.UserName
        //        });
        //    }).HasKey(c => c.Id);
        //    modelBuilder.Entity().HasMany(c => c.Logins).WithOptional().HasForeignKey(c => c.UserId);
        //    modelBuilder.Entity().HasMany(c => c.Claims).WithOptional().HasForeignKey(c => c.UserId);
        //    modelBuilder.Entity().HasMany(c => c.Roles).WithRequired().HasForeignKey(c => c.UserId);

        //    modelBuilder.Entity().Map(c =>
        //    {
        //        c.ToTable("UserRole");
        //        c.Properties(p => new
        //        {
        //            p.UserId,
        //            p.RoleId
        //        });
        //    })
        //    .HasKey(c => new { c.UserId, c.RoleId });

        //    modelBuilder.Entity().Map(c =>
        //    {
        //        c.ToTable("UserClaim");
        //        c.Property(p => p.Id).HasColumnName("UserClaimId");
        //        c.Properties(p => new
        //        {
        //            p.UserId,
        //            p.ClaimValue,
        //            p.ClaimType
        //        });
        //    }).HasKey(c => c.Id);
        //}

    }
}