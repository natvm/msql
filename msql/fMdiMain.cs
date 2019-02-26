using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using mysqlcore;
using MySql.Data.MySqlClient;

namespace msql
{
    public partial class fMdiMain : Form
    {
        public enum ToolbarButtons
        {
            OpenFile,
            Save,
            Undo,Redo,
            Cut,Copy,Paste,
            Connect,Disconnect,
            ChangeDatabase,
            Execute,StopQuery,Parse,
            QueryMenu
        };

        private int NroConexiones = 0;
        private Dictionary<int, OServer> _Conexiones = new Dictionary<int, OServer>();

        public fObjectExplorer fObjectExplorer = new fObjectExplorer();
        private int NroScript = 0;
        
        public fMdiMain()
        {
            InitializeComponent();
            
            //Iniciar Object Explorer
            this.fObjectExplorer.fMain = this;
            this.fObjectExplorer.Show(dockPanel1, DockState.DockLeft);
            dockPanel1.ActiveDocumentChanged += new System.EventHandler(this.dockPanel1_ActiveDocumentChanged);
            this.ShowObjectExplorer();
        }

        private void btnNewQuery_Click(object sender, EventArgs e)
        {
            this.NewScript();
        }

        private void dockPanel1_ActiveDocumentChanged(object sender, EventArgs e)
        {
            
        }

        private void fQuery_ChangeState(object sender, EventArgs e)
        {
            
        }

        private void mnuNewConnection_Click(object sender, EventArgs e)
        {
            this.NewConnection();
        }

        private void mnuObjectExplorer_Click(object sender, EventArgs e)
        {
            this.ShowObjectExplorer();
        }

        private void ShowObjectExplorer()
        {
            this.fObjectExplorer.Show();
        }

        //Funciones Genéricas

        public void NewConnection()
        {
            fConex fc = new fConex();
            fc.MyCnn = new MySqlConnection();
            fc.ShowDialog();
            if (fc.MyCnn.State == ConnectionState.Open)
            { 
                NroConexiones++;
                OServer s = new OServer(fc.MyCnn, fc.ServerName, fc.LoginName);
                this._Conexiones.Add(NroConexiones, s);
                this.TreeViewAddServer(s);
            }
            fc.Dispose();
        }

        public void Disconnect(int id)
        {
            this._Conexiones.Remove(id);
        }

        //Control de Arbol

        private void TreeViewAddServer(OServer s)
        {
            //Instalamos la raiz
            TreeNode t = new TreeNode(s.ServerName + " (" + s.ServerVersion + ")");
            t.Tag = s;
            this.fObjectExplorer.treeView1.Nodes.Add(t);
            this.TreeViewAddNode(s, t);
            t.Expand();
        }

        public void TreeViewAddNode(AObject a, TreeNode tn)
        { 
            TreeNode t;

            a.Retrieve();
            tn.Nodes.Clear();

            foreach (AObject p in a.Childs)
            {
                t = new TreeNode(p.Name, p.ItemIndex, p.ItemIndex);
                t.Tag = p;
                t.Nodes.Add("--");
                tn.Nodes.Add(t);
            }
        }

        //Barras de menu

        private void btnUndo_Click(object sender, EventArgs e)
        {
            this.Undo();
        }

        private void mnuUndo_Click(object sender, EventArgs e)
        {
            this.Undo();
        }

        private void btnRedo_Click(object sender, EventArgs e)
        {
            this.Redo();
        }

        private void mnuRedo_Click(object sender, EventArgs e)
        {
            this.Redo();
        }

        private void btnCut_Click(object sender, EventArgs e)
        {
            this.Cut();
        }

        private void mnuCut_Click(object sender, EventArgs e)
        {
            this.Cut();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            this.Copy();
        }

        private void mnuCopy_Click(object sender, EventArgs e)
        {
            this.Copy();
        }

        private void btnPaste_Click(object sender, EventArgs e)
        {
            this.Paste();
        }

        private void mnuPaste_Click(object sender, EventArgs e)
        {
            this.Paste();
        }

        private void btnNewConnection_Click(object sender, EventArgs e)
        {
            this.Connect();
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            this.Disconnect();
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            this.Execute();
        }

        private void mnuExecute_Click(object sender, EventArgs e)
        {
            this.Execute();
        }

        private void btnParse_Click(object sender, EventArgs e)
        {
            this.Parse();
        }

        //Control de Ventanas

