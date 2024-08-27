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
    public class BlogService : BaseService<Blog>, IBlogService
    {
        public BlogService(IUnitOfWork unitOfWork, ILogger<BlogService> logger): base(logger, unitOfWork) {}

        public async Task<PaginatedResult<Blog>> GetByPagingAsync(string filter = "", string sortBy = "", int pageIndex = 1, int pageSize = 10)
        {
            Func<IQueryable<Blog>, IOrderedQueryable<Blog>> orderBy = null;

            switch (sortBy.ToLower())
            {
                case "title":
                    orderBy = q => q.OrderBy(b => b.Title);
                    break;
                case "id":
                    orderBy = q => q.OrderBy(b => b.Id);
                    break;
                default:
                    orderBy = q => q.OrderBy(b => b.CreatedAt);
                    break;
            }

            Expression<Func<Blog, bool>> filterQuery = null;

            if (!string.IsNullOrWhiteSpace(filter))
            {
                filterQuery = b => b.Title.Contains(filter) || b.Content.Contains(filter);
            }

            return await GetAsync(filterQuery, orderBy, "", pageIndex, pageSize);
        }
    }
}
