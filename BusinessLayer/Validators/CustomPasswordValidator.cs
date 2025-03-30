using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccessLayer.Models;

public class CustomPasswordValidator : IPasswordValidator<ApplicationUser>
{
    public async Task<IdentityResult> ValidateAsync(UserManager<ApplicationUser> manager, ApplicationUser user, string password)
    {
        var errors = new List<IdentityError>();

        if (password.Contains("123"))
        {
            errors.Add(new IdentityError
            {
                Code = "WeakPassword",
                Description = "Password cannot contain the pattern like 123."
            });
        }

        if (password.Length < 10) 
        {
            errors.Add(new IdentityError
            {
                Code = "ShortPassword",
                Description = "Password should be at least 10 characters."
            });
        }

        return errors.Count == 0 ? IdentityResult.Success : IdentityResult.Failed(errors.ToArray());
    }
}
