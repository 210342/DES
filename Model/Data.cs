using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Data
    {
        private readonly byte _blockSize = 8;
        private readonly byte[] _bytes;
        private readonly Encoding _encoding;

        public Data(string data) : this(data, Encoding.UTF8) { }

        public Data(string data, Encoding encoding)
        {
            _encoding = encoding;
            _bytes = encoding.GetBytes(data);
            int leftover = _bytes.Count() % _blockSize; // if plain text is not divisible by 64 bits (8 bytes)
            if(leftover != 0)
            {
                byte[] nulls = Enumerable.Repeat((byte)0, leftover).ToArray(); // generate missing bytes
                _bytes.Concat(nulls);                                          // concat arrays
            }
        }

        /// <summary>
        /// Takes n-th block of plain unecrypted text
        /// </summary>
        /// <param name="blockIndex">Index of the block</param>
        /// <returns>Byte array of that block</returns>
        public byte[] GetNBlock(int blockIndex)
        {
            if ((blockIndex - 1) * _blockSize < _bytes.Count())
            {
                return _bytes.Skip((blockIndex - 1) * _blockSize).Take(_blockSize).ToArray();
            }
            throw new IndexOutOfRangeException("blockIndex is too big");
        }
    }
}
