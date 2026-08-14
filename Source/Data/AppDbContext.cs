using Bookmarks.Common;
using Bookmarks.Features.BookmarkGroups;
using Bookmarks.Features.Bookmarks;
using EntityFramework.Exceptions.Sqlite;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bookmarks;

// #TODO: LastModifiedAt doesn't auto update properly.
public class AppDbContext(DbContextOptions<AppDbContext> options, ICurrentUserService currentUser) : IdentityDbContext(options)
{
    public DbSet<Bookmark> Bookmarks {get; set;}
    public DbSet<BookmarkGroup> BookmarkGroups {get; set;}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseExceptionProcessor();
    }

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
                  .ValueGeneratedOnAdd();

            entity.Property(e => e.Description)
                  .HasMaxLength(500)
                  .HasDefaultValue(null);
            
            
            entity.HasOne(e => e.Owner)
                  .WithMany()
                  .HasForeignKey(e => e.OwnerId)
                  .OnDelete(DeleteBehavior.Cascade);
                
            entity.HasMany(e => e.Bookmarks)
                  .WithOne(e => e.Group)
                  .HasForeignKey(e => e.GroupId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(e => new { e.OwnerId ,e.Name })
                  .IsUnique();

            entity.HasQueryFilter(QueryFilters.UserFilter ,e => e.OwnerId == currentUser.Id);
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
                  .HasDefaultValue(false);

            entity.Property(e => e.IsFavourite)
                  .HasDefaultValue(false);
            
            entity.Property(e => e.IsDeleted)
                  .HasDefaultValue(false);
            
            entity.Property(e => e.CreatedAt)
                  .HasDefaultValueSql("CURRENT_TIMESTAMP")
                  .ValueGeneratedOnAdd();

            entity.Property(e => e.LastModifiedAt)
                  .HasDefaultValueSql("CURRENT_TIMESTAMP")
                  .ValueGeneratedOnAddOrUpdate();

            entity.HasOne(e => e.Owner)
                  .WithMany()
                  .HasForeignKey(e => e.OwnerId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(e => new { e.OwnerId, e.Name, e.Url })
                  .IsUnique();
            
            entity.HasQueryFilter(QueryFilters.UserFilter ,e => e.OwnerId == currentUser.Id);
            entity.HasQueryFilter(QueryFilters.SoftDeletionFilter, e => e.IsDeleted == false);
            entity.HasQueryFilter(QueryFilters.ArchivedFilter, e => e.IsArchived == false);
        });
    #endregion
    }
} 