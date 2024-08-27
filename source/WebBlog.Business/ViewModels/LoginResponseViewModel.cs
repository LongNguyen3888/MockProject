using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBlog.Business.ViewModels
{
    public class LoginResponseViewModel
    {
        public required string UserInformation { get; set; }
        public required string Token { get; set; }
        public DateTime Expires { get; set; }
    }
}
