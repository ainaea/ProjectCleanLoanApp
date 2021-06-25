using Loan.ApplicationCore.DTOs;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Loan.ApplicationCore.Interfaces.Services
{
    public interface ILoanOfficerService
    {
        Task<LoanOfficerDTO> GetLoanOfficer(ClaimsPrincipal principal);
        Task<LoanOfficerDTO> GetLoanOfficer(string id);
        Task<LoanOfficerDTO> LoanOfficerLogin(string email);
        Task<string> GetLoanOfficerId(ClaimsPrincipal principal);
        Task<string> AddLoanOfficer(AddLoanOfficerDTO user);
        IEnumerable<LoanOfficerDTO> GetAllLoanOfficers();
        Task<bool> LoginLoanOfficer(LoanOfficerLoginDTO model);
        Task LogoutLoanOfficer();
    }
}
