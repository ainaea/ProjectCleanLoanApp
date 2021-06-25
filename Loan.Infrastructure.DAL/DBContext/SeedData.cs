using Loan.ApplicationCore.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Loan.Infrastructure.DAL.DBContext
{
    public static class SeedData
    {
        public async static Task AddLoanOfficer(UserManager<LoanOfficer> officerManager, RoleManager<IdentityRole> roleManager)
        {
            if(await officerManager.FindByEmailAsync("ebenezer.a.aina@gmail.com") == null)
            {
                var loanOfficer = new LoanOfficer
                {
                    Firstname = "Ebenezer",
                    Lastname = "Aina",
                    Email = "ebenezer.a.aina@gmail.com",
                    UserName = "ebenezer.a.aina@gmail.com"
                };
                var result = await officerManager.CreateAsync(loanOfficer, "Pass@1234");

                if (result.Succeeded)
                {
                    await CreateOfficerRole(roleManager);
                    await officerManager.AddToRoleAsync(loanOfficer, "Top");
                }
            }
        }

        private static async Task CreateOfficerRole(RoleManager<IdentityRole> roleManager)
        {
            if(!await roleManager.RoleExistsAsync("Top"))
            {
                await roleManager.CreateAsync(new IdentityRole { Name = "Top" });
                await roleManager.CreateAsync(new IdentityRole { Name = "Middle" });
                await roleManager.CreateAsync(new IdentityRole { Name = "Basic" });
            }
        }
    }
}
