using System.Collections.Generic;
using System.Windows.Input;
using DPA_Musicsheets_Thijn_van_Dijk.Command;

namespace DPA_Musicsheets_Thijn_van_Dijk.Chain
{
    public abstract class ChainHandler
    {
        protected CommandRegister commandRegister;
        protected Context context;
        protected ChainHandler next;

        public virtual bool Handle(List<System.Windows.Input.Key> keys)
        {
            if (next != null){return next.Handle(keys);}
            else { return false;}
        }

        public ChainHandler(Context con, CommandRegister reg, ChainHandler nextHandler)
        {
            this.context = con;
            this.commandRegister = reg;
            this.next = nextHandler;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>The new next handler to enable chaining</returns>
        public ChainHandler SetNext(ChainHandler handler)
        {
            this.next = handler;
            return next;
        }

        public abstract bool CanHandle(List<System.Windows.Input.Key> keys);

        public bool CompareKeyLists(List<Key> list1, List<Key> list2)
        {
            bool reval = false;
            if (list1.Count == list2.Count)
            {
                reval = true;
                foreach (Key key in list1)
                {
                    if (!list2.Contains(key))
                    {
                        reval = false;
                    }
                }
            }
            return reval;
        }
    }
}

