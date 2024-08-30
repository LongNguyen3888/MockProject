using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBlog.Business.Services.Base;
using WebBlog.Data;
using WebBlog.Data.Models;

namespace WebBlog.Business.Services
{
    public class TagService : BaseService<Tag>, ITagService
    {
        public TagService(IUnitOfWork unitOfWork, ILogger<BaseService<Tag>> logger) : base(unitOfWork, logger)
        {
        }
    }
}
