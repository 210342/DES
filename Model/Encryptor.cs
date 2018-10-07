using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Enum;

namespace Model
{
    public class Encryptor
    {
        private readonly byte[] _encrypted;
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
                        int bitShift = 7 - masksOrder[i] - j;
                        int tmp = (byte)(original[7 - j] & (byte)masks[masksOrder[i]]);
                        tmp = LeftBitShift(tmp, bitShift);
                        result[i] |= (byte)((byte)BitMasks.Full & tmp);
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
                        int bitShift = -7 + i + j;
                        int tmp = (byte)(original[j] & (byte)masks[7 - i]);
                        tmp = LeftBitShift(tmp, bitShift);
                        result[bytesOrder[i]] |= (byte)((byte)BitMasks.Full & tmp);
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
        private static int LeftBitShift(int operand, int value)
        {
            if (value >= 0)
                return (operand << value);
            else
                return (operand >> Math.Abs(value));
        }
    }
}
