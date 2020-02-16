using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SargeStore.Hubs
{
    public class InformationHub : Hub
    {
        public async Task Send(string Message) => await Clients.All.SendAsync("Send", Message);
    }
}