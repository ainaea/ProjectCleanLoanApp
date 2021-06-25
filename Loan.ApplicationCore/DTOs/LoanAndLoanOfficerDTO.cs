using System;
using System.Collections.Generic;
using System.Text;

namespace Loan.ApplicationCore.DTOs
{
    public class LoanAndLoanOfficerDTO
    {
        public string LoanId { get; set; }
        public string Amount { get; set; }
        public string DateCreated { get; set; }
        public string LoanOfficerName { get; set; }
        public string LoanOfficerId { get; set; }
        public string BorrowerId { get; set; }
        public string BorrowerName { get; set; }
        public string Status { get; set; }
        public string Duration { get; set; }
    }
}
