/* This file groups everything that concerns the inner workings of panels.
 * All interaction with external controls stay in the Form.cs
 * 
 * Note that this separation is unorthodox in C# paradigm.
 */
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
    {//here we hide everything related to saving/opening/closing/managing projects.
        private bool LoadProjectFromXml(string p)
        {
            bool result = true;
            if (String.IsNullOrEmpty(p))
            {
                return false;
            }
            XmlSerializer serializer = new XmlSerializer(typeof(Project));
            try
            {
                using (FileStream fileStream = new FileStream(p, FileMode.Open))
                {

                    project = (Project)serializer.Deserialize(fileStream);
                }
            }
            catch //(InvalidOperationException)
            {
                result = false;
                MessageBox.Show("Something went wrong on deserialisation", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            if (result)
            {
                currentProj = project.name;
            }
            else
            {
                currentProj = "";
            }
            return result;
        }

        private void wipeProject()
        {//wipe the project;
            // throw new NotImplementedException(); 
            project = new Project();
            ProjectToControls(project);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveEntry(null, null);
            if (Saved())
            {
                //MessageBox.Show("Saved", currentProj, MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            else
            {
                MessageBox.Show("Problems with saving", currentProj, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SavedAs();
        }

        private bool Saved()
        {
            bool result = true;
            try
            {

                //string result
                string s = Properties.Settings.Default.CPFname;
                if (!(String.IsNullOrEmpty(s)))
                {
                    if (SerializeProjectToXML(s))
                    {
                        MessageBox.Show("Project saved", "Done", MessageBoxButtons.OK);//need to catch some exceptions here imho
                        ProjectNeedsSaving = false;
                    }
                    else
                    {
                        result = false;
                    }
                }
                else
                {
                    //MessageBox.Show("Problem with application settings: no project file reference", "Warning!", MessageBoxButtons.OK);
                    result = SavedAs();
                }

            }
            catch
            {
                MessageBox.Show("Something went wrong with saving", "Error", MessageBoxButtons.OK);
                result = false;
            }
            return result;
        }

        private bool SavedAs()//here goes the code for "saving as"
        {
            //Stream myStream;
            bool result = true;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "All files (*.*)|*.*|blscxml files (*.blscxml)|*.blscxml";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.FileName = Properties.Settings.Default.CP;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (!String.IsNullOrEmpty(saveFileDialog1.FileName))
                {
                    if (SerializeProjectToXML(saveFileDialog1.FileName))
                    {
                        currentProj = Path.GetFileNameWithoutExtension(saveFileDialog1.FileName);
                        //string CPFname = saveFileDialog1.FileName;
                        Properties.Settings.Default.CPFname = saveFileDialog1.FileName;

                        Properties.Settings.Default.CP = currentProj;
                        Properties.Settings.Default.Save();
                        ProjectNeedsSaving = false;
                        MessageBox.Show("Saved", currentProj, MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                    else
                    {
                        result = false;
                        MessageBox.Show("Problems with saving", currentProj, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else
                {
                    result = false;
                }
            }
            else
            {
                result = false;
            }
            return result;//Надо всё переписать, слишком много условий
        }

        public bool SerializeProjectToXML(String fname)
        {
            bool result = true;
            XmlSerializer ser = new XmlSerializer(typeof(Project));
            try
            {
                TextWriter tw = new StreamWriter(fname);

                ser.Serialize(tw, project);

                tw.Close();
                //ser.Serialize(tw,)
            }
            catch
            {
                MessageBox.Show("Error with serialisation", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                result = false;
            }
            return result;

        }


        private bool closeProject(clSource ClSource)
        {
            if (ProjectNeedsSaving)
            {
                DialogResult DR = MessageBox.Show("The current project has some unsaved changes. " + MessageStrings.Mstring(ClSource),
                    "Attention", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                switch (DR)
                {
                    case DialogResult.Cancel:
                        return false;
                    case DialogResult.No:
                        return true;
                    case DialogResult.Yes:
                        SaveEntry(null, null);
                        //  saveToolStripMenuItem_Click(null, null);
                        return Saved();
                    default:
                        return false;
                }
            }
            else
            {
                return true;
            }
        }

        private bool LoadAndShowProject(String fname)
        {
            bool result = false;
            if (LoadProjectFromXml(fname))
            {
                EntryToPanels(project.entries[0]);
                result = true;
                currentProj = project.name;
            }
            else
            {
                MessageBox.Show("Loading of the file " + fname + " failed",
                    "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return result;
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "All files (*.*)|*.*|blscxml files (*.blscxml)|*.blscxml";
            ofd.FilterIndex = 2;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                LoadAndShowProject(ofd.FileName);
            }
        }

    }
    public enum clSource { regular = 0, newproject = 1, formclose = 2, openproject }
    public static class MessageStrings
    {
        public static String Mstring(clSource cs)
        {
            String res = "Would you like to save changes before ";
            switch (cs)
            {
                case clSource.regular:
                    res += "closing this project?";
                    break;
                case clSource.newproject:
                    res += "starting new project?";
                    break;
                case clSource.formclose:
                    res += "closing this program?";
                    break;
                case clSource.openproject:
                    res += "opening another project?";
                    break;
                //default:
                //    break;
            }
            return res;
        }
    }
}
