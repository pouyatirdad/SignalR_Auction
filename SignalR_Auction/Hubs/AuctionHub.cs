using Microsoft.AspNetCore.SignalR;
using SignalR_Auction.Models;

namespace SignalR_Auction.Hubs
{
    public class AuctionHub :Hub
    {
        public async Task NotifyNewBid(AuctionNotify auction)
        {
            var groupName = $"auction-{auction.AuctionId}";

            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.OthersInGroup(groupName).SendAsync("NotifyOutbid", auction);

            await Clients.All.SendAsync("ReceiveNewBid", auction);
        }
    }
}
