using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Helpers
    {
        public static UInt32 GetUInt32FromByteArray(byte[] original)
        {
            UInt32 result = 0;
            for (byte i = 0; i < original.Count(); i++)
            {
                result = result << 8;
                result |= original[i];
            }
            return result;
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

        /// <summary>
        /// Execute XOR command on two byte arrays of 
        /// the same size, element by element
        /// </summary>
        /// <param name="left">left operand</param>
        /// <param name="right">right operand</param>
        /// <exception cref="ArgumentException">Arrays have different sizes</exception>
        /// <returns>Byte array with the result</returns>
        public static byte[] XORByteTables(byte[] left, byte[] right)
        {
            if (left.Count() != right.Count())
                throw new ArgumentException("Byte arrays have different sizes");
            byte[] result = new byte[left.Count()];
            for (byte i = 0; i < result.Count(); ++i)
            {
                result[i] = (byte)(left[i] ^ right[i]);
            }
            return result;
        }
    }
}
