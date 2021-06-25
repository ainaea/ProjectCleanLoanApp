using System;
using System.Collections.Generic;
using System.Text;

namespace Loan.ApplicationCore.DTOs
{
    public class AddLoanDTO
    {
        public decimal Amount { get; set; }
        public string BorrowerId { get; set; }
        public int Duration { get; set; }
    }
}
