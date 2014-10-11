using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using LaTeXCompilation.StringWorks;


namespace LaTeXCompilation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Compose a string that consists of three lines.
            string lines = "First line.\r\nSecond line.\r\nThird line.\r\néèêúûéð";

            using (StreamWriter writer = new StreamWriter("TeX\\text.txt", false, Encoding.UTF8))
            {
                writer.WriteLine(lines);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string strCmdText = "/C latexmk test -pdf";
            System.Diagnostics.Process.Start("CMD.exe", strCmdText);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string strCmdText = "/C latexmk test -c";
            System.Diagnostics.Process.Start("CMD.exe", strCmdText);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string headerlines = "\\ProvidesFile{testStyle.bbx}[2014/09/11 v1.1d biblatex test bibliography style]\r\n\\RequireBibliographyStyle{numeric-comp}\r\n\r\n";
            headerlines += "\\ExecuteBibliographyOptions{ firstinits,  maxnames= 5,  maxcitenames  = 2,\r\n useprefix,}\r\n";
            string optionlines = "\\renewcommand*{\\bibinitdelim}"; 
            optionlines += checkBox1.Checked ? "{\\space}" : "{}";
            optionlines += "\r\n";
            optionlines += "\\renewcommand*{\\bibinitperiod}";
            optionlines += checkBox2.Checked ? "{\\adddot}" : "{}";
//\renewcommand*{\newunitpunct}{\addcomma\space}";

            string articleLines = "#1";
            articleLines = StringWorks.embraceBibEmph(articleLines, checkedListBox1.GetItemCheckState(1) == System.Windows.Forms.CheckState.Checked);
            articleLines = StringWorks.embraceBibBold(articleLines, checkedListBox1.GetItemCheckState(2) == System.Windows.Forms.CheckState.Checked);
            articleLines = StringWorks.embraceBibQuote(articleLines, checkedListBox1.GetItemCheckState(0) == System.Windows.Forms.CheckState.Checked,true);

            articleLines = "\\DeclareFieldFormat[article]{title}{" + articleLines + "\\addcomma\\space\\nopunct}";
 
            //articleLines += "\\addcomma\\space\\nopunct}";//just some stuff that we will obviously change later
            /*\DeclareFieldFormat[article]{title}{#1\addcomma\space\nopunct}
\DeclareFieldFormat[thesis]{title}{\mkbibemph{#1}\addcomma\space\nopunct}
             */

            using (StreamWriter writer = new StreamWriter("testStyle.bbx", false, Encoding.UTF8))
            {
                writer.WriteLine(headerlines);//header for the style
                writer.WriteLine(optionlines);//write options
                writer.WriteLine(articleLines);//write stuff about the article (no quotations/paranthese atm)
            }
            using (StreamWriter writer = new StreamWriter("testStyle.cbx", false, Encoding.UTF8))
            {
                writer.WriteLine("\\ProvidesFile{testStyle.cbx}[some info]%\r\n\\RequireCitationStyle{numeric-comp}%");
            }//write citation style
            MessageBox.Show("Style loaded", "Done", MessageBoxButtons.OK);

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            showNameExample();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            showNameExample();
        }
        private void showNameExample()
        {
            string example = "J";
            example += checkBox2.Checked ? "." : "";
            example += checkBox1.Checked ? " " : "";
            example += "E";
            example += checkBox2.Checked ? "." : "";
            example += " Doe";
            richTextBox1.Clear();
            richTextBox1.AppendText(example);
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            richTextBox2.Clear();
            //richTextBox1.SelectionFont = new Font("Tahoma", 12, FontStyle.Bold);
            FontStyle f = FontStyle.Regular;
            f |= (checkedListBox1.GetItemCheckState(1)==System.Windows.Forms.CheckState.Checked) ? FontStyle.Italic : FontStyle.Regular;
            f |= (checkedListBox1.GetItemCheckState(2) == System.Windows.Forms.CheckState.Checked) ? FontStyle.Bold : FontStyle.Regular;
            richTextBox2.SelectionFont = new Font("Tahoma", 12, f);

            string article = "An Article about Nothing";
            article =
                StringWorks.inParens(article, checkedListBox1.GetItemCheckState(0) == System.Windows.Forms.CheckState.Checked);
            
            richTextBox2.AppendText(article);
        }




        
    }
}
