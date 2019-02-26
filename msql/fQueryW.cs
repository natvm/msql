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
using MySql.Data.MySqlClient;
using Fireball.Windows.Forms;
using Fireball.CodeEditor.SyntaxFiles;

namespace msql
{
    public partial class fQueryW : DockContent 
    {
        public enum PropertyDatabase
        {
            CurrentUser,
            DatabaseName,
            Version,
            Port,
            Host,
            ConnectionId
        }

        private CodeEditorControl _CodeEditor;
        private string _filename;
        public MySqlConnection Cnn;
        /*private bool _canCut = false;
        private bool _canCopy = false;
        private bool _canPaste = false;*/

        public fQueryW()
        {
            InitializeComponent();
            this.Cnn = new MySqlConnection();
            this.Cnn.StateChange += Cnn_StateChange;

            this.LoadEditor();
        }

        private void fQueryW_Load(object sender, EventArgs e)
        {
            
        }

        public string Filename { get { return this._filename; } }

        public bool CanUndo { get { return this._CodeEditor.CanUndo; } }
        public bool CanRedo { get { return this._CodeEditor.CanRedo; } }

        public bool CanCut { get { return this._CodeEditor.CanCopy; } }
        public bool CanCopy { get { return this._CodeEditor.CanCopy; } }
        public bool CanPaste { get { return this._CodeEditor.CanPaste; } }

        public bool CanConnect { get { return this.Cnn.State != ConnectionState.Open; } }
        public bool CanDisconnect { get { return this.Cnn.State == ConnectionState.Open; } }

        /*Métodos públicos*/

