using Loan.ApplicationCore.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Loan.ApplicationCore.Interfaces.Services
{
    public interface ILoanAndLoanOfficerService
    {
        Task<LoanAndLoanOfficerDTO> GetALoanAndLoanOfficer(string id);
        Task<IEnumerable<LoanAndLoanOfficerDTO>> GetAllLoanAndLoanOfficer(int count = 15);
        Task<IEnumerable<LoanAndLoanOfficerDTO>> GetAllLoanAndLoanOfficer(string loanOfficerId, int count = 15);
        Task DeleteLoanAndLoanOfficer(string borrowerId);
        Task<string> AddLoanAndLoanOfficer(LoanAndLoanOfficerDTO loanAndLoanOfficerToAdd);
        Task<IEnumerable<LoanAndLoanOfficerDTO>> GetAllLoansDecidedBy(string loanOfficerId, int count = 15);
        Task<IEnumerable<LoanAndLoanOfficerDTO>> GetAllDueLoans(int count = 15);
    }
}
