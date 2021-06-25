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
    public class BorrowerService : IBorrowerService
    {
        private readonly IUnitOfWork unitOfWork;

        public BorrowerService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<string> AddBorrower(BorrowerDTO borrowerToAdd)
        {
            var borrower = new Borrower()
            {
                Firstname = borrowerToAdd.Firstname,
                Lastname = borrowerToAdd.Lastname,
                Email = borrowerToAdd.Email,
                BVN = borrowerToAdd.BVN,
                Telephone = borrowerToAdd.Telephone
            };
            await unitOfWork.Borrowers.Add(borrower);
            await unitOfWork.Complete();

            return borrower.Id;
            //throw new NotImplementedException();
        }

        public async Task DeleteBorrower(string borrowerId)
        {
            var borrower = await unitOfWork.Borrowers.Find(borrowerId);
            if (borrower != null)
            {
                unitOfWork.Borrowers.Remove(borrower);
                await unitOfWork.Complete();
            }            
        }

        public async Task<BorrowerDTO> GetABorrower(string id)
        {
            var borrower = await unitOfWork.Borrowers.Find(id);
            if (borrower != null)
                return Map(borrower);
            return new BorrowerDTO();
            //throw new NotImplementedException();
        }

        public async Task<IEnumerable<BorrowerDTO>> GetAllBorrowers(int count = 15)
        {
            var allBorrowers = await unitOfWork.Borrowers.GetAll( a => a.IsDeleted == false);
            return allBorrowers.Select(a => Map(a));
            //throw new NotImplementedException();
        }

        public Task<IEnumerable<BorrowerDTO>> GetAllBorrowersUnder(string loanOfficerId, int count = 15)
        {
            //var allBorrowers = await unitOfWork.Borrowers.GetAll(a => (a.Loans.LastOrDefault().LoanOfficer.Id == loanOfficerId) & a.IsDeleted == false);
            //return allBorrowers.Select(a => Map(a));
            throw new NotImplementedException();
        }

        public async Task<BorrowerDTO> LoginABorrower(string email)
        {
            return Map( await unitOfWork.Borrowers.Get(a => a.Email == email));
            //throw new NotImplementedException();
        }

        private static BorrowerDTO Map(Borrower borrower)
        {
            if (borrower != null)
                return new BorrowerDTO
                {
                Id = borrower.Id,
                Firstname = borrower.Firstname,
                Lastname = borrower.Lastname,
                Email = borrower.Email,
                BVN = borrower.BVN,
                Telephone = borrower.Telephone
                };
            return new BorrowerDTO();
        }

        
    }
}
