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
    public class EncryptorTests
    {
        private readonly Encryptor _sut = new Encryptor();

        [TestMethod()]
        public void EncryptTest()
        {
        }

        [TestMethod()]
        public void InitialPermutationTest()
        {
            byte[] example = Enumerable.Repeat((byte)0x5A, 8).ToArray();
            example = _sut.InitialPermutation(example);
            Assert.AreEqual(0xFF, example[0]);
            Assert.AreEqual(0xFF, example[1]);
            Assert.AreEqual(0x00, example[2]);
            Assert.AreEqual(0x00, example[3]);
            Assert.AreEqual(0x00, example[4]);
            Assert.AreEqual(0x00, example[5]);
            Assert.AreEqual(0xFF, example[6]);
            Assert.AreEqual(0xFF, example[7]);
        }

        [TestMethod()]
        public void ReversedInitialPermutationTest()
        {
            byte[] example = Enumerable.Repeat((byte)0x5A, 8).ToArray();
            example = _sut.ReversedInitialPermutation(example);
            Assert.AreEqual(0xFF, example[0]);
            Assert.AreEqual(0xFF, example[1]);
            Assert.AreEqual(0x00, example[2]);
            Assert.AreEqual(0x00, example[3]);
            Assert.AreEqual(0x00, example[4]);
            Assert.AreEqual(0x00, example[5]);
            Assert.AreEqual(0xFF, example[6]);
            Assert.AreEqual(0xFF, example[7]);
        }

        [TestMethod()]
        public void LeftBitShiftTest()
        {
            byte example = 0x08;
            Assert.AreEqual(0x10, Encryptor.LeftBitShift(example, 1));
            Assert.AreEqual(0x04, Encryptor.LeftBitShift(example, -1));
        }

        [TestMethod()]
        public void GetBitTest()
        {
            byte[] example = Enumerable.Repeat((byte)0x01, 2).ToArray();
            Assert.AreEqual(0x00, Encryptor.GetBit(example, 0));
            Assert.AreEqual(0x01, Encryptor.GetBit(example, 15));
        }
    }
}