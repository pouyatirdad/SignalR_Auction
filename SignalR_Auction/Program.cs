using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SignalR;
using SignalR_Auction.Hubs;
using SignalR_Auction.Models;
using SignalR_Auction.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();
builder.Services.AddSingleton<IAuctionRepo, AuctionMemoryRepo>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapPost("auction/{auctionId}/newbid", (int auctionId, int currentBid, IAuctionRepo auctionRepo) =>
{
    auctionRepo.NewBid(auctionId, currentBid);
});

app.MapPost("auction", (Auction auction, IAuctionRepo auctionRepo, IHubContext<AuctionHub> hubContext) =>
{
    auctionRepo.AddAuction(auction);
    hubContext.Clients.All.SendAsync("ReceiveNewAuction", auction);
});

app.MapHub<AuctionHub>("/auctionhub");

app.Run();
