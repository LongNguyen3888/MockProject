using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBlog.Data.Models;
using WebBlog.Data.Repositories;

namespace WebBlog.Data.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Category> Categories { get; }
        IGenericRepository<Tag> Tags { get; }
        IGenericRepository<Post> Posts { get; }

        Task<int> SaveAsync();
    }
}
