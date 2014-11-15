using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
//using LaTeXCompilation;
using System.Xml.Serialization;

namespace BLSC
{
    public enum EFType { author, title, journal, year, pages }//WIP
    public class Type
    {
        EFType t;
        public Type()
        {
            t = EFType.author;
        }
        public Type(EFType et)
        {
            t = et;
        }
        public override string ToString()
        {
            switch (t)
            {
                case EFType.author:
                    return "author";
                case EFType.title:
                    return "title";
                case EFType.journal:
                    return "journal";
                case EFType.year:
                    return "year";
                case EFType.pages:
                    return "pages";
                default:
                    return "none";
            }
        }
    }
    public enum EPunct { space = 2, dot = 3, comma = 4, none = 5, commaspace = 0, dotspace = 1 };
    //possible punctuation after blocks

    [Serializable]
    public class Punct
    {//a class that allows to export possible styles in different formats
        public EPunct p;
        public Punct()
        {
            p = EPunct.none;
        }
        public Punct(EPunct punct)
        {
            p = punct;
        }
        public string ToCommandString()//exporting to biblatex macro commands
        {
            switch (p)
            {
                case EPunct.space:
                    return "\\addspace";
                // break;
                case EPunct.dot:
                    return "\\adddot";
                //break;
                case EPunct.comma:
                    return "\\addcomma";
                //break;
                case EPunct.none:
                    return "";
                //break;
                case EPunct.commaspace:
                    return "\\addcomma\\addspace";
                case EPunct.dotspace:
                    return "\\adddot\\addspace";
                default:
                    return "";
                //break;
            }
        }
        public string ToComboString()//export to populate  combobox strings
        {
            switch (p)
            {
                case EPunct.space:
                    return "\" \"";
                case EPunct.dot:
                    return "\".\"";
                case EPunct.comma:
                    return "\",\"";
                case EPunct.none:
                    return "\"\"";
                case EPunct.commaspace:
                    return "\", \"";
                case EPunct.dotspace:
                    return "\". \"";
                default:
                    return "";
            }
        }
        public override string ToString()//override for standard method.
        {
            switch (p)
            {
                case EPunct.space:
                    return "space";
                // break;
                case EPunct.dot:
                    return "dot";
                //break;
                case EPunct.comma:
                    return "comma";
                //break;
                case EPunct.none:
                    return "no symbol";
                //break;
                case EPunct.commaspace:
                    return "comma and space";
                case EPunct.dotspace:
                    return "dot and space";
                default:
                    return "";
                //break;
            }
        }
    }


    [Serializable]
    public class Punctstyle
    {//adding possible font styles to punctuation

        public Punct p;

        public FontStyle fs;
        public Punctstyle(Punct p1, FontStyle fs1)
        {
            p = p1;
            fs = fs1;
        }
        public Punctstyle(EPunct p1, FontStyle fs1)
        {
            p = new Punct(p1);
            fs = fs1;
        }
        public Punctstyle()
        {
            p = new Punct(EPunct.none);
            fs = FontStyle.Regular;
        }
        public string exportDBD()
        {//this method sets a new unit and pushes new delimiter. cf. \setunit* documentation in biblatex.
            string temp = p.ToCommandString();
            temp = Embrace.embraceBibStyle(temp, fs, true);

            return "\\setunit*{" + temp + "}%\r\n";
        }


    }
    public enum Envelope { none = 0, parens = 1, quote = 2 };//possible envelopes for a field

    [Serializable]
    public class Envelopestyle
    {//adding possible font styles
        public Envelope env;
        public FontStyle fs;
        public Envelopestyle(Envelope e, FontStyle s)
        {
            env = e;
            fs = s;
        }
        public Envelopestyle()
        {
            env = Envelope.none;
            fs = FontStyle.Regular;
        }
        public string inEnv(string word)
        {
            string temp = "";
            switch (env)
            {
                case Envelope.none:
                    temp = word;
                    break;
                case Envelope.quote:
                    temp = Embrace.embraceBibQuote(word, true);
                    break;
                case Envelope.parens:
                    temp = Embrace.embraceBibParens(word, true);
                    break;
                default:
                    temp = word;
                    break;
            }
            return Embrace.embraceBibStyle(temp, fs, true);
        }
    }



