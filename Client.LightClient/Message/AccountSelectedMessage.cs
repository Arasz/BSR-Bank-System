namespace Client.LightClient.Message
{
    public class AccountSelectedMessage
    {
        public string AccountNumber { get; set; }

        public decimal Balance { get; set; }

        public string Username { get; set; }

        public AccountSelectedMessage()
        {
        }

        public AccountSelectedMessage(string accountNumber, decimal balance, string username)
        {
            AccountNumber = accountNumber;
            Balance = balance;
            Username = username;
        }
    }
}