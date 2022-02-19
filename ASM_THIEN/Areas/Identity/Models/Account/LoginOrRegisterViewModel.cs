using App.Areas.Identity.Models.AccountViewModels;

namespace ASM_THIEN.Areas.Identity.Models.Account
{
    public class LoginOrRegisterViewModel
    {
        public LoginViewModel Login { get; set; }
        public RegisterViewModel Register { get; set; }
    }
}
