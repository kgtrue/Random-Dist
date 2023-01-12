using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Random_Distro.Runners
{
    public interface IWithParallelForEachMulti
    {
        string Run(int randomMatches, int upperBound);
    }

    public class WithParallelForEachMulti : IWithParallelForEachMulti
    {

        public string Run(int randomMatches, int upperBound)
        {

            var random = new Random();
            var countToSearchFor = randomMatches;
            var numbers = Enumerable.Range(0, randomMatches * (upperBound)).Select(_ => random.Next(0, upperBound)).ToList();

            var ng = numbers.GroupBy(x => x);
            foreach (var g in ng)
            {
                Console.WriteLine($"Group: {g.Key} count {g.Count()}");
            }

            var found = ContainsParallelForEachMulti(numbers, countToSearchFor);
            return found.ToString();
        }

        public bool ContainsParallelForEachMulti(List<int> counts, int countToSearchFor)
        {
            var foundValue = false;

            Parallel.ForEach(
                counts,
                body: count =>
                {
                    if (count == countToSearchFor)
                        foundValue = true;
                }
            );

            return foundValue;
        }
    }
}
