using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System.Linq;
using System.Text;

namespace Model.Tests
{
    [TestClass()]
    public class HelpersTests
    {
        [TestMethod()]
        public void GetIntFromByteArrayTest()
        {
            byte[] example = Enumerable.Repeat((byte)0xAA, 4).ToArray();
            Assert.AreEqual(0xAAAAAAAA, Helpers.GetUInt32FromByteArray(example));
        }

        [TestMethod()]
        public void LeftBitShiftTest()
        {
            byte example = 0x08;
            Assert.AreEqual(0x10, Helpers.LeftBitShift(example, 1));
            Assert.AreEqual(0x04, Helpers.LeftBitShift(example, -1));
        }

        [TestMethod()]
        public void GetBitTest()
        {
            byte[] example = Enumerable.Repeat((byte)0x01, 2).ToArray();
            Assert.AreEqual(0x00, Helpers.GetBit(example, 0));
            Assert.AreEqual(0x01, Helpers.GetBit(example, 15));
        }

        [TestMethod()]
        public void XORByteTablesTest()
        {
            byte[] left = { 0xFF, 0x00, 0x00, 0xFF };
            byte[] right = { 0x00, 0xFF, 0x00, 0xFF };
            byte[] result = Helpers.XORByteTables(left, right);
            Assert.AreEqual(0xFF, result[0]);
            Assert.AreEqual(0xFF, result[1]);
            Assert.AreEqual(0x00, result[2]);
            Assert.AreEqual(0x00, result[3]);
        }

        [TestMethod()]
        public void ConvertToHexadecimalStringTest()
        {
            Assert.AreEqual("4C 6F 72 65 6D 20 69 70 73 75 6D",
                Helpers.ConvertToHexadecimalString("Lorem ipsum"));
        }

        [TestMethod()]
        public void ConvertToHexadecimalStringTest1()
        {
            Assert.AreEqual("4C 6F 72 65 6D 20 69 70 73 75 6D",
                Helpers.ConvertToHexadecimalString
                (Encoding.Default.GetBytes("Lorem ipsum")));
        }

        [TestMethod()]
        public void ConvertFromHexadecimalStringTest()
        {
            Assert.AreEqual("Lorem ipsum",
                Helpers.ConvertFromHexadecimalString
                ("4C 6F 72 65 6D 20 69 70 73 75 6D"));
        }

        [TestMethod()]
        public void TruncateEndingZerosTest()
        {
            byte[] example = new byte[] { 0x00, 0x15, 0x22, 0x00, 0x00 };
            example = example.TruncateEndingZeros();
            Assert.AreEqual(3, example.Count());
        }
    }
}