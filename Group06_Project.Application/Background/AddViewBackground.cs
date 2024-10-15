using Group06_Project.Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis;

namespace Group06_Project.Application.Background;

public class AddViewBackground : BackgroundService
{
    private static readonly TimeSpan Period = TimeSpan.FromHours(1);
    private readonly IServer _cache;
    private readonly IDatabase _database;
    private readonly IServiceScopeFactory _scopeFactory;

    public AddViewBackground(IServer cache, IDatabase database, IServiceScopeFactory scopeFactory)
    {
        _cache = cache;
        _database = database;
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = _scopeFactory.CreateScope();
        var filmService = scope.ServiceProvider.GetRequiredService<IFilmService>();
        using var timer = new PeriodicTimer(Period);
        while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken))
        {
            var keys = _cache.Keys(pattern: "view_film_*");
            foreach (var key in keys)
            {
                var view = _database.StringGet(key);
                if (view.HasValue)
                {
                    var viewCount = int.Parse(view.ToString());
                    var filmId = int.Parse(key.ToString().Replace("view_film_", ""));
                    filmService.AddView(filmId, viewCount);
                    _database.KeyDelete(key);
                }
            }
        }
    }
}