using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;

namespace mysqlcore
{
    public class FColumns : FObject
    {
        public FColumns(OTable parentObject)
        {
            this._name = "Columns";
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

            MySqlCommand Cmd = new MySqlCommand("SHOW COLUMNS FROM " + d.Name + "." + t.Name + ";", Cnn);

            dr = Cmd.ExecuteReader();

            while (dr.Read())
            {
                this.Childs.Add(new OColumn(dr[0].ToString() + " - "+ dr[1].ToString(), this));
            }

            dr.Close();
        }

        public new SortedList<int, CommandDb> GetContextualMenu()
        {
            SortedList<int, CommandDb> ls = base.GetContextualMenu();

            ls.Add(1, new CommandDb("New Column..."));

            return ls;
        }
    }
}
