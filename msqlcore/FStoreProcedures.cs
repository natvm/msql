using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;

namespace mysqlcore
{
    public class FStoreProcedures : FObject
    {
        public FStoreProcedures(FProgramability parentObject)
        {
            this._name = "Store Procedures";
            this._parent = parentObject;
        }

        public override void Retrieve()
        {
            this.Childs.Clear();

            MySqlConnection Cnn = this.ActiveConnection;

            MySqlDataReader dr;

            FProgramability p = (FProgramability)this.Parent;
            ODatabase d = (ODatabase)p.Parent;

            MySqlCommand Cmd = new MySqlCommand("SHOW PROCEDURE STATUS WHERE `Db`='" + d.Name + "';", Cnn);

            dr = Cmd.ExecuteReader();

            while (dr.Read())
            {
                this.Childs.Add(new OStoreProcedure(dr[0].ToString(), this));
            }

            dr.Close();
        }

        public new SortedList<int, CommandDb> GetContextualMenu()
        {
            SortedList<int, CommandDb> ls = base.GetContextualMenu();

            ls.Add(1, new CommandDb("New Store Procedure..."));

            return ls;
        }
    }
}
