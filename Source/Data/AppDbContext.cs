using Bookmarks.Common;
using Bookmarks.Features.BookmarkGroups;
using Bookmarks.Features.Bookmarks;
using EntityFramework.Exceptions.Sqlite;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bookmarks;

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

        new BookmarkGroupEntityTypeConfiguration().Configure(builder.Entity<BookmarkGroup>());
        builder.Entity<BookmarkGroup>().HasQueryFilter(QueryFilters.UserFilter ,e => e.OwnerId == currentUser.Id);

        
        new BookmarkEntityTypeConfiguration().Configure(builder.Entity<Bookmark>());
        builder.Entity<Bookmark>(entity =>
        {
            entity.HasQueryFilter(QueryFilters.UserFilter ,e => e.OwnerId == currentUser.Id);
            entity.HasQueryFilter(QueryFilters.SoftDeletionFilter, e => e.IsDeleted == false);
            entity.HasQueryFilter(QueryFilters.ArchivedFilter, e => e.IsArchived == false);
        });
    }
} 