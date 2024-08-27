using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBlog.Data.Models
{
    public class Blog
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
       
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

        public User User { get; set; }

        public string UserId { get; set; }
    }
}
