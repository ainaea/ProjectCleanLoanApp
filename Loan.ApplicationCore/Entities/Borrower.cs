using System;
using System.Collections.Generic;
using System.Text;

namespace Loan.ApplicationCore.Entities
{
    public class Borrower : AuditableEntity
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string BVN { get; set; }
        public ICollection<OneLoan> Loans { get; set; }
        public ICollection<LoanAndLoanOfficer> LoanAndLoanOfficers { get; set; }
    }
}
