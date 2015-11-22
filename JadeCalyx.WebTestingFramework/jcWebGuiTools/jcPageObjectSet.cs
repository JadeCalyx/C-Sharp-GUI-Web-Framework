using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jcWebGuiTools
{
    class jcPageObjectSet
    {
        List<jcPageObjectLookupPair> _objectSet;

        public jcPageObjectSet(string[] items)
        {
            addToSet(items);
        }

        private void addToSet(string[] items)
        {
            if ((items.Length % 2) != 0)
            {
                throw new Exception(String.Format("Passed object list has odd number of items: {0}",
                    String.Join("\t", items)));
            }

            for (var i = 0; i < items.Length; i += 2)
            {
                _objectSet.Add(new jcPageObjectLookupPair(items[i], items[i+1]));
            }
        }

    }
}
