﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Data
    {
        #region fields
        private readonly byte _blockSize;
        private readonly byte[] _bytes;
        private readonly Encoding _encoding;
        #endregion

        #region properties
        public int NumberOfBlocks { get { return _bytes.Count() / _blockSize; } }
        #endregion

        #region constructors
        public Data(string data) : this(data, Encoding.Unicode, 8) { }

        public Data(string data, Encoding encoding, byte blockSize)
        {
            _blockSize = blockSize;
            _encoding = encoding;
            _bytes = encoding.GetBytes(data);
            int leftover = _bytes.Count() % _blockSize; // if plain text is not divisible by size of a block
            if(leftover != 0)
            {
                byte[] nulls = Enumerable.Repeat((byte)0, _blockSize - leftover).ToArray(); // generate missing bytes
                _bytes = _bytes.Concat(nulls).ToArray();                                                       // concat arrays
            }
        }
        #endregion

        #region methods
        /// <summary>
        /// Takes n-th block of plain unecrypted text
        /// </summary>
        /// <param name="blockIndex">Index of the block</param>
        /// <returns>Byte array of that block</returns>
        public byte[] GetNBlock(int blockIndex)
        {
            if ((blockIndex) * _blockSize < _bytes.Count())
            {
                return _bytes.Skip((blockIndex) * _blockSize).Take(_blockSize).ToArray();
            }
            throw new IndexOutOfRangeException("blockIndex is too big");
        }
        #endregion
    }
}
