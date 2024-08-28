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
		public required string Title { get; set; }
		public string? Description { get; set; }
		public required string Content { get; set; }
	}
}
