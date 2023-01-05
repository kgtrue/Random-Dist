using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Random_Distro.Runners
{
    public class PrintWithParallelFor : IPrintRandoDistro
    {
        private readonly IRandomDistroService randomDistroService;

        public PrintWithParallelFor(IRandomDistroService randomDistroService)
        {
            this.randomDistroService = randomDistroService;
        }

        public void RunPrint()
        {
            var distroList = new ConcurrentDictionary<string, int>();

            Parallel.For(0, 100000000, new ParallelOptions() { MaxDegreeOfParallelism = 1000000 }, i =>
            {
                AddRandom(distroList, randomDistroService);
            });

            var max = distroList.MaxBy(kvp => kvp.Value).Key;

            Console.WriteLine($"Final Distro: {max}");
        }

        private void AddRandom(ConcurrentDictionary<string, int> distrolist, IRandomDistroService randomDistroService)
        {
            var distro = randomDistroService.GetDistroName();
            distrolist.AddOrUpdate(distro, 1, (key, value) => value + 1);
            Console.WriteLine($"Distro: {distro} distro value: {distrolist[distro]}");
        }
    }
}
