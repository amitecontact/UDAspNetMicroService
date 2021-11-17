using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Contracts.Infrastucture;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Models;
using Ordering.Infrastructure.Mail;
using Ordering.Infrastructure.Persistence;
using Ordering.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure
{
    public static class InfrastuctureServiceRegistration
    {
        public static IServiceCollection AddInfrastuctureService(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext<OrderContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("OrderConnectionString")));

            service.AddScoped(typeof(IAsynRepository<>), typeof(BaseRepository<>));
            service.AddScoped<IOrderRepository, OrderRepository>();

            service.Configure<EmailSettings>(c => configuration.GetSection("EmailSettings"));
            service.AddTransient<IEmailService, EmailService>();

            return service;
        }
    }
}
