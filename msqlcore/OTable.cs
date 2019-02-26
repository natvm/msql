using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mysqlcore
{
    public class OTable : DbObject
    {
        public OTable(string Name, FTables parentObject)
        {
            this._name = Name;
            this._parent = parentObject;
            this._itemindex = 3;
        }
        
        public override string GenerateScript()
        {
            return "";
        }
        
        public override void Retrieve()
        {
            //throw new NotImplementedException();
            this.Childs.Add(new FColumns(this));
            this.Childs.Add(new FTriggers(this));
            this.Childs.Add(new FIndexes(this));
        }

        public new SortedList<int, CommandDb> GetContextualMenu()
        {
            SortedList<int, CommandDb> ls = base.GetContextualMenu();

            ls.Add(1, new CommandDb("New Table..."));
            ls.Add(2, new CommandDb("Design"));
            ls.Add(3, new CommandDb(true));
            ls.Add(4, new CommandDb("Select 1000 rows"));
            ls.Add(5, new CommandDb("Edit 200 rows"));
            ls.Add(6, new CommandDb(true));

            CommandDb cmd1 = new CommandDb("Script Table as");
            cmd1.ChildsCommands.Add(1, new CommandDb("CREATE"));
            cmd1.ChildsCommands.Add(2, new CommandDb("SELECT"));
            cmd1.ChildsCommands.Add(3, new CommandDb("INSERT"));
            cmd1.ChildsCommands.Add(4, new CommandDb("UPDATE"));
            cmd1.ChildsCommands.Add(5, new CommandDb("DELETE"));
            ls.Add(7, cmd1);

            ls.Add(8, new CommandDb(true));
            ls.Add(9, new CommandDb("Rename"));
            ls.Add(10, new CommandDb("Delete"));

            ls.Add(1001, new CommandDb("Properties"));

            return ls;
        }

    }
}
