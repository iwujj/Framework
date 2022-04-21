namespace Framework.Repository
{
    public partial class FrameworkContext : DbContext
    {
        public FrameworkContext(DbContextOptions options) : base(options)
        {
            Console.WriteLine(typeof(FrameworkContext).FullName);
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(AppSettings.ConnectionString);
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

     
    }
}
