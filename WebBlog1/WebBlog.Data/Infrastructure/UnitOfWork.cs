using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBlog.Data.Models;
using WebBlog.Data.Repositories;

namespace WebBlog.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        public IGenericRepository<Category> Categories => throw new NotImplementedException();

        public IGenericRepository<Tag> Tags => throw new NotImplementedException();

        public IGenericRepository<Post> Posts => throw new NotImplementedException();

        public Task<int> SaveAsync()
        {
            throw new NotImplementedException();
        }
    }
}
