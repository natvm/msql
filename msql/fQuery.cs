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
using System.Windows.Markup;
using System.IO;
using Fireball.Windows.Forms;
using Fireball.CodeEditor.SyntaxFiles;
using MySql.Data.MySqlClient;

namespace msql
{
    public partial class fQuery : DockContent 
    {
        public enum IconState
	    {
            Disconnected,
            Connected,
            Executing,
            Warning,
            Ok
	    }

        public enum PropertyDatabase
        { 
            CurrentUser,
            DatabaseName,
            Version,
            Port,
            Host,
            ConnectionId
        }

        private CodeEditorControl _CodeEditor = null;
        private MySqlConnection _Cnn;
        private fMdiMain _fMain;
        private string _FileName;
        private bool IsExecute;

        public fQuery(fMdiMain fmain, MySqlConnection pCnn)
        {
            InitializeComponent();

            _CodeEditor = new CodeEditorControl();
            this._CodeEditor.SelectionChange += _CodeEditor_SelectionChange;
            this._CodeEditor.FileSavedChanged += _CodeEditor_FileSavedChanged;
            this._CodeEditor.TextChanged +=_CodeEditor_TextChanged;
            this._CodeEditor.ContextMenuStrip = this.contextMenuStrip1;

            this._fMain = fmain;
            this._Cnn = pCnn;

            this.IsExecute = false;

            //this._Cnn.StateChange += _Cnn_StateChange;

            this.SetTssStateStatusBarText("Disconnect", IconState.Disconnected);            
        }

        void _Cnn_InfoMessage(object sender, MySqlInfoMessageEventArgs args)
        {
            /*this._fMain.SetButtonEnabledState(fMdiMain.ToolbarButtons.Execute, !this.IsExecute);
            this._fMain.SetButtonEnabledState(fMdiMain.ToolbarButtons.StopQuery, this.IsExecute);
            this._fMain.SetButtonEnabledState(fMdiMain.ToolbarButtons.Parse, !this.IsExecute);*/
        }

        void _Cnn_StateChange(object sender, StateChangeEventArgs e)
        {
            SetQueyToolbarState(e.CurrentState);
        }

        internal void SetQueyToolbarState(ConnectionState t)
        {
            this._fMain.SetButtonEnabledState(fMdiMain.ToolbarButtons.ChangeDatabase, t == ConnectionState.Open ? true : false);

            switch (t)
            {
                case ConnectionState.Broken:
                    this.SetTssStateStatusBarText("Disconnected", IconState.Disconnected);
                    this._fMain.SetButtonEnabledState(fMdiMain.ToolbarButtons.Connect, true);
                    this._fMain.SetButtonEnabledState(fMdiMain.ToolbarButtons.Disconnect, false);
                    break;
                case ConnectionState.Closed:
                    this._fMain.SetButtonEnabledState(fMdiMain.ToolbarButtons.Connect, true);
                    this._fMain.SetButtonEnabledState(fMdiMain.ToolbarButtons.Disconnect, false);
                    this.SetTssStateStatusBarText("Disconnected", IconState.Disconnected);
                    break;
                case ConnectionState.Connecting:
                    this.SetTssStateStatusBarText("Attemping connection", IconState.Executing);
                    break;
                case ConnectionState.Executing:
                    this.SetTssStateStatusBarText("Executing", IconState.Executing);
                    break;
                case ConnectionState.Fetching:
                    this.SetTssStateStatusBarText("Fetching", IconState.Executing);
                    break;
                case ConnectionState.Open:
                    this._fMain.SetButtonEnabledState(fMdiMain.ToolbarButtons.Connect, false);
                    this._fMain.SetButtonEnabledState(fMdiMain.ToolbarButtons.Disconnect, true);
                    this.SetTssStateStatusBarText("Connected", IconState.Connected);
                    this.tssTime.Text = "00:00:00";
                    this.tssRowsAffected.Text = "0 Rows";
                    break;
                default:
                    break;
            }
        }

        private void _CodeEditor_TextChanged(object sender, EventArgs e)
        {
            this._fMain.SetButtonEnabledState(fMdiMain.ToolbarButtons.Undo, this._CodeEditor.CanUndo);
            this._fMain.SetButtonEnabledState(fMdiMain.ToolbarButtons.Redo, this._CodeEditor.CanRedo);
        }

