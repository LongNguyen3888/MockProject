using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebBlog.Business.Services.Base;
using WebBlog.Data;
using WebBlog.Data.Models;
using System.Globalization;
using System.Linq.Expressions;
using WebBlog.Business.Dtos.Post;

namespace WebBlog.Business.Services
{
    public class PostService :BaseService<Post>,IPostService
    {
        public PostService(IUnitOfWork unitOfWork,ILogger<PostService> logger) :base(unitOfWork, logger) 
        {
            
        }

		public async Task<PaginatedResult<Post>> GetByPagingAsync(string filter = "", string sortBy = "", int pageIndex = 1, int pageSize = 10)
        {
            Func<IQueryable<Post>, IOrderedQueryable<Post>> orderBy = null;

            switch (sortBy.ToLower())
            {
                case "title":
                    orderBy = q => q.OrderBy(p => p.Title);
                    break;
                case "id":
                    orderBy = q => q.OrderBy(p => p.Id);
                    break;

            }

            Expression<Func<Post, bool>> filterQuery = null;

            if (!string.IsNullOrWhiteSpace(filter))
            {
                filterQuery = p => p.Title.Contains(filter);
            }

            return await GetAsync(filterQuery, orderBy, "", pageIndex, pageSize);

        }
    }
}
