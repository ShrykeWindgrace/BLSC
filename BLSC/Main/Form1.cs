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
using System.Configuration;

namespace BLSC
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

        public const int maxPanels = 4;
        public const int vskip = 5;
        public const int hskip = 5;
        public const int ycoord = 150;
        public int globalCounter = 100;
        public string currentProj = "";

        public Form1()
        {
            InitializeComponent();
        }
        //user control events
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
            this.Size = new System.Drawing.Size(1000, 800);
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
            panel1.Height = 100;
            panel1.BackColor = Color.BurlyWood;
            buttonPlus.Dock = DockStyle.Fill;

            button1.Location = new Point(15, menuStrip1.Height + 2);
            textBox1.Location = new Point(button1.Location.X + button1.Width + 5, button1.Location.Y);

            buttonAddField.Location = new Point(button1.Location.X, button1.Location.Y + button1.Width + vskip);
            buttonRemLastField.Location = new Point(buttonAddField.Location.X + buttonAddField.Width + hskip,
                buttonAddField.Location.Y);

            ButtonTestOrderOnPanel.Location = new Point(this.ClientSize.Width - hskip - ButtonTestOrderOnPanel.Width, menuStrip1.Height + vskip);
            textBox2.Width = ButtonTestOrderOnPanel.Width;
            textBox2.Location = new Point(ButtonTestOrderOnPanel.Location.X,
                ButtonTestOrderOnPanel.Location.Y + ButtonTestOrderOnPanel.Height + vskip);

            var anc = AnchorStyles.Right | AnchorStyles.Top;

            textBox2.Anchor = anc;
            //Point(this.ClientSize.Width - hskip - buttonTesttSerialisation.Width,
            //ButtonTestOrderOnPanel.Location.Y+  ButtonTestOrderOnPanel.Height + vskip);

            buttonTesttSerialisation.Height = 24;
            buttonTesttSerialisation.Location = new Point(this.ClientSize.Width - hskip - buttonTesttSerialisation.Width,
                textBox2.Height + textBox2.Location.Y + vskip);
            buttonTesttSerialisation.Anchor = anc;

            buttonDeserialiseField.Location = new Point(this.ClientSize.Width - hskip - buttonDeserialiseField.Width,
               buttonTesttSerialisation.Height + buttonTesttSerialisation.Location.Y + hskip);

            buttonDeserialiseField.Anchor = anc;

            buttonPopulateField.Location = new Point(-hskip - buttonPopulateField.Width + buttonDeserialiseField.Location.X,
                buttonDeserialiseField.Location.Y);
            buttonPopulateField.Anchor = anc;

            buttonResetEntry.Width = 100;
            buttonResetEntry.Height = buttonRemLastField.Height;
            buttonResetEntry.Location = new Point(buttonRemLastField.Location.X + buttonRemLastField.Width + hskip,
                buttonRemLastField.Location.Y);

            comboBoxEntrySelector.Location = new Point(buttonAddField.Location.X,
                buttonAddField.Location.Y - vskip - comboBoxEntrySelector.Height);
            comboBoxEntrySelector.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxEntrySelector.Items.AddRange(Enum.GetNames(typeof(EEType)));


            // clBtn = new List<Button>();

            field = new Field();
            ABox = new AboutBox1();

            ReadSettings();
            this.Text = "Working on the project: " + currentProj;

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
        private void button7_Click(object sender, EventArgs e)
        {
            if (plist.Count >= 1)
            {
                foreach (Control c in plist[0].Controls)
                {
                    textBox2.Text += c.Name;
                }
            }
            //resetPanels();

        }
        private void button8_Click(object sender, EventArgs e)
        {
            appendPanel();
        }
        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("This is the Biblatex Style creator software, v0.0.0.1\r\n © Timofey Zakrevskiy, 2014","About" , MessageBoxButtons.OK);
            ABox.Show();
        }

        private void quitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
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

        static public void SerializeToXML(Field field)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Field));
            TextWriter textWriter = new StreamWriter(@field.type + ".xml");
            serializer.Serialize(textWriter, field);
            textWriter.Close();
        }

        //panel works
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
        private void resetPanels()
        {
            while (plist.Count > 1)
            {
                removePanel(0);
            }
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

        private void appendPanel()
        {
            int i = plist.Count;
            plist.Add(new Panel());
            populatePanel(plist[i - 1]);
            buttonPlus.Parent = plist[i];
            plist[i].Height = 100;
            plist[i].Width = 224;
            buttonPlus.Dock = DockStyle.Fill;
            this.Controls.Add(plist[i]);
            relocatePanels();
        }
        private void EntryToPanels(Entry e)
        {
            resetPanels();
            foreach (var field in e.fields)
            {
                //appendPanel();
                FieldToLastPanel(field);
            }
        }

        private void FieldToLastPanel(Field field)
        {
            //throw new NotImplementedException();
            appendPanel();
            int i = plist.Count - 1;
            foreach (Control c in plist[i].Controls)
            {
                if (c.Name.StartsWith("ComboBoxO"))
                {
                    //f.type = (c as ComboBox).Text;
                    (c as ComboBox).SelectedIndex = (c as ComboBox).FindStringExact(field.ToString());
                }
                if (c.Name.StartsWith("ComboBoxP"))
                {
                    (c as ComboBox).SelectedIndex = (c as ComboBox).FindStringExact(field.ps.p.ToComboString());
                    //f.ps.p = ((c as ComboBox).SelectedItem as CBItem).value;
                }
                if (c.Name.StartsWith("CheckedListBoxO"))
                {
                    FStoCBindex((c as CheckedListBox), field.fs);
                }
                if (c.Name.StartsWith("CheckedListBoxP"))
                {
                    FStoCBindex((c as CheckedListBox), field.ps.fs);
                }
            }

        }
        //populate panels
        private void populatePanel(Panel panel)
        {
            panel.BackColor = Color.BurlyWood;
            panel.Width = 224;

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

            Button insBtn = new Button();
            insBtn.Parent = panel;
            populateIBtn(insBtn);
            insBtn.Name = "ButtonInsertPanel" + globalCounter.ToString();

            //panel.Controls.Add(clBt);
            //panel.Controls.Add(ord);
            //panel.Controls.Add(ost);
            //panel.Controls.Add(pst);
            //panel.Controls.Add(pu);

            globalCounter++;//panel is done, all its child controls are activated.
        }

        private void populateIBtn(Button btn)
        {
            btn.Text = "+";
            btn.Width = 24;
            btn.Dock = DockStyle.Right;
            btn.Click += new EventHandler(insertPanelBtn);
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
        private void insertPanelBtn(object sender, EventArgs e)
        {//Onclick event for insert buttons of panels
            //throw new NotImplementedException();
            int i = plist.IndexOf((Panel)(((Button)sender).Parent)) + 1;
            Panel p = new Panel();
            populatePanel(p);
            plist.Insert(i, p);
            p.Visible = false;
            this.Controls.Add(p);
            relocatePanels();
            p.Visible = true;
        }
        private void removePanelBtn(object sender, EventArgs e)
        {//onclick event for remove buttons on panels
            //int i = ((Button)sender).Parent.
            //int index = clBtn.Where<Button>( x => return x  ==(sender as Button); ).Select<Button,int>( x => clBtn.IndexOf(x)).Single<int>();
            int i = plist.IndexOf((Panel)(((Button)sender).Parent));
            removePanel(i);
        }

        public Field PanelToField(Panel p)
        {
            Field f = new Field();
            f.ps = new Punctstyle();

            foreach (Control c in p.Controls)
            {
                if (c.Name.StartsWith("ComboBoxO"))
                {
                    f.type = (c as ComboBox).Text;
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            resetPanels();
            //need to add the method to drop the flag "changed" in the corresponding entry
        }

        //Latex
        private void compileLatex()
        {
            string strCmdText = "/C latexmk test -pdf";
            System.Diagnostics.Process.Start("CMD.exe", strCmdText);
        }

        private void clearAuxLatexFiles()
        {
            string strCmdText = "/C latexmk test -c";
            System.Diagnostics.Process.Start("CMD.exe", strCmdText);
        }



        //static private  void ReadAllSettings()
        //{
        //    try
        //    {
        //        var appSettings = ConfigurationManager.AppSettings;

        //        if (appSettings.Count == 0)
        //        {
        //            Console.WriteLine("AppSettings is empty.");
        //        }
        //        else
        //        {
        //            foreach (var key in appSettings.AllKeys)
        //            {
        //                Console.WriteLine("Key: {0} Value: {1}", key, appSettings[key]);
        //            }
        //        }
        //    }
        //    catch (ConfigurationErrorsException)
        //    {
        //        Console.WriteLine("Error reading app settings");
        //    }
        //}
        private void ReadSettings()
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                if (appSettings.Count > 0)
                {
                    //string result
                    currentProj = appSettings["CP"] ?? "Not Found";
                }
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
            }

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
