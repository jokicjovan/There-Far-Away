using Microsoft.EntityFrameworkCore;

namespace Three_Far_Away.DbContexts
{
    public class ThereFarAwayDbContextFactory
    {
        private readonly string _connectionString;
        public ThereFarAwayDbContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ThereFarAwayDbContext CreateDbContext()
        {
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlServer(_connectionString).Options;
            return new ThereFarAwayDbContext(options);
        }
    }
}
