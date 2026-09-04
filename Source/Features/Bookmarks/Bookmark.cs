using Bookmarks.Features.BookmarkGroups;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookmarks.Features.Bookmarks;

public class Bookmark :  Entity
{
    public required string Name {get; set;}
    public  required string Url {get; set;}
    public string? Description {get; set;}
    
    public bool IsArchived {get; set;}
    public bool IsFavourite {get; set;}
    
    public bool IsDeleted {get; set;}

    public DateTime CreatedAt { get; init; }
    public DateTime LastModifiedAt {get; set;}
    
    public BookmarkGroup? Group { get; init; }
    public int? GroupId {get; set;}
}


public class BookmarkEntityTypeConfiguration : IEntityTypeConfiguration<Bookmark>
{
    public void Configure(EntityTypeBuilder<Bookmark> entity)
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
              .ValueGeneratedOnAdd();

        entity.HasOne(e => e.Owner)
              .WithMany()
              .HasForeignKey(e => e.OwnerId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.HasIndex(e => new { e.OwnerId, e.Name, e.Url })
              .IsUnique();
    }
}


public static class BookmarkEndpointNames
{
    public const string CreateBookmark = nameof(CreateBookmark);
    public const string DeleteBookmark =  nameof(DeleteBookmark);
    public const string EmptyTrash =  nameof(EmptyTrash);
    public const string GetAll =  nameof(GetAll);
    public const string GetById =  nameof(GetById);
    public const string Trash = nameof(Trash);
    public const string UpdateBookmark =  nameof(UpdateBookmark);
}