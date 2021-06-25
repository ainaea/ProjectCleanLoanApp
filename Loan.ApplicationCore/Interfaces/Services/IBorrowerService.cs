using Loan.ApplicationCore.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Loan.ApplicationCore.Interfaces.Services
{
    public interface IBorrowerService
    {
        Task<BorrowerDTO> GetABorrower(string id);
        Task<IEnumerable<BorrowerDTO>> GetAllBorrowers(int count = 15);
        Task<IEnumerable<BorrowerDTO>> GetAllBorrowersUnder(string loanOfficerId, int count = 15);
        Task DeleteBorrower(string borrowerId);
        Task<string> AddBorrower(BorrowerDTO borrowerToAdd);
        Task<BorrowerDTO> LoginABorrower(string email);
    }
}
