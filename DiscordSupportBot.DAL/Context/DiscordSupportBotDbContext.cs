using Microsoft.EntityFrameworkCore;

namespace DiscordSupportBot.DAL.Context
{
    public class DiscordSupportBotDbContext : DbContext, IDiscordSupportBotDbContext
    {
        public DiscordSupportBotDbContext()
            : base(GetOptions(""))
        {
        }

        private static DbContextOptions GetOptions(string connectionString)
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;
        }
    }
}