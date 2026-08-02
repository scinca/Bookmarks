using Bookmarks.Features.BookmarkGroups;
using Bookmarks.Features.Bookmarks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bookmarks;


public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext(options)
{
    public DbSet<Bookmark> Bookmarks {get; set;}
    public DbSet<BookmarkGroup> BookmarkGroups {get; set;}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

    #region BookmarkGroup
        builder.Entity<BookmarkGroup>(entity =>
        {
            entity.Property(e => e.Name)
                  .HasMaxLength(100)
                  .IsRequired();

            entity.Property(e => e.CreatedAt)
                  .HasDefaultValueSql("CURRENT_TIMESTAMP")
                  .IsRequired();
            
            entity.Property(e => e.Description)
                  .HasMaxLength(500)
                  .HasDefaultValue(null)
                  .IsRequired();
            
            
            entity.HasOne(e => e.Owner)
                  .WithMany()
                  .HasForeignKey(e => e.OwnerId)
                  .OnDelete(DeleteBehavior.Cascade);
                
            entity.HasMany(e => e.Bookmarks)
                  .WithOne(e => e.Group)
                  .HasForeignKey(e => e.GroudId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(e => new { e.Name })
                  .IsUnique();

            // TODO: Default Query Filter on UserId
        });
    #endregion
        
   #region Bookmark
        builder.Entity<Bookmark>(entity =>
        {
            entity.Property(e => e.Name)
                  .HasMaxLength(2048)
                  .IsRequired();

            entity.Property(e => e.Url)
                  .HasMaxLength(2048)
                  .IsRequired();

            entity.Property(e => e.Description)
                  .HasMaxLength(500)
                  .HasDefaultValue(null);

            entity.Property(e => e.IsArchived)
                  .IsRequired()
                  .HasDefaultValue(false);

            entity.Property(e => e.IsFavourite)
                  .IsRequired()
                  .HasDefaultValue(false);

            entity.Property(e => e.CreatedAt)
                  .IsRequired()
                  .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.Property(e => e.LastModifiedAt)
                  .IsRequired()
                  .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(e => e.Owner)
                  .WithMany()
                  .HasForeignKey(e => e.OwnerId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(e => new { e.Name, e.Url })
                  .IsUnique();
        });
    #endregion


    }
} 