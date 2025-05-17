using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyBlogAppAPI.Data.Concrete.EfCore;
using MyBlogAppAPI.Entity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<BlogContext>(options => options.UseSqlite(builder.Configuration["ConnectionStrings:SQLite_Connection"]));
builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<BlogContext>().AddDefaultTokenProviders();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
