using Bookstore.Application.Services;
using Bookstore.Domain.Interfaces.IRepositories;
using Bookstore.Domain.Interfaces.IServices;
using Bookstore.Domain.Validations.Auth;
using Bookstore.Domain.Validations.Genres;
using Bookstore.Infrastructure.Repositories;
using Bookstore.Infrastructure.Utils;

namespace Bookstore.Api
{
    public static class DependencyInjection 
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<IGenreService, GenreService>();

            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IBookService, BookService>();

            services.AddScoped<IInventoryRepository, InventoryRepository>();
            services.AddScoped<IInventoryService, InventoryService>();

            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IAuthService, AuthService>();           

            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<ICartService, CartService>();

            services.AddTransient<CreateUserRequestValidator>();            
            
            services.AddTransient<CreateGenreRequestValidator>();
            services.AddTransient<UpdateGenreRequestValidator>();

            services.AddTransient<IPasswordHasher, PasswordHasher>(); 

            return services;
        }
    }
}
