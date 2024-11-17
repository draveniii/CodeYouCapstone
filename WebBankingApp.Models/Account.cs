namespace WebBankingApp.Models
{
    public class Account
    {
        public int Id { get; set; }
        public List<Member> Members { get; set; } = new List<Member>();
        public List<Share> Shares { get; set; } = new List<Share>();
        public List <Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
