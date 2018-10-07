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
    public class DataTests
    {
        private Data _sut;

        [TestMethod()]
        public void DataTest()
        {
            _sut = new Data("Noelo");
            Assert.AreEqual(2, _sut.NumberOfBlocks);
        }

        [TestMethod()]
        public void GetNBlockTest()
        {
            _sut = new Data("Noelo");
            Assert.AreEqual("Noel", Encoding.Unicode.GetString(_sut.GetNBlock(0)));
            Assert.AreEqual("o\0\0\0", Encoding.Unicode.GetString(_sut.GetNBlock(1)));
        }
    }
}