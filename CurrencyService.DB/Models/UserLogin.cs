using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CurrencyService.DB.Models
{
    public class UserLogin :  IdentityUserLogin<Guid>
    {
    }
}
