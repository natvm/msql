namespace msql
{
    partial class fQueryW
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
            this.components = new System.ComponentModel.Container();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tssIcon = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssTextState = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssHostServer = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssDatabase = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssRowsAffected = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuCut = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuExecute = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssIcon,
            this.tssTextState,
            this.tssHostServer,
            this.tssUser,
            this.tssDatabase,
            this.tssTime,
            this.tssRowsAffected});
            this.statusStrip1.Location = new System.Drawing.Point(0, 383);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(659, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tssIcon
            // 
            this.tssIcon.Image = global::msql.Properties.Resources.plug_minus;
            this.tssIcon.Name = "tssIcon";
            this.tssIcon.Size = new System.Drawing.Size(16, 17);
            // 
            // tssTextState
            // 
            this.tssTextState.Name = "tssTextState";
            this.tssTextState.Size = new System.Drawing.Size(628, 17);
            this.tssTextState.Spring = true;
            this.tssTextState.Text = "Disconnect";
            this.tssTextState.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tssHostServer
            // 
            this.tssHostServer.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.tssHostServer.Name = "tssHostServer";
            this.tssHostServer.Size = new System.Drawing.Size(68, 19);
            this.tssHostServer.Text = "192.168.1.7";
            this.tssHostServer.Visible = false;
            // 
            // tssUser
            // 
            this.tssUser.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.tssUser.Name = "tssUser";
            this.tssUser.Size = new System.Drawing.Size(92, 19);
            this.tssUser.Text = "root@localhost";
            this.tssUser.Visible = false;
            // 
            // tssDatabase
            // 
            this.tssDatabase.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.tssDatabase.Name = "tssDatabase";
            this.tssDatabase.Size = new System.Drawing.Size(53, 19);
            this.tssDatabase.Text = "tempdb";
            this.tssDatabase.Visible = false;
            // 
            // tssTime
            // 
            this.tssTime.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.tssTime.Name = "tssTime";
            this.tssTime.Size = new System.Drawing.Size(53, 19);
            this.tssTime.Text = "00:00:00";
            this.tssTime.Visible = false;
            // 
            // tssRowsAffected
            // 
            this.tssRowsAffected.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.tssRowsAffected.Name = "tssRowsAffected";
            this.tssRowsAffected.Size = new System.Drawing.Size(63, 19);
            this.tssRowsAffected.Text = "1598 rows";
            this.tssRowsAffected.Visible = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.splitContainer1.Panel2Collapsed = true;
            this.splitContainer1.Size = new System.Drawing.Size(659, 383);
            this.splitContainer1.SplitterDistance = 219;
            this.splitContainer1.TabIndex = 2;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCut,
            this.mnuCopy,
            this.mnuPaste,
            this.toolStripSeparator1,
            this.mnuExecute});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(115, 98);
            // 
            // mnuCut
            // 
            this.mnuCut.Image = global::msql.Properties.Resources.scissors;
            this.mnuCut.Name = "mnuCut";
            this.mnuCut.Size = new System.Drawing.Size(114, 22);
            this.mnuCut.Text = "Cut";
            this.mnuCut.Click += new System.EventHandler(this.mnuCut_Click);
            // 
            // mnuCopy
            // 
            this.mnuCopy.Image = global::msql.Properties.Resources.document_copy;
            this.mnuCopy.Name = "mnuCopy";
            this.mnuCopy.Size = new System.Drawing.Size(114, 22);
            this.mnuCopy.Text = "Copy";
            this.mnuCopy.Click += new System.EventHandler(this.mnuCopy_Click);
            // 
            // mnuPaste
            // 
            this.mnuPaste.Image = global::msql.Properties.Resources.clipboard_text;
            this.mnuPaste.Name = "mnuPaste";
            this.mnuPaste.Size = new System.Drawing.Size(114, 22);
            this.mnuPaste.Text = "Paste";
            this.mnuPaste.Click += new System.EventHandler(this.mnuPaste_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(111, 6);
            // 
            // mnuExecute
            // 
            this.mnuExecute.Image = global::msql.Properties.Resources.compile_error;
            this.mnuExecute.Name = "mnuExecute";
            this.mnuExecute.Size = new System.Drawing.Size(114, 22);
            this.mnuExecute.Text = "Execute";
            this.mnuExecute.Click += new System.EventHandler(this.mnuExecute_Click);
            // 
            // fQueryW
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(659, 405);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "fQueryW";
            this.Text = "fQueryW";
            this.Load += new System.EventHandler(this.fQueryW_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tssIcon;
        private System.Windows.Forms.ToolStripStatusLabel tssTextState;
        private System.Windows.Forms.ToolStripStatusLabel tssHostServer;
        private System.Windows.Forms.ToolStripStatusLabel tssUser;
        private System.Windows.Forms.ToolStripStatusLabel tssDatabase;
        private System.Windows.Forms.ToolStripStatusLabel tssTime;
        private System.Windows.Forms.ToolStripStatusLabel tssRowsAffected;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuCut;
        private System.Windows.Forms.ToolStripMenuItem mnuCopy;
        private System.Windows.Forms.ToolStripMenuItem mnuPaste;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuExecute;
        private System.Windows.Forms.Timer timer1;
    }
}