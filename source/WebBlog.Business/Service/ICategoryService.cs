using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBlog.Business.Service.Base;
using WebBlog.Business.ViewModels;
using WebBlog.Data.Models;

namespace WebBlog.Business.Service
{
    public interface ICategoryService : IBaseService<Category>
    {
    }
}
