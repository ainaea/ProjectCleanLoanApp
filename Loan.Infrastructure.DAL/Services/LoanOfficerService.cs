using Loan.ApplicationCore.DTOs;
using Loan.ApplicationCore.Entities;
using Loan.ApplicationCore.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Loan.Infrastructure.DAL.Services
{
    public class LoanOfficerService : ILoanOfficerService
    {
        private readonly UserManager<LoanOfficer> userManager;
        private readonly SignInManager<LoanOfficer> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public LoanOfficerService( UserManager<LoanOfficer> userManager, SignInManager<LoanOfficer> signInManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }
        public async Task<string> AddLoanOfficer(AddLoanOfficerDTO user)
        {
            var officer = new LoanOfficer()
            {
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Email = user.Email,
                UserName = user.Email
                
            };
            var result = await userManager.CreateAsync(officer, user.Password);
            string lOPost = user.OfficerRole.ToLower() == "allowed"? "Top": "Basic";

            if (result.Succeeded)
            {
                if (! await roleManager.RoleExistsAsync(lOPost))
                {
                    await roleManager.CreateAsync( new IdentityRole() { Name = lOPost });
                }
                await userManager.AddToRoleAsync(officer, user.OfficerRole);
            }
            return officer.Id;
            //throw new NotImplementedException();
        }

        public async Task<LoanOfficerDTO> GetLoanOfficer(ClaimsPrincipal principal)
        {
            var officer = await userManager.GetUserAsync(principal);
            return new LoanOfficerDTO()
            {
                Id = officer.Id,
                Firstname = officer.Firstname,
                Lastname = officer.Lastname,
                DateCreated = officer.DateCreated.ToString("D"),
                OfficerRole = officer.OfficerRole
            };
            //throw new NotImplementedException();
        }

        public async Task<string> GetLoanOfficerId(ClaimsPrincipal principal)
        {
            var officer = await GetLoanOfficer(principal);
            return officer.Id;
            //throw new NotImplementedException();
        }

        public async Task<LoanOfficerDTO> GetLoanOfficer(string id)
        {
            var loanOfficer = await userManager.FindByIdAsync(id);
            return Map(loanOfficer);
            //throw new NotImplementedException();
        }
        public IEnumerable<LoanOfficerDTO> GetAllLoanOfficers()
        {
            var allOfficers = userManager.Users;
            return allOfficers.Select( a => Map(a));
        }

        public async Task<bool> LoginLoanOfficer(LoanOfficerLoginDTO model)
        {
            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            if (result.Succeeded)
                return true;
            return false;
        }

        public async Task LogoutLoanOfficer()
        {
            await signInManager.SignOutAsync();
        }

        public async Task<LoanOfficerDTO> LoanOfficerLogin(string email)
        {
            var loanOfficer = await userManager.FindByEmailAsync(email);
            return Map(loanOfficer);
            //throw new NotImplementedException();
        }

        private static LoanOfficerDTO Map(LoanOfficer officer) 
        {
            if (officer == null)
                return new LoanOfficerDTO();

            return new LoanOfficerDTO()
            {
                Id = officer.Id,
                Firstname = officer.Firstname,
                Lastname = officer.Lastname,
                DateCreated = officer.DateCreated.ToString("D"),
                OfficerRole = officer.OfficerRole
            };
        }

        
    }
}
