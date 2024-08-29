using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebBlog.Data.Models
{
    public class Role :IdentityRole<Guid>
    {
        [StringLength(50,MinimumLength =3)]
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
