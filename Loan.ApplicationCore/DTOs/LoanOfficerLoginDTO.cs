using System;
using System.Collections.Generic;
using System.Text;

namespace Loan.ApplicationCore.DTOs
{
    public class LoanOfficerLoginDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
