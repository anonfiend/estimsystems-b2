using System;
using System.Linq;
using System.Threading.Tasks;

namespace EStimSystems.B2.Example
{
    public class Program
    {
        public static async Task Main()
        {
            var factory = new B2ClientFactory();
            var comPort = factory.DiscoverComPorts().Single();

            Console.WriteLine($"Connecting to COM port '{comPort}'.");
            using var client = factory.CreateClient(comPort);

            Console.WriteLine("Getting status...");
            while (true)
            {
                await Task.Delay(TimeSpan.FromSeconds(1));
                var status = client.GetStatus();
                Console.WriteLine(status);
            }

            // ReSharper disable once FunctionNeverReturns
        }
    }
}