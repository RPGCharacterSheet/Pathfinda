using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinda
{
    public static partial class ImportedData
    {
        public class Class
        {

        }

        private static List<Class> _classes = null;
        public static List<Class> Classes
        {
            get
            {
                if (_classes == null)
                {
                    _classes = new List<Class>()
                    {

                    };
                }
                return _classes;
            }
        }
    }
}
