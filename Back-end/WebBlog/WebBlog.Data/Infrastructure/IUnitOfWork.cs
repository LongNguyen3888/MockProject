using WebBlog.Data.Data;
using WebBlog.Data.Models;
using WebBlog.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBlog.Data
{
    public interface IUnitOfWork : IDisposable
    {
        WebBlogDbContext Context { get; }

        IGenericRepository<Post> PostRepository { get; }
        IGenericRepository<Tag> TagRepository { get; }
        IGenericRepository<User> UserRepository { get; }    
        IGenericRepository<Role> RoleRepository { get; }

        IGenericRepository<TEntity> GenericRepository<TEntity>() where TEntity : class;

        int SaveChanges();

        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollBackTransactionAsync();
    }
}
