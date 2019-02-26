using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;

namespace mysqlcore
{
    public class ODatabase : DbObject
    {
        public override string GenerateScript()
        {
            return "";
        }

        public ODatabase(string Name, FDatabases parentObject)
        {
            this._name = Name;
            this._itemindex = 1;
            this._parent = parentObject;
        }

        public override void Retrieve()
        {
            this.Childs.Add(new FTables(this));
            this.Childs.Add(new FViews(this));
            this.Childs.Add(new FProgramability(this));
        }

        public void Select()
        {
            MySqlConnection Cnn = this.ActiveConnection;

            MySqlCommand Cmd = new MySqlCommand("USE " + this.Name + ";",Cnn);

            Cmd.ExecuteNonQuery();
        }

        public new SortedList<int, CommandDb> GetContextualMenu()
        {
            SortedList<int, CommandDb> ls = base.GetContextualMenu();

            ls.Add(1, new CommandDb("New Database..."));
            ls.Add(2, new CommandDb("New Query"));

            CommandDb cmd1 = new CommandDb("Script Database As");
            cmd1.ChildsCommands.Add(1, new CommandDb("CREATE"));
            cmd1.ChildsCommands.Add(2, new CommandDb("DROP"));

            ls.Add(3, cmd1);
            ls.Add(4, new CommandDb(true));
            ls.Add(5, new CommandDb("Backup"));
            ls.Add(6, new CommandDb("Restore"));

            ls.Add(1001, new CommandDb("Properties"));

            return ls;
        }

    }
}
