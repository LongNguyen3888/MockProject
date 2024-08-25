using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBlog.Data.Models
{
	public class Comment
	{
		public Guid Id { get; set; }
		public required string Content { get; set; }
		public required DateTime CreatedOn { get; set; }
	}
}
