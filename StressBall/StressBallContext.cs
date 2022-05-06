using Microsoft.EntityFrameworkCore;



namespace StressBall
{
    public class StressBallContext : DbContext
    {
        public StressBallContext(DbContextOptions<StressBallContext> options) : base(options)
        { }
        public DbSet<StressBallData> StressBall { get; set; }

        public static readonly string ConnectionString = "Server=tcp:newstressballserver.database.windows.net,1433;Initial Catalog=StressBallServer;Persist Security Info=False;User ID=Malthemus;Password=Malthe10;";


    }
}