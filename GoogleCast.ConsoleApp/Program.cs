
using System.Net;
using GoogleCast;
using GoogleCast.Channels;
using GoogleCast.Models.Cast;

var sender = new Sender();
var ip = "10.0.0.2";

// Connect to the Chromecast
Console.Write("Connection...");

var connected = await sender.ConnectAsync(IPAddress.Parse(ip), 8009, 5000);
if (connected)
{
    Console.WriteLine("Done.");

    var alive = false;
    var heartBeatChannel = sender.GetChannel<IHeartbeatChannel>();
    heartBeatChannel.PingReceived += (object? sender, GoogleCast.Models.HeartBeat.PingEvent e) =>
    {
        alive = true;
    };

    Console.Write("Waiting for ChromeCast Feedback");
    while (!alive)
    {
        await Task.Delay(1000);
        Console.Write(".");

    }
    Console.WriteLine(" Done.");

    // Launch the default media receiver application
    Console.Write("Initializing Cast Channel...");
    var castChannel = sender.GetChannel<ICastChannel>();
    await sender.LaunchAsync(castChannel);
    Console.WriteLine(" Done.");

    // Load an example website
    Console.Write("Load URL http://www.example.com...");
    await castChannel.LoadUrl(new CastInformation { Url = "https://www.example.com" });
    Console.WriteLine(" Done.");
}
else
{
    Console.WriteLine("Unable to connect to " + ip);
}
