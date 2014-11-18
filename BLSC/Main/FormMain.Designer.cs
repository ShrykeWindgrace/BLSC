namespace BLSC
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonAddField = new System.Windows.Forms.Button();
            this.buttonRemLastField = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonPlus = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.FileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exportStyleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportStyleAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.quitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.laTeXToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.compileTestFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearAuxFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonResetEntry = new System.Windows.Forms.Button();
            this.comboBoxEntrySelector = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonAddField
            // 
            this.buttonAddField.Location = new System.Drawing.Point(4, 63);
            this.buttonAddField.Margin = new System.Windows.Forms.Padding(1);
            this.buttonAddField.Name = "buttonAddField";
            this.buttonAddField.Size = new System.Drawing.Size(63, 46);
            this.buttonAddField.TabIndex = 3;
            this.buttonAddField.Text = "Add a field";
            this.buttonAddField.UseVisualStyleBackColor = true;
            this.buttonAddField.Click += new System.EventHandler(this.button3_Click);
            // 
            // buttonRemLastField
            // 
            this.buttonRemLastField.Location = new System.Drawing.Point(76, 63);
            this.buttonRemLastField.Margin = new System.Windows.Forms.Padding(1);
            this.buttonRemLastField.Name = "buttonRemLastField";
            this.buttonRemLastField.Size = new System.Drawing.Size(60, 46);
            this.buttonRemLastField.TabIndex = 4;
            this.buttonRemLastField.Text = "Remove last field";
            this.buttonRemLastField.UseVisualStyleBackColor = true;
            this.buttonRemLastField.Click += new System.EventHandler(this.button4_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.panel1.Controls.Add(this.buttonPlus);
            this.panel1.Location = new System.Drawing.Point(404, 36);
            this.panel1.Margin = new System.Windows.Forms.Padding(1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(208, 87);
            this.panel1.TabIndex = 7;
            // 
            // buttonPlus
            // 
            this.buttonPlus.AutoSize = true;
            this.buttonPlus.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonPlus.Location = new System.Drawing.Point(0, 0);
            this.buttonPlus.Margin = new System.Windows.Forms.Padding(1);
            this.buttonPlus.Name = "buttonPlus";
            this.buttonPlus.Size = new System.Drawing.Size(208, 48);
            this.buttonPlus.TabIndex = 1;
            this.buttonPlus.Text = "+";
            this.buttonPlus.UseVisualStyleBackColor = true;
            this.buttonPlus.Click += new System.EventHandler(this.button8_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem1,
            this.laTeXToolStripMenuItem,
            this.AboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(1, 1, 0, 1);
            this.menuStrip1.Size = new System.Drawing.Size(1414, 50);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // FileToolStripMenuItem1
            // 
            this.FileToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.toolStripSeparator3,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.closeToolStripMenuItem,
            this.toolStripSeparator2,
            this.exportStyleToolStripMenuItem,
            this.exportStyleAsToolStripMenuItem,
            this.toolStripSeparator1,
            this.quitToolStripMenuItem1});
            this.FileToolStripMenuItem1.Name = "FileToolStripMenuItem1";
            this.FileToolStripMenuItem1.Size = new System.Drawing.Size(75, 48);
            this.FileToolStripMenuItem1.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(293, 46);
            this.newToolStripMenuItem.Text = "New";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(290, 6);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(293, 46);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(293, 46);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(293, 46);
            this.saveAsToolStripMenuItem.Text = "Save as";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(293, 46);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(290, 6);
            // 
            // exportStyleToolStripMenuItem
            // 
            this.exportStyleToolStripMenuItem.Name = "exportStyleToolStripMenuItem";
            this.exportStyleToolStripMenuItem.Size = new System.Drawing.Size(293, 46);
            this.exportStyleToolStripMenuItem.Text = "Export Style";
            this.exportStyleToolStripMenuItem.Click += new System.EventHandler(this.exportStyleToolStripMenuItem_Click);
            // 
            // exportStyleAsToolStripMenuItem
            // 
            this.exportStyleAsToolStripMenuItem.Name = "exportStyleAsToolStripMenuItem";
            this.exportStyleAsToolStripMenuItem.Size = new System.Drawing.Size(293, 46);
            this.exportStyleAsToolStripMenuItem.Text = "Export Style As";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(290, 6);
            // 
            // quitToolStripMenuItem1
            // 
            this.quitToolStripMenuItem1.Name = "quitToolStripMenuItem1";
            this.quitToolStripMenuItem1.Size = new System.Drawing.Size(293, 46);
            this.quitToolStripMenuItem1.Text = "Quit";
            this.quitToolStripMenuItem1.Click += new System.EventHandler(this.quitToolStripMenuItem1_Click);
            // 
            // laTeXToolStripMenuItem
            // 
            this.laTeXToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.compileTestFileToolStripMenuItem,
            this.clearAuxFilesToolStripMenuItem});
            this.laTeXToolStripMenuItem.Name = "laTeXToolStripMenuItem";
            this.laTeXToolStripMenuItem.Size = new System.Drawing.Size(109, 48);
            this.laTeXToolStripMenuItem.Text = "LaTeX";
            // 
            // compileTestFileToolStripMenuItem
            // 
            this.compileTestFileToolStripMenuItem.Name = "compileTestFileToolStripMenuItem";
            this.compileTestFileToolStripMenuItem.Size = new System.Drawing.Size(311, 46);
            this.compileTestFileToolStripMenuItem.Text = "Compile test file";
            this.compileTestFileToolStripMenuItem.Click += new System.EventHandler(this.compileTestFileToolStripMenuItem_Click);
            // 
            // clearAuxFilesToolStripMenuItem
            // 
            this.clearAuxFilesToolStripMenuItem.Name = "clearAuxFilesToolStripMenuItem";
            this.clearAuxFilesToolStripMenuItem.Size = new System.Drawing.Size(311, 46);
            this.clearAuxFilesToolStripMenuItem.Text = "Clear aux files";
            this.clearAuxFilesToolStripMenuItem.Click += new System.EventHandler(this.clearAuxFilesToolStripMenuItem_Click);
            // 
            // AboutToolStripMenuItem
            // 
            this.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem";
            this.AboutToolStripMenuItem.Size = new System.Drawing.Size(112, 48);
            this.AboutToolStripMenuItem.Text = "About";
            this.AboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            // 
            // buttonResetEntry
            // 
            this.buttonResetEntry.Location = new System.Drawing.Point(616, 45);
            this.buttonResetEntry.Name = "buttonResetEntry";
            this.buttonResetEntry.Size = new System.Drawing.Size(231, 78);
            this.buttonResetEntry.TabIndex = 11;
            this.buttonResetEntry.Text = "Reset Current Entry Type";
            this.buttonResetEntry.UseVisualStyleBackColor = true;
            this.buttonResetEntry.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // comboBoxEntrySelector
            // 
            this.comboBoxEntrySelector.FormattingEnabled = true;
            this.comboBoxEntrySelector.Location = new System.Drawing.Point(895, 66);
            this.comboBoxEntrySelector.Name = "comboBoxEntrySelector";
            this.comboBoxEntrySelector.Size = new System.Drawing.Size(121, 39);
            this.comboBoxEntrySelector.TabIndex = 12;
            this.comboBoxEntrySelector.SelectedIndexChanged += new System.EventHandler(this.comboBoxEntrySelector_SelectedIndexChanged);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1414, 824);
            this.Controls.Add(this.comboBoxEntrySelector);
            this.Controls.Add(this.buttonResetEntry);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.buttonRemLastField);
            this.Controls.Add(this.buttonAddField);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(1);
            this.Name = "FormMain";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormMain_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonAddField;
        private System.Windows.Forms.Button buttonRemLastField;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonPlus;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem AboutToolStripMenuItem;
        private System.Windows.Forms.Button buttonResetEntry;
        private System.Windows.Forms.ComboBox comboBoxEntrySelector;
        private System.Windows.Forms.ToolStripMenuItem exportStyleAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exportStyleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem laTeXToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem compileTestFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearAuxFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
    }
}

