using Bookmarks.Features.User;

namespace Bookmarks;

/// <summary>
/// This is a base class for all DB Tables.
/// It provides a Primary Key and relationship with the <see cref="ApplicationUser"/> table.
/// </summary>
public abstract class Entity
{
    public int Id {get; init;}
    
    public ApplicationUser Owner { get; init; } = null!;
    public required string OwnerId {get; init;}
}