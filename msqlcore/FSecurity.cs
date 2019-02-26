using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;

namespace mysqlcore
{
    public class FSecurity : FObject
    {
        public FSecurity(OServer parentObject)
        {
            this._name = "Security";
            this._parent = parentObject;
        }

        public override void Retrieve()
        {
            this.Childs.Add(new FLogins(this));
        }

        public new SortedList<int, CommandDb> GetContextualMenu()
        {
            SortedList<int, CommandDb> ls = base.GetContextualMenu();

            ls.Add(1, new CommandDb("New Login..."));

            return ls;
        }
    }
}
