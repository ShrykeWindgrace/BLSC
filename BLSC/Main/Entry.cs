using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
//using StringWorks;

namespace BLSC
{
    public enum EEType
    {
        article = 0,
        book = 1,
        booklet,
        inbook,
        incollection,
        inproceedings,// ≈ conference
        manual,
        mastersthesis,
        phdthesis,
        misc,
        proceedings,
        techreport,
        unpublished
    };//entry enummerator. Supports BibTeX entry types.



    [Serializable]
    public class EType
    {
        public EEType etype;
        public EType()
        {
            etype = EEType.article;
        }
        public EType(EEType et)
        {
            etype = et;
        }
        public override string ToString()
        {
            switch (etype)
            {
                case EEType.article:
                    return "article";
                case EEType.book:
                    return "book";
                case EEType.booklet:
                    return "booklet";
                case EEType.inbook:
                    return "inbook";
                case EEType.incollection:
                    return "incollection";
                case EEType.inproceedings:
                    return "inproceedings";
                case EEType.manual:
                    return "manual";
                case EEType.mastersthesis:
                    return "mastersthesis";
                case EEType.phdthesis:
                    return "phdthesis";
                case EEType.misc:
                    return "misc";
                case EEType.proceedings:
                    return "proceedings";
                case EEType.techreport:
                    return "techreport";
                case EEType.unpublished:
                    return "unpublished";
                default:
                    return "error";
            }
        }



    }
    [Serializable]
    public class Entry
    {
        public List<Field> fields;
        private bool myCF=false;
        public bool changedFlag
        {
            get
            {
                return myCF;
            }
            set
            {
                myCF = value;
                foreach (Field f in fields)
                {
                    f.changed = value;
                }
            }
        }
        public EType etype;

        public string DBDString;
        public string DFFString;

        public Entry()
        {
            etype = new EType();
            
            fields = new List<Field>();
            changedFlag = false;
        }

        public Entry(EEType eet)
        {
            etype = new EType(eet);
            
            fields = new List<Field>();
            changedFlag = false;
        }

        public void resetDefault() { changedFlag = false; }//this will reset the order of elements to default; also it will strike down the flag "changedFlag"
        public void exportToControlStrings()
        {
            /*we will put all formatting and envelopes to "declare field formats"
             and try to move all punctuation and ordering to drivers*/
            //  Tuple<string, string> t = new Tuple<string, string>("", "");
            //first string contains DFF and the second one contains DBD.
            DBDString = "";
            DFFString = "";
            if (changedFlag)
            {
                DBDString += "\\DeclareBibliographyDriver" + StringWorks.insymb(etype.ToString(), '{') + "{%\r\n";
                foreach (Field field in fields)
                {
                    DFFString += ("\\DeclareFieldFormat" + StringWorks.insymb(etype.ToString(), '[')
                        + field.exportDFF());
                    DBDString += field.exportDBD();
                    // t.Item1.
                    // FieldFormats += field.
                }
                DBDString += "}%\r\n";

            }//this will export all the formatting to the corresponding driver
            /*order of elements, punctuation, quotations...*/

        }
    }
}
