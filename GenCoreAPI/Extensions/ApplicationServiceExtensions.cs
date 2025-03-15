using GCDomain.Data;
using GCDomain.Helpers;
using GCServices.UserService;
using Microsoft.EntityFrameworkCore;

namespace GenCoreAPI.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("GenCoreDbContext"), c => c.MigrationsAssembly("GCServices"));
            });

            services.AddMvc();
            services.AddControllers();
            services.AddSwaggerGen();
            services.AddHttpContextAccessor();
            services.AddSession();
            services.AddDistributedMemoryCache();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowCorsPolicy", c =>
                {
                    c.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IResponseHelper, ResponseHelper>();

            return services;
        }
    }
}
