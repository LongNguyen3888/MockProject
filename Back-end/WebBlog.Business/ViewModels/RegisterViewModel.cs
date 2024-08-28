namespace WebBlog.Business
{
    public class RegisterViewModel
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public string FirstName { get; set; }
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }
    }
}
