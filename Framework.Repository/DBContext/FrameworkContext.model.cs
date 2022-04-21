
namespace Framework.Repository
{
    public partial class FrameworkContext:DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}