    [Serializable]
    public class Field
    {
        public Punctstyle ps;
        public List<Envelopestyle> envsl;
        public FontStyle fs;
        public string type;

        public bool changed;

        public Field()
        {
            type = "null";
            changed = false;
            envsl = new List<Envelopestyle>();
            fs = FontStyle.Regular;
            ps = new Punctstyle();

        }
        /*this flag is raised whenever we modify the order or punctuation in this entry type
         * this flag is striken whenever we use the "resetDefault" method.
         * if at the moment of style import the flag is down, then we do nothing (i.e. the inherited driver is used)
         * if at the momnet of style import the flag is up, then we export to the field format declarations and to the driver declarations
         */
        public override string ToString()
        {
            return type;
        }
        public string exportDFF()
        {//exporting to declare field format. 
            if (!changed)
            {
                return "%\r\n";
            }
            else
            {
                //string temp = ""//"\\DeclareFieldFormat";
                //temp += StringWorks.insymb(type, '[');//переделать!

                string temp2 = "#1";

                temp2 = Embrace.embraceBibStyle(temp2, fs, true);

                foreach (Envelopestyle envs in envsl)
                {
                    temp2 = envs.inEnv(temp2);
                    temp2 = Embrace.embraceBibStyle(temp2, envs.fs, true);
                }



                return StringWorks.insymb(type, '{') + StringWorks.insymb(temp2, '{') + "%\r\n";// тут надо сильно подумать, насколько мы позволим модифицировать стиль окружающих скобок
                //пока вариант - наследовать или не наследовать стиль от самого текста. Надо проверить, что случится тогда с пустыми полями в скобках.
                /*текущий подход: снаружи стиль скобок, скобки, потом стиль текста*/
                //return temp + "\r\n";
            }
        }

        public string exportDBD()
        {//export to bibliography drivers
            //prone to modifications as not all field might have corresponding macros.
            //need further studies of the standard styles and \iffieldundef conditions
            return ps.exportDBD() + "\\usebibmacro{" + type + "}%\r\n";
        }

        // private string inStyle(string word, FontStyle fs)
        // {
        //     switch (fs)
        //     {
        //         case FontStyle.Bold:
        //             return "\\mkbibbold{" + word + "}";

        //         case FontStyle.Italic:
        //             return "\\mkbibemph{" + word + "}";
        //         case FontStyle.Regular:
        //             return word;
        //         case FontStyle.Strikeout:
        //             return word;
        //         case FontStyle.Underline:
        //             return word;
        //         default:
        //             return word;
        //     }
        //     //return "";
        // }




    }

    public class CBItem
    {//a class to make comboboxes with punctuation possible
        //public CBItem() { }
        public CBItem(Punct p)
        {
            name = p.ToComboString();
            value = p;
        }
        public CBItem(EPunct p)
        {
            value = new Punct(p);
            name = value.ToComboString();
        }
        public string name;
        public Punct value;
        public override string ToString()
        {
            return name;
        }

    }

    class Embrace
    {//a class that collect different typical string operations,
        //like enveloping in different brackets/parenthesis
        public static string embraceBibQuote(string word, bool doit = false)
        {

            return (doit ? ("\\mkbibquote{" + word + "}") : word);

        }

        public static string embraceBibParens(string word, bool doit = false)
        {
            return (doit ? ("\\mkbibparens{" + word + "}") : word);
        }
        public static string embraceBibBold(string word, bool doit = false)
        {

            return (doit ? ("\\mkbibbold{" + word + "}") : word);
        }
        public static string embraceBibEmph(string word, bool doit = false)
        {
            return (doit ? ("\\mkbibemph{" + word + "}") : word);

        }
        public static string embraceBibStyle(string word, FontStyle fs, bool doit = false)
        {
            string temp = word;
            if (doit)
            {
                temp = embraceBibEmph(temp, (FontStyle.Italic & fs) != 0);
                temp = embraceBibBold(temp, (FontStyle.Bold & fs) != 0);
            }
            return temp;
        }
    }
}
