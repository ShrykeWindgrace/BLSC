using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;

namespace DrangDrop
{
    public partial class Form1 : Form
    {


        //public List<ComboBox> order;
        //public List<ComboBox> punctuation;
        //public List<CheckedListBox> ostyle;
        //public List<CheckedListBox> pstyle;
        public List<Panel> plist;
        //public List<Button> clBtn;

        public Field field;
        public AboutBox1 ABox;

        public const int maxPanels = 5;
        public const int vskip = 5;
        public const int hskip = 5;
        public const int ycoord = 250;
        public int globalCounter = 100;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            button1.DoDragDrop(button1.Text, DragDropEffects.Copy | DragDropEffects.Move);
        }

        private void textBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void textBox1_DragDrop(object sender, DragEventArgs e)
        {
            textBox1.Text += e.Data.GetData(DataFormats.Text).ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Set up the form.
            //this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.White;
            this.ForeColor = Color.Black;
            // this.Size = new System.Drawing.Size(655, 265);
            this.Text = "Test for XML and drag n drop  elements";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;

            //order = new List<ComboBox>();
            //punctuation = new List<ComboBox>();
            //ostyle = new List<CheckedListBox>();
            //pstyle = new List<CheckedListBox>();

            plist = new List<Panel>();
            plist.Add(panel1);
            panel1.Location = new Point(15, ycoord);

            // clBtn = new List<Button>();

            field = new Field();
            ABox = new AboutBox1();

        }
        static public void SerializeToXML(Field field)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Field));
            TextWriter textWriter = new StreamWriter(@field.type + ".xml");
            serializer.Serialize(textWriter, field);
            textWriter.Close();
        }

        public void button2_Click(object sender, EventArgs e)//test serialisation
        {
            Field test = new Field();
            test.type = "title";
            test.changed = true;
            test.fs = FontStyle.Bold;
            test.ps = new Punctstyle(EPunct.comma, FontStyle.Regular);
            test.envsl = new List<Envelopestyle>();
            test.envsl.Add(new Envelopestyle());
            test.envsl.Add(new Envelopestyle(Envelope.quote, FontStyle.Italic));
            // test.ps.symb = Punct.none;
            //Field test2 = new Field();
            SerializeToXML(test);







            //XmlSerializer serializer = new XmlSerializer(typeof(Punctstyle));
            //TextWriter textWriter = new StreamWriter(@"ps.xml");
            //serializer.Serialize(textWriter, test.ps);
            //textWriter.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            appendPanel();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //int i = order.Count - 1;
            int j = plist.Count - 1;
            if (j >= 1)
            {


                this.Controls.Remove(plist[j]);
                plist[j - 1].Controls.Clear();
                buttonPlus.Parent = plist[j - 1];
                buttonPlus.BringToFront();
                plist.RemoveAt(j);
            }
            //relocatePanels(2);

        }

        private void button5_Click(object sender, EventArgs e)
        {
            //int i = plist.Count;
            //if (i != 0)
            //{
            //    field.type = order[0].Text;
            //    field.ps = new Punctstyle();
            //    field.ps.fs = clbtoFS(pstyle[0]);
            //    field.fs = clbtoFS(ostyle[0]);
            //    field.ps.p = (punctuation[0].SelectedItem as CBItem).value;
            //    SerializeToXML(field);
            //}
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int i = plist.Count - 1;
            if (i == 0) { button3_Click(null, null); }
            XmlSerializer serializer = new XmlSerializer(typeof(Field));
            using (FileStream fileStream = new FileStream("author2.xml", FileMode.Open))
            {
                Field result = (Field)serializer.Deserialize(fileStream);
                //SerializeToXML(result);
                //plist[0].SelectedIndex = (int)result.ps.p.p; не умеем влазить в панельку
            }//Здесь мы научились писать и читать xml с данными о размётке

        }

        private FontStyle clbtoFS(CheckedListBox clb)
        {
            FontStyle fs = FontStyle.Regular;
            if (clb.GetItemCheckState(0) == CheckState.Checked)
            {
                fs |= FontStyle.Bold;
            }
            if (clb.GetItemCheckState(1) == CheckState.Checked)
            {
                fs |= FontStyle.Italic;
            }
            return fs;

        }
        private void FStoCBindex(CheckedListBox clb, FontStyle fs)
        {

            if ((fs & FontStyle.Bold) != 0)
            {
                clb.SetItemCheckState(0, CheckState.Checked);
            }
            else
            {
                clb.SetItemCheckState(0, CheckState.Unchecked);
            }
            if ((fs & FontStyle.Italic) != 0)
            {
                clb.SetItemCheckState(1, CheckState.Checked);
            }
            else
            {
                clb.SetItemCheckState(1, CheckState.Unchecked);
            }

        }

        private void button8_Click(object sender, EventArgs e)
        {

            appendPanel();
        }
        private void removePanel(int i)
        {
            //plist[i].Controls.Remove(order[i]);
            //plist[i].Controls.Remove(punctuation[i]);
            //plist[i].Controls.Remove(pstyle[i]);
            //plist[i].Controls.Remove(ostyle[i]);
            //plist[i].Controls.Remove(clBtn[i]);
            // plist[i].Controls.Remove(clBtn[i]);
            this.Controls.Remove(plist[i]);//removed everything from the form


            plist.RemoveAt(i);
            //order.RemoveAt(i);
            //ostyle.RemoveAt(i);
            //pstyle.RemoveAt(i);
            //clBtn.RemoveAt(i);
            //punctuation.RemoveAt(i);//removed everything from the lists

            relocatePanelsIndex(i);//reorder panels

            //I wonder what happens with those poor objects. Does the garbage collector kicks in?

        }
        private void relocatePanels()
        {
            relocatePanelsIndex(0);
        }
        private void relocatePanelsIndex(int index)
        {

            for (int j = index; j < plist.Count; j++)
            {
                plist[j].Location = new Point((j % maxPanels == 0) ? 15 : (plist[j - 1].Location.X + plist[j - 1].Width + hskip),
                  ycoord + ((j == 0) ? 0 : ((j / maxPanels) * (plist[j - 1].Height + vskip))));
            }

        }
        private void removePanelBtn(object sender, EventArgs e)
        {
            //int i = ((Button)sender).Parent.
            //int index = clBtn.Where<Button>( x => return x  ==(sender as Button); ).Select<Button,int>( x => clBtn.IndexOf(x)).Single<int>();
            int i = plist.IndexOf((Panel)(((Button)sender).Parent));
            removePanel(i);
        }


        private void populatePanel(Panel panel)
        {
            panel.BackColor = Color.BurlyWood;

            ComboBox pu = new ComboBox();
            populateCBP(pu);
            pu.Parent = panel;
            pu.Name = "ComboBoxPunct" + globalCounter.ToString();


            ComboBox ord = new ComboBox();
            populateOrd(ord);
            ord.Parent = panel;
            ord.Name = "ComboBoxOrder" + globalCounter.ToString();


            CheckedListBox ost = new CheckedListBox();
            populateFS(ost);
            ost.Parent = panel;
            ost.Name = "CheckedListBoxOrder" + globalCounter.ToString();


            CheckedListBox pst = new CheckedListBox();
            populateFS(pst, false);
            pst.Parent = panel;
            pst.Name = "CheckedListBoxPunct" + globalCounter.ToString();


            Button clBt = new Button();
            clBt.Parent = panel;
            populateXBtn(clBt);
            clBt.Name = "ButtonClosePanel" + globalCounter.ToString();


            panel.Controls.Add(clBt);
            panel.Controls.Add(ord);
            panel.Controls.Add(ost);
            panel.Controls.Add(pst);
            panel.Controls.Add(pu);

            globalCounter++;//panel is done, all its child controls are activated.
        }

        private void populateXBtn(Button btn)
        {
            btn.Text = "X";
            btn.Width = 24;
            btn.Dock = DockStyle.Right;
            btn.Click += new EventHandler(removePanelBtn);
        }

        private void populateOrd(ComboBox ord)
        {
            ord.Items.Add(new Field() { type = "author1" });
            ord.Items.Add(new Field() { type = "journal1" });//change this!
            ord.DropDownStyle = ComboBoxStyle.DropDownList;
            ord.SelectedIndex = 0;
            ord.Location = new Point(0, 0);
        }

        private void populateCBP(ComboBox pu)
        {

            foreach (EPunct p in Enum.GetValues(typeof(EPunct)))
            {
                pu.Items.Add(new CBItem(p));//;
                //i++;
            }
            pu.DropDownStyle = ComboBoxStyle.DropDownList;
            pu.SelectedIndex = 0;
            pu.Width = 50;
            pu.Location = new Point(126, 0);

        }
        private void populateFS(CheckedListBox cb, bool left = true)
        {
            cb.Items.Add("Bold", false);
            cb.Items.Add("Italic", false);
            cb.Height = 40;
            cb.Location = new Point(left ? 0 : 126, 35);
            if (!left)
            {
                cb.Width = 50;
            }
        }
        private void appendPanel()
        {
            int i = plist.Count;
            plist.Add(new Panel());
            populatePanel(plist[i - 1]);
            buttonPlus.Parent = plist[i];
            this.Controls.Add(plist[i]);
            relocatePanels();
        }
        public Field PanelToField(Panel p)
        {
            Field f = new Field();
            f.ps = new Punctstyle();

            foreach (Control c in p.Controls)
            {
                if (c.Name.StartsWith("ComboBoxO")){
                    f.type=(c as ComboBox).Text;
                }
                if (c.Name.StartsWith("ComboBoxP"))
                {
                    f.ps.p = ((c as ComboBox).SelectedItem as CBItem).value;
                }
                if (c.Name.StartsWith("CheckedListBoxO"))
                {
                    f.fs = clbtoFS((CheckedListBox)c);
                }
                if (c.Name.StartsWith("CheckedListBoxP"))
                {
                    f.ps.fs = clbtoFS((CheckedListBox)c);
                }
            }
            return f;
            //    field.type = order[0].Text;
            //    field.ps = new Punctstyle();
            //    field.ps.fs = clbtoFS(pstyle[0]);
            //    field.fs = clbtoFS(ostyle[0]);
            //    field.ps.p = (punctuation[0].SelectedItem as CBItem).value;
            //    SerializeToXML(field);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            foreach (Control c in plist[1].Controls)
            {
                textBox2.Text += c.Name;
            }
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("This is the Biblatex Style creator software, v0.0.0.1\r\n © Timofey Zakrevskiy, 2014","About" , MessageBoxButtons.OK);
            ABox.Show();
        }



        //private void comboBox3_SelectionChangeCommitted(object sender, EventArgs e)
        //{
        //    CBItem item = (CBItem)comboBox3.SelectedItem;
        //    textBox2.Text = item.value.ToString();
        //}

        //private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    CBItem item = (CBItem)comboBox3.SelectedItem;
        //    textBox2.Text = item.value.ToString();
        //}


    }
}
