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
    public class OneLoanController : ControllerBase
    {
        private readonly IOneLoanService oneLoanService;
        public OneLoanController(IOneLoanService oneLoanService)
        {
            this.oneLoanService = oneLoanService;
        }

        //GET api/loan_v1/oneloan
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllLoans()
        {
            return Ok(await oneLoanService.GetAllLoans());
        }

        //GET api/loan_v1/oneloan/id
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetALoan(string id)
        {
            var loan = await oneLoanService.GetALoan(id);
            if (loan != null)
                return Ok(loan);
            return NotFound();
        }

        ////GET api/loan_v1/oneloan/alldue_{count}
        //[HttpGet]
        //[Route("alldue_{count}")]
        //public async Task<IActionResult> GetAllDueLoan(int count = 15)
        //{
        //    var dueLoans = await oneLoanService.GetAllDueLoans(count);
        //    if (dueLoans.Count() != 0)
        //        return Ok(dueLoans);
        //    return NotFound();
        //}


        //GET api/loan_v1/oneloan/allpendingloansfrom_{id}
        [HttpGet]
        [Route("pendingloansfrom_{id}")]
        public async Task<IActionResult> GetAllPendingLoansFrom(string id)
        {
            var loansPending = await oneLoanService.GetAllPendingLoansFrom(id);
            if (loansPending.Count() != 0)
                return Ok(loansPending);
            return NotFound();
        }

        //GET api/loan_v1/oneloan/alldecidedloansfrom_{id}
        [HttpGet]
        [Route("decidedloansfrom_{id}")]
        public async Task<IActionResult> GetAllDecidedLoansFrom(string id)
        {
            var loansDecided = await oneLoanService.GetAllDecidedLoansFrom(id);
            if (loansDecided.Count() != 0)
                return Ok(loansDecided);
            return NotFound();
        }

        //GET api/loan_v1/oneloan/allloansfrom_{id}
        [HttpGet]
        [Route("allloansfrom_{id}")]
        public async Task<IActionResult> GetAllLoansFrom(string id)
        {
            var loansFrom = await oneLoanService.GetAllLoansFrom(id);
            if (loansFrom.Count() != 0)
                return Ok(loansFrom);
            return NotFound();
        }

        ////GET api/loan_v1/oneloan/{count}_loansunder_{id}
        //[HttpGet]
        //[Route("{count}_loansunder_{id}")]
        //public async Task<IActionResult> GetLoansUnder(string officerId, int count = 15)
        //{
        //    var loansUnder = await oneLoanService.GetAllLoansUnder(officerId, count);
        //    if (loansUnder.Count() != 0)
        //        return Ok(loansUnder);
        //    return NotFound();
        //}

        //GET api/loan_v1/oneloan/{count}_pendingloans
        [HttpGet]
        [Route("{count}_pendingloans")]
        public async Task<IActionResult> GetPendingLoans(int count = 15)
        {
            var loansPending = await oneLoanService.GetAllPendingLoans(count);
            if (loansPending.Count() != 0)
                return Ok(loansPending);
            return NotFound();
        }

        //POST api/loan_v1/oneloan
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateALoan([FromBody] AddLoanDTO entry)
        {
            var loanId = await oneLoanService.AddLoan(entry);
            if (loanId != null)
                return CreatedAtAction("GetALoan", new { id = loanId }, entry);

            return BadRequest();
        }

        // DELETE api/loan_v1/oneloan/id
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteALoan(string id)
        {
            await oneLoanService.DeleteLoan(id);
            return NoContent();
        }

        // UPDATE api/loan_v1/oneloan/id_isupdatedto_status_by_loanofficerid
        [HttpPut]
        [Route("/isupdated")]
        public async Task<IActionResult> UpdateALoan([FromBody] OneLoanDTO model)
        {
            var response = await oneLoanService.UpdateALoan(model);
            //return NoContent();
            return Ok(response);
        }

    }
}
