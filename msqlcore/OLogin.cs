using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mysqlcore
{
    public class OLogin : DbObject
    {
        public OLogin(string Name, FLogins parentObject)
        {
            this._name = Name;
            this._parent = parentObject;
            this._itemindex = 6;
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

            ls.Add(1, new CommandDb("New Login..."));
            ls.Add(2, new CommandDb("Script Login As"));
            ls.Add(3, new CommandDb(true));
            ls.Add(4, new CommandDb("Rename"));
            ls.Add(5, new CommandDb("Delete"));

            ls.Add(1001, new CommandDb("Properties"));

            return ls;
        }
    }
}
