using Facturacion.Data;
using Facturacion.DTOs;
using Facturacion.Mappers;
using Facturacion.Models;
using Facturacion.Repositories;
using Facturacion.Services;
using Facturacion.Utilities;
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
      builder.Services.AddScoped<IRepository<Client>, ClientRepository>();
      builder.Services.AddScoped<IGetPostRepository<Billing>, BillingRepository>();
      builder.Services.AddScoped<IFilterRepository<Billing, BillingFilterDto>, BillingRepository>();

      // Services
      builder.Services.AddScoped<IService<ArticleDto, CreateArticleDto, UpdateArticleDto>, ArticleService>();
      builder.Services.AddScoped<IService<SellerDto, CreateSellerDto, UpdateSellerDto>, SellerService>(); 
      builder.Services.AddScoped<IService<ClientDto, CreateClientDto, UpdateClientDto>, ClientService>();
      builder.Services.AddScoped<ICreateReadService<BillingDto, CreateBillingDto>, BillingService>();

      // Mappers
      builder.Services.AddAutoMapper(typeof(ArticleMapper));
      builder.Services.AddAutoMapper(typeof(SellerMapper));
      builder.Services.AddAutoMapper(typeof(ClientMapper));
      builder.Services.AddAutoMapper(typeof(BillingMapper));

      // Validators
      builder.Services.AddScoped<IValidator<CreateArticleDto>, CreateArticleValidator>();  
      builder.Services.AddScoped<IValidator<UpdateArticleDto>, UpdateArticleValidator>();
      builder.Services.AddScoped<IValidator<CreateSellerDto>, CreateSellerValidator>();
      builder.Services.AddScoped<IValidator<UpdateSellerDto>, UpdateSellerValidator>();
      builder.Services.AddScoped<IValidator<CreateClientDto>, CreateClientValidator>();
      builder.Services.AddScoped<IValidator<UpdateClientDto>, UpdateClientValidator>();
      builder.Services.AddScoped<IValidator<CreateBillingDto>, CreateBillingValidator>();

      // Utilities
      builder.Services.AddScoped<IValidationResultHelper, ValidationResultHelper>();
      builder.Services.AddScoped<EntityExistenceChecker>();

      // CORS
      builder.Services.AddCors(options => 
      { 
        options.AddPolicy("AllowAllOrigins", policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
      });

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

      app.UseCors("AllowAllOrigins");

      app.UseAuthorization();

      app.MapControllers();

      app.Run();
    }
  }
}
