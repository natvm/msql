using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mysqlcore
{
    public abstract class FObject : AObject
    {
        public FObject()
        {
            this._itemindex = 2;
        }

        public void NewChildObject(AObject pAObject)
        { 

        }

        public bool DeleteChildObject(AObject pAObject)
        {
            return true;
        }
    }
}