        private void _CodeEditor_FileSavedChanged(object sender, EventArgs e)
        {
            this._fMain.SetButtonEnabledState(fMdiMain.ToolbarButtons.Save, !this._CodeEditor.Saved);
        }

        void _CodeEditor_SelectionChange(object sender, EventArgs e)
        {
            this._fMain.SetButtonEnabledState(fMdiMain.ToolbarButtons.Copy, this._CodeEditor.CanCopy);
            this.SetStateToContextualMenu(fMdiMain.ToolbarButtons.Copy, this._CodeEditor.CanCopy);
            this._fMain.SetButtonEnabledState(fMdiMain.ToolbarButtons.Cut, this._CodeEditor.CanCopy);
            this.SetStateToContextualMenu(fMdiMain.ToolbarButtons.Cut, this._CodeEditor.CanCopy);
            this._fMain.SetButtonEnabledState(fMdiMain.ToolbarButtons.Paste, this._CodeEditor.CanPaste);            
        }

        private void fQuery_Load(object sender, EventArgs e)
        {
            this.splitContainer1.Panel1.Controls.Add(_CodeEditor);
            _CodeEditor.Dock = DockStyle.Fill;
            CodeEditorSyntaxLoader.SetSyntax(_CodeEditor, SyntaxLanguage.SqlServer2K5);
        }

        //Métodos generales

