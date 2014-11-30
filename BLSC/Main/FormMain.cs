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
            }
        }
        private bool ENS;//entry need saving
        private bool PNS;//project needs saving
        private bool IsLoading = true;// this is a flag that distinguishes the origin of controls' changes true for programmatical, false for user
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
                else
                {
                    //SaveEntry(null, null);
                }
            }
        }

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

            toolStrip1.Location = new Point(0, menuStrip1.Height);

            buttonAddField.Location = new Point(15, toolStrip1.Location.Y + toolStrip1.Height + vskip);
            buttonRemLastField.Location = new Point(buttonAddField.Location.X + buttonAddField.Width + hskip,
                buttonAddField.Location.Y);

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

            LoadAndShowProject(Properties.Settings.Default.CPFname);

            //ShowPanels(EEType.article);
            EntryNeedsSaving = false;
            ProjectNeedsSaving = false;
            //IsLoading = false;

        }







        private void button3_Click(object sender, EventArgs e)
        {
            appendPanel();
        }







        private void button8_Click(object sender, EventArgs e)
        {
            appendPanel();
            EntryNeedsSaving = true;

        }
        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ABox.Show();
        }

        private void quitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
            // FormMain_FormClosing(sender, e);
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

        //static public void SerializeToXML(Field field)
        //{
        //    XmlSerializer serializer = new XmlSerializer(typeof(Field));
        //    TextWriter textWriter = new StreamWriter(@field.type + ".xml");
        //    serializer.Serialize(textWriter, field);
        //    textWriter.Close();
        //}


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
            IsLoading = true;
            EntryToPanels(project.entries[comboBoxEntrySelector.SelectedIndex]);
            IsLoading = false;

        }

        protected void SaveEntry(object sender, EventArgs e)
        {
            //if (!((sender as ComboBox).Name == "comboBoxEntrySelector"))

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
                EntryNeedsSaving = false;

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



        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.Control && e.KeyCode == Keys.S)
            //{
            //    //SerializeProjectToXML()
            //    SaveEntry(sender, e);
            //saveToolStripMenuItem_Click(null, null);
            //}
            //if (e.Control && e.KeyCode == Keys.W)//closing the project. У нас проблема - проект закрывать умеет
            ////просто закрытие, открытие нового, закрытие формы. Всё у них одинаково, только сообщения различаются. 
            ////Собственно, их и будем передавать
            //{
            //    if (closeProject(clSource.regular))
            //    {
            //        wipeProject();
            //    }
            //}
            //if (e.Control && e.KeyCode == Keys.Q)
            //{
            //    Application.Exit();
            //}
            //if (e.Control && e.KeyCode == Keys.O)
            //{
            //    openToolStripMenuItem_Click(null, null);
            //}


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

        protected void UpdateField(object sender, EventArgs e)
        {
            //this will be the autoupdate event for automatic saving of fields in current entries
            if (!IsLoading)
            {
                int i = plist.IndexOf((sender as Control).Parent as Panel);
                //EEType eet = (EEType)(comboBoxEntrySelector.SelectedItem);
                int j = comboBoxEntrySelector.SelectedIndex;
                PanelToFieldF(plist[i], project.entries[j].fields[i]);
                //project.entries[j].fields[i] = PanelToField(plist[i]);
                EntryNeedsSaving = false;
            }
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            //donothing
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            openToolStripMenuItem_Click(sender, e);
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //donothing
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            saveToolStripMenuItem_Click(sender, e);
        }

        private void compileLAtexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            compileTestFileToolStripMenuItem_Click(sender, e);
        }

        private void clearAuxLAtexFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //clearAuxLAtexFilesToolStripMenuItem_Click(sender, e);
            clearAuxFilesToolStripMenuItem_Click(sender, e);
        }



    }
}
//Где в проект вписывается измененность - как только я открываю новую вкладку, но ничего на ней не делаю.
// Нужна вменяемая процедура сохранения.
// Нужна проверка на многоопределённость полей
/* Известные баги:
 * dff без форматирования выдаёт некомпилирующийся макрос
 * откуда-то появились пустые dff
 * какие-то точки перед некоторыми записями
 * 
 * надо сделать:
 * разобраться с неправильным макросом - проверка на пустоту форматирования
 * провести внешнее тестирование на поведение этих точек
 * можно гонять на другом тестовом файлике - пара статей, пара книг, мануал
 * реализовать создание нового проекта
*/