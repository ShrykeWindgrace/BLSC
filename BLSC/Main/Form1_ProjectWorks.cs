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
        

        private void wipeProject()
        {//wipe the project;
            // throw new NotImplementedException(); 
        }

        private bool closeProject(clSource ClSource)
        {
            if (ProjectNeedsSaving)
            {
                DialogResult DR = MessageBox.Show("The current project has some unsaved changes." + MessageStrings.Mstring(ClSource),
                    "Attention", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                switch (DR)
                {
                    case DialogResult.Cancel:
                        return false;
                    case DialogResult.No:
                        return true;
                    case DialogResult.Yes:
                        SaveEntry(null, null);
                        saveToolStripMenuItem_Click(null, null);
                        return true;
                    default:
                        return false;
                }
            }
            else
            {
                return true;
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
