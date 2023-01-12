using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Random_Distro.Runners
{
    public class WithSingleThreaded
    {
        private static bool ContainsSingleThreaded(List<int> counts, int countToSearchFor)
        {
            foreach (var count in counts)
                if (countToSearchFor == count)
                    return true;

            return false;
        }

    }
}
