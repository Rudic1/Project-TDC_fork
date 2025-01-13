using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TDC.Constants
{
    public class InvalidCharacters
    {
        public readonly List<string> InvalidTitle = [";", "\\", "/"];
        public readonly List<string> InvalidGeneral = [";", "\n"];
    }
}
