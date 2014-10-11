using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace TestingStrings
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        bool initials =  true; //whether we typeset full words or only initials
        string initialsSymbol = "";
        string initialsSplit = "";
        
        

        private void button2_Click(object sender, EventArgs e) 
        {
           // button2.Text = Regex.Matches(textBox1.Text,"sub").Count.ToString();
            int commas = textBox1.Text.Count(c=> c==',');
            string answer = "";
            switch (commas)
            {
                case 0:
                    answer = "First";
                    FirstTypeNameProcessor(textBox1.Text);
                    break;
                case 1:
                    answer = "Second";
                    break;
                case 2:
                    answer = "Third";
                    break;
                default:
                    answer = "Wtf???";
                    break;
            }
            if (!answer.Contains('?')){
                answer+=" form of names";
            }
            label1.Text=answer;

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
  /*      public void FirstTypeNameProcessor(string fullName)
        {
            fullName = fullName.Trim();
            string[] words = fullName.Split(new string[] {" "}, System.StringSplitOptions.RemoveEmptyEntries);
            if (words.Length<=1) 
            {
                textBox2.Text = "Not enough words in your name, wtf?";
            }
            else
            {
                bool[] upper = words.Select(x=>startsUppercase(x)).ToArray();
                if (upper.Count(c=>c)==upper.Length)//all words are uppercase; by default we will take 
                    //the last word as a Family name and averything else as a given name; this behaviour might
                    //change
                {
                    int i = 0;
                    string fn = "";
                    string vn = "";
                    string ln = "";
                    while (upper[i])
                    {
                        fn += words[i];
                        fn += " ";
                        i++;
                    }
                    fn = fn.Trim();
                    i++;
                    while (!upper[i])
                    {
                        vn += words[i];
                        vn += " ";
                        i++;
                    }

                    for (int i = 0; i < words.Length-2; i++)
                    {
                        if (upper[i])
                        {
                            fn += words[i];
                            //if (i<words.Length-2)
                            //{
                                fn += " ";
                            //}
                        }   
                    }
                    fn += words[words.Length - 2];
                    ln = words[words.Length - 1];
                   
                    textBox2.Text = fn;
                    textBox3.Text = vn;

                    textBox4.Text = ln;

                    //words = words.Where(o => o != (words.Length)).ToArray();
                    //string fn = string.Join(" ", words);//this typesets the names. Should be changed to reflect other options
                    //textBox2.Text = fn;


                }
               
            }
        }*/
        public string[] FirstTypeNameProcessor(string fullName)
        {
            string[] words = fullName.Split(new string[] { " " }, System.StringSplitOptions.RemoveEmptyEntries);
            string fn = "";
            string vn = "";
            string ln = "";
            int f = 0;
            int l = words.Length - 2;

            switch (words.Length)
            {
                case 0:
                    fn = "empty name, wtf";
                    //textBox3.Text = words.Length.ToString();
                    break;
                case 1:
                    ln = words[0];
                    vn = "we are in the case 1";
                    break;
                default:
                    bool[] upper = words.Select(x => startsUppercase(x)).ToArray();
                    while (upper[f]&&f<words.Length-1)
                    {
                        f++;
                    }
                    while (upper[l]&&l>f)
                    {
                        l--;
                    }
                    l++;
                    for (int i = 0; i < f; i++)
                    {
                        fn += words[i];
                        fn += " ";
                    }
                    fn.TrimEnd();
                    for (int i = f; i < l; i++)
                    {
                        vn += words[i];
                        vn += " ";
                    }
                    vn.TrimEnd();
                    for (int i = l; i < words.Length; i++)
                    {
                        ln += words[i];
                        ln += " ";
                    }
                    ln.TrimEnd();
                    break;
            }
            
            textBox2.Text = fn;
            textBox3.Text = vn;
            textBox4.Text = ln;
            return new string[]{fn, vn, ln, ""};
            
        }
        public bool startsUppercase(string word)
        {
            return !string.IsNullOrEmpty(word) && char.IsUpper(word[0]);
        }
        public string FirstNameProcessor(string fn){
            return fn;//заглушка
        }
    }
}
/*
 * current name recognition rules are compatible with conditions of BiBTeX up to a certain degree:
 * splitting on the base of white spaces and capitalisation of true characters.
 * Typesetting is not working.
 * 
 */

