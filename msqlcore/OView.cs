using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mysqlcore
{
    public class OView : DbObject
    {
        public OView(string Name, FViews parentObject)
        {
            this._name = Name;
            this._parent = parentObject;
            this._itemindex = 4;
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

            ls.Add(1, new CommandDb("New View..."));
            ls.Add(2, new CommandDb("Design"));
            ls.Add(3, new CommandDb(true));
            ls.Add(4, new CommandDb("Select 1000 rows"));
            ls.Add(5, new CommandDb("Edit 200 rows"));
            ls.Add(6, new CommandDb(true));
            ls.Add(7, new CommandDb("Rename"));
            ls.Add(8, new CommandDb("Delete"));

            ls.Add(1001, new CommandDb("Properties"));

            return ls;
        }
    }
}
