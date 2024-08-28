using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBlog.Business.ViewModels
{
    public class TagCreateViewModel
    {
        [Required]
        public string Name { get; set; }

        public string? UrlSlug { get; set; }

        public string? Description { get; set; }
    }
}
