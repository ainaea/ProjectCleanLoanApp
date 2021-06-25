using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Loan.ApplicationCore.Entities
{
    public class LoanOfficer : IdentityUser
    {
        public DateTimeOffset DateCreated { get; set; } = DateTimeOffset.Now;
        public bool IsDeleted { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        // public ICollection<OneLoan> LoansUnderOfficer { get; set; }
        public ICollection<LoanAndLoanOfficer> LoanAndLoanOfficers { get; set; }
        public string OfficerRole { get; set; } = "Basic";
    }
}
