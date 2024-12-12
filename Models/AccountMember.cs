namespace WebBankingApp.Models
{
    public class AccountMember
    {
        public Int32 AccountMemberId { get; set; }

        public Int32 AccountId { get; set; }
        public Account Account { get; set; }

        public Int32 MemberId { get; set; }
        public Member Member { get; set; }

    }
}
