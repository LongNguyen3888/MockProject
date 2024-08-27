using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBlog.Data.Data;
using WebBlog.Data.Models;
using WebBlog.Data.Repositories;

namespace WebBlog.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WebBlogDbContext _context;
        private IGenericRepository<Category>? _categoryRepository;
        private IGenericRepository<Blog>? _blogRepository;
        private IGenericRepository<User>? _userRepository;
        public UnitOfWork(WebBlogDbContext context)
        {
            _context = context;
        }
        public WebBlogDbContext Context => _context;

        public IGenericRepository<Blog> BlogRepository => _blogRepository ?? new GenericRepository<Blog>(_context);

        public IGenericRepository<User> UserRepository => _userRepository ?? new GenericRepository<User>(_context);

        public IGenericRepository<Category> CategoryRepository => _categoryRepository ?? new GenericRepository<Category>(_context);

        public async Task BeginTransactionAsync()
        {
            await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            await _context.Database.CommitTransactionAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public IGenericRepository<TEntity> GenericRepository<TEntity>() where TEntity : class
        {
            return new GenericRepository<TEntity>(_context);
        }

        public async Task RollBackTransactionAsync()
        {
            await _context.Database.RollbackTransactionAsync();
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
