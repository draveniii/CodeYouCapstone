namespace WebBankingApp.Models
{
    public class Member
    {
        public Int32 Id { get; set; }
        public String Name { get; set; }
        public List<AccountMember> Accounts { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public Int32 SSNumber { get; set; }

        // Default Constructor
        public Member() { }

        // Parameterized Constructor
        public Member(int id, string name, DateOnly dateOfBirth, int sSNumber)
        {
            Id = id;
            Name = name;
            DateOfBirth = dateOfBirth;
            SSNumber = sSNumber;
        }
    }
}
