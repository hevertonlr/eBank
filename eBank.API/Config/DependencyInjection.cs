using AutoMapper;
using eBank.API.Mutations;
using eBank.API.Queries;
using eBank.API.Schemas;
using eBank.API.Types;
using eBank.Infra.Data;
using eBank.Infra.Repositories.Interfaces;
using eBank.Infra.Repositories.Repositories;
using eBank.Infra.Services.Interfaces;
using eBank.Infra.Services.Services;
using GraphQL;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace eBank.API.Config
{
    public static class DependencyInjection
    {
        public static void RegisterDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IBankAccountRepository, BankAccountRepository>();
            services.AddScoped<IBankAccountService, BankAccountService>();
            services.AddScoped<BankAccountContext>();

            services.AddDbContext<BankAccountContext>(opt => opt
                .UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
               //.UseLoggerFactory(
               //     new LoggerFactory()
               //     .AddConsole((category, level) => level == LogLevel.Information && 
               //                 category == DbLoggerCategory.Database.Command.Name, 
               //                 true))
               .EnableSensitiveDataLogging());

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConfiguration(configuration.GetSection("Logging"));
                loggingBuilder.AddConsole();
                loggingBuilder.AddDebug();
            });

            services.AddAutoMapper(typeof(MappingProfile));

            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<AccountBalanceQuery>();
            services.AddSingleton<BankAccountMutation>();
            services.AddSingleton<BankAccountOperationsType>();
            services.AddSingleton<BankAccountSchema>();

            var sp = services.BuildServiceProvider();
            services.AddSingleton<ISchema>(new BankAccountSchema(new FuncDependencyResolver(type => sp.GetService(type))));
        }
    }
}
