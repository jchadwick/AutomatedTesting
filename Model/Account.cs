namespace AutomatedTesting
{
    public class Account : IEntity
    {
        public long Id { get; set; }
        public long ClientId { get; set; }
        public string Name { get; set; }
        public AccountType Type { get; set; }
        public decimal Balance { get; set; }

        public virtual Client Client { get; set; }
    }
}
