namespace Ticket.ViewModel.Account
{
    public class LoginMain
    {
        public LoginMain(string account, string password)
        {
            Account = account;
            Password = password;
        }

        public string Account { get; set; }
        public string Password { get; set; }
    }
}
