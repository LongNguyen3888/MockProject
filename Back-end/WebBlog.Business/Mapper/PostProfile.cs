using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBlog.Business.Dtos.Post;
using WebBlog.Data.Models;

namespace WebBlog.Business.Mapper
{
	public class PostProfile : Profile
	{
		public PostProfile() 
		{ 
			CreateMap<Post, PostViewModel>();
			CreateMap<CreatePostDto, Post>();
		}
	}
}
