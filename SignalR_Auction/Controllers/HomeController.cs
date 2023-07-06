using Microsoft.AspNetCore.Mvc;
using SignalR_Auction.Models;
using SignalR_Auction.Repositories;
using System.Diagnostics;

namespace SignalR_Auction.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAuctionRepo _AuctionRepo;

        public HomeController(IAuctionRepo auctionRepo)
        {
            _AuctionRepo = auctionRepo;
        }

        public IActionResult Index()
        {
            var auctions = _AuctionRepo.GetAll();
            return View(auctions);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}