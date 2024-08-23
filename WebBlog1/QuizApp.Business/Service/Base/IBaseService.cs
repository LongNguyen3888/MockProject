using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBlog.Data.Models;

namespace QuizApp.Business.Service.Base
{
    public interface IBaseService<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T post);
        Task<T> UpdateAsync(T post);
        Task<bool> DeleteAsync(int id);
    }
}