        private void fMdiMain_MdiChildActivate(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild != null)
            {
                this.toolStrip2.Visible = true;
                this.SetControlZOrder(this.toolStrip2, 2);
            }
            else
            {
                this.toolStrip2.Visible = false;
                /*this.SetButtonEnabledState(ToolbarButtons.Undo, false);
                this.SetButtonEnabledState(ToolbarButtons.Redo, false);

                this.SetButtonEnabledState(ToolbarButtons.Cut, false);
                this.SetButtonEnabledState(ToolbarButtons.Copy, false);
                this.SetButtonEnabledState(ToolbarButtons.Paste, false);

                this.SetButtonEnabledState(ToolbarButtons.Connect, false);
                this.SetButtonEnabledState(ToolbarButtons.Disconnect, false);*/
            }
        }

#region FuncionesPrincipales

        internal fQueryW NewScript()
        {
            fQueryW f = new fQueryW();
            f.Text = "Script " + this.NroScript;
            f.ShowHint = DockState.Document;
            f.Show(this.dockPanel1);

            return f;
        }

        internal void OpenFile()
        {

            DialogResult dr;
            dr = this.openFileDialog1.ShowDialog();

            if (dr == DialogResult.OK)
            {
                string filename = this.openFileDialog1.FileName;

                fQueryW f = new fQueryW();
                f.Text = filename;

                if (f.Open(openFileDialog1.FileName) == true)
                {
                    f.ShowHint = DockState.Document;
                    f.Show(this.dockPanel1);
                }
            }
        }

        private void Save()
        {
            if (this.ActiveMdiChild != null)
            {
                fQueryW f = (fQueryW)this.ActiveMdiChild;

                if (f.Filename != null)
                {
                    f.Save(f.Filename);
                }
                else
                {
                    this.SaveAs();
                }
            }
        }

        private void SaveAs()
        {
            if (this.ActiveMdiChild != null)
            {
                DialogResult dr = this.saveFileDialog1.ShowDialog();

                if (dr == DialogResult.OK)
                {
                    fQueryW f = (fQueryW)this.ActiveMdiChild;
                    string filename = this.saveFileDialog1.FileName;

                    f.Save(filename);
                }


            }
        }

        private void Undo()
        {
            //if (this.ActiveMdiChild != null)
            //{
            //    fQuery f = (fQuery)this.ActiveMdiChild;
            //    f.Undo();
            //}
        }

        private void Redo()
        {
            //if (this.ActiveMdiChild != null)
            //{
            //    fQuery f = (fQuery)this.ActiveMdiChild;
            //    f.Redo();
            //}
        }

        private void Cut()
        {
            //if (this.ActiveMdiChild != null)
            //{
            //    fQuery f = (fQuery)this.ActiveMdiChild;
            //    f.Cut();
            //}
        }

        private void Copy()
        {
            //if (this.ActiveMdiChild != null)
            //{
            //    fQuery f = (fQuery)this.ActiveMdiChild;
            //    f.Copy();
            //}
        }

        private void Paste()
        {
            //if (this.ActiveMdiChild != null)
            //{
            //    fQuery f = (fQuery)this.ActiveMdiChild;
            //    f.Paste();
            //}
        }

        private void Connect()
        {
            if (this.ActiveMdiChild != null)
            {
                fQueryW f = (fQueryW)this.ActiveMdiChild;
                f.Connect();
            }
        }

        private void Disconnect()
        {
            if (this.ActiveMdiChild != null)
            {
                fQueryW f = (fQueryW)this.ActiveMdiChild;
                f.Disconnect();
            }
        }

        private void ChangeDatabase()
        {
            //if (this.ActiveMdiChild != null)
            //{
            //    fQuery f = (fQuery)this.ActiveMdiChild;
            //    f.ChangeDatabase(this.cmbChangeDatabase.Text);
            //}
        }

        private void Execute()
        {
            //if (this.ActiveMdiChild != null)
            //{
            //    fQuery f = (fQuery)this.ActiveMdiChild;
            //    f.Execute();
            //}
        }

        private void CancelQuery()
        {
            //if (this.ActiveMdiChild != null)
            //{
            //    fQuery f = (fQuery)this.ActiveMdiChild;
            //    f.CancelQuery();
            //}
        }

        private void Parse()
        {
            //if (this.ActiveMdiChild != null)
            //{
            //    fQuery f = (fQuery)this.ActiveMdiChild;
            //    f.Parse();
            //}
        }

#endregion

