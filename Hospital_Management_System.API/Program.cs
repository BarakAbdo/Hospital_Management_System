using Hospital_Management_System.Application.Repositories.Base;
using Hospital_Management_System.Application.Services;
using Hospital_Management_System.Application.Services.Base;
using Hospital_Management_System.Infrastructure.Data;
using Hospital_Management_System.Infrastructure.Repositories.Base;
using Hospital_Management_System.Infrastructure.Repositories.CartRepo;
using Hospital_Management_System.Infrastructure.Repositories.CategoryRepo;
using Hospital_Management_System.Infrastructure.Repositories.NotificationRepo;
using Hospital_Management_System.Infrastructure.Repositories.OrderRepo;
using Hospital_Management_System.Infrastructure.Repositories.ProductRepo;
using Hospital_Management_System.Infrastructure.Repositories.UserRepo;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var conectionString = builder.Configuration.GetConnectionString("DefaultDatabase");

builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(conectionString));


builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();


builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<INotificationService, NotificationService>();




builder.Services.AddControllers();
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

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
