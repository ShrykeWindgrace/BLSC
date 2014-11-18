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
    public enum clSource { regular = 0, newproject = 1, formclose = 2, openproject }
    public static class MessageStrings
    {
        public static String Mstring(clSource cs)
        {
            String res = "Would you like to save changes before ";
            switch (cs)
            {
                case clSource.regular:
                    res += "closing this project?";
                    break;
                case clSource.newproject:
                    res += "starting new project?";
                    break;
                case clSource.formclose:
                    res += "closing this program?";
                    break;
                case clSource.openproject:
                    res += "opening another project?";
                    break;
                //default:
                //    break;
            }
            return res;
        }
    }
}
