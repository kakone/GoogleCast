
using System.Net;
using CommandLine;
using GoogleCast;
using GoogleCast.Channels;
using GoogleCast.Models.Cast;


class ConsoleApp
{
    public class Options
    {
        public Options() { Host = string.Empty; }

        [Option('h', "host", Required = true, HelpText = "Host")]
        public string Host { get; set; }
        [Option('p', "port", Required = false, Default = 8009, HelpText = "Port")]
        public int Port { get; set; }
        [Option('t', "timeout", Required = false, Default = 5000, HelpText = "Timeout")]
        public int Timeout { get; set; }
    }

    static void Main(string[] args)
    {
        Parser.Default.ParseArguments<Options>(args)
            .WithParsed<Options>(o =>
            {
                Task.Run(async () =>
                {
                    var sender = new Sender();
                    Console.Write("Connection...");

                    var connected = await sender.ConnectAsync(IPAddress.Parse(o.Host), o.Port, o.Timeout);
                    if (!connected)
                    {
                        Console.WriteLine("Unable to connect to " + o.Host);
                        return;
                    }

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
                }).GetAwaiter().GetResult();
            });
    }
}
