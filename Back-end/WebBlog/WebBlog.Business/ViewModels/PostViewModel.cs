using WebBlog.Data.Models;

namespace WebBlog.Business
{
    public class PostViewModel
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public required string Content { get; set; }
        public DateTime PostedOn { get; set; }
        public bool Published { get; set; }
    }
}
