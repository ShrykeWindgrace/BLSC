using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLSC
{
    [Serializable]
    public class Project
    {
        public Entry[] entries;
        public Project()
        {
            entries = new Entry[Enum.GetNames(typeof(EEType)).Length];
            foreach (EEType eet in Enum.GetValues(typeof(EEType)))
            {
                entries[(int)eet] = new Entry(eet);
            }
        }
    }
}
