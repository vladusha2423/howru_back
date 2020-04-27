using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Howru.Core.Hubs
{
    public class ChatHub : Hub
    {
        //public async override Task OnConnectedAsync()
        //{
        //    await Clients.All.SendAsync("Info",
        //        Context.User.FindFirst(ClaimTypes.NameIdentifier).Value, "enter");
        //    await base.OnConnectedAsync();
        //}

        //public async override Task OnDisconnectedAsync(Exception ex)
        //{
        //    await Clients.All.SendAsync("Info",
        //        Context.User.FindFirst(ClaimTypes.NameIdentifier).Value, "exit");
        //    await base.OnDisconnectedAsync(ex);
        //}
    }
}
