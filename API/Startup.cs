using API.Extensions;
using API.Helper;
using API.Middlewares;
using AutoMapper;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis;

namespace API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<StoreContext>(options => options.UseSqlServer(_configuration.GetConnectionString("skinet")));
            services.AddDbContext<IdentityContext>(options => options.UseSqlServer(_configuration.GetConnectionString("identity")));

            services.AddSingleton<IConnectionMultiplexer>(conf =>
            {
                ConfigurationOptions configuration = ConfigurationOptions.Parse(_configuration.GetConnectionString("redis"), true);
                return ConnectionMultiplexer.Connect(configuration);
            });

            services.ConfigureIdentityServices(_configuration);
            services.ConfigureApplicationServices();
            services.AddSwaggerService();
            services.AddAutoMapper(typeof(MappingProfile));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // app.UseDeveloperExceptionPage();
                app.UseSwaggerService();
            }

            app.UseMiddleware<ApiExceptionErrorMiddleware>();

            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseCors("corsPolicy");
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
