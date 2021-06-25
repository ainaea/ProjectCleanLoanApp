using Loan.ApplicationCore.DTOs;
using Loan.ApplicationCore.Entities;
using Loan.ApplicationCore.Interfaces.Repositories;
using Loan.ApplicationCore.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan.ApplicationCore.Services
{
    public class LoanAndLoanOfficerService : ILoanAndLoanOfficerService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly string[] includes = new string[] {"LoanOfficer", "OneLoan", "Borrower"};

        public LoanAndLoanOfficerService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<string> AddLoanAndLoanOfficer(LoanAndLoanOfficerDTO loanAndLoanOfficerToAdd)
        {
            var loanAndLoanOfficer = new LoanAndLoanOfficer
            {
                LoanOfficerId = loanAndLoanOfficerToAdd.LoanOfficerId,
                OneLoanId = loanAndLoanOfficerToAdd.LoanId,
                BorrowerId = loanAndLoanOfficerToAdd.BorrowerId
            };
            await unitOfWork.LoansAndLoanOfficers.Add(loanAndLoanOfficer);
            await unitOfWork.Complete();

            return loanAndLoanOfficer.Id;
            //throw new NotImplementedException();
        }

        public async Task DeleteLoanAndLoanOfficer(string borrowerId)
        {
            var loanAndLoanOfficerToAdd = await unitOfWork.LoansAndLoanOfficers.Find(borrowerId);
            unitOfWork.LoansAndLoanOfficers.Remove(loanAndLoanOfficerToAdd);

            await unitOfWork.Complete();
            //throw new NotImplementedException();
        }

        public async Task<IEnumerable<LoanAndLoanOfficerDTO>> GetAllLoanAndLoanOfficer(int count = 15)
        {
            var loanAndLoanOfficers = await unitOfWork.LoansAndLoanOfficers.GetAll(includes, count);
            return loanAndLoanOfficers.Select( l => Map(l) ).ToList();
            //throw new NotImplementedException();
        }

        public async Task<IEnumerable<LoanAndLoanOfficerDTO>> GetAllLoanAndLoanOfficer(string loanOfficerId, int count = 15)
        {
            var loanAndLoanOfficers = await unitOfWork.LoansAndLoanOfficers.GetAll( a => a.LoanOfficerId == loanOfficerId, includes);
            return loanAndLoanOfficers.Select(l => Map(l)).ToList();
            //throw new NotImplementedException();
        }

        public async Task<LoanAndLoanOfficerDTO> GetALoanAndLoanOfficer(string id)
        {
            return Map(await unitOfWork.LoansAndLoanOfficers.Get(a => a.Id == id, includes));
            //throw new NotImplementedException();
        }

        public async Task<IEnumerable<LoanAndLoanOfficerDTO>> GetAllLoansDecidedBy(string loanOfficerId, int count = 15)
        {
            var allLoans = await unitOfWork.LoansAndLoanOfficers.GetAll( l => l.LoanOfficer.Id == loanOfficerId, includes );
            return allLoans.Select(a => Map(a)).ToList();
            //throw new NotImplementedException();
        }

        public Task<IEnumerable<LoanAndLoanOfficerDTO>> GetAllDueLoans(int count = 15)
        {

            throw new NotImplementedException();
        }

        public static LoanAndLoanOfficerDTO Map(LoanAndLoanOfficer entry)
        {
            return new LoanAndLoanOfficerDTO
            {
                LoanId = entry.Id,
                Amount = entry.OneLoan.Amount.ToString(),
                DateCreated = entry.OneLoan.DateCreated.ToString(),
                LoanOfficerName = entry.LoanOfficer.Firstname + " " + entry.LoanOfficer.Lastname,
                BorrowerName = entry.OneLoan.Borrower.Firstname + " " + entry.OneLoan.Borrower.Lastname,
                Status = entry.OneLoan.Status,
                Duration = entry.OneLoan.Duration.ToString()
            };
        }

        
    }
}
