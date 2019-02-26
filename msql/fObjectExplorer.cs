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
    public partial class fObjectExplorer : DockContent
    {
        public fMdiMain fMain;
        public fObjectExplorer()
        {
            InitializeComponent();
        }

        private void fObjectExplorer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
        }

        private void fObjectExplorer_Load(object sender, EventArgs e)
        {

        }

        private void btnNewConnection_Click(object sender, EventArgs e)
        {
            this.AddConnection();
        }

        private void btnRemoveConnection_Click(object sender, EventArgs e)
        {

        }

        /*******Generico*********/

        public void AddConnection()
        {
            this.fMain.NewConnection();
        }

        public void Disconnect()
        {

        }

        private void treeView1_AfterExpand(object sender, TreeViewEventArgs e)
        {
            TreeNode t = e.Node;
            
            if (t.Nodes[0].Text == "--")
                this.fMain.TreeViewAddNode((AObject)t.Tag, t);
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            AObject p = (AObject)e.Node.Tag;
            

            if (p.GetType().Name == "ODatabase")
            {
                ODatabase q = (ODatabase)p;
                q.Select();
            }
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            { 
                //Generar Menu
                AObject p = (AObject)e.Node.Tag;
                this.GenerateContextualMenu(p);
                e.Node.ContextMenuStrip = this.contextMenuStrip1;
                contextMenuStrip1.Show();
            }
        }

        private void GenerateContextualMenu(AObject p)
        {
            string clsname = p.GetType().ToString();
            SortedList<int,CommandDb> mndb;
            CommandDb cmdb;

            this.contextMenuStrip1.Items.Clear();

            if (clsname == "mysqlcore.OServer")
            {
                OServer s = (OServer)p;
                mndb = s.GetContextualMenu();
            }
            else if (clsname == "mysqlcore.FDatabases")
            {
                FDatabases s = (FDatabases)p;
                mndb = s.GetContextualMenu();
            }
            else if (clsname == "mysqlcore.ODatabase")
            {
                ODatabase s = (ODatabase)p;
                mndb = s.GetContextualMenu();
            }
            else if (clsname == "mysqlcore.FTables")
            {
                FTables s = (FTables)p;
                mndb = s.GetContextualMenu();
            }
            else if (clsname == "mysqlcore.OTable")
            {
                OTable s = (OTable)p;
                mndb = s.GetContextualMenu();
            }
            else if (clsname == "mysqlcore.FColumns")
            {
                FColumns s = (FColumns)p;
                mndb = s.GetContextualMenu();
            }
            else if (clsname == "mysqlcore.OColumn")
            {
                OColumn s = (OColumn)p;
                mndb = s.GetContextualMenu();
            }
            else if (clsname == "mysqlcore.FIndexes")
            {
                FIndexes s = (FIndexes)p;
                mndb = s.GetContextualMenu();
            }
            else if (clsname == "mysqlcore.OIndex")
            {
                OIndex s = (OIndex)p;
                mndb = s.GetContextualMenu();
            }

            else if (clsname == "mysqlcore.FTriggers")
            {
                FTriggers s = (FTriggers)p;
                mndb = s.GetContextualMenu();
            }

            else if (clsname == "mysqlcore.OTrigger")
            {
                OTrigger s = (OTrigger)p;
                mndb = s.GetContextualMenu();
            }

            else if (clsname == "mysqlcore.FViews")
            {
                FViews s = (FViews)p;
                mndb = s.GetContextualMenu();
            }

            else if (clsname == "mysqlcore.OView")
            {
                OView s = (OView)p;
                mndb = s.GetContextualMenu();
            }
            else if (clsname == "mysqlcore.FProgramability")
            {
                FProgramability s = (FProgramability)p;
                mndb = s.GetContextualMenu();
            }
            else if (clsname == "mysqlcore.FStoreProcedures")
            {
                FStoreProcedures s = (FStoreProcedures)p;
                mndb = s.GetContextualMenu();
            }
            else if (clsname == "mysqlcore.OStoreProcedure")
            {
                OStoreProcedure s = (OStoreProcedure)p;
                mndb = s.GetContextualMenu();
            }
            else if (clsname == "mysqlcore.FSecurity")
            {
                FSecurity s = (FSecurity)p;
                mndb = s.GetContextualMenu();
            }
            else if (clsname == "mysqlcore.FLogins")
            {
                FLogins s = (FLogins)p;
                mndb = s.GetContextualMenu();
            }
            else if (clsname == "mysqlcore.OLogin")
            {
                OLogin s = (OLogin)p;
                mndb = s.GetContextualMenu();
            }
            else
            {
                return;
            }

            foreach (var mn in mndb)
            { 
                cmdb = mn.Value;

                if (cmdb.IsSeparator == false)
                {
                    this.contextMenuStrip1.Items.Add(cmdb.MenuName);
                }
                else
                {
                    ToolStripSeparator tls = new ToolStripSeparator();
                    this.contextMenuStrip1.Items.Add(tls);
                }
            }
        }

        public MySqlConnection getActiveConnection()
        {
            MySqlConnection Cnn = new MySqlConnection();

            TreeNode t = this.treeView1.SelectedNode;

            if (t == null)
            {
                return null;
            }
            else
            {
                AObject a = (AObject)t.Tag;
                Cnn = a.ActiveConnection;
                return Cnn;
            }
        }
    }
}
