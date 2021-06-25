using Loan.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Loan.ApplicationCore.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        public IRepository<OneLoan> Loans { get;}
        public IRepository<Borrower> Borrowers { get; }
        public IRepository<LoanAndLoanOfficer> LoansAndLoanOfficers { get; }
        Task<int> Complete();
    }
}
