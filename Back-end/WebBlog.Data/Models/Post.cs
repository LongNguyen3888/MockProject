using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBlog.Data.Models
{
    public class Post
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public required string Content { get; set; }
        public string UrlSlug { get; set; }
        public DateTime PostedOn { get; set; }
        public bool Published { get; set; }
		public Guid? CategoryId { get; set; }
		public Category? Category { get; set; }
        public IList<PostTagMap>? PostTagMaps { get; set; }
    }
}
