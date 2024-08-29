using WebBlog.Data.Data;
using WebBlog.Data.Models;
using WebBlog.Data.Repositories;

namespace WebBlog.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WebBlogDbContext _context;
        private IGenericRepository<Post>? _postRepository;
        private IGenericRepository<Tag>? _tagRepository;
        private IGenericRepository<User>? _userRepository;
        private IGenericRepository<Role>? _roleRepository;

        public UnitOfWork(WebBlogDbContext context)
        {
            _context=context;
        }
        public WebBlogDbContext Context => _context;

        public IGenericRepository<Post> PostRepository => _postRepository ?? new GenericRepository<Post>(_context);

        public IGenericRepository<Tag> TagRepository => _tagRepository ?? new GenericRepository<Tag>(_context);

        public IGenericRepository<User> UserRepository => _userRepository ?? new GenericRepository<User>(_context);

        public IGenericRepository<Role> RoleRepository => _roleRepository ?? new GenericRepository<Role>(_context);

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
