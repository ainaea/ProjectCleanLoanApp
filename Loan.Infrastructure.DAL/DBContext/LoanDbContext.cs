using Loan.ApplicationCore.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Loan.Infrastructure.DAL.DBContext
{
    public class LoanDbContext : IdentityDbContext<LoanOfficer>
    {
        public LoanDbContext(DbContextOptions<LoanDbContext> options) : base(options)
        {  }

        public DbSet<Borrower> AllBorrowers { get; set; }
        public DbSet<LoanOfficer> AllLoanOfficers { get; set; }
        public DbSet<OneLoan> AllLoans { get; set; }
        public DbSet<LoanAndLoanOfficer> AllLoanAndLoanOfficers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<OneLoan>().Property(o => o.InterestRate).HasColumnType("decimal(18,2)");
            builder.Entity<OneLoan>().Property(o => o.Amount).HasColumnType("decimal(18,2)");
        }
    }
}
