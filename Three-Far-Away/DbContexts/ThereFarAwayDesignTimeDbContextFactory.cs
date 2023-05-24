using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Three_Far_Away.DbContexts
{
    public class ThereFarAwayDesignTimeDbContextFactory : IDesignTimeDbContextFactory<ThereFarAwayDbContext>
    {
        public ThereFarAwayDbContext CreateDbContext(string[] args) {
            string CONNECTION_STRING = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ThereFarAwayDB;Integrated Security=True";
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlServer(CONNECTION_STRING).Options;
            return new ThereFarAwayDbContext(options);
        }
    }
}
