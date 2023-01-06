// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Random_Distro;
using Random_Distro.Runners;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) => {
        services.AddSingleton<IRandomDistroService, RandomDistroService>();
        services.AddSingleton<IPrintRandoDistro, PrintWithParallelForEach>();
        //services.AddSingleton<IPrintRandoDistro, PrintWithWhile>();
    })
    .Build();
var stopWatch = new Stopwatch();
stopWatch.Start();
host.Services.GetRequiredService<IPrintRandoDistro>().RunPrint(100000);
stopWatch.Stop();
Console.WriteLine($"Seconds: {stopWatch.ElapsedMilliseconds/1000}");