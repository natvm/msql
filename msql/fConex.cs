using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Collections.Specialized;


namespace msql
{
    public partial class fConex : Form
    {
        public string ServerName = "";
        public string LoginName = "";
        public MySqlConnection MyCnn;
        private string ErrorMessage;
        private bool MoreOptions = false;

        List<string> usernameHist = new List<string>();

        CancellationTokenSource cts;

        public fConex()
        {
            InitializeComponent();
        }

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            string strConnection = this.prepareConnectionStr();
            cts = new CancellationTokenSource();

            this.ControlStateConnecting();

            MySqlConnection Cnn = this.MyCnn;

            Cnn.ConnectionString = strConnection;

            Task getCnn = Task.Run(async () =>
            {
                Thread.Sleep(2000);
                await Cnn.OpenAsync(cts.Token);
            });

            //Intentar conexion
            try
            {
                await getCnn;
            }
            catch (MySqlException ex)
            {
                this.ErrorMessage = ex.Number + ": " + ex.Message;
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.Message;
            }

            cts = null;

            //Resultado Final
            if (Cnn.State == ConnectionState.Open)
            {
                this.ServerName = this.cmbServerName.Text;
                this.LoginName = this.cmbLogin.Text;
                this.Hide();
            }
            else
            {
                if (getCnn.IsCanceled == true)
                {
                    this.ControlStateReady();
                }
                else
                {
                    this.ControlStateReady();
                    MessageBox.Show(this.ErrorMessage, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ControlStateConnecting()
        {
            this.progressBar1.Visible = true;
            this.btnConnect.Enabled = false;
            this.lblServerName.Enabled = false;
            this.cmbServerName.Enabled = false;
            this.lblLogin.Enabled = false;
            this.cmbLogin.Enabled = false;
            this.lblPassword.Enabled = false;
            this.txtPassword.Enabled = false;
        }

        private void ControlStateReady()
        {
            this.progressBar1.Visible = false;
            this.btnConnect.Enabled = true;
            this.lblServerName.Enabled = true;
            this.cmbServerName.Enabled = true;
            this.lblLogin.Enabled = true;
            this.cmbLogin.Enabled = true;
            this.lblPassword.Enabled = true;
            this.txtPassword.Enabled = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (this.cts == null)
            {
                this.Hide();
            }
            else
            {
                this.cts.Cancel();
            }
        }

        private string prepareConnectionStr()
        {
            string strConn;

            strConn = "server=" + this.cmbServerName.Text + ";";
            strConn += "user=" + this.cmbLogin.Text + ";";
            strConn += "database=mysql;";
            strConn += "port=3306;";
            strConn += "pwd=" + this.txtPassword.Text + ";";

            return strConn;
        }

        private void cmbServerName_TextChanged(object sender, EventArgs e)
        {
            ComboBox c = (ComboBox)sender;

            if (c.Text.Length == 0)
            {
                this.btnConnect.Enabled = false;
            }
            else
            {
                this.btnConnect.Enabled = true;
            }
        }
    }
}
