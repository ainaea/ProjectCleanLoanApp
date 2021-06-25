using Loan.ApplicationCore.DTOs;
using Loan.ApplicationCore.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loan.Presentation.API.Controllers
{
    [Route("api/loan_v1/[controller]")]
    [ApiController]
    public class LoanOfficerController : ControllerBase
    {
        private readonly ILoanOfficerService loanOfficerService;

        public LoanOfficerController(ILoanOfficerService loanOfficerService)
        {
            this.loanOfficerService = loanOfficerService;
        }

        [HttpGet]
        [Route("")]
        //[Authorize(Roles = "Basic")]
        public IActionResult GetAllLoanOfficers()
        {
            return Ok( loanOfficerService.GetAllLoanOfficers());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetALoanOfficersAsync(string id)
        {
            //return Ok(new LoanOfficerDTO());
            return Ok(await loanOfficerService.GetLoanOfficer(id));
        }

        [HttpGet]
        [Route("login_{email}")]
        public async Task<IActionResult> LoanOfficersLoginAsync(string email)
        {
            //return Ok(new LoanOfficerDTO());
            return Ok(await loanOfficerService.LoanOfficerLogin(email));
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateLoanOfficers([FromBody] AddLoanOfficerDTO cadet)
        {
            var result = await loanOfficerService.AddLoanOfficer(cadet);
            if (result != null)
                return CreatedAtAction(nameof(GetALoanOfficersAsync), new { id = result }, cadet);
            return BadRequest();
        }



    }
}
