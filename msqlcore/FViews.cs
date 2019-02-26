using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;

namespace mysqlcore
{
    public class FViews : FObject
    {
        public FViews(ODatabase parentObject)
        {
            this._name = "Views";
            this._parent = parentObject;
        }

        public override void Retrieve()
        {
            this.Childs.Clear();

            MySqlConnection Cnn = this.ActiveConnection;

            MySqlDataReader dr;

            ODatabase d = (ODatabase)this.Parent;

            MySqlCommand Cmd = new MySqlCommand("SHOW TABLE STATUS FROM " + d.Name + " WHERE Comment='VIEW';", Cnn);

            dr = Cmd.ExecuteReader();

            while (dr.Read())
            {
                this.Childs.Add(new OView(dr[0].ToString(), this));
            }

            dr.Close();
        }

        public new SortedList<int, CommandDb> GetContextualMenu()
        {
            SortedList<int, CommandDb> ls = base.GetContextualMenu();

            ls.Add(1, new CommandDb("New View..."));

            return ls;
        }
    }
}
