using System;
using System.Collections.Generic;
using System.Text;

namespace Loan.ApplicationCore.DTOs
{
    public class AddLoanOfficerDTO : LoanOfficerDTO
    {
        public string Password { get; set; }
    }
}
