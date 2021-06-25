using System;
using System.Collections.Generic;
using System.Text;

namespace Loan.ApplicationCore.DTOs
{
    public class LoanOfficerDTO
    {
        public string Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string OfficerRole { get; set; }
        public string DateCreated { get; set; }

    }
}
