using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mysqlcore
{
    public abstract class DbObject : mysqlcore.AObject
    {
        public abstract string GenerateScript();

        public void Rename(string NewName)
        {
            this.Name = NewName;
        }
    }
}