        internal void OpenFile(string FileName)
        {
            try
            {
                this._CodeEditor.Open(FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        internal void Save()
        {
            if (this._FileName == null)
            {
                this.SaveAs();
            }
            else
            {
                this._CodeEditor.Save(this._FileName);
            }

        }

        internal void SaveAs()
        {
            this.GetFileName();
            if (this._FileName != null)
            {
                this._CodeEditor.Save(this._FileName);
            }
        }

        public void GetFileName()
        {
            DialogResult dr;
            dr = this.saveFileDialog1.ShowDialog();

            if (dr == DialogResult.OK)
            {
                this._FileName = this.saveFileDialog1.FileName;
            }

        }

        public void Undo()
        {
            if (this._CodeEditor.CanUndo == true) this._CodeEditor.Undo();
        }

        public void Redo()
        {
            if (this._CodeEditor.CanRedo == true) this._CodeEditor.Redo();
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
            if (this._CodeEditor.CanPaste==true) this._CodeEditor.Paste();
        }

        internal void Connect()
        {
            if (this._Cnn.State == ConnectionState.Closed)
            {
                fConex fc = new fConex();
                fc.MyCnn = this._Cnn;
                fc.ShowDialog();
                if (fc.MyCnn.State == ConnectionState.Open)
                {
                    this._Cnn.StateChange += _Cnn_StateChange;
                    this._Cnn.InfoMessage += _Cnn_InfoMessage;
                    this.SetTssStateStatusBarText("Connected", IconState.Connected);
                    this.SetQueyToolbarState(ConnectionState.Open);
                }
                fc.Dispose();
            }
        }

        internal void SilentConnect()
        {
            if (this._Cnn.State == ConnectionState.Closed)
            {
                try
                {
                    this._Cnn.Open();
                    this._Cnn.StateChange += _Cnn_StateChange;
                    this._Cnn.InfoMessage += _Cnn_InfoMessage;
                    this.SetTssStateStatusBarText("Connected", IconState.Connected);
                    this.SetQueyToolbarState(ConnectionState.Open);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        internal void Disconnect()
        {
            if (this._Cnn.State == ConnectionState.Open) this._Cnn.Close();
            //this._Cnn.StateChange -= _Cnn_StateChange;
        }

        internal void ChangeDatabase(string DatabaseName)
        {
            if (this._Cnn.State == ConnectionState.Open)
            try
            {
                this._Cnn.ChangeDatabase(DatabaseName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        internal void Execute()
        {
            string sql;

            if (this._CodeEditor.Selection.Text.Length > 0)
            {
                sql = _CodeEditor.Selection.Text;
            }
            else
            {
                sql = _CodeEditor.Document.Text;
            }

            MySqlCommand Cmd = new MySqlCommand(sql, this._Cnn);
            MySqlDataReader dr;
            DataTable dt = new DataTable();

            try
            {
                this.SetTssStateStatusBarText("Executing query", IconState.Executing);
                dr = Cmd.ExecuteReader();
                this.SetTssStateStatusBarText("Execute Query succesfully", IconState.Ok);
            }
            catch (Exception ex)
            {
                this.SetTssStateStatusBarText("Query executed with errors.", IconState.Warning);
                this.txtMessage.Clear();
                this.txtMessage.AppendText(ex.Message);
                this.tabControl1.SelectedIndex = 1;
                return;
                throw;
            }

            dt.Load(dr);
            dr.Close();

            this.dataGridView1.DataSource = dt;

            this.tabControl1.SelectedIndex = 0;
        }

        internal void CancelQuery()
        {
            throw new NotImplementedException();
        }

        internal void Parse()
        {
            throw new NotImplementedException();
        }

        private void SetStateToContextualMenu(fMdiMain.ToolbarButtons t, bool isEnabled)
        {
            switch (t)
            {
                case fMdiMain.ToolbarButtons.Cut:
                    this.mnuCut.Enabled = isEnabled;
                    break;
                case fMdiMain.ToolbarButtons.Copy:
                    this.mnuCopy.Enabled = isEnabled;
                    break;
                case fMdiMain.ToolbarButtons.Paste:
                    this.mnuPaste.Enabled = isEnabled;
                    break;
                case fMdiMain.ToolbarButtons.Execute:
                    break;
            }
        }

        public bool QueryControlIsEnabled(fMdiMain.ToolbarButtons t)
        {
            bool IsEnabled = false;

            switch (t)
            {
                case fMdiMain.ToolbarButtons.Save:
                    IsEnabled = this._CodeEditor.Saved == true ? false : true;
                    break;
                case fMdiMain.ToolbarButtons.Undo:
                    IsEnabled = this._CodeEditor.CanUndo;
                    break;
                case fMdiMain.ToolbarButtons.Redo:
                    IsEnabled = this._CodeEditor.CanRedo;
                    break;
                case fMdiMain.ToolbarButtons.Cut:
                    IsEnabled = this._CodeEditor.CanCopy;
                    break;
                case fMdiMain.ToolbarButtons.Copy:
                    IsEnabled = this._CodeEditor.CanCopy;
                    break;
                case fMdiMain.ToolbarButtons.Paste:
                    IsEnabled = this._CodeEditor.CanPaste;
                    break;
                case fMdiMain.ToolbarButtons.Connect:
                    if (this._Cnn.State == ConnectionState.Open) IsEnabled = true;
                    break;
                case fMdiMain.ToolbarButtons.Disconnect:
                    if (this._Cnn.State == ConnectionState.Closed) IsEnabled = true;
                    break;
                case fMdiMain.ToolbarButtons.ChangeDatabase:
                    if (this._Cnn.State == ConnectionState.Closed)
                        IsEnabled = false;
                    else
                        IsEnabled = true;
                    break;
                case fMdiMain.ToolbarButtons.Execute:
                    //Nothing
                    break;
                case fMdiMain.ToolbarButtons.Parse:

                    break;
                
            }

            return IsEnabled;
        }

        public void SetCnnStateStatusBarText()
        {
            this.tssDatabase.Text = this.getPropertiesServer(PropertyDatabase.DatabaseName);
            this.tssUser.Text = this.getPropertiesServer(PropertyDatabase.CurrentUser) + "(" + this.getPropertiesServer(PropertyDatabase.ConnectionId) + ")";
            this.tssHostServer.Text = this.getPropertiesServer(PropertyDatabase.Host) + ':' + this.getPropertiesServer(PropertyDatabase.Port) + "(" + this.getPropertiesServer(PropertyDatabase.Version) + ")";
        }

        public void SetTssStateStatusBarText(string textState, IconState icn)
        {
            this.tssTextState.Text = textState;

            switch (icn)
            {
                case IconState.Disconnected:
                    this.tssIcon.Image = Properties.Resources.plug_minus;
                    break;
                case IconState.Connected:
                    this.tssIcon.Image = Properties.Resources.plug_plus;
                    break;
                case IconState.Executing:
                    this.tssIcon.Image = Properties.Resources.loading;
                    break;
                case IconState.Warning:
                    this.tssIcon.Image = Properties.Resources.exclamation_octagon_frame;
                    break;
                case IconState.Ok:
                    this.tssIcon.Image = Properties.Resources.tick_circle;
                    break;
                default:
                    break;
            }

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

            MySqlCommand Cmd = new MySqlCommand(sql, this._Cnn);

            dr = Cmd.ExecuteReader();

            while (dr.Read())
            {
                strReturn = dr[0].ToString();
            }

            dr.Close();

            return strReturn;
        }

    }
}
