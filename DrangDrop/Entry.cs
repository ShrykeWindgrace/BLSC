using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using LaTeXCompilation;

namespace DrangDrop
{
    class Entry
    {
        public List<Field> fields;
        public bool changedFlag;


        public void resetDefault() { }//this will reset the order of elements to default; also it will strike down the flag "changedFlag"
        public void exportToControlStrings(string FieldFormats, string FieldOrder)
        {
            /*we will put all formatting and envelopes to "declare field formats"
             and try to move all punctuation and ordering to drivers*/
            if (changedFlag)
            {
                foreach (Field field in fields)
                {
                    // FieldFormats += field.
                }

            }//this will export all the formatting to the corresponding driver
            /*order of elements, punctuation, quotations...*/


        }
    }
}
