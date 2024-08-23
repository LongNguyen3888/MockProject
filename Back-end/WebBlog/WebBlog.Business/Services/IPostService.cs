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
    }
}
