using Loan.ApplicationCore.Interfaces.Repositories;
using Loan.ApplicationCore.Interfaces.Services;
using Loan.ApplicationCore.Services;
using Loan.Infrastructure.DAL.Repository;
using Loan.Infrastructure.DAL.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Loan.Infrastructure.IoC
{
    public static class LoanDependencyContainer
    {
        public static IServiceCollection AddLoanServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBorrowerService, BorrowerService>();
            services.AddScoped<ILoanOfficerService, LoanOfficerService>();
            services.AddScoped<IOneLoanService, OneLoanService>();
            services.AddScoped<ILoanAndLoanOfficerService, LoanAndLoanOfficerService>();

            return services;
        }
    }
}
