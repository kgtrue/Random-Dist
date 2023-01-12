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
        string Run2(int randomMatches, int upperBound);
    }

    public class WithParallelForEachMulti : IWithParallelForEachMulti
    {

        public string Run(int randomMatches, int upperBound)
        {

            var random = new Random();
            var countToSearchFor = randomMatches;
            var numbers = Enumerable.Range(0, randomMatches * (upperBound)).Select(_ => random.Next(0, upperBound)).ToList();
             

            var found = ContainsParallelForEachMulti(numbers, countToSearchFor);
            return found.ToString();
        }

        public string Run2(int randomMatches, int upperBound)
        {
            var random = new Random();
            var countToSearchFor = randomMatches;
            var numbers = Enumerable.Range(0, randomMatches * (upperBound)).Select(_ => random.Next(0, upperBound)).ToList();

            return ContainsLinqGroup(numbers, randomMatches).ToString();
        }

        public bool ContainsLinqGroup(List<int> counts, int countToSearchFor)
        {
           return counts.GroupBy(x => x).Any(g => g.Count() == countToSearchFor);            
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
