using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Data;

namespace mysqlcore
{
    public class FDatabases : FObject
    {
        public FDatabases(AObject parentObject)
        {
            this._name = "Databases";
            this._parent = parentObject;
        }

        public override void Retrieve()
        {
            this.Childs.Clear();

            MySqlConnection Cnn = this.ActiveConnection;

            MySqlDataReader dr;

            MySqlCommand Cmd = new MySqlCommand("SHOW DATABASES;", Cnn);

            dr = Cmd.ExecuteReader();

            while (dr.Read())
            {
                this.Childs.Add(new ODatabase(dr[0].ToString(),this));
            }

            dr.Close();
        }

        public new SortedList<int, CommandDb> GetContextualMenu()
        {
            SortedList<int, CommandDb> ls = base.GetContextualMenu();

            ls.Add(1, new CommandDb("New Database..."));

            return ls;
        }

    }
}
