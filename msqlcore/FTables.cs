using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;

namespace mysqlcore
{
    public class FTables : FObject
    {
        public FTables(ODatabase parentObject)
        {
            this._name = "Tables";
            this._parent = parentObject;
        }

        public override void Retrieve()
        {
            this.Childs.Clear();

            MySqlConnection Cnn = this.ActiveConnection;

            MySqlDataReader dr;

            MySqlCommand Cmd = new MySqlCommand("SHOW TABLE STATUS FROM " + this._parent.Name + " WHERE Comment!='VIEW';", Cnn);

            dr = Cmd.ExecuteReader();

            while (dr.Read())
            {
                this.Childs.Add(new OTable(dr[0].ToString(), this));
            }

            dr.Close();
        }

        public new SortedList<int, CommandDb> GetContextualMenu()
        {
            SortedList<int, CommandDb> ls = base.GetContextualMenu();

            ls.Add(1, new CommandDb("New Table..."));

            return ls;
        }
    }
}
