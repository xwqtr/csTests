using CurrencyService.DB.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CurrencyService.WebApi.Controllers
{
    [Authorize]
    [Route("[controller]/[Action]")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        //private readonly IEmailSender _emailSender;
        private readonly ILogger<AccountController> _logger;
        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            //IEmailSender emailSender,
            ILoggerFactory loggerFactory)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            //_emailSender = emailSender;
            _logger = loggerFactory.CreateLogger<AccountController>();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> LogIn(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpGet("LogOut")]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public async Task<IActionResult> LogOut()
        {
            await AuthenticationHttpContextExtensions.SignOutAsync(HttpContext);
            return new ViewResult();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult ExternalLogin(string provider, string returnUrl = null)
        {
            // Request a redirect to the external login provider.
            string redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Account", new { returnUrl });
            AuthenticationProperties properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            if (remoteError != null)
            {
                return RedirectToAction(nameof(LogIn));
            }
            ExternalLoginInfo info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction(nameof(LogIn));
            }

            // Sign in the user with this external login provider if the user already has a login.
            var result = await TryExternalLoginSignInAsync(info);
            if (result.Succeeded)
            {
                _logger.LogInformation("User logged in with {Name} provider.", info.LoginProvider);
                return RedirectToLocal(returnUrl);
            }
            else
            {
                var email = info.Principal.Claims.ToList()[5].Value;
                var dbUser = await _userManager.FindByEmailAsync(email);
                if (dbUser == null)
                {
                    var user = new ApplicationUser() { Email = email, UserName = info.Principal.Identity.Name.Replace(" ", string.Empty), LockoutEnabled = false };

                    var createResult = await _userManager.CreateAsync(user, "P@ssw0rd");
                    var login = await _userManager.AddLoginAsync(user, info);
                    if (createResult.Succeeded)
                    {
                        await TryExternalLoginSignInAsync(info);
                        _logger.LogInformation("User logged in with {Name} provider.", info.LoginProvider);
                        return RedirectToLocal(returnUrl);
                    }
                    else
                    {
                        throw new InvalidOperationException($"Cannot create user:{user.Email} because {string.Join(';', createResult.Errors)}");
                    }
                }
                else
                {
                    throw new InvalidOperationException($"Cannot Sign In");

                }

            }
            
            
        }

        private async Task<Microsoft.AspNetCore.Identity.SignInResult> TryExternalLoginSignInAsync(ExternalLoginInfo info)
        {
            return  await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
        }
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {

                return RedirectToAction(nameof(CurrenciesController.Get), "Currencies");
            }
        }
    }
}