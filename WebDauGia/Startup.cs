using Owin;
using Microsoft.Owin;
using Microsoft.Extensions.DependencyInjection;

[assembly: OwinStartup(typeof(WebDauGia.Startup))]

namespace WebDauGia
{
    public class Startup
    {

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedMemoryCache();
            services.AddMemoryCache();
          //  services.AddControllers();
        }


        public void Configuration(IAppBuilder app)
        {
            // Any connection or hub wire up and configuration should go here
            app.MapSignalR();
        }
    }
}