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
            _sut = new Key(0x133457799BBCDFF1);
            _sut.GenerateSubkeys();
            Assert.AreEqual(0x1B, _sut.Subkeys.ElementAt(0).ElementAt(0));
            Assert.AreEqual(0x02, _sut.Subkeys.ElementAt(0).ElementAt(1));
            Assert.AreEqual(0xEF, _sut.Subkeys.ElementAt(0).ElementAt(2));
            Assert.AreEqual(0xFC, _sut.Subkeys.ElementAt(0).ElementAt(3));
            Assert.AreEqual(0x70, _sut.Subkeys.ElementAt(0).ElementAt(4));
            Assert.AreEqual(0x72, _sut.Subkeys.ElementAt(0).ElementAt(5));
            Assert.AreEqual(0x79, _sut.Subkeys.ElementAt(1).ElementAt(0));
            Assert.AreEqual(0xAE, _sut.Subkeys.ElementAt(1).ElementAt(1));
            Assert.AreEqual(0xD9, _sut.Subkeys.ElementAt(1).ElementAt(2));
            Assert.AreEqual(0xDB, _sut.Subkeys.ElementAt(1).ElementAt(3));
            Assert.AreEqual(0xC9, _sut.Subkeys.ElementAt(1).ElementAt(4));
            Assert.AreEqual(0xE5, _sut.Subkeys.ElementAt(1).ElementAt(5));
            Assert.AreEqual(0x55, _sut.Subkeys.ElementAt(2).ElementAt(0));
            Assert.AreEqual(0xFC, _sut.Subkeys.ElementAt(2).ElementAt(1));
            Assert.AreEqual(0x8A, _sut.Subkeys.ElementAt(2).ElementAt(2));
            Assert.AreEqual(0x42, _sut.Subkeys.ElementAt(2).ElementAt(3));
            Assert.AreEqual(0xCF, _sut.Subkeys.ElementAt(2).ElementAt(4));
            Assert.AreEqual(0x99, _sut.Subkeys.ElementAt(2).ElementAt(5));
            Assert.AreEqual(0x72, _sut.Subkeys.ElementAt(3).ElementAt(0));
            Assert.AreEqual(0xAD, _sut.Subkeys.ElementAt(3).ElementAt(1));
            Assert.AreEqual(0xD6, _sut.Subkeys.ElementAt(3).ElementAt(2));
            Assert.AreEqual(0xDB, _sut.Subkeys.ElementAt(3).ElementAt(3));
            Assert.AreEqual(0x35, _sut.Subkeys.ElementAt(3).ElementAt(4));
            Assert.AreEqual(0x1D, _sut.Subkeys.ElementAt(3).ElementAt(5));
            Assert.AreEqual(0x7C, _sut.Subkeys.ElementAt(4).ElementAt(0));
            Assert.AreEqual(0xEC, _sut.Subkeys.ElementAt(4).ElementAt(1));
            Assert.AreEqual(0x07, _sut.Subkeys.ElementAt(4).ElementAt(2));
            Assert.AreEqual(0xEB, _sut.Subkeys.ElementAt(4).ElementAt(3));
            Assert.AreEqual(0x53, _sut.Subkeys.ElementAt(4).ElementAt(4));
            Assert.AreEqual(0xA8, _sut.Subkeys.ElementAt(4).ElementAt(5));
            Assert.AreEqual(0x63, _sut.Subkeys.ElementAt(5).ElementAt(0));
            Assert.AreEqual(0xA5, _sut.Subkeys.ElementAt(5).ElementAt(1));
            Assert.AreEqual(0x3E, _sut.Subkeys.ElementAt(5).ElementAt(2));
            Assert.AreEqual(0x50, _sut.Subkeys.ElementAt(5).ElementAt(3));
            Assert.AreEqual(0x7B, _sut.Subkeys.ElementAt(5).ElementAt(4));
            Assert.AreEqual(0x2F, _sut.Subkeys.ElementAt(5).ElementAt(5));
            Assert.AreEqual(0xEC, _sut.Subkeys.ElementAt(6).ElementAt(0));
            Assert.AreEqual(0x84, _sut.Subkeys.ElementAt(6).ElementAt(1));
            Assert.AreEqual(0xB7, _sut.Subkeys.ElementAt(6).ElementAt(2));
            Assert.AreEqual(0xF6, _sut.Subkeys.ElementAt(6).ElementAt(3));
            Assert.AreEqual(0x18, _sut.Subkeys.ElementAt(6).ElementAt(4));
            Assert.AreEqual(0xBC, _sut.Subkeys.ElementAt(6).ElementAt(5));
            Assert.AreEqual(0xF7, _sut.Subkeys.ElementAt(7).ElementAt(0));
            Assert.AreEqual(0x8A, _sut.Subkeys.ElementAt(7).ElementAt(1));
            Assert.AreEqual(0x3A, _sut.Subkeys.ElementAt(7).ElementAt(2));
            Assert.AreEqual(0xC1, _sut.Subkeys.ElementAt(7).ElementAt(3));
            Assert.AreEqual(0x3B, _sut.Subkeys.ElementAt(7).ElementAt(4));
            Assert.AreEqual(0xFB, _sut.Subkeys.ElementAt(7).ElementAt(5));
            Assert.AreEqual(0xE0, _sut.Subkeys.ElementAt(8).ElementAt(0));
            Assert.AreEqual(0xDB, _sut.Subkeys.ElementAt(8).ElementAt(1));
            Assert.AreEqual(0xEB, _sut.Subkeys.ElementAt(8).ElementAt(2));
            Assert.AreEqual(0xED, _sut.Subkeys.ElementAt(8).ElementAt(3));
            Assert.AreEqual(0xE7, _sut.Subkeys.ElementAt(8).ElementAt(4));
            Assert.AreEqual(0x81, _sut.Subkeys.ElementAt(8).ElementAt(5));
            Assert.AreEqual(0xB1, _sut.Subkeys.ElementAt(9).ElementAt(0));
            Assert.AreEqual(0xF3, _sut.Subkeys.ElementAt(9).ElementAt(1));
            Assert.AreEqual(0x47, _sut.Subkeys.ElementAt(9).ElementAt(2));
            Assert.AreEqual(0xBA, _sut.Subkeys.ElementAt(9).ElementAt(3));
            Assert.AreEqual(0x46, _sut.Subkeys.ElementAt(9).ElementAt(4));
            Assert.AreEqual(0x4F, _sut.Subkeys.ElementAt(9).ElementAt(5));
            Assert.AreEqual(0x21, _sut.Subkeys.ElementAt(10).ElementAt(0));
            Assert.AreEqual(0x5F, _sut.Subkeys.ElementAt(10).ElementAt(1));
            Assert.AreEqual(0xD3, _sut.Subkeys.ElementAt(10).ElementAt(2));
            Assert.AreEqual(0xDE, _sut.Subkeys.ElementAt(10).ElementAt(3));
            Assert.AreEqual(0xD3, _sut.Subkeys.ElementAt(10).ElementAt(4));
            Assert.AreEqual(0x86, _sut.Subkeys.ElementAt(10).ElementAt(5));
            Assert.AreEqual(0x75, _sut.Subkeys.ElementAt(11).ElementAt(0));
            Assert.AreEqual(0x71, _sut.Subkeys.ElementAt(11).ElementAt(1));
            Assert.AreEqual(0xF5, _sut.Subkeys.ElementAt(11).ElementAt(2));
            Assert.AreEqual(0x94, _sut.Subkeys.ElementAt(11).ElementAt(3));
            Assert.AreEqual(0x67, _sut.Subkeys.ElementAt(11).ElementAt(4));
            Assert.AreEqual(0xE9, _sut.Subkeys.ElementAt(11).ElementAt(5));
            Assert.AreEqual(0x97, _sut.Subkeys.ElementAt(12).ElementAt(0));
            Assert.AreEqual(0xC5, _sut.Subkeys.ElementAt(12).ElementAt(1));
            Assert.AreEqual(0xD1, _sut.Subkeys.ElementAt(12).ElementAt(2));
            Assert.AreEqual(0xFA, _sut.Subkeys.ElementAt(12).ElementAt(3));
            Assert.AreEqual(0xBA, _sut.Subkeys.ElementAt(12).ElementAt(4));
            Assert.AreEqual(0x41, _sut.Subkeys.ElementAt(12).ElementAt(5));
            Assert.AreEqual(0x5F, _sut.Subkeys.ElementAt(13).ElementAt(0));
            Assert.AreEqual(0x43, _sut.Subkeys.ElementAt(13).ElementAt(1));
            Assert.AreEqual(0xB7, _sut.Subkeys.ElementAt(13).ElementAt(2));
            Assert.AreEqual(0xF2, _sut.Subkeys.ElementAt(13).ElementAt(3));
            Assert.AreEqual(0xE7, _sut.Subkeys.ElementAt(13).ElementAt(4));
            Assert.AreEqual(0x3A, _sut.Subkeys.ElementAt(13).ElementAt(5));
            Assert.AreEqual(0xBF, _sut.Subkeys.ElementAt(14).ElementAt(0));
            Assert.AreEqual(0x91, _sut.Subkeys.ElementAt(14).ElementAt(1));
            Assert.AreEqual(0x8D, _sut.Subkeys.ElementAt(14).ElementAt(2));
            Assert.AreEqual(0x3D, _sut.Subkeys.ElementAt(14).ElementAt(3));
            Assert.AreEqual(0x3F, _sut.Subkeys.ElementAt(14).ElementAt(4));
            Assert.AreEqual(0x0A, _sut.Subkeys.ElementAt(14).ElementAt(5));
            Assert.AreEqual(0xCB, _sut.Subkeys.ElementAt(15).ElementAt(0));
            Assert.AreEqual(0x3D, _sut.Subkeys.ElementAt(15).ElementAt(1));
            Assert.AreEqual(0x8B, _sut.Subkeys.ElementAt(15).ElementAt(2));
            Assert.AreEqual(0x0E, _sut.Subkeys.ElementAt(15).ElementAt(3));
            Assert.AreEqual(0x17, _sut.Subkeys.ElementAt(15).ElementAt(4));
            Assert.AreEqual(0xF5, _sut.Subkeys.ElementAt(15).ElementAt(5));
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