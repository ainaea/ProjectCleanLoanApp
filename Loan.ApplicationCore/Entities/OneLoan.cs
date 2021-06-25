using System;
using System.Collections.Generic;
using System.Text;

namespace Loan.ApplicationCore.Entities
{
    public class OneLoan : AuditableEntity
    {
        public decimal Amount { get; set; }
        public Borrower Borrower { get; set; }
        public decimal InterestRate { get; set; }
        public int Duration { get; set; }
        //public string LoanOfficerId { get; set; }
        //public LoanOfficer LoanOfficer { get; set; }
        public ICollection<LoanAndLoanOfficer> LoanAndLoanOfficers { get; set; }
        public string Status { get; set; } = "Pending";
        public DateTimeOffset DueDate { get; set; }

    }
}
