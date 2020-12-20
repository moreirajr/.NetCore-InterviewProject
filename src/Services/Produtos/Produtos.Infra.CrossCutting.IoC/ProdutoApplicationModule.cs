using Microsoft.Extensions.DependencyInjection;
using Produtos.Application.Produtos;
using Produtos.Application.Produtos.Interfaces;
using Produtos.Domain.Interfaces;
using Produtos.Domain.Services;
using Produtos.Infra.CrossCutting.Files;
using Produtos.Infra.CrossCutting.Logging;
using Produtos.Infra.Data.EF;
using Produtos.Infra.Data.Repositories;

namespace Produtos.Infra.CrossCutting.IoC
{
    public static class ProdutoApplicationModule
    {
        public static IServiceCollection ConfigureDependencies(this IServiceCollection services)
        {
            //create factory
            services.AddScoped(typeof(IProdutoLog<>), typeof(ProdutoLog<>));

            services.AddScoped<IProdutoRepository, ProdutoRepository>();

            services.AddScoped<IProdutoService, ProdutoService>(x => new ProdutoService(
                x.GetService<IProdutoRepository>(),
                x.GetService<IProdutoLog<ProdutoService>>()
                ));

            services.AddScoped<IProdutoAppService, ProdutoAppService>();

            return services;
        }

        public static IServiceCollection ConfigureEF(this IServiceCollection services, string connectionString)
        {
            services.InitializeEFConfiguration(connectionString);

            return services;
        }

        public static IServiceCollection ConfigureFileService(this IServiceCollection services, string path)
        {
            services.AddSingleton<IFileService, FileService>(x => new FileService(path));

            return services;
        }
    }
}