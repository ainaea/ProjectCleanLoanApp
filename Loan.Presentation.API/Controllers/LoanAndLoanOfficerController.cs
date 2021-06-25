using Loan.ApplicationCore.DTOs;
using Loan.ApplicationCore.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loan.Presentation.API.Controllers
{
    [Route("api/loan_v1/[controller]/")]
    [ApiController]
    public class LoanAndLoanOfficerController : ControllerBase
    {
        private readonly ILoanAndLoanOfficerService loanAndLoanOfficerService;

        public LoanAndLoanOfficerController(ILoanAndLoanOfficerService loanAndLoanOfficerService)
        {
            this.loanAndLoanOfficerService = loanAndLoanOfficerService;
        }
        //GET api/loan_v1/loanAndLoanOfficer
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetLoanAndLoanOfficer()
        {
            return Ok(await loanAndLoanOfficerService.GetAllLoanAndLoanOfficer());
        }

        //GET api/loan_v1/loanAndLoanOfficer/id
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetALoanAndLoanOfficer(string id)
        {
            var loanAndLoanOfficer = await loanAndLoanOfficerService.GetALoanAndLoanOfficer(id);
            if (loanAndLoanOfficer != null)
                return Ok(loanAndLoanOfficer);
            return NotFound();
        }

        //GET api/loan_v1/loanAndLoanOfficer/id
        [HttpGet]
        [Route("under_{id}")]
        public async Task<IActionResult> GetAllLoanAndLoanOfficerUnder(string id)
        {
            var loanAndLoanOfficer = await loanAndLoanOfficerService.GetAllLoanAndLoanOfficer(id);
            if (loanAndLoanOfficer != null)
                return Ok(loanAndLoanOfficer);
            return NotFound();
        }

        //POST api/loan_v1/loanAndLoanOfficer
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateALoanAndLoanOfficer([FromBody] LoanAndLoanOfficerDTO entry)
        {
            var loanAndLoanOfficerId = await loanAndLoanOfficerService.AddLoanAndLoanOfficer(entry);
            if (loanAndLoanOfficerId != null)
                return CreatedAtAction("GetABorrower", new { id = loanAndLoanOfficerId }, entry);

            return BadRequest();
        }



        //GET api/loan_v1/loanAndLoanOfficer/alldue_{count}
        [HttpGet]
        [Route("alldue_{count}")]
        public async Task<IActionResult> GetAllDueLoan(int count = 15)
        {
            var dueLoans = await loanAndLoanOfficerService.GetAllDueLoans(count);
            if (dueLoans.Count() != 0)
                return Ok(dueLoans);
            return NotFound();
        }

        //GET api/loan_v1/loanAndLoanOfficer/{count}_loansunder_{officerId}
        [HttpGet]
        [Route("{count}_loansunder_{officerId}")]
        public async Task<IActionResult> GetLoansUnder(string officerId, int count = 15)
        {
            var loansUnder = await loanAndLoanOfficerService.GetAllLoansDecidedBy(officerId, count);
            if (loansUnder.Count() != 0)
                return Ok(loansUnder);
            return NotFound();
        }


        // DELETE api/loan_v1/borrower/id
        [HttpDelete]
        [Route("delete_{id}")]
        public async Task<IActionResult> DeleteALoanAndLoanOfficer(string id)
        {
            await loanAndLoanOfficerService.DeleteLoanAndLoanOfficer(id);
            return NoContent();
        }
    }
}
