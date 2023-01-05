using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Random_Distro
{
    public class RandomDistroService : IRandomDistroService
    {
        private Dictionary<int, string> distros = new Dictionary<int, string>()
        {
            { 1, "Arch Linux" },
            { 2, "CentOS" },
            { 3, "Debian GNU/Linux" },
            { 4, "Elementary OS" },
            { 5, "Fedora Workstation" },
            { 6, "Kali Linux" },
            { 7, "Manjaro Linux" },
            { 8, "openSUSE" },
            { 9, "Pop!_OS" },
            { 10, "Puppy Linux" },
            { 11, "Raspberry Pi OS" },
            { 12, "Sabayon Linux" },
            { 13, "Solus OS" },
            { 14, "Ubuntu" },
        };

        public string GetDistroName()
        {
            Random rnd = new Random();
            var distroNumber = rnd.Next(1, distros.Count);

            return distros[distroNumber];
        }
    }
}
