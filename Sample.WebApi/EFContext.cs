using Microsoft.EntityFrameworkCore;
namespace Sample.WebApi
{
    public class EFContext : DbContext
    {
        public EFContext(DbContextOptions<EFContext> options) : base(options)
        {
        }
    }
}