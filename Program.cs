// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Random_Distro;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    services.AddSingleton<IRandomDistroService, RandomDistroService>())
    .Build();

Run(host.Services);
host.RunAsync();

static async void Run(IServiceProvider serviceProvider)
{
    var randomDistroService = serviceProvider.GetRequiredService<IRandomDistroService>();
    var distroList = new ConcurrentDictionary<string, int>();
   
    Parallel.For(0, 100000000, new ParallelOptions() { MaxDegreeOfParallelism = 1000000 }, i =>
    {
        AddRandom(distroList, randomDistroService);
    });

    var max = distroList.MaxBy(kvp => kvp.Value).Key;

    Console.WriteLine($"Final Distro: {max}");

}
static void AddRandom(ConcurrentDictionary<string, int> distrolist, IRandomDistroService randomDistroService)
{
    var distro = randomDistroService.GetDistroName();
    distrolist.AddOrUpdate(distro, 1, (key, value) => value + 1);
    Console.WriteLine($"Distro: {distro} distro value: {distrolist[distro]}");
}
