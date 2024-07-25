namespace schoolMvc.PL.ViewModel.User
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
