using Microsoft.AspNetCore.SignalR;
using SignalR_Auction.Models;

namespace SignalR_Auction.Hubs
{
    public class AuctionHub :Hub
    {
        public async Task NotifyNewBid(AuctionNotify auctionNotify)
        {
            await Clients.All.SendAsync("ReceiveNewBid",
                auctionNotify);
        }
    }
}
