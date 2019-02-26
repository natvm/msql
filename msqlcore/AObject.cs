using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace mysqlcore
{
    public abstract class AObject
    {
        private string _id;
        protected string _name;
        protected int _itemindex;
        protected AObject _parent;
        private List<AObject> _childs;
    
        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = Name;
            }
        }

        /// <summary>
        /// Representa la figura en ViewNode
        /// </summary>
        /// <remarks>ItemIndex for ViewNode</remarks>
        public int ItemIndex
        {
            get
            {
                return this._itemindex;
            }
            set
            {
                this._itemindex = ItemIndex;
            }
        }

        public List<AObject> Childs
        {
            get
            {
                return this._childs;
            }
            set
            {
            }
        }

        public AObject Parent
        {
            get
            {
                return this._parent;
            }
            set
            {
                this._parent = Parent;
            }
        }

        public string Id
        {
            get
            {
                return this._id;
            }
            set
            {
            }
        }

        public MySqlConnection ActiveConnection
        {
            get
            {
                AObject p;
                OServer d;

                p = this._parent;

                while (true)
                {

                    if (p.GetType().ToString() == "mysqlcore.OServer")
                    {
                        d = (OServer)p;
                        break;
                    }
                    else
                    {
                        p = p._parent;
                    }
                }

                return d.Connection;
            }
            set
            {
            }
        }


        public AObject()
        {
            this._id = new Guid().ToString();
            this._childs = new List<AObject>();
        }

        public abstract void Retrieve();

        public SortedList<int, CommandDb> GetContextualMenu()
        {
            SortedList<int, CommandDb> ls = new SortedList<int, CommandDb>();

            ls.Add(999, new CommandDb(true));
            ls.Add(1000, new CommandDb("Refresh"));

            return ls;
        }
    }
}
