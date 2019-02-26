using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mysqlcore
{
    public class CommandDb
    {
        private string _menuname;
        private bool _isseparator = false;        
        private SortedList<int,CommandDb> _childscommands = new SortedList<int,CommandDb>();

        public string MenuName
        {
            get
            {
                return this._menuname;
            }
            set
            {
                this._menuname = MenuName;
            }
        }

        public bool IsSeparator
        {
            get
            {
                return this._isseparator;
            }
            set
            {
                this._isseparator = IsSeparator;
            }
        }

        public SortedList<int,CommandDb> ChildsCommands
        {
            get
            {
                return this._childscommands;
            }
            set
            {
            }
        }

        public CommandDb(string menuname)
        {
            this._menuname = menuname;
        }

        public CommandDb(bool isseparator)
        {
            this._isseparator = isseparator;
        }
    }
}
