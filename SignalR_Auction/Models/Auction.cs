using System.ComponentModel.DataAnnotations;

namespace SignalR_Auction.Models
{
    public class Auction
    {
        public int Id { get; set; }
        public string ItemName { get; set; } = "";
        public int CurrentBid { get; set; }
    }
}
