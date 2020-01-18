using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(WebGLGames.Areas.Identity.IdentityHostingStartup))]
namespace WebGLGames.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}