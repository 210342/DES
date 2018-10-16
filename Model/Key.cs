using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Enum;

// Source: https://www.nku.edu/~christensen/DESschneier.pdf

namespace Model
{
    public class Key
    {
        private readonly byte[] _key = new byte[8]; // 8 bytes = 64 bits
        private readonly byte[] _56BitKey = new byte[7];

#if DEBUG
        public byte[] MainKey { get { return _key; } }
#endif

        public ICollection<byte[]> Subkeys = new List<byte[]>();

        public Key()
        {
            Random rng = new Random();
            rng.NextBytes(_key);
            _56BitKey = KeyPermutation();
        }

        public Key(byte[] key)
        {
            if (key.Count() == 8)
            {
                _key = key;
            }
            _56BitKey = KeyPermutation();
        }

        public Key(UInt64 key)
        {
            _key = new byte[8];
            for (byte i = 0; i < 8; ++i)
            {
                _key[i] = (byte)(key >> (7 - i) * 8);
            }
            _56BitKey = KeyPermutation();
        }

#if DEBUG
        public byte[] KeyPermutation()
#else
        private byte[] KeyPermutation()
#endif
        {
            byte[] result = new byte[7]; // already ignore every 8th bit
            for (byte i = 0; i < result.Count(); ++i)
            {
                for (byte j = 0; j < 8; ++j) // 8 bits in a byte
                {
                    byte bitIndex = (byte)(i * 8 + j);
                    sbyte bitShift = (sbyte)(7 - j);
                    byte tmp = Helpers.GetBit(_key, Tables.KeyPermutation[bitIndex]);
                    result[i] |= Helpers.LeftBitShift(tmp, bitShift);
                }
            }
            return result;
        }

#if DEBUG
        public void GenerateSubkeys()
#else
        private void GenerateSubkeys()
#endif
        {
            byte[] shifts = { 1, 1, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 1 };
            UInt32 leftHalf = Helpers.GetUInt32FromByteArray(_56BitKey.Take(4).ToArray()); // both halves will have 28 bits, 
            UInt32 rightHalf = Helpers.GetUInt32FromByteArray(_56BitKey.Skip(3).Take(4).ToArray()); // 4 bits reserved
            leftHalf = (leftHalf >> 4);                 // move all bits to make most significant bits reserved
            rightHalf = (rightHalf & 0x0FFFFFFF);       // ignore first 4 bits
            for (byte i = 0; i < 16; ++i)
            {
                leftHalf = SubkeyShift(leftHalf, shifts[i]);
                rightHalf = SubkeyShift(rightHalf, shifts[i]);
                Subkeys.Add(SubkeyPermutation(MergeHalves(leftHalf, rightHalf)));
            }
        }

#if DEBUG
        public static byte[] MergeHalves(UInt32 leftHalf, UInt32 rightHalf)
#else
        private byte[] MergeHalves(UInt32 leftHalf, UInt32 rightHalf)
#endif
        {
            byte[] result = new byte[7];
            leftHalf <<= 4; // make least significant bits reserved
            for (byte i = 0; i < 3; ++i) // rewrite first 3 bytes
            {
                result[i] = (byte)(leftHalf >> (3 - i) * 8);
            }
            // rewrite 4th byte
            byte leftMiddleByte = (byte)leftHalf;
            byte rightMiddleByte = (byte)(rightHalf >> 24);
            result[3] = (byte)(leftMiddleByte | rightMiddleByte);
            for (byte i = 1; i < 4; ++i) // rewrite last 3 bytes
            {
                result[3 + i] = (byte)(rightHalf >> (3 - i) * 8);
            }
            return result;
        }

#if DEBUG
        public static UInt32 SubkeyShift(UInt32 halfKey, byte step)
#else
        private UInt32 SubkeyShift(UInt32 halfKey, byte step)
#endif
        {
            UInt32 result = 0;
            result = halfKey << step;
            UInt32 carryOver = result & 0xF0000000;
            carryOver >>= 28; // get bits that should be carried over (circulated)
            result = (result & 0x0FFFFFFF) | carryOver;   // set carried over bits
            return result;
        }

        /// <summary>
        /// Also called 'Compression permutation'
        /// </summary>
        /// <returns></returns>
#if DEBUG
        public static byte[] SubkeyPermutation(byte[] key)
#else
        private byte[] SubkeyPermutation(byte[] key) 
#endif
        {
            byte[] result = new byte[6]; // Generate 48 bit subkeys
            for (int i = 0; i < result.Count(); ++i)
            {
                for (int j = 0; j < 8; ++j) // 8 bits in a byte
                {
                    byte bitIndex = (byte)(i * 8 + j);
                    sbyte bitShift = (sbyte)(7 - j);
                    byte tmp = Helpers.GetBit(key, Tables.SubkeyPermutation[bitIndex]);
                    result[i] |= Helpers.LeftBitShift(tmp, bitShift);
                }
            }
            return result;
        }
    }
}
