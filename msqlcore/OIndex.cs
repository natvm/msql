using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mysqlcore
{
    public class OIndex : DbObject
    {
        public OIndex(string Name, FIndexes parentObject)
        {
            this._name = Name;
            this._parent = parentObject;
            this._itemindex = 11;
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

            ls.Add(1, new CommandDb("New Index..."));
            ls.Add(2, new CommandDb("Rebuild"));
            ls.Add(3, new CommandDb("Rename"));
            ls.Add(4, new CommandDb("Delete"));

            ls.Add(1001, new CommandDb("Properties"));

            return ls;
        }
    }
}
