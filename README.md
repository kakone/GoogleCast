# GoogleCast
Implementation of the Google Cast protocol (.NET Standard 2.0 library).

This [documentation](https://github.com/thibauts/node-castv2#protocol-description) was really helpful to understand the protocol.

These two others C# projects were also helpful to implement the protocol : [SharpCast](https://github.com/jpepiot/SharpCast) and [SharpCaster](https://github.com/Tapanila/SharpCaster).

## Usage
```cs
// Use the DeviceLocator to find a Chromecast
var receiver = (await new DeviceLocator().FindReceiversAsync()).First();

var sender = new Sender();
// Connect to the Chromecast
await sender.ConnectAsync(receiver);
// Launch the default media receiver application
var mediaChannel = sender.GetChannel<IMediaChannel>();
await sender.LaunchAsync(mediaChannel);
// Load and play Big Buck Bunny video
var mediaStatus = await mediaChannel.LoadAsync(
    new MediaInformation() { ContentId = "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4" });
```

For Website casting purpose, with a static address

```cs
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

```

## Download
[![NuGet](https://img.shields.io/nuget/v/GoogleCast.svg)](https://www.nuget.org/packages/GoogleCast)
