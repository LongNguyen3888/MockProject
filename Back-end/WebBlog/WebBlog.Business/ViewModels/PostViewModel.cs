namespace WebBlog.Business
{
    public class PostViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Content { get; set; }
        public bool IsActive { get; set; } 
    }
}
