using Loan.ApplicationCore.DTOs;
using Loan.ApplicationCore.Entities;
using Loan.ApplicationCore.Interfaces.Repositories;
using Loan.ApplicationCore.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan.ApplicationCore.Services
{
    public class OneLoanService : IOneLoanService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<LoanOfficer> userManager;
        private readonly string[] includes = new string[] { "Borrower" };

        public OneLoanService(IUnitOfWork unitOfWork, UserManager<LoanOfficer> userManager)
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
        }
        public async Task<string> AddLoan(AddLoanDTO loanToAdd)
        {
            var borrower = await unitOfWork.Borrowers.Find(loanToAdd.BorrowerId);
            if (borrower != null)
            {
                var loan = new OneLoan
                {
                    Amount = loanToAdd.Amount,
                    Duration = loanToAdd.Duration,
                    Borrower = borrower
                };
                await unitOfWork.Loans.Add(loan);
                await unitOfWork.Complete();

                return loan.Id;
            }
            return "Error";
            //throw new NotImplementedException();
        }

        public async Task DeleteLoan(string loanId)
        {
            var loan = await unitOfWork.Loans.Find(loanId);
            unitOfWork.Loans.Remove(loan);

            await unitOfWork.Complete();
        }

        //public async Task<IEnumerable<OneLoanDTO>> GetAllDueLoans(int count = 15)
        //{
        //    var now = DateTimeOffset.Now;
        //    var allLoans = await unitOfWork.Loans.GetAll( l => l.DueDate < now, includes); 
        //    return allLoans.Select(a => Map(a)).ToList();
        //}

        public async Task<IEnumerable<OneLoanDTO>> GetAllLoans(int count = 15)
        {
            //string[] includes = new string[] { "Borrower", "LoanOfficer"};
            var allLoans = await unitOfWork.Loans.GetAll(includes);
            if (allLoans != null)
                return allLoans.Select(a => Map(a)).ToList();
            return new List<OneLoanDTO>();
        }

        //Task<IEnumerable<OneLoanDTO>> IOneLoanService.GetAllLoansUnder(string loanOfficerId, int count)
        //{
        //    throw new NotImplementedException();
        //}
        //public async Task<IEnumerable<OneLoanDTO>> GetAllLoansUnder(string loanOfficerId, int count = 15)
        //{
        //    var allLoans = await unitOfWork.Loans.GetAll(l => l.LoanOfficer.Id == loanOfficerId, includes);
        //    return allLoans.Select(a => Map(a)).ToList();
        //    throw new NotImplementedException();
        //}

        public async Task<IEnumerable<OneLoanDTO>> GetAllPendingLoans(int count = 15)
        {
            var allLoans = await unitOfWork.Loans.GetAll(l => l.Status == "Pending", includes);
            if (allLoans != null)
                return allLoans.Select(a => Map(a)).ToList();
            return new List<OneLoanDTO>();
        }

        public async Task<OneLoanDTO> GetALoan(string id)
        {
            return Map(await unitOfWork.Loans.Get( a => a.Id == id, includes));
        }

        public async Task<OneLoanDTO> UpdateALoan(OneLoanDTO toUpdate)
        {
            var loanOfInterest = await unitOfWork.Loans.Get(a => a.Id == toUpdate.Id, includes);
            var loanOfficer = await userManager.FindByIdAsync(toUpdate.LoanOfficerId);
            //var loanOfficer1 = await unitOfWork.LoansAndLoanOfficers.Get(o => o.LoanOfficerId == toUpdate.LoanOfficerId);
            if (loanOfInterest != null && loanOfficer != null)
            {
                //A corresponding  loanAndLoanOfficer object should be created and store in the database while making modification
                if (loanOfInterest.Status.ToLower() == "pending")
                {
                    var loanAndLoanOfficer = new LoanAndLoanOfficer
                    {
                        OneLoanId = loanOfInterest.Id,
                        BorrowerId = loanOfInterest.Borrower.Id,
                        LoanOfficerId = loanOfficer.Id
                    };
                    await unitOfWork.LoansAndLoanOfficers.Add(loanAndLoanOfficer);
                    await unitOfWork.Complete();
                }
                //ends
                loanOfInterest.Status = toUpdate.Status;

                unitOfWork.Loans.Update(loanOfInterest);
                await unitOfWork.Complete();
                return await GetALoan(toUpdate.Id);
            }
            return new OneLoanDTO
            {
                LoanOfficerId = (loanOfInterest != null).ToString() + (loanOfficer != null),
                Status = "Error occured"
            };
            //return Map(await unitOfWork.Loans.Get(a => a.Id == id, includes));
        }

        private static OneLoanDTO Map(OneLoan loan)
        {
            if (loan != null)
                return new OneLoanDTO
                {
                    Id = loan.Id,
                    Amount = loan.Amount,
                    Borrower = $"{loan.Borrower.Firstname} {loan.Borrower.Lastname}",
                    BorrowerId = loan.Borrower.Id,
                    Duration = $"{loan.Duration}",
                    //LoanOfficer = $"{loan.LoanOfficer.Firstname} {loan.LoanOfficer.Lastname}",
                    //LoanOfficerId = loan.LoanOfficer.Id,
                    Status = loan.Status
                };
            return new OneLoanDTO();
        }

        public async Task<IEnumerable<OneLoanDTO>> GetAllPendingLoansFrom(string id)
        {
            var allPendingLoans = await unitOfWork.Loans.GetAll(l => (l.Status == "Pending" && l.Borrower.Id == id), includes);
            if (allPendingLoans != null)
                return allPendingLoans.Select(a => Map(a)).ToList();
            return new List<OneLoanDTO>();
        }

        public async Task<IEnumerable<OneLoanDTO>> GetAllLoansFrom(string id)
        {
            var allLoansFrom = await unitOfWork.Loans.GetAll(l => l.Borrower.Id == id, includes);
            if (allLoansFrom != null)
                return allLoansFrom.Select(a => Map(a)).ToList();
            return new List<OneLoanDTO>();
        }

        public async Task<IEnumerable<OneLoanDTO>> GetAllDecidedLoansFrom(string id)
        {
            var allDecidedLoans = await unitOfWork.Loans.GetAll(l => (l.Status != "Pending" && l.Borrower.Id == id), includes);
            if (allDecidedLoans != null)
                return allDecidedLoans.Select(a => Map(a)).ToList();
            return new List<OneLoanDTO>();
        }
    }
}