        //public void SetButtonEnabledState(ToolbarButtons t, bool IsEnabled)
        //{
        //    //switch (t)
        //    //{
        //    //    case ToolbarButtons.Save:
        //    //        this.btnSave.Enabled = IsEnabled;
        //    //        this.mnuSave.Enabled = IsEnabled;
        //    //        break;
        //    //    case ToolbarButtons.Cut:
        //    //        this.btnCut.Enabled = IsEnabled;
        //    //        this.mnuCut.Enabled = IsEnabled;
        //    //        break;
        //    //    case ToolbarButtons.Copy:
        //    //        this.btnCopy.Enabled = IsEnabled;
        //    //        this.mnuCopy.Enabled = IsEnabled;
        //    //        break;
        //    //    case ToolbarButtons.Paste:
        //    //        this.btnPaste.Enabled = IsEnabled;
        //    //        this.mnuPaste.Enabled = IsEnabled;
        //    //        break;
        //    //    case ToolbarButtons.Undo:
        //    //        this.btnUndo.Enabled = IsEnabled;
        //    //        this.mnuUndo.Enabled = IsEnabled;
        //    //        break;
        //    //    case ToolbarButtons.Redo:
        //    //        this.btnRedo.Enabled = IsEnabled;
        //    //        this.mnuRedo.Enabled = IsEnabled;
        //    //        break;
        //    //    case ToolbarButtons.Connect:
        //    //        this.btnConnect.Enabled = IsEnabled;
        //    //        this.mnuConnect.Enabled = IsEnabled;
        //    //        break;
        //    //    case ToolbarButtons.Disconnect:
        //    //        this.btnDisconnect.Enabled = IsEnabled;
        //    //        this.mnuDisconnect.Enabled = IsEnabled;
        //    //        break;
        //    //    case ToolbarButtons.ChangeDatabase:
        //    //        this.cmbChangeDatabase.Enabled = IsEnabled;
        //    //        break;
        //    //    case ToolbarButtons.Execute:
        //    //        this.btnExecute.Enabled = IsEnabled;
        //    //        this.mnuParse.Enabled = IsEnabled;
        //    //        break;
        //    //    case ToolbarButtons.Parse:
        //    //        this.btnParse.Enabled = IsEnabled;
        //    //        this.mnuParse.Enabled = IsEnabled;
        //    //        break;
        //    //    case ToolbarButtons.QueryMenu:
        //    //        this.mnuQuery.Visible = IsEnabled;
        //    //        break;
        //    //}            
        //}

        public void SetControlZOrder(Control ctrl, int z)
        {
            ctrl.Parent.Controls.SetChildIndex(ctrl, z);
        }

        private void mnuCancelQuery_Click(object sender, EventArgs e)
        {
            this.CancelQuery();
        }

        private void btnCancelQuery_Click(object sender, EventArgs e)
        {
            this.CancelQuery();
        }

        private void mnuParse_Click(object sender, EventArgs e)
        {
            this.Parse();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            this.OpenFile();
        }

        private void mnuOpen_Click(object sender, EventArgs e)
        {
            this.OpenFile();
        }

        private void mnuNewScript_Click(object sender, EventArgs e)
        {
            this.NewScript();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Save();
        }

        private void mnuSave_Click(object sender, EventArgs e)
        {
            this.Save();
        }

        private void mnuSaveAs_Click(object sender, EventArgs e)
        {
            this.SaveAs();
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            
        }

        private void mnuConnect_Click(object sender, EventArgs e)
        {
            this.Connect();
        }

        private void mnuDisconnect_Click(object sender, EventArgs e)
        {
            this.Disconnect();
        }

        private void mnuResultToText_Click(object sender, EventArgs e)
        {

        }

        private void mnuResultToGrid_Click(object sender, EventArgs e)
        {

        }

        private void cmbChangeDatabase_TextUpdate(object sender, EventArgs e)
        {
            this.ChangeDatabase();
        }

        private void fMdiMain_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild == null)
            {
                this.btnCut.Enabled = false;
                this.btnCopy.Enabled = false;
                this.btnPaste.Enabled = false;
            }
            else
            {
                fQueryW f = (fQueryW) this.ActiveMdiChild;
                this.btnUndo.Enabled = f.CanUndo;
                this.btnRedo.Enabled = f.CanRedo;

                this.btnCut.Enabled = f.CanCut;
                this.btnCopy.Enabled = f.CanCopy;
                this.btnPaste.Enabled = f.CanPaste;

                this.btnConnect.Enabled = f.CanConnect;
                this.btnDisconnect.Enabled = f.CanDisconnect;
                this.cmbChangeDatabase.Enabled = f.CanDisconnect;


            }

        }

    }
}