        public bool Open(string filename)
        {
            try
            {
                this._CodeEditor.Open(filename);
                this._filename = filename;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public void Save(string filename)
        {
            try
            {
                this._CodeEditor.Save(filename);
                this._filename = filename;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Cut()
        {
            this._CodeEditor.Cut();
        }

        public void Copy()
        {
            if (this._CodeEditor.CanCopy == true) this._CodeEditor.Copy();
        }

        public void Paste()
        {
            if (this._CodeEditor.CanPaste == true) this._CodeEditor.Paste();
        }

        public void Undo()
        {
            if (this._CodeEditor.CanUndo == true) this._CodeEditor.Undo();
        }

        public void Redo()
        {
            if (this._CodeEditor.CanRedo == true) this._CodeEditor.Redo();
        }

        public void Connect()
        {
            if (this.Cnn.State == ConnectionState.Closed)
            {
                fConex fc = new fConex();
                fc.MyCnn = this.Cnn;
                fc.ShowDialog();
                if (fc.MyCnn.State == ConnectionState.Open)
                {
                    this.Cnn = fc.MyCnn;
                    this.checkStateStatusBar(ConnectionState.Open);
                }
                fc.Dispose();
            }
            this.checkStateStatusDB();
        }

        public void Disconnect()
        {
            if (this.Cnn.State == ConnectionState.Open) this.Cnn.Close();
        }

        public void Execute()
        {

        }

        public void StopQueryExecute()
        {

        }

        /*Métodos privados*/

        private void LoadEditor()
        {
            this._CodeEditor = new CodeEditorControl();
            this._CodeEditor.SelectionChange += _CodeEditor_SelectionChange;
            //this._CodeEditor.FileSavedChanged += _CodeEditor_FileSavedChanged;
            //this._CodeEditor.TextChanged += _CodeEditor_TextChanged;
            this._CodeEditor.ContextMenuStrip = this.contextMenuStrip1;

            this.splitContainer1.Panel1.Controls.Add(_CodeEditor);
            this._CodeEditor.Dock = DockStyle.Fill;
            CodeEditorSyntaxLoader.SetSyntax(this._CodeEditor, SyntaxLanguage.SqlServer2K5);
        }

        private void checkStateContextualMenu()
        {
            this.mnuCut.Enabled = this._CodeEditor.CanCopy;
            this.mnuCopy.Enabled = this._CodeEditor.CanCopy;
            this.mnuPaste.Enabled = this._CodeEditor.CanPaste;
        }

        private void checkStateStatusBar(ConnectionState t)
        {
            switch (t)
            {
                case ConnectionState.Broken:
                    this.tssTextState.Text = "Disconnected";
                    this.tssIcon.Image = Properties.Resources.plug_minus; //Disconnect
                    break;
                case ConnectionState.Closed:
                    this.tssTextState.Text = "Disconnected";
                    this.tssIcon.Image = Properties.Resources.plug_minus; //Disconnect
                    break;
                case ConnectionState.Connecting:
                    this.tssTextState.Text = "Attemping connection";
                    this.tssIcon.Image = Properties.Resources.loading; //Execute
                    break;
                case ConnectionState.Executing:
                    this.tssTextState.Text = "Executing";
                    this.tssIcon.Image = Properties.Resources.loading; //Execute
                    break;
                case ConnectionState.Fetching:
                    this.tssTextState.Text = "Fetching";
                    this.tssIcon.Image = Properties.Resources.loading; //Execute
                    break;
                case ConnectionState.Open:
                    this.tssTextState.Text = "Connected";
                    this.tssTime.Text = "00:00:00";
                    this.tssRowsAffected.Text = "0 Rows";
                    this.tssIcon.Image = Properties.Resources.plug_plus; //Connected
                    break;
                default:
                    break;
            }
        }

        private void checkStateStatusDB()
        {
            if (this.Cnn.State == ConnectionState.Open)
            {

                this.tssHostServer.Visible = true;
                this.tssUser.Visible = true;
                this.tssDatabase.Visible = true;
                this.tssTime.Visible = true;
                this.tssRowsAffected.Visible = true;

                this.tssDatabase.Text = this.getPropertiesServer(PropertyDatabase.DatabaseName);
                this.tssUser.Text = this.getPropertiesServer(PropertyDatabase.CurrentUser) + "(" + this.getPropertiesServer(PropertyDatabase.ConnectionId) + ")";
                this.tssHostServer.Text = this.getPropertiesServer(PropertyDatabase.Host) + ':' + this.getPropertiesServer(PropertyDatabase.Port) + "(" + this.getPropertiesServer(PropertyDatabase.Version) + ")";
            }
        }

        public string getPropertiesServer(PropertyDatabase p)
        {
            string sql = "";
            string strReturn = "";

            switch (p)
            {
                case PropertyDatabase.CurrentUser:
                    sql = "SELECT CURRENT_USER();";
                    break;
                case PropertyDatabase.DatabaseName:
                    sql = "SELECT DATABASE();";
                    break;
                case PropertyDatabase.Version:
                    sql = "SELECT VERSION();";
                    break;
                case PropertyDatabase.Port:
                    sql = "select variable_value from information_schema.global_variables where variable_name = 'port';";
                    break;
                case PropertyDatabase.Host:
                    sql = "select SUBSTRING_INDEX(host,':',1) as 'ip' from information_schema.processlist WHERE ID=connection_id();";
                    break;
                case PropertyDatabase.ConnectionId:
                    sql = "select connection_id();";
                    break;
            }
            MySqlDataReader dr;

            MySqlCommand Cmd = new MySqlCommand(sql, this.Cnn);

            dr = Cmd.ExecuteReader();

            while (dr.Read())
            {
                strReturn = dr[0].ToString();
            }

            dr.Close();

            return strReturn;
        }

        /*Eventos genéricos de los controles*/

        private void _CodeEditor_SelectionChange(object sender, EventArgs e)
        {
            this.checkStateContextualMenu();
        }

        private void Cnn_StateChange(object sender, StateChangeEventArgs e)
        {
            this.checkStateStatusBar(e.CurrentState);
        }

        private void mnuCut_Click(object sender, EventArgs e)
        {
            this.Cut();
        }

        private void mnuCopy_Click(object sender, EventArgs e)
        {
            this.Copy();
        }

        private void mnuPaste_Click(object sender, EventArgs e)
        {
            this.Paste();
        }

        private void mnuExecute_Click(object sender, EventArgs e)
        {
            this.Execute();
        }
    }
}
