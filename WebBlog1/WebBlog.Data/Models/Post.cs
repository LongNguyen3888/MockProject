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
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string UrlSlug { get; set; }
        public DateTime PostedOn { get; set; }
        public bool Published { get; set; }
        public DateTime? Modified { get; set; }

        public Category Category { get; set; }
        public IList<PostTagMap> PostTagMaps { get; set; }
        public int CategoryId { get; set; }
    }
}
