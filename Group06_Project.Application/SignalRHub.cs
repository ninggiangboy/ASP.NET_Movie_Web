using Microsoft.AspNetCore.SignalR;

namespace Group06_Project.Application;

public class SignalRHub : Hub
{
    public async Task JoinFilmGroup(int filmId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, filmId.ToString());
    }

    public async Task LeaveFilmGroup(string filmId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, filmId);
    }
}