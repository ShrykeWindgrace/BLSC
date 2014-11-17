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
    {

        public List<Panel> plist;
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

        //panel works
        private void removePanel(int i)
        {

            this.Controls.Remove(plist[i]);//removed everything from the form
            plist.RemoveAt(i);
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
            NeedsSaving = false;
        }

        private void FieldToLastPanel(Field field)
        {
            //throw new NotImplementedException();
            appendPanel();
            int i = plist.Count - 2;
            foreach (Control c in plist[i].Controls)
            {
                if (c.Name.StartsWith("ComboBoxO"))
                {
                    //MessageBox.Show("Found ComboBoxO", "yes", MessageBoxButtons.OK);
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

            globalCounter++;//panel is done, all its child controls are activated.

            foreach (ComboBox control in panel.Controls.OfType<ComboBox>())
            {
                control.SelectedIndexChanged += new EventHandler(OnContentChanged);
            }
            foreach (CheckedListBox control in panel.Controls.OfType<CheckedListBox>())
            {
                //clb.
                control.ItemCheck += new ItemCheckEventHandler(OnCheckedItemChange);//new ItemCheckEventHandler(OnContentChanged);
            }
        }

        private void OnCheckedItemChange(object sender, ItemCheckEventArgs e)
        {
            OnContentChanged(sender, null);
        }

        protected void OnContentChanged(object sender, EventArgs e)
        {
            NeedsSaving = true;
            //if (!((sender as ComboBox).Name == "comboBoxEntrySelector"))
           /* {
                //int i = plist.IndexOf( ((sender as ComboBox).Parent as Panel));
                //we can do a very unefficient algorithm of total rewriting of fields upon cnaging one of them
                entries[(int)((comboBoxEntrySelector.SelectedItem as EType).etype)].fields
                   = new List<Field>();
                
                List<Field> lf = entries[(int)((comboBoxEntrySelector.SelectedItem as EType).etype)].fields;
                if (plist.Count > 1)
                {
                    for (int i = 0; i < plist.Count - 1; i++)
                    {
                        lf.Add(new Field());
                        PanelToFieldF(plist[i], lf[i]);//почему-то не заполняеются строчки. Даже догадываюсь, почему
                        //lf[i].changed = true;

                        //lf.Add(PanelToField(plist[i]));
                    }
                    entries[(int)((comboBoxEntrySelector.SelectedItem as EType).etype)].changedFlag = true;
                }
                //MessageBox.Show("we see total fields:",
                //    //lf.Count.ToString(),
                //    entries[(int)((comboBoxEntrySelector.SelectedItem as EType).etype)].fields.Count.ToString(),
                //    MessageBoxButtons.OK);


            }*/
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
            //ord.Items.Add(new Field() { type = "author1" });
            //ord.Items.Add(new Field() { type = "journal1" });//change this!
            foreach (EFType eft in Enum.GetValues(typeof(EFType)))
            {
                ord.Items.Add(new Type(eft));
            }
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
            NeedsSaving = true;
        }
        private void removePanelBtn(object sender, EventArgs e)
        {//onclick event for remove buttons on panels
            //int i = ((Button)sender).Parent.
            //int index = clBtn.Where<Button>( x => return x  ==(sender as Button); ).Select<Button,int>( x => clBtn.IndexOf(x)).Single<int>();
            int i = plist.IndexOf((Panel)(((Button)sender).Parent));
            removePanel(i);
            NeedsSaving = true;
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
        }
        public void PanelToFieldF(Panel p, Field f)
        {
            //Field f = new Field();
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
            //return f;
        }
    }
}
