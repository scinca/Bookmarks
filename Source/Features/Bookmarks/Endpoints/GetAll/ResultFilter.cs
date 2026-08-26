using System.Text.Json.Serialization;

namespace Bookmarks.Features.Bookmarks.Endpoints;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ResultFilter
{
    All,
    Favourites,
    Archived,
    Deleted,
}