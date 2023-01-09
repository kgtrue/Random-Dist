using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Random_Distro.Runners
{
    public interface IRepeatingMatches
    {
        public string GetMaxRandomRepeatingMatches(int randomMatches);
    }
}
