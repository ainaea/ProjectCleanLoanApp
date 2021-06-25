using System;
using System.Collections.Generic;
using System.Text;

namespace Loan.ApplicationCore.DTOs
{
    public class OneLoanDTO
    {
        public string Id { get; set; }
        public decimal Amount { get; set; }
        public string Borrower { get; set; }
        public string BorrowerId { get; set; }
        public string Duration { get; set; }
        public string LoanOfficer { get; set; }
        public string LoanOfficerId { get; set; }
        public string Status { get; set; }
    }
}
