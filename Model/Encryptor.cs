using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Enum;

// Source: https://www.nku.edu/~christensen/DESschneier.pdf

namespace Model
{
    public class Encryptor
    {
        //private readonly byte[] _encrypted;
        private readonly byte _blockSize = 8; // bytes

        public void Encrypt(string message)
        {
            Encrypt(message, Encoding.Unicode);
        }

        public void Encrypt(string message, Encoding encoding)
        {
            Data data = new Data(message, encoding, _blockSize);
            for(int i = 0; i < data.NumberOfBlocks; ++i)
            {
                try
                {
                    byte[] block = data.GetNBlock(i);
                    block = InitialPermutation(block);


                    block = ReversedInitialPermutation(block);
                }
                catch
                {
                    throw;
                }

            }
        }

#if DEBUG
        public byte[] InitialPermutation(byte[] original)
#else
        private byte[] InitialPermutation(byte[] original)
#endif
        {
            if(_blockSize == original.Count())
            {
                byte[] result = new byte[_blockSize];
                BitMasks[] masks = { BitMasks.First, BitMasks.Second, BitMasks.Third, BitMasks.Fourth,
                                    BitMasks.Fifth, BitMasks.Sixth, BitMasks.Seventh, BitMasks.Eighth };
                byte[] masksOrder = { 6, 4, 2, 0, 7, 5, 3, 1 };
                for (int i = 0; i < result.Count(); ++i)
                {
                    for (int j = 0; j < 8; ++j) // 8 bits in a byte
                    {
                        sbyte bitShift = (sbyte)(7 - masksOrder[i] - j);
                        int tmp = (byte)(original[7 - j] & (byte)masks[masksOrder[i]]);
                        result[i] |= LeftBitShift(tmp, bitShift);
                    }
                }
                return result;
            }
            throw new ArgumentException("Array size doesn't match block size");
        }

#if DEBUG
        public byte[] ReversedInitialPermutation(byte[] original)
#else
        private byte[] ReversedInitialPermutation(byte[] original)
#endif
        {
            if (_blockSize == original.Count())
            {
                byte[] result = new byte[_blockSize];
                BitMasks[] masks = { BitMasks.First, BitMasks.Second, BitMasks.Third, BitMasks.Fourth,
                                     BitMasks.Fifth, BitMasks.Sixth, BitMasks.Seventh, BitMasks.Eighth };
                byte[] bytesOrder = { 4, 0, 5, 1, 6, 2, 7, 3 };
                for(int i = 0; i < bytesOrder.Count(); ++i)
                {
                    for (int j = 7; j >= 0; --j) // 8 bits in a byte
                    {
                        sbyte bitShift = (sbyte)(-7 + i + j);
                        int tmp = (byte)(original[j] & (byte)masks[7 - i]);
                        result[bytesOrder[i]] |= LeftBitShift(tmp, bitShift);
                    }
                }
                return result;
            }
            throw new ArgumentException("Array size doesn't match block size");
        }

        /// <summary>
        /// Shifts bits value times to the left
        /// If value is negative, it shifts bits to the right instead
        /// </summary>
        /// <param name="operand">Operand</param>
        /// <param name="value">Length of the shift</param>
        /// <returns></returns>
        public static byte LeftBitShift(int operand, sbyte value)
        {
            if (value >= 0)
                return (byte)(operand << value);
            else
                return (byte)(operand >> Math.Abs(value));
        }

        /// <summary>
        /// Get bit from byte array given by index
        /// (where zero is the most significant bit
        /// in a byte)
        /// </summary>
        /// <param name="source">Byte array</param>
        /// <param name="index">Index of interested bit</param>
        /// <returns></returns>
        public static byte GetBit(byte[] source, byte index)
        {
            byte byteIndex = (byte)(index / 8);
            byte bitIndex = (byte)(index % 8);
            byte bitMask = (byte)(0x80 >> bitIndex);
            byte bit = (byte)((bitMask & source[byteIndex]) >> (7 - bitIndex));
            return bit;
        }
    }
}
