using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Random_Distro.Runners
{
    public class PrintWithWhile : IPrintRandoDistro
    {
        private readonly IRandomDistroService randomDistroService;
        private Dictionary<string, int> distroList = new Dictionary<string, int>();

        public PrintWithWhile(IRandomDistroService randomDistroService)
        {
            this.randomDistroService = randomDistroService;
        }

        public void RunPrint(int randomMatches)
        {
            while (distroList.Count()==0 || distroList.MaxBy(item => item.Value).Value < randomMatches)
            {
                AddRandom(randomDistroService);
            };

            var max = distroList.MaxBy(kvp => kvp.Value).Key;

            Console.WriteLine($"Final Distro: {max}");
        }

        private void AddRandom(IRandomDistroService randomDistroService)
        {
            var distro = randomDistroService.GetDistroName();
            AddOrUpdate(distro);
            Console.WriteLine($"Distro: {distro} distro value: {distroList[distro]}");
        }

        private void AddOrUpdate(string distro)
        {

            if (distroList.ContainsKey(distro))
            {
                distroList[distro] += 1;
            }
            else
            {
                distroList.Add(distro, 1);
            }
        }
    }
}
