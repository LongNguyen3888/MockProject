using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBlog.Data.Models;

namespace WebBlog.Business.Dtos.Post
{
	public class CreatePostDto
	{
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public required string Content { get; set; }
        public DateTime PostedOn { get; set; }
        public Guid? CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
