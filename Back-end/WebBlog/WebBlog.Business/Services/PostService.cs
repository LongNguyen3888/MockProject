using Azure;
using Microsoft.Extensions.Logging;
using WebBlog.Business.Services.Base;
using Microsoft.EntityFrameworkCore;
using WebBlog.Data;
using WebBlog.Data.Models;
using System.Globalization;
using System.Linq.Expressions;
using System.Linq;
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
                case "date":
                    orderBy = q => q.OrderByDescending(p => p.PostedOn);
                    break;

            }

            Expression<Func<Post, bool>> filterQuery = null;

            if (!string.IsNullOrWhiteSpace(filter))
            {
                filterQuery = p => p.Title.Contains(filter);
            }

            return await GetAsync(filterQuery, orderBy, "", pageIndex, pageSize);

        }

        public async Task<IEnumerable<Post>> SearchPostsByTitleAsync(string title)
        {
            return await _unitOfWork.PostRepository.GetAll()
                .AsQueryable()
                .Where(p => p.Title.Contains(title))
                .ToListAsync();
        }

        public async Task<IEnumerable<Post>> SearchPostsByContentAsync(string content)
        {
            return await _unitOfWork.PostRepository.GetAll()
                .AsQueryable()
                .Where(p => p.Content.Contains(content))
                .ToListAsync();
        }

        public async Task<IEnumerable<Post>> SearchPostsByDescriptionAsync(string description)
        {
            return await _unitOfWork.PostRepository.GetAll()
                .AsQueryable()
                .Where(p => p.Description.Contains(description))
                .ToListAsync();
        }

        public async Task<IEnumerable<Post>> SearchPostsByUrlSlugAsync(string urlSlug)
        {
            return await _unitOfWork.PostRepository.GetAll()
                .AsQueryable()
                .Where(p => p.UrlSlug.Contains(urlSlug))
                .ToListAsync();
        }

        public async Task<IEnumerable<Post>> SearchPostsByCategoryIdAsync(Guid categoryId)
        {
            return await _unitOfWork.PostRepository.GetAll()
                .AsQueryable()
                .Where(p => p.CategoryId == categoryId)
                .ToListAsync();
        }
    }
}
