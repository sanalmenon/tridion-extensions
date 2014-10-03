using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tridion.Extensions.CoreService.Events
{
    public delegate void ItemSaved(string arg);


    class ItemEvent
    {
        public event ItemSaved ItemSaved;
    
        public void OnItemSaved(string arg)
        {
            if (ItemSaved != null)
                ItemSaved(arg);
        }
    }
}
