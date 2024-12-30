using FluentValidation;
using Library.Application.Validators.Author;
using Library.Application.Validators.User;
using Library.Application.Validators.Book;
using Library.Application.Validators.ReaderBook;

namespace Library.API.Extensions
{
    public static class ValidatorsSetup
    {
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddValidatorsFromAssemblyContaining<AuthorBaseDTOValidator>();
            services.AddValidatorsFromAssemblyContaining<AddAuthorRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateAuthorRequestValidator>();

            services.AddValidatorsFromAssemblyContaining<SignInRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<RegistrationRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<AddAdminRequestValidator>();

            services.AddValidatorsFromAssemblyContaining<AddBookRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateBookRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<BookBaseDtoValidator>();

            services.AddValidatorsFromAssemblyContaining<GiveABookRequestValidator>();

            return services;
        }
    }
}
