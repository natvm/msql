using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace mysqlcore
{
    public class OServer : DbObject
    {
        private MySql.Data.MySqlClient.MySqlConnection _connection;
        private string _loginname;
        private string _servername;
        private string _serverversion;

        /// <remarks>Connection Object</remarks>
        public MySqlConnection Connection
        {
            get
            {
                return this._connection;
            }
            set
            {
            }
        }

        /// <remarks>Server Version</remarks>
        public string ServerVersion
        {
            get
            {
                return this._serverversion;
            }
            set
            {
            }
        }

        /// <remarks>Login Name from Connection</remarks>
        public string LoginName
        {
            get
            {
                return this._loginname;
            }
            set
            {
            }
        }

        /// <remarks>Server Name (IP o NetBios Name)</remarks>
        public string ServerName
        {
            get
            {
                return this._servername;
            }
            set
            {
            }
        }

        /// <summary>
        /// Make Disconnect for Connection Object
        /// </summary>
        public void Disconnect()
        {
            this._connection.Close();
        }

        public OServer(MySqlConnection pCnn, string pServerName, string pLoginName)
        { 
            if (pCnn.State == System.Data.ConnectionState.Open)
            {
                this._connection = pCnn;
                this._loginname = pLoginName;
                this._serverversion = pCnn.ServerVersion;
                this._servername = pServerName;
                this.ItemIndex = 1;
            }
            else
            {
                throw new Exception("When to create new OServer, the connection must be open.");
            }
        }

        public override string GenerateScript()
        {
            return "";
            
        }

        public override void Retrieve()
        {
            //Crear los nodos child
            this.Childs.Add(new FDatabases(this));
            this.Childs.Add(new FSecurity(this));
        }

        public new SortedList<int, CommandDb> GetContextualMenu()
        {
            SortedList<int, CommandDb> ls = base.GetContextualMenu();

            ls.Add(1, new CommandDb("Connect..."));
            ls.Add(2, new CommandDb("Disconnect"));
            ls.Add(3, new CommandDb(true));
            ls.Add(4, new CommandDb("New Query"));
            ls.Add(1001, new CommandDb("Properties"));

            return ls;
        }

    }
}
