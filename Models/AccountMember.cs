namespace WebBankingApp.Models
{
    public class AccountMember
    {
        public Int32 AccountMemberId { get; set; }

        public Int32 AccountId { get; set; }
        public Account Account { get; set; }

        public Int32 MemberId { get; set; }
        public Member Member { get; set; }

        public AccountMember() { }

        public AccountMember(int accountMemberId, int accountId, Account account, int memberId, Member member)
        {
            AccountMemberId = accountMemberId;
            AccountId = accountId;
            Account = null;
            MemberId = memberId;
            Member = null;
        }       
    }

    

}
