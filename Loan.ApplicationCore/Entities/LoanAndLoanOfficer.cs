using System;
using System.Collections.Generic;
using System.Text;

namespace Loan.ApplicationCore.Entities
{
    public class LoanAndLoanOfficer : AuditableEntity
    {
        public string OneLoanId { get; set; }
        public OneLoan OneLoan { get; set; }
        public string LoanOfficerId { get; set; }
        public LoanOfficer LoanOfficer { get; set; }
        public string BorrowerId { get; set; }
        public Borrower Borrower { get; set; }
    }
}
