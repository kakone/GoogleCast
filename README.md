# GoogleCast
Implementation of the Google Cast protocol (.NET Standard 1.4 library).

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
await sender.GetChannel<IReceiverChannel>().LaunchAsync("CC1AD845");
// Load and play Big Buck Bunny video
var mediaStatus = await sender.GetChannel<IMediaChannel>().LoadAsync(
    new Media() { ContentId = "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4" });
```

## Download
[![NuGet](https://img.shields.io/nuget/v/GoogleCast.svg)](https://www.nuget.org/packages/GoogleCast)
