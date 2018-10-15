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
            if(key.Count() == 8)
            {
                _key = key;
            }
            _56BitKey = KeyPermutation();
        }

        public Key(UInt64 key)
        {
            _key = new byte[8];
            for(byte i = 0; i < 8; ++i)
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
            byte[] bitOrder = { 56, 48, 40, 32, 24, 16, 8, 0,    // Table 12.2 from source
                                57, 49, 41, 33, 25, 17, 9, 1,    // with substracted one
                                58, 50, 42, 34, 26, 18, 10, 2,   // from every number
                                59, 51, 43, 35, 62, 54, 46, 38,  // to match array indexing
                                30, 22, 14, 6, 61, 53, 45, 37,
                                29, 21, 13, 5, 60, 52, 44, 36,
                                28, 20, 12, 4, 27, 19, 11, 3}; 
            for (byte i = 0; i < result.Count(); ++i)
            {
                for (byte j = 0; j < 8; ++j) // 8 bits in a byte
                {
                    byte bitIndex = (byte)(i * 8 + j);
                    sbyte bitShift = (sbyte)(7 - j);
                    byte tmp = Encryptor.GetBit(_key, bitOrder[bitIndex]);
                    result[i] |= Encryptor.LeftBitShift(tmp, bitShift);
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
            UInt32 leftHalf = GetIntFromByteArray(_56BitKey.Take(4).ToArray());    // both halves will have 28 bits, 
            UInt32 rightHalf = GetIntFromByteArray(_56BitKey.Skip(3).Take(4).ToArray());   // 4 bits reserved
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
            for(byte i = 0; i < 3; ++i) // rewrite first 3 bytes
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
            byte[] bitOrder = { 13, 16, 10, 23, 0, 4, 2, 27,     // Table 12.4 from source
                                14, 5, 20, 9, 22, 18, 11, 3,     // with substracted one
                                25, 7, 15, 6, 26, 19, 12, 1,     // from every number
                                40, 51, 30, 36, 46, 54, 29, 39,  // to match array indexing
                                50, 44, 32, 47, 43, 48, 38, 55,
                                33, 52, 45, 41, 49, 35, 28, 31};
            for (int i = 0; i < result.Count(); ++i)
            {
                for (int j = 0; j < 8; ++j) // 8 bits in a byte
                {
                    byte bitIndex = (byte)(i * 8 + j);
                    sbyte bitShift = (sbyte)(7 - j);
                    byte tmp = Encryptor.GetBit(key, bitOrder[bitIndex]);
                    result[i] |= Encryptor.LeftBitShift(tmp, bitShift);
                }
            }
            return result;
        }

#if DEBUG
        public static UInt32 GetIntFromByteArray(byte[] original)
#else
        private static UInt32 GetIntFromByteArray(byte[] original)
#endif
        {
            UInt32 result = 0;
            for(byte i = 0; i < original.Count(); i++)
            {
                result = result << 8;
                result |= original[i];
            }
            return result;
        }
    }
}
