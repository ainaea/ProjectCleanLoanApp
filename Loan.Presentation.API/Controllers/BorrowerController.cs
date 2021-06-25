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
    public class BorrowerController : ControllerBase
    {
        private readonly IBorrowerService borrowerService;

        public BorrowerController(IBorrowerService borrowerService)
        {
            this.borrowerService = borrowerService;
        }

        //GET api/loan_v1/borrower
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetBorrower()
        {
            return Ok(await borrowerService.GetAllBorrowers());
        }

        //GET api/loan_v1/borrower/id
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetABorrower(string id)
        {
            var borrower = await borrowerService.GetABorrower(id);
            if (borrower != null)
                return Ok(borrower);
            return NotFound();
        }

        //GET api/loan_v1/borrower/login_email
        [HttpGet]
        [Route("login_{email}")]
        public async Task<IActionResult> LoginBorrower(string email)
        {
            var borrower = await borrowerService.LoginABorrower(email);
            if (borrower != null)
                return Ok(borrower);
            return NotFound();
        }

        //POST api/loan_v1/borrower
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateABorrower([FromBody] BorrowerDTO entry)
        {
            var borrowerId = await borrowerService.AddBorrower(entry);
            if (borrowerId != null)
                return CreatedAtAction("GetABorrower", new { id = borrowerId}, entry);

            return BadRequest();
        }

        // DELETE api/loan_v1/borrower/id
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteABorrower(string id)
        {
            await borrowerService.DeleteBorrower(id);
            return NoContent();
        }

    }
}
