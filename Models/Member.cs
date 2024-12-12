namespace WebBankingApp.Models
{
    public class Member
    {
        public Int32 Id { get; set; }
        public String Name { get; set; }
        public List<AccountMember> Accounts { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public Int32 SSNumber { get; set; }
    }
}
