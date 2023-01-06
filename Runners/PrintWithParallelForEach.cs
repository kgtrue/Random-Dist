using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Random_Distro.Runners
{
    public class PrintWithParallelForEach : IPrintRandoDistro
    {
        private readonly IRandomDistroService randomDistroService;
        private ConcurrentDictionary<string, int> distroList = new ConcurrentDictionary<string, int>();

        public PrintWithParallelForEach(IRandomDistroService randomDistroService)
        {
            this.randomDistroService = randomDistroService;
        }

        public void RunPrint(int randomMatches = 100000000)
        {
            Parallel.ForEach(IterateUntilFalse(() => { return distroList.Count() == 0 || distroList.MaxBy(item => item.Value).Value < randomMatches; }), new ParallelOptions() { MaxDegreeOfParallelism = -1 }, i =>
            {
                AddRandom(randomDistroService);
            });

            var max = distroList.MaxBy(kvp => kvp.Value).Key;

            Console.WriteLine($"Final Distro: {max}");
        }

        private IEnumerable<bool> IterateUntilFalse(Func<bool> condition)
        {
            while (condition()) yield return true;
        }

        private void AddRandom(IRandomDistroService randomDistroService)
        {
            var distro = randomDistroService.GetDistroName();
            distroList.AddOrUpdate(distro, 1, (key, value) => value + 1);
            Console.WriteLine($"Distro: {distro} distro value: {distroList[distro]}");
        }
    }
}
