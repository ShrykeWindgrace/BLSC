using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using LaTeXCompilation;

namespace DrangDrop
{
    public enum EEType {article=0, book =1 };//entry enummerator. WIP
    [Serializable]
    public class EType{
        public EEType etype;
        public EType(){
            etype = EEType.article;
        }
        public EType(EEType et){
            etype=et;
        }
        public override string ToString(){
            switch (etype)
            {
                case EEType.article:
                    return "article";
                    //break;
                case EEType.book:
                    return "book";
                    //break;
                default:
                    return "error";
                    //break;
            }
        }

    }
    class Entry
    {
        public List<Field> fields;
        public bool changedFlag;
        public EType etype;
        public Entry()
        {
            etype = new EType();
            changedFlag = false;
            fields = new List<Field>();
        }

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
