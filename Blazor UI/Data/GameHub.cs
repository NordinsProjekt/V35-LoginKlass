using GlobalChat;
using LoginClass;
using Microsoft.AspNetCore.Components.Server.Circuits;
using Microsoft.AspNetCore.SignalR;
using System.Drawing;

namespace Blazor_UI.Data
{
    public class GameHub : Hub
    {
        public const string HubUrl = "/game";

        public async Task Broadcast(string username, string message)
        {
            await Clients.All.SendAsync("Broadcast", username, message);
        }

        public async Task UpdateButtons(int id,string color)
        {
            await Clients.All.SendAsync("UpdateButtons", id, color);
        }

        public async Task Refresh()
        {
            await Clients.All.SendAsync("Refresh", true);
        }

        public override Task OnConnectedAsync()
        {
            Console.WriteLine($"{Context.ConnectionId} connected");
            return base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception e)
        {
            Console.WriteLine($"Disconnected {e?.Message} {Context.ConnectionId}");
            await base.OnDisconnectedAsync(e);
        }
    }
}
