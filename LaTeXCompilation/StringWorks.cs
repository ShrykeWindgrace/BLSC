using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace LaTeXCompilation
{
    public class StringWorks
    {
        public static string embraceBibQuote(string word, bool doit = false, bool biber=false)
        {
            string temp = word;
            if (doit)
            {
                if (biber)
                {
                    temp = "\\mkbibquote{" + word + "}";
                }
                else{
                    temp = "\""+word+"\"";
                }
            }

            return temp;

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
        
        public static string inQuotes(string word, bool doit = false)
        {
            if (doit)
            {
                return "\"" + word + "\"";
            }
            else
            {
                return word;
            }
        }



        public static string inbr(string word)
        {
            return "{" + word + "}";
        }
        public static string incr(string word)
        {
            return "[" + word + "]";
        }
        public static string inParens(string word, bool doit)
        {
            return doit ? "(" + word + ")" : word;
        }
        public static string insymb(string word, char s)
        {
            switch (s)
            {
                case '(':
                    return inParens(word, true);
                case '{':
                    return inbr(word);
                case '[':
                    return incr(word);
                default:
                    return word;
            }
        }

    }
}
