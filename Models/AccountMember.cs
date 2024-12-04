using WebBankingApp.Models;

namespace MVCWebBanking.Models
{
    public class AccountMember
    {
        public int AccountMemberId { get; set; }

        public int AccountId { get; set; }
        public Account Account { get; set; }

        public int MemberId { get; set; }
        public Member Member { get; set; }

    }
}
