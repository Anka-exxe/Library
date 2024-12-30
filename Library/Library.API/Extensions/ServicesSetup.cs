using Library.Application.Interfaces;
using Library.Application.Services;
using Library.Domain.Interfaces.Repositories;
using Library.Infrastructure.Data.Repositories;
using Library.Infrastructure.Services;
using Library.Application.Use_Cases.AuthorUseCase;
using Library.Application.Use_Cases.UserUseCase;
using Library.Infrastructure.Data;
using Library.Application.Use_Cases.BookUseCase;
using Library.Application.Use_Cases.ReaderBookUseCase;

namespace Library.API.Extensions
{
    public static class ServicesSetup
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IValidationService, ValidationService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IEmailService, EmailService>();

            services.AddScoped<ValidateCredentials>();
            services.AddScoped<AddAuthor>();
            services.AddScoped<UpdateAuthor>();
            services.AddScoped<DeleteAuthor>();
            services.AddScoped<GetAllAuthors>();
            services.AddScoped<GetAuthorById>();
            services.AddScoped<Login>();
            services.AddScoped<RegisterUser>();
            services.AddScoped<GetAllUsers>();
            services.AddScoped<AddAdmin>();
            services.AddScoped<AddBook>();
            services.AddScoped<GetAllBooks>();
            services.AddScoped<GetBooksByAuthorId>();
            services.AddScoped<GetBookByISBN>();
            services.AddScoped<DeleteBook>();
            services.AddScoped<UpdateBook>();
            services.AddScoped<GiveABook>();
            services.AddScoped<ReturnBook>();
            services.AddScoped<GetTakenBooksByReaderId>();
            services.AddScoped<GetAllBooksWithTakenInfo>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IReaderRepository, ReaderRepository>();
            services.AddScoped<IReaderBookRepository, ReaderBookRepository>();
            services.AddScoped<IImageRepository, ImageRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
