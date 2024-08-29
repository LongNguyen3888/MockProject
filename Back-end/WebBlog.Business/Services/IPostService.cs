using WebBlog.Business.Dtos.Post;
using WebBlog.Data.Models;

namespace WebBlog.Business.Services
{
    public interface IPostService : IBaseService<Post>
    {
        Task<PaginatedResult<Post>> GetByPagingAsync(
            string filter = "",
            string sortBy = "",
            int pageIndex = 1,
            int pageSize = 10);

        Task<IEnumerable<Post>> SearchPostsByTitleAsync(string title);
        Task<IEnumerable<Post>> SearchPostsByContentAsync(string content);
        Task<IEnumerable<Post>> SearchPostsByDescriptionAsync(string description);
        Task<IEnumerable<Post>> SearchPostsByUrlSlugAsync(string urlSlug);
        Task<IEnumerable<Post>> SearchPostsByCategoryIdAsync(Guid categoryId);
    }
}
