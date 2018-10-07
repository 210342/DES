using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Enum
{
    enum BitMasks : byte
    {
        Zero = 0x00,
        First = 0x01,
        Second = 0x02,
        Third = 0x04,
        Fourth = 0x08,
        Fifth = 0x10,
        Sixth = 0x20,
        Seventh = 0x40,
        Eighth = 0x80,
        Full = 0xFF
    }
}
