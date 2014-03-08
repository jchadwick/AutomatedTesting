namespace AutomatedTesting.WebServices.Contracts
{
    public class Account
    {
        public long Id { get; set; }
        public long ClientId { get; set; }
        public string Name { get; set; }
        public AccountType Type { get; set; }
        public decimal Balance { get; set; }
    }
}