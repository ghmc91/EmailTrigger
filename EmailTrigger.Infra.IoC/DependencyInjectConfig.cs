using EmailTrigger.Domain.Interfaces.Reposiories;
using EmailTrigger.Domain.Interfaces.Services;
using EmailTrigger.Infra.Data.Repositories;
using EmailTrigger.Service.Services;
using Infra.Framework.Email;
using Infra.Framework.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace EmailTrigger.Infra.IoC
{
    public static class DependencyInjectConfig
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<ISellerRepository, SellerRepository>();
            services.AddTransient<ISellerService, SellerService>();
            services.AddScoped<IExchangeService, ExchangeService>();
            services.AddScoped<IEmailService, EmailService>();
        }
    }
}
