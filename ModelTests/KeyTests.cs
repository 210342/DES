using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Tests
{
    [TestClass()]
    public class KeyTests
    {
        private Key _sut;

        [TestMethod()]
        public void KeyTest()
        {
            _sut = new Key();
            Assert.IsNotNull(_sut);
        }

        [TestMethod()]
        public void KeyPermutationTest()
        {
            byte[] example = Enumerable.Repeat((byte)0xC0, 8).ToArray();
            _sut = new Key(example);
            example = _sut.KeyPermutation();
            Assert.AreEqual(0xFF, example[0]);
            Assert.AreEqual(0xFF, example[1]);
            Assert.AreEqual(0x00, example[2]);
            Assert.AreEqual(0x00, example[3]);
            Assert.AreEqual(0x00, example[4]);
            Assert.AreEqual(0x00, example[5]);
            Assert.AreEqual(0x00, example[6]);
        }

        [TestMethod()]
        public void GenerateSubkeysTest()
        {
        }

        [TestMethod()]
        public void SubkeyShiftTest()
        {
            UInt32 example = 0x077FFFFA;
            Assert.AreEqual((UInt32)0x07FFFFA7, Key.SubkeyShift(example, 4));
        }

        [TestMethod()]
        public void CarryOverTest()
        {
        }

        [TestMethod()]
        public void SubkeyPermutationTest()
        {
        }

        [TestMethod()]
        public void GetIntFromByteArrayTest()
        {
            byte[] example = Enumerable.Repeat((byte)0xAA, 4).ToArray();
            Assert.AreEqual(0xAAAAAAAA, Key.GetIntFromByteArray(example));
        }
    }
}