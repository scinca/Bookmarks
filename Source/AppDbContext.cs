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

        builder.Entity<BookmarkGroup>(entity =>
        {
            entity.Property(e => e.Name)
                  .HasMaxLength(100)
                  .IsRequired();

            entity.Property(e => e.CreatedAt)
                  .HasDefaultValueSql("CURRENT_TIMESTAMP")
                  .IsRequired();
            
            entity.HasMany(e => e.Bookmarks)
                  .WithOne(e => e.Group)
                  .HasForeignKey(e => e.GroudId)
                  .OnDelete(DeleteBehavior.Cascade);
            
            // TODO: Default Query Filter on UserId
        });
    }
} 