using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebBlog.Data.Models
{
    public class Role :IdentityRole<Guid>
    {
        public string Name {  get; set; }
        [StringLength(50,MinimumLength =3)]
        public string Description { get; set; }
    }
}
