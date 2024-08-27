using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBlog.Business.Services.Base;
using WebBlog.Data;
using WebBlog.Data.Models;

namespace WebBlog.Business.Services
{
    public class CategoryService : BaseService<Category>, ICategoryService
    {
        public CategoryService(IUnitOfWork unitOfWork, ILogger<BaseService<Category>> logger) : base(unitOfWork, logger)
        {
        }
    }
}
