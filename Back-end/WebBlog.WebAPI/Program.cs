using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebBlog.Business.Services;
using WebBlog.Data;
using WebBlog.Data.Data;
using WebBlog.Data.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<WebBlogDbContext>(option => {
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IPostService, PostService>();

//builder.Services.AddIdentity<User, Role>()
//	.AddEntityFrameworkStores<WebBlogDbContext>()
//	.AddDefaultTokenProviders();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

//using (var scope = app.Services.CreateScope())
//{
//	var services = scope.ServiceProvider;
//	var context = services.GetRequiredService<WebBlogDbContext>();
//	var userManager = services.GetRequiredService<UserManager<User>>();
//	var roleManager = services.GetRequiredService<RoleManager<Role>>();

//	// Ensure database is created and run the seed data
//	context.Database.Migrate();
//	SeedData.Initialize(context, userManager, roleManager);
//}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
