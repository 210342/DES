using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Enum
{
    enum BitMasks : byte
    {
        First = 0x01,
        Second = 0x02,
        Third = 0x04,
        Fourth = 0x08,
        Fifth = 0x10,
        Sixth = 0x20,
        Seventh = 0x40,
        Eighth = 0x80,
        Zero = 0x00,
        Full = 0xFF,
        Left = 0xF0,
        Right = 0x0F,
        MostSignificantOne = 0x80,
        MostSignificantTwo = 0xC0,
        MostSignificantThree = 0xE0,
        MostSignificantFive = 0xF8,
        MostSignificantSix = 0xFC,
        MostSignificantSeven = 0xFE,
        LeastSignificantOne = 0x01,
        LeastSignificantTwo = 0x03,
        LeastSignificantThree = 0x07,
        LeastSignificantFive = 0x1F,
        LeastSignificantSix = 0x3F,
        LeastSignificantSeven = 0x7F,

    }
}
