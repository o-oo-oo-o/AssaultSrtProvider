using System;
using AssaultSrtProvider.Interfaces;
using AssaultSrtProvider.Providers;
using AssaultSrtProvider.Representation;

namespace AssaultSrtProvider
{
    class Program
    {
        static void Main(string[] args)
        {
            IDataProvider provider = new SrtProvider(args[0], 24);
            
            foreach (var snap in provider.Snapshots())
            {
                foreach(var item in snap.Tags)
                {
                    Console.Write(item.ToString() + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
