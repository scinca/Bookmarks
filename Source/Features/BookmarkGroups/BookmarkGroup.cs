using Bookmarks.Features.Bookmarks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookmarks.Features.BookmarkGroups;

public class BookmarkGroup : Entity
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; init; }

    public IReadOnlyCollection<Bookmark> Bookmarks { get; init; } = null!;
}


public class BookmarkGroupEntityTypeConfiguration : IEntityTypeConfiguration<BookmarkGroup>
{
    public void Configure(EntityTypeBuilder<BookmarkGroup> entity)
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
    }
}


public static class BookmarkGroupEndpointNames
{
    public const string GetGroupById = nameof(GetGroupById);
    public const string DeleteGroup = nameof(DeleteGroup);
    public const string GetAllGroups = nameof(GetAllGroups);
    public const string UpdateGroup = nameof(UpdateGroup);
    public const string CreateGroup =  nameof(CreateGroup);
}