namespace UserApi.Contract.Entities
{
    public class UserViewModel
    {
        public string SurName { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public override string ToString() => $"{SurName} {Name} {MiddleName} {PhoneNumber} {Email}";
    }
}
