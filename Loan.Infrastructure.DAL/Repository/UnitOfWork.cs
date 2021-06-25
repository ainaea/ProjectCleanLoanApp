using Loan.ApplicationCore.Entities;
using Loan.ApplicationCore.Interfaces.Repositories;
using Loan.Infrastructure.DAL.DBContext;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Loan.Infrastructure.DAL.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LoanDbContext dbContext;

        public IRepository<OneLoan> Loans { get; set; }

        public IRepository<Borrower> Borrowers { get; set; }

        public IRepository<LoanAndLoanOfficer> LoansAndLoanOfficers { get; set; }

        public UnitOfWork(IRepository<Borrower> borrowerRepository, IRepository<OneLoan> loanRepository, IRepository<LoanAndLoanOfficer> loanAndLoanOfficerRepository, LoanDbContext dbContext )
        {
            Loans = loanRepository;
            this.dbContext = dbContext;
            Borrowers = borrowerRepository;
            LoansAndLoanOfficers = loanAndLoanOfficerRepository;
        }

        public async Task<int> Complete()
        {
            return await dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}
