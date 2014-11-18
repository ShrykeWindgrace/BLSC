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
    public partial class FormMain : Form
    {

        public Project project;

        public Field field;
        public AboutBox1 ABox;
        public Button btnExport;
        public Button btnSaveCurrentEntry;

        public const int maxPanels = 4;
        public const int vskip = 5;
        public const int hskip = 5;
        public const int ycoord = 150;
        public int globalCounter = 100;

        private string CP = "";
        public string currentProj
        {
            get
            {
                return CP;
            }
            set
            {
                CP = value;
                this.Text = "Working on project: " + value;
                //AddUpdateAppSettings("CP", value);
                //AddUpdateAppSettings("myset", "set");
            }
        }
        private bool ENS;//entry need saving
        private bool PNS;//project needs saving
        public bool EntryNeedsSaving
        {
            get
            {
                return ENS;
            }
            set
            {
                ENS = value;
                btnSaveCurrentEntry.Enabled = value;
                if (value)
                {
                    ProjectNeedsSaving = value;//i.e. if we need to save internally one entry, then we need to save the project, too.
                }
            }
        }//А может, ну его лесом? Пусть данные форм сохраняются немедленно по изменению контрола?
        //И все изменения останутся только в текущем состоянии, в текст-то ничего же не уйдёт
        //Пока идея мне нравится

        //Ещё  нужна кнопка на ресет текущего проекта
        //и на закрытие текущего проекта
        public bool ProjectNeedsSaving
        {
            get
            {
                return PNS;
            }
            set
            {
                PNS = value;
                //btnSaveCurrentEntry.Enabled = value;

            }
        }
        public FormMain()
        {
            InitializeComponent();
        }
        //user control events



        private void Form1_Load(object sender, EventArgs e)
        {
            //Set up the form.
            //this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.White;
            this.ForeColor = Color.Black;
            this.Size = new System.Drawing.Size(1000, 800);
            //this.Text = "Test for XML and drag n drop  elements";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;


            // entries = new Entry[Enum.GetNames(typeof(EEType)).Length];
            project = new Project();

            btnSaveCurrentEntry = new Button();
            btnSaveCurrentEntry.Name = "btnSaveCurrentEntry";
            btnSaveCurrentEntry.Text = "save current entry";
            btnSaveCurrentEntry.AutoSize = true;
            btnSaveCurrentEntry.AutoEllipsis = false;
            btnSaveCurrentEntry.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            /*btnSaveCurrentEntry.location = new point(-hskip + this.clientsize.width - btnSaveCurrentEntry.width,
                buttonDeserialiseField.Location.Y+buttonDeserialiseField.Height+vskip);*/
            //btnSaveCurrentEntry.Location = new Point(450, 120);

            btnSaveCurrentEntry.Click += new EventHandler(SaveEntry);

            this.Controls.Add(btnSaveCurrentEntry);

            plist = new List<Panel>();
            plist.Add(panel1);
            panel1.Location = new Point(15, ycoord);
            panel1.Height = 100;
            panel1.BackColor = Color.BurlyWood;
            buttonPlus.Dock = DockStyle.Fill;


            buttonAddField.Location = new Point(15, menuStrip1.Height + vskip);
            buttonRemLastField.Location = new Point(buttonAddField.Location.X + buttonAddField.Width + hskip,
                buttonAddField.Location.Y);


            var anc = AnchorStyles.Right | AnchorStyles.Top;




            buttonResetEntry.Width = 100;
            buttonResetEntry.Height = buttonRemLastField.Height;
            buttonResetEntry.Location = new Point(buttonRemLastField.Location.X + buttonRemLastField.Width + hskip,
                buttonRemLastField.Location.Y);

            btnSaveCurrentEntry.Location = new Point(buttonResetEntry.Location.X + buttonResetEntry.Width + hskip,
buttonResetEntry.Location.Y);

            comboBoxEntrySelector.Location = new Point(buttonAddField.Location.X,
                buttonAddField.Location.Y + vskip + buttonAddField.Height);
            comboBoxEntrySelector.DropDownStyle = ComboBoxStyle.DropDownList;


            foreach (EEType eet in Enum.GetValues(typeof(EEType)))
            {
                comboBoxEntrySelector.Items.Add(new EType(eet));
            }
            comboBoxEntrySelector.SelectedIndex = 0;

            field = new Field();
            ABox = new AboutBox1();

            btnExport = new Button();
            btnExport.Name = "btnExport";
            btnExport.Text = "Export current project";
            btnExport.AutoSize = true;
            btnExport.AutoEllipsis = false;
            btnExport.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            /*btnexport.location = new point(-hskip + this.clientsize.width - btnexport.width,
                buttonDeserialiseField.Location.Y+buttonDeserialiseField.Height+vskip);*/
            btnExport.Location = new Point(btnSaveCurrentEntry.Location.X,
                btnSaveCurrentEntry.Location.Y + vskip + btnSaveCurrentEntry.Height);
            btnExport.Click += new EventHandler(exportToTex);
            this.Controls.Add(btnExport);



            //ReadSettings();
            currentProj = Properties.Settings.Default.CP;
            if (LoadFromXml(Properties.Settings.Default.CPFname))
            {
                EntryToPanels(project.entries[0]);
            }
            else
            {
                MessageBox.Show("Loading of the file " + Properties.Settings.Default.CPFname + " failed",
                    "Error",
                    MessageBoxButtons.OK);
            }
            //ShowPanels(EEType.article);
            EntryNeedsSaving = false;

        }



        private bool LoadFromXml(string p)
        {
            bool result = true;
            XmlSerializer serializer = new XmlSerializer(typeof(Project));
            using (FileStream fileStream = new FileStream(p, FileMode.Open))
            {
                try
                {
                    project = (Project)serializer.Deserialize(fileStream);
                }

                catch (InvalidOperationException)
                {
                    result = false;
                    MessageBox.Show("Something went wrong on deserialisation", "Error", MessageBoxButtons.OK);

                }
            }
            return result;


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

        private void button8_Click(object sender, EventArgs e)
        {
            appendPanel();
            EntryNeedsSaving = true;

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

        public void SerializeProjectToXML(String fname)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Project));
            TextWriter tw = new StreamWriter(fname);

            ser.Serialize(tw, project);

            tw.Close();
            //ser.Serialize(tw,)


        }
        //panel works




        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            resetPanels();
            EEType eet = (comboBoxEntrySelector.SelectedItem as EType).etype;
            project.entries[(int)eet] = new Entry(eet);
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


        private void exportToTex(object sender, EventArgs e)
        {
            //donothing, atm
            //string header = "";
            string header = "\\ProvidesFile{" + currentProj + ".bbx}[" + DateTime.Today.ToString("yyyy-MM-dd") + " biblatex " + currentProj
                + " bibliography style]\r\n\\RequireBibliographyStyle{numeric-comp}\r\n\r\n";
            header += "\\ExecuteBibliographyOptions{ firstinits,  maxnames= 5,  maxcitenames  = 2,\r\n useprefix,}\r\n";
            string formats = "";
            string drivers = "";
            foreach (Entry en in project.entries)
            {
                //formats += e.exportToControlStrings();
                en.exportToControlStrings();
                formats += en.DFFString;
                drivers += en.DBDString;
            }
            using (StreamWriter writer = new StreamWriter(currentProj + ".bbx", false, Encoding.UTF8))
            {
                writer.WriteLine(header);//header for the style
                writer.WriteLine("%code for declarefieldformats");
                writer.WriteLine(formats);//write options
                writer.WriteLine("%code for declarebibdrivers");
                writer.WriteLine(drivers);//write stuff about the article (no quotations/paranthese atm)
            }
            using (StreamWriter writer = new StreamWriter(currentProj + ".cbx", false, Encoding.UTF8))
            {
                writer.WriteLine("\\ProvidesFile{" + currentProj + ".cbx}[some info]%\r\n\\RequireCitationStyle{numeric-comp}%");
            }//write citation style
            MessageBox.Show("Style loaded", "Done", MessageBoxButtons.OK);
        }

        private void comboBoxEntrySelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnContentChanged(sender, e);
            EntryToPanels(project.entries[(int)((comboBoxEntrySelector.SelectedItem as EType).etype)]);

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
        protected void SaveEntry(object sender, EventArgs e)
        {
            //if (!((sender as ComboBox).Name == "comboBoxEntrySelector"))
            EntryNeedsSaving = false;
            {
                //int i = plist.IndexOf( ((sender as ComboBox).Parent as Panel));
                //we can do a very unefficient algorithm of total rewriting of fields upon cnaging one of them
                project.entries[(int)((comboBoxEntrySelector.SelectedItem as EType).etype)].fields
                   = new List<Field>();

                List<Field> lf = project.entries[(int)((comboBoxEntrySelector.SelectedItem as EType).etype)].fields;
                if (plist.Count > 1)
                {
                    for (int i = 0; i < plist.Count - 1; i++)
                    {
                        lf.Add(new Field());
                        PanelToFieldF(plist[i], lf[i]);//почему-то не заполняеются строчки. Даже догадываюсь, почему
                        //lf[i].changed = true;

                        //lf.Add(PanelToField(plist[i]));
                    }
                    project.entries[(int)((comboBoxEntrySelector.SelectedItem as EType).etype)].changedFlag = true;
                }
                //MessageBox.Show("we see total fields:",
                //    //lf.Count.ToString(),
                //    entries[(int)((comboBoxEntrySelector.SelectedItem as EType).etype)].fields.Count.ToString(),
                //    MessageBoxButtons.OK);


            }
        }

        private void exportStyleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            exportToTex(sender, e);
        }

        private void compileTestFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            compileLatex();
        }

        private void clearAuxFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clearAuxLatexFiles();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //  var appSettings = ConfigurationManager.AppSettings;
                //if (appSettings.Count > 0)
                {
                    //string result
                    string s = Properties.Settings.Default.CPFname;
                    if (!(String.IsNullOrEmpty(s)))
                    {
                        SerializeProjectToXML(s);
                        MessageBox.Show("Project saved", "Done", MessageBoxButtons.OK);//need to catch some exceptions here imho
                    }
                    else
                    {
                        //MessageBox.Show("Problem with application settings: no project file reference", "Warning!", MessageBoxButtons.OK);
                        saveAsToolStripMenuItem_Click(sender, e);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Something went wrong with saving", "Error", MessageBoxButtons.OK);
            }
        }

        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                //SerializeProjectToXML()
                SaveEntry(sender, e);
                saveToolStripMenuItem_Click(null, null);
            }
            if (e.Control && e.KeyCode == Keys.W)//closing the project. У нас проблема - проект закрывать умеет
            //просто закрытие, открытие нового, закрытие формы. Всё у них одинаково, только сообщения различаются. 
            //Собственно, их и будем передавать
            {
                if (closeProject(clSource.regular))
                {
                    wipeProject();
                }
            }

        }

     






        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Saved())
            {
                MessageBox.Show("Saved", currentProj, MessageBoxButtons.OK,MessageBoxIcon.None);
            }
            else
            {
                MessageBox.Show("Problems with saving", currentProj, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool Saved()
        {
            //Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "All files (*.*)|*.*|blscxml files (*.blscxml)|*.blscxml";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.FileName = Properties.Settings.Default.CP;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (!String.IsNullOrEmpty(saveFileDialog1.FileName))
                {
                    SerializeProjectToXML(saveFileDialog1.FileName);
                    currentProj = Path.GetFileNameWithoutExtension(saveFileDialog1.FileName);
                    //string CPFname = saveFileDialog1.FileName;
                    Properties.Settings.Default.CPFname = saveFileDialog1.FileName;
                    
                    Properties.Settings.Default.CP = currentProj;
                    Properties.Settings.Default.Save();

                }
                //if ((myStream = saveFileDialog1.OpenFile()) != null)
                //{
                //    // Code to write the stream goes here.
                //    myStream.Close();
                //}
            }
            return (saveFileDialog1.ShowDialog() == DialogResult.OK);
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !closeProject(clSource.formclose);
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (closeProject(clSource.regular))
            {
                wipeProject();
            }
        }



    }
}
