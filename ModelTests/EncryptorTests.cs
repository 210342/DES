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
        private readonly Encryptor _sut = new Encryptor(new Key(0x0000000000000000));

        [TestMethod()]
        public void EncryptTest()
        {
            string message = "Lorem ipsum dolor sit amet";
            _sut.Encrypt(message);
            _sut.Decrypt(_sut.EncryptedMessage);
            string decrypted = _sut.DecryptedToString();
            Assert.AreEqual(message, decrypted);
        }

        [TestMethod()]
        public void InitialPermutationTest()
        {
            byte[] example = { 0x01, 0x23, 0x45, 0x67, 0x89, 0xAB, 0xCD, 0xEF };
            example = _sut.InitialPermutation(example);
            Assert.AreEqual(8, example.Count());
            Assert.AreEqual(0xCC, example[0]);
            Assert.AreEqual(0x00, example[1]);
            Assert.AreEqual(0xCC, example[2]);
            Assert.AreEqual(0xFF, example[3]);
            Assert.AreEqual(0xF0, example[4]);
            Assert.AreEqual(0xAA, example[5]);
            Assert.AreEqual(0xF0, example[6]);
            Assert.AreEqual(0xAA, example[7]);
        }

        [TestMethod()]
        public void ReversedInitialPermutationTest()
        {
            byte[] example = Enumerable.Repeat((byte)0x5A, 8).ToArray();
            example = _sut.ReversedInitialPermutation(example);
            Assert.AreEqual(8, example.Count());
            Assert.AreEqual(0x00, example[0]);
            Assert.AreEqual(0xFF, example[1]);
            Assert.AreEqual(0x00, example[2]);
            Assert.AreEqual(0xFF, example[3]);
            Assert.AreEqual(0xFF, example[4]);
            Assert.AreEqual(0x00, example[5]);
            Assert.AreEqual(0xFF, example[6]);
            Assert.AreEqual(0x00, example[7]);
        }

        [TestMethod()]
        public void ReversedInitialPermutationTest2()
        {
            byte[] example = { 0x0A, 0x4C, 0xD9, 0x95, 0x43, 0x42, 0x32, 0x34 };
            example = _sut.ReversedInitialPermutation(example);
            Assert.AreEqual(8, example.Count());
            Assert.AreEqual(0x85, example[0]);
            Assert.AreEqual(0xE8, example[1]);
            Assert.AreEqual(0x13, example[2]);
            Assert.AreEqual(0x54, example[3]);
            Assert.AreEqual(0x0F, example[4]);
            Assert.AreEqual(0x0A, example[5]);
            Assert.AreEqual(0xB4, example[6]);
            Assert.AreEqual(0x05, example[7]);
        }

        [TestMethod()]
        public void ExpansionPermutationTest()
        {
            byte[] example = { 0xF0, 0xAA, 0xF0, 0xAA };
            example = Encryptor.ExpansionPermutation(example);
            Assert.AreEqual(6, example.Count());
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
            Assert.AreEqual(8, example.Count());
            Assert.AreEqual((byte)0x03, example[0]);
            Assert.AreEqual((byte)0x3F, example[1]);
            Assert.AreEqual((byte)0x00, example[2]);
            Assert.AreEqual((byte)0x0F, example[3]);
            Assert.AreEqual((byte)0x3C, example[4]);
            Assert.AreEqual((byte)0x00, example[5]);
            Assert.AreEqual((byte)0x3F, example[6]);
            Assert.AreEqual((byte)0x30, example[7]);
        }

        [TestMethod()]
        public void CalculateSBoxOutputsTest()
        {
            byte[] example = { 0x18, 0x11, 0x1E, 0x3A, 0x21, 0x26, 0x14, 0x27 };
            example = _sut.CalculateSBoxOutputs(example);
            Assert.AreEqual(4, example.Count());
            Assert.AreEqual(0x5C, example[0]);
            Assert.AreEqual(0x82, example[1]);
            Assert.AreEqual(0xB5, example[2]);
            Assert.AreEqual(0x97, example[3]);
        }

        [TestMethod()]
        public void RoundTest()
        {
            byte[] left = { 0xCC, 0x00, 0xCC, 0xFF };
            byte[] right = { 0xF0, 0xAA, 0xF0, 0xAA };
            byte[] key = { 0x1B, 0x02, 0xEF, 0xFC, 0x70, 0x72 };
            byte[] result = _sut.Round(left, right, key);
            Assert.AreEqual(8, result.Count());
            Assert.AreEqual(0xF0, result[0]);
            Assert.AreEqual(0xAA, result[1]);
            Assert.AreEqual(0xF0, result[2]);
            Assert.AreEqual(0xAA, result[3]);
            Assert.AreEqual(0xEF, result[4]);
            Assert.AreEqual(0x4A, result[5]);
            Assert.AreEqual(0x65, result[6]);
            Assert.AreEqual(0x44, result[7]);
        }

        [TestMethod()]
        public void CalculateRightHalfTest()
        {
            byte[] left = { 0xCC, 0x00, 0xCC, 0xFF };
            byte[] right = { 0xF0, 0xAA, 0xF0, 0xAA };
            byte[] key = { 0x1B, 0x02, 0xEF, 0xFC, 0x70, 0x72 };
            byte[] result = _sut.CalculateRightHalf(left, right, key);
            Assert.AreEqual(4, result.Count());
            Assert.AreEqual(0xEF, result[0]);
            Assert.AreEqual(0x4A, result[1]);
            Assert.AreEqual(0x65, result[2]);
            Assert.AreEqual(0x44, result[3]);
        }

        [TestMethod()]
        public void RoundsFinalPermutationTest()
        {
            byte[] example = { 0x5C, 0x82, 0xB5, 0x97 };
            example = _sut.RoundsFinalPermutation(example);
            Assert.AreEqual(4, example.Count());
            Assert.AreEqual(0x23, example[0]);
            Assert.AreEqual(0x4A, example[1]);
            Assert.AreEqual(0xA9, example[2]);
            Assert.AreEqual(0xBB, example[3]);
        }

        [TestMethod()]
        public void ReverseLeftAndRightTest()
        {
            byte[] example = { 0x00, 0x00, 0x00, 0x00, 0xFF, 0xFF, 0xFF, 0xFF };
            example = _sut.ReverseLeftAndRight(example);
            Assert.AreEqual(0xFF, example[0]);
            Assert.AreEqual(0xFF, example[1]);
            Assert.AreEqual(0xFF, example[2]);
            Assert.AreEqual(0xFF, example[3]);
            Assert.AreEqual(0x00, example[4]);
            Assert.AreEqual(0x00, example[5]);
            Assert.AreEqual(0x00, example[6]);
            Assert.AreEqual(0x00, example[7]);
        }

        [TestMethod()]
        public void DecryptedToStringTest()
        {
            string example = "Lorem\0\0\0\0\0\0\0\0\0\0\0";
            _sut.SetDecMes(example);
            Assert.AreEqual("Lorem", _sut.DecryptedToString());
        }
    }
}