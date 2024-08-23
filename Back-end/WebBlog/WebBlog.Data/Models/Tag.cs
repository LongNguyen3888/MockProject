using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBlog.Data.Models
{
    public class Tag
    {
        public int Id
        { get; set; }

        public required string Name
        { get; set; }

        public string? UrlSlug
        { get; set; }

        public string? Description
        { get; set; }

        //public IList<Post>? Posts
        //{ get; set; }
    }
}
