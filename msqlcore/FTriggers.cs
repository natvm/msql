using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Data;

namespace mysqlcore
{
    public class FTriggers : FObject
    {
        public FTriggers(OTable parentObject)
        {
            this._name = "Triggers";
            this._parent = parentObject;
        }

        public override void Retrieve()
        {
            this.Childs.Clear();

            MySqlConnection Cnn = this.ActiveConnection;

            MySqlDataReader dr;

            OTable t = (OTable)this.Parent;
            FTables ts = (FTables)t.Parent;
            ODatabase d = (ODatabase)ts.Parent;

            MySqlCommand Cmd = new MySqlCommand("SHOW TRIGGERS FROM "+ d.Name +";", Cnn);

            dr = Cmd.ExecuteReader();

            while (dr.Read())
            {
                this.Childs.Add(new OTrigger(dr[0].ToString(),this));
            }

            dr.Close();
        }

        public new SortedList<int, CommandDb> GetContextualMenu()
        {
            SortedList<int, CommandDb> ls = base.GetContextualMenu();

            ls.Add(1, new CommandDb("New Trigger..."));

            return ls;
        }
    }
}
