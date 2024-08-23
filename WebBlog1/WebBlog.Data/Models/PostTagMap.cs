using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBlog.Data.Models
{
    public class PostTagMap
    {
        [Key]
        public int PostId { get; set; }
        [Key]
        public int TagId { get; set; }

        public Post Post { get; set; }
        public Tag Tag { get; set; }
    }
}
