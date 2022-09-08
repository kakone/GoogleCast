
using System.Net;
using GoogleCast;
using GoogleCast.Channels;
using GoogleCast.Models.Cast;

var sender = new Sender();

// Connect to the Chromecast
await sender.ConnectAsync(
                        new Receiver
                        {
                            IPEndPoint = new System.Net.IPEndPoint(
                                IPAddress.Parse("10.0.0.2"),
                                8009
                            )
                        });

// Launch the default media receiver application
var castChannel = sender.GetChannel<ICastChannel>();
await sender.LaunchAsync(castChannel);

// Load an example website
await castChannel.LoadUrl(new CastInformation { Url = "https://www.example.com" });
