using Loan.ApplicationCore.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Loan.ApplicationCore.Interfaces.Services
{
    public interface IOneLoanService
    {
        Task<OneLoanDTO> GetALoan(string id);
        Task<IEnumerable<OneLoanDTO>> GetAllLoans(int count = 15);
        Task<IEnumerable<OneLoanDTO>> GetAllPendingLoans( int count = 15);
        Task<IEnumerable<OneLoanDTO>> GetAllPendingLoansFrom(string id);
        Task<IEnumerable<OneLoanDTO>> GetAllLoansFrom(string id);
        Task<IEnumerable<OneLoanDTO>> GetAllDecidedLoansFrom(string id);
        //Task<IEnumerable<OneLoanDTO>> GetAllDueLoans(int count = 15);
        Task DeleteLoan(string loanId);
        Task<string> AddLoan(AddLoanDTO loanToAdd);
        public Task<OneLoanDTO> UpdateALoan(OneLoanDTO toUpdate);
    }
}
