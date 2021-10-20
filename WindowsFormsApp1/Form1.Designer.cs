namespace WindowsFormsApp1
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.TSCBFormLayout = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.TSMIpost_message = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMIthread_name = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMIfile_name_2 = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMIgif = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMIhr = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMIhc = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMIr = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMIs = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMIb = new System.Windows.Forms.ToolStripMenuItem();
            this.BPrevious = new System.Windows.Forms.ToolStripButton();
            this.TbStart = new System.Windows.Forms.ToolStripTextBox();
            this.TbEnd = new System.Windows.Forms.ToolStripTextBox();
            this.TbFetch = new System.Windows.Forms.ToolStripTextBox();
            this.BNext = new System.Windows.Forms.ToolStripButton();
            this.TbSearch = new System.Windows.Forms.ToolStripTextBox();
            this.BSearch = new System.Windows.Forms.ToolStripButton();
            this.CBSpecial = new System.Windows.Forms.ToolStripComboBox();
            this.CBMostUsedWords = new System.Windows.Forms.ToolStripComboBox();
            this.Pan = new System.Windows.Forms.Panel();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.toolStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 2, 0);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSCBFormLayout,
            this.toolStripDropDownButton1,
            this.BPrevious,
            this.TbStart,
            this.TbEnd,
            this.TbFetch,
            this.BNext,
            this.TbSearch,
            this.BSearch,
            this.CBSpecial,
            this.CBMostUsedWords});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1492, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // TSCBFormLayout
            // 
            this.TSCBFormLayout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.TSCBFormLayout.Items.AddRange(new object[] {
            "Standard",
            "SmallGrid"});
            this.TSCBFormLayout.Name = "TSCBFormLayout";
            this.TSCBFormLayout.Size = new System.Drawing.Size(121, 25);
            this.TSCBFormLayout.Text = "Layout...";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMIpost_message,
            this.TSMIthread_name,
            this.TSMIfile_name_2,
            this.TSMIgif,
            this.TSMIhr,
            this.TSMIhc,
            this.TSMIr,
            this.TSMIs,
            this.TSMIb});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(77, 22);
            this.toolStripDropDownButton1.Text = "Search in...";
            // 
            // TSMIpost_message
            // 
            this.TSMIpost_message.Checked = true;
            this.TSMIpost_message.CheckOnClick = true;
            this.TSMIpost_message.CheckState = System.Windows.Forms.CheckState.Checked;
            this.TSMIpost_message.Name = "TSMIpost_message";
            this.TSMIpost_message.Size = new System.Drawing.Size(148, 22);
            this.TSMIpost_message.Text = "post_message";
            // 
            // TSMIthread_name
            // 
            this.TSMIthread_name.Checked = true;
            this.TSMIthread_name.CheckOnClick = true;
            this.TSMIthread_name.CheckState = System.Windows.Forms.CheckState.Checked;
            this.TSMIthread_name.Name = "TSMIthread_name";
            this.TSMIthread_name.Size = new System.Drawing.Size(148, 22);
            this.TSMIthread_name.Text = "thread_name";
            // 
            // TSMIfile_name_2
            // 
            this.TSMIfile_name_2.Checked = true;
            this.TSMIfile_name_2.CheckOnClick = true;
            this.TSMIfile_name_2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.TSMIfile_name_2.Name = "TSMIfile_name_2";
            this.TSMIfile_name_2.Size = new System.Drawing.Size(148, 22);
            this.TSMIfile_name_2.Text = "file_name_2";
            // 
            // TSMIgif
            // 
            this.TSMIgif.Checked = true;
            this.TSMIgif.CheckOnClick = true;
            this.TSMIgif.CheckState = System.Windows.Forms.CheckState.Checked;
            this.TSMIgif.Name = "TSMIgif";
            this.TSMIgif.Size = new System.Drawing.Size(148, 22);
            this.TSMIgif.Text = "/gif/";
            // 
            // TSMIhr
            // 
            this.TSMIhr.Checked = true;
            this.TSMIhr.CheckOnClick = true;
            this.TSMIhr.CheckState = System.Windows.Forms.CheckState.Checked;
            this.TSMIhr.Name = "TSMIhr";
            this.TSMIhr.Size = new System.Drawing.Size(148, 22);
            this.TSMIhr.Text = "/hr/";
            // 
            // TSMIhc
            // 
            this.TSMIhc.Checked = true;
            this.TSMIhc.CheckOnClick = true;
            this.TSMIhc.CheckState = System.Windows.Forms.CheckState.Checked;
            this.TSMIhc.Name = "TSMIhc";
            this.TSMIhc.Size = new System.Drawing.Size(148, 22);
            this.TSMIhc.Text = "/hc/";
            // 
            // TSMIr
            // 
            this.TSMIr.Checked = true;
            this.TSMIr.CheckOnClick = true;
            this.TSMIr.CheckState = System.Windows.Forms.CheckState.Checked;
            this.TSMIr.Name = "TSMIr";
            this.TSMIr.Size = new System.Drawing.Size(148, 22);
            this.TSMIr.Text = "/r/";
            // 
            // TSMIs
            // 
            this.TSMIs.Checked = true;
            this.TSMIs.CheckOnClick = true;
            this.TSMIs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.TSMIs.Name = "TSMIs";
            this.TSMIs.Size = new System.Drawing.Size(148, 22);
            this.TSMIs.Tag = "";
            this.TSMIs.Text = "/s/";
            // 
            // TSMIb
            // 
            this.TSMIb.Checked = true;
            this.TSMIb.CheckOnClick = true;
            this.TSMIb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.TSMIb.Name = "TSMIb";
            this.TSMIb.Size = new System.Drawing.Size(148, 22);
            this.TSMIb.Text = "/b/";
            // 
            // BPrevious
            // 
            this.BPrevious.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.BPrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.BPrevious.Image = ((System.Drawing.Image)(resources.GetObject("BPrevious.Image")));
            this.BPrevious.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BPrevious.Name = "BPrevious";
            this.BPrevious.Size = new System.Drawing.Size(56, 22);
            this.BPrevious.Text = "Previous";
            this.BPrevious.Click += new System.EventHandler(this.BPrevious_Click);
            // 
            // TbStart
            // 
            this.TbStart.Enabled = false;
            this.TbStart.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.TbStart.Name = "TbStart";
            this.TbStart.Size = new System.Drawing.Size(25, 25);
            this.TbStart.Text = "0";
            // 
            // TbEnd
            // 
            this.TbEnd.Enabled = false;
            this.TbEnd.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.TbEnd.Name = "TbEnd";
            this.TbEnd.Size = new System.Drawing.Size(25, 25);
            // 
            // TbFetch
            // 
            this.TbFetch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.TbFetch.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.TbFetch.Name = "TbFetch";
            this.TbFetch.Size = new System.Drawing.Size(25, 25);
            this.TbFetch.Text = "20";
            // 
            // BNext
            // 
            this.BNext.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.BNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.BNext.Image = ((System.Drawing.Image)(resources.GetObject("BNext.Image")));
            this.BNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BNext.Name = "BNext";
            this.BNext.Size = new System.Drawing.Size(36, 22);
            this.BNext.Text = "Next";
            this.BNext.Click += new System.EventHandler(this.BNext_Click);
            // 
            // TbSearch
            // 
            this.TbSearch.BackColor = System.Drawing.Color.Khaki;
            this.TbSearch.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.TbSearch.MaxLength = 50000;
            this.TbSearch.Name = "TbSearch";
            this.TbSearch.Size = new System.Drawing.Size(150, 25);
            // 
            // BSearch
            // 
            this.BSearch.BackColor = System.Drawing.Color.Khaki;
            this.BSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.BSearch.Image = ((System.Drawing.Image)(resources.GetObject("BSearch.Image")));
            this.BSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BSearch.Name = "BSearch";
            this.BSearch.Size = new System.Drawing.Size(46, 22);
            this.BSearch.Text = "Search";
            this.BSearch.ToolTipText = "search";
            this.BSearch.Click += new System.EventHandler(this.ToolStripButton1_Click);
            // 
            // CBSpecial
            // 
            this.CBSpecial.BackColor = System.Drawing.Color.LightSalmon;
            this.CBSpecial.Items.AddRange(new object[] {
            "Top Duplicates",
            "Top Duplicates Today",
            "Newest With Prictures Or Webms",
            "Newest With Pictures",
            "Newest All",
            "Newest Webms"});
            this.CBSpecial.Name = "CBSpecial";
            this.CBSpecial.Size = new System.Drawing.Size(175, 25);
            this.CBSpecial.Text = "Special Query...";
            this.CBSpecial.SelectedIndexChanged += new System.EventHandler(this.CBSpecial_SelectedIndexChanged);
            // 
            // CBMostUsedWords
            // 
            this.CBMostUsedWords.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.CBMostUsedWords.Name = "CBMostUsedWords";
            this.CBMostUsedWords.Size = new System.Drawing.Size(115, 25);
            this.CBMostUsedWords.Text = "Popular Words...";
            this.CBMostUsedWords.SelectedIndexChanged += new System.EventHandler(this.CBMostUsedWords_SelectedIndexChanged);
            this.CBMostUsedWords.Click += new System.EventHandler(this.CBMostUsedWords_Click);
            // 
            // Pan
            // 
            this.Pan.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Pan.AutoScroll = true;
            this.Pan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.Pan.Location = new System.Drawing.Point(0, 25);
            this.Pan.Margin = new System.Windows.Forms.Padding(0);
            this.Pan.Name = "Pan";
            this.Pan.Size = new System.Drawing.Size(1492, 425);
            this.Pan.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(1492, 450);
            this.Controls.Add(this.Pan);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "4chan";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripTextBox TbSearch;
        private System.Windows.Forms.ToolStripButton BSearch;
        private System.Windows.Forms.ToolStripComboBox CBSpecial;
        private System.Windows.Forms.ToolStripButton BPrevious;
        private System.Windows.Forms.ToolStripTextBox TbStart;
        private System.Windows.Forms.ToolStripTextBox TbEnd;
        private System.Windows.Forms.ToolStripTextBox TbFetch;
        private System.Windows.Forms.ToolStripButton BNext;
        private System.Windows.Forms.Panel Pan;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem TSMIpost_message;
        private System.Windows.Forms.ToolStripMenuItem TSMIthread_name;
        private System.Windows.Forms.ToolStripMenuItem TSMIfile_name_2;
        private System.Windows.Forms.ToolStripMenuItem TSMIgif;
        private System.Windows.Forms.ToolStripMenuItem TSMIhr;
        private System.Windows.Forms.ToolStripMenuItem TSMIhc;
        private System.Windows.Forms.ToolStripMenuItem TSMIr;
        private System.Windows.Forms.ToolStripMenuItem TSMIs;
        private System.Windows.Forms.ToolStripMenuItem TSMIb;
        private System.Windows.Forms.ToolStripComboBox CBMostUsedWords;
        private System.Windows.Forms.ToolStripComboBox TSCBFormLayout;
    }
}

