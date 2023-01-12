using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Random_Distro.Runners
{
    public interface IWithHandheldParallel
    {

    }
    public class WithHandheldParallel:IWithHandheldParallel
    {
        private static Task<bool> ContainsHandheldParallel(List<int> counts, int countToSearchFor)
        {
            var tcs = new TaskCompletionSource<bool>();

            var processorCount = System.Environment.ProcessorCount;
            var sliceLength = counts.Count / processorCount;
            var tasks = new List<Task>();
            for (var i = 0; i < processorCount; i++)
            {
                var j = i;
                var task = Task.Run(() =>
                {
                    var start = j * sliceLength;
                    var end = (j + 1) * sliceLength;
                    if (j == processorCount - 1)
                        end = counts.Count;
                    for (var k = start; k < end; k++)
                        if (counts[k] == countToSearchFor)
                        {
                            tcs.TrySetResult(true);
                            return;
                        }

                });
                tasks.Add(task);
            }

            async Task AfterAll()
            {
                foreach (var task in tasks)
                    await task;

                tcs.TrySetResult(false);
            }

            _ = AfterAll();

            return tcs.Task;
        }
    }
}
