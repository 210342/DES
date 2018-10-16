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
        private readonly Key _key;
        private readonly byte[][] sBoxes = { Tables.S1, Tables.S2, Tables.S3, Tables.S4,
                                             Tables.S5, Tables.S6, Tables.S7, Tables.S8 };

        public Encryptor(Key key)
        {
            _key = key;
        }

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

                    for(byte round = 0; round < 16; ++round)
                    {
                        block = Round(block.Take(4).ToArray(), block.Skip(4).Take(4).ToArray(), round);
                    }

                    block = ReversedInitialPermutation(block);
                }
                catch
                {
                    throw;
                }

            }
        }

#if DEBUG
        public byte[] Round(byte[] left, byte[] right, byte iteration)
#else
        private byte[] Round(byte[] left, byte[] right, byte iteration)
#endif
        {
            byte[] result = new byte[64];
            byte[] newLeft = right;
            byte[] newright = CalculateRightHalf(left, right, iteration);

            return result;
        }

#if DEBUG
        public byte[] CalculateRightHalf(byte[] left, byte[] right, byte iteration)
#else
        private byte[] CalculateRightHalf(byte[] left, byte[] right, byte iteration)
#endif
        {
            byte[] result = new byte[6];
            right = ExpansionPermutation(right);
            byte[] XORed = Helpers.XORByteTables(right, _key.Subkeys.ElementAt(iteration));

            // change array of 6 bytes to an array of 8 bytes with 2 reserved bits

            //

            return result;
        }

#if DEBUG
        public static byte[] ExpansionPermutation(byte[] right)
#else
        private byte[] ExpansionPermutation(byte[] right)
#endif
        {
            byte[] result = new byte[6];
            for (int i = 0; i < result.Count(); ++i)
            {
                for (int j = 0; j < 8; ++j) // 8 bits in a byte
                {
                    byte bitIndex = (byte)(i * 8 + j);
                    sbyte bitShift = (sbyte)(7 - j);
                    byte tmp = Helpers.GetBit(right, Tables.ExpansionPermutation[bitIndex]);
                    result[i] |= Helpers.LeftBitShift(tmp, bitShift);
                }
            }
            return result;
        }

#if DEBUG
        public byte GetSBoxValue(byte sixBits, byte iteration)
#else
        private byte GetSBoxValue(byte sixBits, byte iteration)
#endif
        {
            byte row = (byte)(sixBits >> 5);
            row = (byte)(row << 1);
            byte column = (byte)(sixBits >> 1);
            column = (byte)(column & 0x0F);
            return sBoxes[iteration][row * 16 + column];
        }

#if DEBUG
        public byte[] ConvertByteArrayTo6BitArray(byte[] original)
#else
        private byte[] ConvertByteArrayTo6BitArray(byte[] original)
#endif
        {
            byte[] result = new byte[8];
            result[0] = (byte)(original[0] >> 2);
            result[1] = (byte)((byte)((byte)(original[0] << 4) | (byte)(original[1] >> 4)) & 0x3F);
            result[2] = (byte)((byte)((byte)(original[1] << 2) | (byte)(original[2] >> 4)) & 0x3F);
            result[3] = (byte)(original[2] & 0x3F);
            result[4] = (byte)(original[3] >> 2);
            result[5] = (byte)((byte)((byte)(original[3] << 4) | (byte)(original[4] >> 4)) & 0x3F);
            result[6] = (byte)((byte)((byte)(original[4] << 2) | (byte)(original[5] >> 4)) & 0x3F);
            result[7] = (byte)(original[5] & 0x3F);
            return result;
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
                        result[i] |= Helpers.LeftBitShift(tmp, bitShift);
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
                        result[bytesOrder[i]] |= Helpers.LeftBitShift(tmp, bitShift);
                    }
                }
                return result;
            }
            throw new ArgumentException("Array size doesn't match block size");
        }
    }
}
