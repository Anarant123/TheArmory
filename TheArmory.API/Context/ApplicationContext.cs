using Microsoft.EntityFrameworkCore;
using TheArmory.Domain.Models.Database;

namespace TheArmory.Context;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // связи сущности Ad 
        modelBuilder.Entity<Ad>()
            .HasOne<Condition>(a => a.Condition)
            .WithMany(c => c.Ads)
            .HasForeignKey(a => a.ConditionId);
        modelBuilder.Entity<Ad>()
            .HasOne<Region>(a => a.Region)
            .WithMany(r => r.Ads)
            .HasForeignKey(a => a.RegionId);
        modelBuilder.Entity<Ad>()
            .HasOne<User>(a => a.User)
            .WithMany(u => u.Ads)
            .HasForeignKey(a => a.UserId);
        modelBuilder.Entity<Ad>()
            .HasOne<Status>(a => a.Status)
            .WithMany(u => u.Ads)
            .HasForeignKey(a => a.StatusId);
        modelBuilder.Entity<Ad>()
            .HasMany<Complaint>(a => a.Complaints )
            .WithOne(c => c.Ad)
            .HasForeignKey(c => c.AdId);
        modelBuilder.Entity<Ad>()
            .HasMany<Favorite>(a => a.Favorites )
            .WithOne(f => f.Ad)
            .HasForeignKey(f => f.AdId);
        modelBuilder.Entity<Ad>()
            .HasMany<Media>(a => a.Medias )
            .WithOne(m => m.Ad)
            .HasForeignKey(m => m.AdId);
        
        
        // связи сущности Complaint
        modelBuilder.Entity<Complaint>()
            .HasOne<Ad>(c => c.Ad)
            .WithMany(a => a.Complaints)
            .HasForeignKey(a => a.AdId);
        modelBuilder.Entity<Complaint>()
            .HasOne<User>(c => c.User)
            .WithMany(u => u.Complaints)
            .HasForeignKey(a => a.UserId);
        
        // связи сущности Favorites
        modelBuilder.Entity<Favorite>()
            .HasOne<Ad>(f => f.Ad)
            .WithMany(a => a.Favorites)
            .HasForeignKey(a => a.AdId);
        modelBuilder.Entity<Favorite>()
            .HasOne<User>(f => f.User)
            .WithMany(u => u.Favorites)
            .HasForeignKey(a => a.UserId);
        
        // связи сущности Media
        modelBuilder.Entity<Media>()
            .HasOne<Ad>(m => m.Ad)
            .WithMany(a => a.Medias)
            .HasForeignKey(a => a.AdId);
        
        // cвязи сущности Region
        modelBuilder.Entity<Region>()
            .HasMany<Ad>(m => m.Ads)
            .WithOne(a => a.Region)
            .HasForeignKey(a => a.RegionId);

        // связи сущности Role
        modelBuilder.Entity<Role>()
            .HasMany<User>(r => r.Users)
            .WithOne(u => u.Role)
            .HasForeignKey(u => u.RoleId);
        
        // связи сущности Status
        modelBuilder.Entity<Status>()
            .HasMany<User>(s => s.Users)
            .WithOne(u => u.Status)
            .HasForeignKey(u => u.StatusId);
        modelBuilder.Entity<Status>()
            .HasMany<Ad>(s => s.Ads)
            .WithOne(a => a.Status)
            .HasForeignKey(a => a.StatusId);
        
        // связи сущности User
        modelBuilder.Entity<User>()
            .HasOne<Region>(u => u.Region)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.RegionId);
        modelBuilder.Entity<User>()
            .HasOne<Role>(u => u.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.RoleId);
        modelBuilder.Entity<User>()
            .HasOne<Status>(u => u.Status)
            .WithMany(s => s.Users)
            .HasForeignKey(u => u.StatusId);
        modelBuilder.Entity<User>()
            .HasMany<Ad>(u => u.Ads)
            .WithOne(a => a.User)
            .HasForeignKey(a => a.UserId);
        modelBuilder.Entity<User>()
            .HasMany<Complaint>(u => u.Complaints)
            .WithOne(c => c.User)
            .HasForeignKey(c => c.UserId);
        modelBuilder.Entity<User>()
            .HasMany<Favorite>(u => u.Favorites)
            .WithOne(f => f.User)
            .HasForeignKey(f => f.UserId);
        modelBuilder.Entity<User>()
            .HasMany<Contact>(u => u.Contacts)
            .WithOne(c => c.User)
            .HasForeignKey(c => c.UserId);
        
        // связи сущности Condition
        modelBuilder.Entity<Condition>()
            .HasMany<Ad>(c => c.Ads)
            .WithOne(a => a.Condition)
            .HasForeignKey(a => a.ConditionId);
        
        // связи сущности Contact
        modelBuilder.Entity<Contact>()
            .HasOne<User>(c => c.User)
            .WithMany(u => u.Contacts)
            .HasForeignKey(c => c.UserId);
    }
    
    public DbSet<Ad> Ads { get; set; }
    public DbSet<Complaint> Complaints { get; set; }
    public DbSet<Favorite> Favorites { get; set; }
    public DbSet<Media> Medias { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Status> Statuses { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Condition> Conditions { get; set; }
    public DbSet<Contact> Contacts { get; set; }
}