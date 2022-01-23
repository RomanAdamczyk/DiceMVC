using DiceMVC.Application.Interfaces;
using DiceMVC.Application.Services;
using DiceMVC.Domain.Interface;
using DiceMVC.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceMVC.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IPlayerRepository, PlayerRepository>();
            services.AddTransient<IGameRepository, GameRepository>();
            return services;
        }
    }
}
