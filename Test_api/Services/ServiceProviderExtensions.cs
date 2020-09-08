﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_api.Models;
using Test_api.Models.Repository;

namespace Test_api.Services
{
    public static class ServiceProviderExtensions
    {
        public static void AddRepositoryService(this IServiceCollection services)
        {
            services.AddTransient<IRepository<Car>, MSSQLRepository<Car>>();
        }

        public static void AddMSSQLContext(this IServiceCollection services, IConfiguration Configuration)
        {
            string connection = Configuration.GetConnectionString("MSSQL");
            // добавляем контекст MobileContext в качестве сервиса в приложение
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(connection));
        }
    }
}
