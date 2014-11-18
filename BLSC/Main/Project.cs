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
        public String name = "";//name of the future style. Not necessarily the same as the filename
        public String inheritedCS = "numeric-comp";
        //public something to implement other settings
        public Project()
        {
            entries = new Entry[Enum.GetNames(typeof(EEType)).Length];
            foreach (EEType eet in Enum.GetValues(typeof(EEType)))
            {
                entries[(int)eet] = new Entry(eet);
            }
            name = "test";
            //inheritedCS = 
        }
        public Project(String pname)
        {
            entries = new Entry[Enum.GetNames(typeof(EEType)).Length];
            foreach (EEType eet in Enum.GetValues(typeof(EEType)))
            {
                entries[(int)eet] = new Entry(eet);
            }
            name = pname;
        }
    }
}
