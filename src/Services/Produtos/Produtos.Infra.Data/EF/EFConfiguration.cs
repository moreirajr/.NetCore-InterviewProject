using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Produtos.Infra.Data.EF
{
    public static class EFConfiguration
    {
        public static IServiceCollection InitializeEFConfiguration(this IServiceCollection services, string connectionString)
        {
            services.AddEntityFrameworkSqlServer()
                .AddDbContext<ProdutoDbContext>(options =>
                {
                    options.UseSqlServer(connectionString,
                        sqlServerOptionsAction: sqlOptions =>
                        {
                            sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                        });
                },
                ServiceLifetime.Scoped
            );

            return services;
        }
    }
}