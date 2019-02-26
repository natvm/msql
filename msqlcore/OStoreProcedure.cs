using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mysqlcore
{
    public class OStoreProcedure : DbObject
    {
        public OStoreProcedure(string Name, FStoreProcedures parentObject)
        {
            this._name = Name;
            this._parent = parentObject;
            this._itemindex = 5;
        }
        
        public override string GenerateScript()
        {
            return "";
        }
        
        public override void Retrieve()
        {
            //throw new NotImplementedException();
            //this.Childs.Add(new FColumn(this));
        }

        public new SortedList<int, CommandDb> GetContextualMenu()
        {
            SortedList<int, CommandDb> ls = base.GetContextualMenu();

            ls.Add(1, new CommandDb("New Store Procedure..."));
            ls.Add(2, new CommandDb("Modify"));
            ls.Add(3, new CommandDb("Execute"));
            ls.Add(4, new CommandDb("Script Store Procedure As"));
            ls.Add(5, new CommandDb(true));
            ls.Add(6, new CommandDb("Rename"));
            ls.Add(7, new CommandDb("Delete"));
            ls.Add(1001, new CommandDb("Properties"));


            return ls;
        }
    }
}
