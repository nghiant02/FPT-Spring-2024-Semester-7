using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;

namespace Eyeglasses_StudentName.Pages
{
    public class LoginModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;

        public LoginModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public string EmailAddress { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        public IActionResult OnPost()
        {
            var account = _unitOfWork.StoreAccountRepository.Get(filter: a => a.EmailAddress == EmailAddress && a.AccountPassword == Password).FirstOrDefault();

            if (account != null && (account.Role == 1 || account.Role == 2))
            {
                HttpContext.Session.SetString("UserId", account.AccountId.ToString());
                HttpContext.Session.SetString("Role", account.Role.ToString());
                return RedirectToPage("/EyeglassesList");
            }
            else
            {
                ErrorMessage = "You do not have permission to do this function or your credentials are wrong.";
                return Page();
            }
        }
    }
}