using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Produtos.Infra.CrossCutting.IoC;

namespace Produtos.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
               .AddControllersConfiguration()
               .AddSwaggerConfiguration(Configuration)
               .AddEFConfiguration(Configuration)
               .AddFileService(Environment)
               .ConfigureDependencies();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {           
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy");
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger().UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Produto Api v1");
            });
        }
    }

    public static class StartupExtensions
    {
        public static IServiceCollection AddControllersConfiguration(this IServiceCollection services)
        {
            services
                .AddControllers(options => options.Conventions.Add(new RouteTokenTransformerConvention(new LowerCaseParameterTransformer())))
                .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.SetIsOriginAllowed((host) => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            return services;
        }

        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Produto Api",
                    Version = "v1",
                    Description = "Serviço de Api de Produtos"
                });
            });

            return services;
        }

        public static IServiceCollection AddEFConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            return services.ConfigureEF(configuration.GetConnectionString("connection"));
        }

        public static IServiceCollection AddFileService(this IServiceCollection services, IWebHostEnvironment env)
        {
            string path;

            if (env.IsDevelopment())
                path = env.ContentRootPath;
            else
                path = env.WebRootPath;

            return services.ConfigureFileService(path);
        }
    }

    public class LowerCaseParameterTransformer : IOutboundParameterTransformer
    {
        public string TransformOutbound(object value)
        {
            return value == null ? null : value.ToString().ToLower();
        }
    }
}
