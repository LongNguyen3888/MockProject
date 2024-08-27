using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBlog.Data.Models;
using WebBlog.Data.Repositories;
using WebBlog.Data.Data;

namespace WebBlog.Data.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        WebBlogDbContext Context { get; }

        IGenericRepository<Blog> BlogRepository { get; }
        IGenericRepository<User> UserRepository { get; }
        IGenericRepository<Category> CategoryRepository { get; }

        IGenericRepository<TEntity> GenericRepository<TEntity>() where TEntity : class;

        int SaveChanges();

        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollBackTransactionAsync();
    }
}
