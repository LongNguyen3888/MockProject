using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebBlog.Data.Models
{
    public class User :IdentityUser<Guid>
    {
        [StringLength(50,MinimumLength =3)]
        public bool IsActive { get; set; }
    }
}
