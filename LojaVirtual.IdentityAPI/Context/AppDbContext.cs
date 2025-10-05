using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LojaVirtual.IdentityAPI.Context;

public class AppDbContext(DbContextOptions options) : IdentityDbContext<User>(options);
