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
        private readonly Encryptor _sut = new Encryptor(new Key());

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
        public void ExpansionPermutationTest()
        {
            byte[] example = { 0xF0, 0xAA, 0xF0, 0xAA };
            example = Encryptor.ExpansionPermutation(example);
            Assert.AreEqual(0x7A, example[0]);
            Assert.AreEqual(0x15, example[1]);
            Assert.AreEqual(0x55, example[2]);
            Assert.AreEqual(0x7A, example[3]);
            Assert.AreEqual(0x15, example[4]);
            Assert.AreEqual(0x55, example[5]);
        }

        [TestMethod()]
        public void GetSBoxValueTest()
        {
            byte example = 0x12;
            example = _sut.GetSBoxValue(example, 0);
            Assert.AreEqual(10, example);
        }

        [TestMethod()]
        public void ConvertByteArrayTo6BitArrayTest()
        {
            byte[] example = { 0x0F, 0xF0, 0x0F, 0xF0, 0x0F, 0xF0 };
            example = _sut.ConvertByteArrayTo6BitArray(example);
            Assert.AreEqual((byte)0x03, example[0]);
            Assert.AreEqual((byte)0x3F, example[1]);
            Assert.AreEqual((byte)0x00, example[2]);
            Assert.AreEqual((byte)0x0F, example[3]);
            Assert.AreEqual((byte)0x3C, example[4]);
            Assert.AreEqual((byte)0x00, example[5]);
            Assert.AreEqual((byte)0x3F, example[6]);
            Assert.AreEqual((byte)0x30, example[7]);
        }
    }
}