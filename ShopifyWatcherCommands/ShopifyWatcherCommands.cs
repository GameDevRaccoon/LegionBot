using System;
using System.Threading.Tasks;

namespace ShopifyWatcherCommands
{
    public class ShopifyWatcherCommands
    {
        // Innitialisation code for this module
        public Task Initialise()
        {
            Console.WriteLine("Initialised Shopify commands ");
            return Task.CompletedTask;
        }

    }
}
