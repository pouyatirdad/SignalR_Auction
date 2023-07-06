const initializeSignalRConnection = () => {
    const connection = new signalR.HubConnectionBuilder()
        .withUrl("auctionhub")
        .build();

    connection.on("ReceiveNewBid", ({ auctionId, newBid }) => {
        const tr = document.getElementById(auctionId + "-tr");
        const input = document.getElementById(auctionId + "-input");
        //start animation
        setTimeout(() => tr.classList.add("animate-highlight"), 20);
        setTimeout(() => tr.classList.remove("animate-highlight"), 2000);

        const bidText = document.getElementById(auctionId + "-bidtext");
        bidText.innerHTML = newBid;
        input.value = newBid + 1;
    })

    connection.start().catch(e => console.error(e.toString()))
    return connection;
}

const connection = initializeSignalRConnection();

const submitBid = (auctionId) => {
    const bid = document.getElementById(auctionId + "-input").value;
    fetch("/auction/" + auctionId + "/newbid?currentBid=" + bid, {
        method: "POST",
        headers: {
            'Content-Type': 'application/json'
        }
    });
    
    connection.invoke("NotifyNewBid", {
        AuctionId: parseInt(auctionId),
        NewBid: parseInt(bid)
    })
}

