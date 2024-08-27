using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebBlog.Business.Service.Base;
using WebBlog.Business.ViewModels;
using WebBlog.Data.Infrastructure;
using WebBlog.Data.Models;

namespace WebBlog.Business.Service
{
    public class CategoryService : BaseService<Category>, ICategoryService
    {
        public CategoryService(IUnitOfWork unitOfWork, ILogger<CategoryService> logger)
       : base(logger, unitOfWork)
        {
        }
    }
}
