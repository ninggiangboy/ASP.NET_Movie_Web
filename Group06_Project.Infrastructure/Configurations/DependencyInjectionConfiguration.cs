using Group06_Project.Application.Services;
using Group06_Project.Domain.Interfaces;
using Group06_Project.Domain.Interfaces.Repositories;
using Group06_Project.Domain.Interfaces.Services;
using Group06_Project.Infrastructure.Data;
using Group06_Project.Infrastructure.Data.Repositories;
using Group06_Project.Infrastructure.Email;
using Group06_Project.Infrastructure.Payment;
using Group06_Project.Infrastructure.Storage;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Group06_Project.Infrastructure.Configurations;

public static class DependencyInjectionConfiguration
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.RegisterData();
        services.RegisterApplicationServices();
    }

    private static void RegisterData(this IServiceCollection services)
    {
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<ICountryRepository, CountryRepository>();
        services.AddScoped<IEpisodeRepository, EpisodeRepository>();
        services.AddScoped<IFilmRepository, FilmRepository>();
        services.AddScoped<IGenreRepository, GenreRepository>();
        services.AddScoped<IRatingRepository, RatingRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IDbInitializer, DbInitializer>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    private static void RegisterApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IEmailSender, EmailSender>();
        services.AddScoped<IStorageService, CloudinaryService>();
        services.AddScoped<IPaymentService, VnPayService>();
        services.AddScoped<IGenreService, GenreService>();
        services.AddScoped<ICountryService, CountryService>();
        services.AddScoped<IFilmService, FilmService>();
        services.AddScoped<ICommentService, CommentService>();
        services.AddScoped<IFavoriteService, FavoriteService>();
        services.AddScoped<IBalanceService, BalanceService>();
    }
}