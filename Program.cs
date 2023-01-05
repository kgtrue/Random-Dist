// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Random_Distro;
using Random_Distro.Runners;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) => {
        services.AddSingleton<IRandomDistroService, RandomDistroService>();
        services.AddSingleton<IPrintRandoDistro, PrintWithParallelFor>();
    })
    .Build();

host.Services.GetRequiredService<IPrintRandoDistro>().RunPrint();