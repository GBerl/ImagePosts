using Microsoft.EntityFrameworkCore;

namespace ImagePosts.Data
{
    public class ImageContext : DbContext
    {
        private string _connection;

        public ImageContext(string connection)
        {
            _connection = connection;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connection);
        }

        public DbSet<Image> Images { get; set; }
    }
}
