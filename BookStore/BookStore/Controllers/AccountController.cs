using BookStore.Models;
using BookStore.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{

    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [Route("signup")]
        public IActionResult Signup()
        {
            return View();
        }

        [Route("signup")]
        [HttpPost]
        public async Task<IActionResult> Signup(SignUpUserModel userModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountRepository.CreateUserAsync(userModel);
                if (!result.Succeeded)
                {
                    foreach (var errorMessage in result.Errors)
                    {
                        ModelState.AddModelError("", errorMessage.Description);
                    }
                    return View(userModel);
                }
                ModelState.Clear();
                return RedirectToAction("ConfirmEmail", new {email = userModel.Email});
            }
            return View();
        }
        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login(SignInModel userModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountRepository.UserSignInAsync(userModel);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return LocalRedirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Invalid Credentials");
            }
            return View(userModel);

        }

        public async Task<IActionResult> Logout()
        {
            await _accountRepository.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [Route("change-password")]
        public  IActionResult ChangePassword() 
        {
        
            return View(); 
        }


        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel passwordModel) 
        {
            if (ModelState.IsValid)
            {
                var result = await _accountRepository.ChangePasswordAsync(passwordModel);
                if (result.Succeeded)
                {
                    ViewBag.isSuccess = true;
                    ModelState.Clear();
                    return View();
                }

                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
        
            return View(passwordModel); 
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string uid, string token, string email)
        {
            EmailConfirmModel model = new EmailConfirmModel
            {
                Email = email
            };
            if (!string.IsNullOrEmpty(uid) && !string.IsNullOrEmpty(token))
            {
              
            token = token.Replace(' ', '+');
              var result =  await _accountRepository.ConfirmEmailAsync(uid, token);
                if (result.Succeeded)
                {
                    model.EmailVerified = true;
                }

            }

            return View(model);
        }
        
        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(EmailConfirmModel emailConfirmModel)
        {
            var user = await _accountRepository.GetUserByEmailAsync(emailConfirmModel.Email);
            if(user != null)
            {
                if (user.EmailConfirmed)
                {
                    emailConfirmModel.EmailVerified = true;
                    return View(emailConfirmModel);
                }
                await _accountRepository.GenerateEmailConfirmationTokenAsync(user);
                emailConfirmModel.EmailSent = true;
                ModelState.Clear();

            }
            else
            {
                ModelState.AddModelError("", "Something went wrong!");
            }
            return View(emailConfirmModel);
        }

        [AllowAnonymous, HttpGet("forgot-password")]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [AllowAnonymous, HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel forgotPasswordModel)
        {
            if (ModelState.IsValid)
            {
                //logic goes here
                var user = await _accountRepository.GetUserByEmailAsync(forgotPasswordModel.Email);
                if(user != null)
                {
                   await _accountRepository.GenerateForgotPasswordTokenAsync(user);
                }
                ModelState.Clear();
                forgotPasswordModel.EmailSent = true;
            }

            return View(forgotPasswordModel);
        }

        [AllowAnonymous, HttpGet("reset-password")]
        public IActionResult ResetPassword(string uid, string token)
        {
            ResetPassword resetPassword = new ResetPassword
            {
                UserId = uid,
                Token = token
            };
            return View(resetPassword);
        }
        
        [AllowAnonymous, HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPassword resetPassword)
        {
            if (ModelState.IsValid)
            {
                resetPassword.Token = resetPassword.Token.Replace(' ', '+');
                var result = await _accountRepository.ResetPasswordAsync(resetPassword);
                if (result.Succeeded)
                {
                    ModelState.Clear();
                    resetPassword.IsSuccess = true;
                    return View(resetPassword);
                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(resetPassword);
        }
    }
}
