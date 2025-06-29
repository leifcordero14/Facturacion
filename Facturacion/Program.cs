using Facturacion.Data;
using Facturacion.DTOs;
using Facturacion.Mappers;
using Facturacion.Models;
using Facturacion.Repositories;
using Facturacion.Services;
using Facturacion.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

namespace Facturacion
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);
      // Add services to the container.

      // Database context
      builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

      // Repositories
      builder.Services.AddScoped<IRepository<Article>, ArticleRepository>();
      builder.Services.AddScoped<IRepository<Seller>, SellerRepository>();

      // Services
      builder.Services.AddScoped<IService<ArticleDto, CreateArticleDto, UpdateArticleDto>, ArticleService>();
      builder.Services.AddScoped<IService<SellerDto, CreateSellerDto, UpdateSellerDto>, SellerService>(); 

      // Mappers
      builder.Services.AddAutoMapper(typeof(ArticleMapper));
      builder.Services.AddAutoMapper(typeof(SellerMapper));

      // Validators
      builder.Services.AddScoped<IValidator<CreateArticleDto>, CreateArticleValidator>();  
      builder.Services.AddScoped<IValidator<UpdateArticleDto>, UpdateArticleValidator>();
      builder.Services.AddScoped<IValidator<CreateSellerDto>, CreateSellerValidator>();
      builder.Services.AddScoped<IValidator<UpdateSellerDto>, UpdateSellerValidator>();

      builder.Services.AddControllers();
      // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
      builder.Services.AddOpenApi();

      var app = builder.Build();

      // Configure the HTTP request pipeline.
      if (app.Environment.IsDevelopment())
      {
        app.MapOpenApi();
        app.MapScalarApiReference();
      }

      app.UseHttpsRedirection();

      app.UseAuthorization();

      app.MapControllers();

      app.Run();
    }
  }
}
