using SignalR_Auction.Models;

namespace SignalR_Auction.Repositories
{
    public interface IAuctionRepo
    {
        IEnumerable<Auction> GetAll();
        void NewBid(int auctionId, int newBid);
    }
}