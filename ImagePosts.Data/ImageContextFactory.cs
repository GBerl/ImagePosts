using System.IO;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ImagePosts.Data
{
    public class ImageContextFactory : IDesignTimeDbContextFactory<ImageContext>
    {
        public ImageContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()

                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), $"..{Path.DirectorySeparatorChar}ImagePosts.Web"))
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true).Build();

            return new ImageContext(config.GetConnectionString("ConStr"));
        }
        
    }
}
