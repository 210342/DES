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
            _sut = new Key();
            // to implement
        }

        [TestMethod()]
        public void SubkeyShiftTest()
        {
            UInt32 example = 0x077FFFFA;
            Assert.AreEqual((UInt32)0x07FFFFA7, Key.SubkeyShift(example, 4));
        }

        [TestMethod()]
        public void SubkeyPermutationTest()
        {
            byte[] example = { 0x43, 0x09, 0x10, 0x66, 0x09, 0x82, 0x12 };
            example = Key.SubkeyPermutation(example);
            Assert.AreEqual(0x00, example[0]);
            Assert.AreEqual(0x00, example[1]);
            Assert.AreEqual(0xFF, example[2]);
            Assert.AreEqual(0xFF, example[3]);
            Assert.AreEqual(0x00, example[4]);
            Assert.AreEqual(0x00, example[5]);
        }

        [TestMethod()]
        public void GetIntFromByteArrayTest()
        {
            byte[] example = Enumerable.Repeat((byte)0xAA, 4).ToArray();
            Assert.AreEqual(0xAAAAAAAA, Key.GetIntFromByteArray(example));
        }

        [TestMethod()]
        public void MergeHalvesTest()
        {
            UInt32 left = 0x0000055F, right = 0x0FAA0000;
            byte[] example = Key.MergeHalves(left, right);
            Assert.AreEqual(0x00, example[0]);
            Assert.AreEqual(0x00, example[1]);
            Assert.AreEqual(0x55, example[2]);
            Assert.AreEqual(0xFF, example[3]);
            Assert.AreEqual(0xAA, example[4]);
            Assert.AreEqual(0x00, example[5]);
            Assert.AreEqual(0x00, example[6]);
        }

        [TestMethod()]
        public void KeyCtorUInt64Test()
        {
            UInt64 tmp = 0x11224488AACCEEFF;
            _sut = new Key(tmp);
            Assert.AreEqual(0x11, _sut.MainKey[0]);
            Assert.AreEqual(0x22, _sut.MainKey[1]);
            Assert.AreEqual(0x44, _sut.MainKey[2]);
            Assert.AreEqual(0x88, _sut.MainKey[3]);
            Assert.AreEqual(0xAA, _sut.MainKey[4]);
            Assert.AreEqual(0xCC, _sut.MainKey[5]);
            Assert.AreEqual(0xEE, _sut.MainKey[6]);
            Assert.AreEqual(0xFF, _sut.MainKey[7]);
        }
    }
}