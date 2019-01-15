using Microsoft.AspNetCore.Identity;
using System;

namespace CurrencyService.DB.Models
{
    public class UserToken : IdentityUserToken<Guid>
    { 
    }
}
