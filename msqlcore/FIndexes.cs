using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;

namespace mysqlcore
{
    public class FIndexes : FObject
    {
        public FIndexes(OTable parentObject)
        {
            this._name = "Indexes";
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

            MySqlCommand Cmd = new MySqlCommand("SHOW INDEX FROM `" + d.Name + "`.`" + t.Name + "`;", Cnn);

            dr = Cmd.ExecuteReader();

            while (dr.Read())
            {
                this.Childs.Add(new OIndex(dr[2].ToString(), this));
            }

            dr.Close();
        }

        public new SortedList<int, CommandDb> GetContextualMenu()
        {
            SortedList<int, CommandDb> ls = base.GetContextualMenu();

            ls.Add(1, new CommandDb("New Index..."));

            return ls;
        }
    }
}
