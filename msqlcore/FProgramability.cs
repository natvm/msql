using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mysqlcore
{
    public class FProgramability : FObject
    {
        public FProgramability(ODatabase parentObject)
        {
            this._name = "Programability";
            this._parent = parentObject;
        }

        public override void Retrieve()
        {
            this.Childs.Add(new FStoreProcedures(this));
        }

        public new SortedList<int, CommandDb> GetContextualMenu()
        {
            SortedList<int, CommandDb> ls = base.GetContextualMenu();

            return ls;
        }
    }
}
