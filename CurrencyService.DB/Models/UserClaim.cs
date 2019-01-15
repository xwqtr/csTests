using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyService.DB.Models
{
    public class UserClaim : IdentityUserClaim<Guid> 
    {
    }
}
