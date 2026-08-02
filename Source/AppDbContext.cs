using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bookmarks;


public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext(options)
{
    
}