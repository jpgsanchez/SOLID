﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using startup.examples.principles.srp;
using startup.examples.transverseobjects.common;

namespace startup.examples.Test.srp
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string strresult = string.Empty;
            pRichTextFile objread = new pRichTextFile();
            var currentdirectory = System.IO.Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;

            strresult = objread.getTextonFile(new clsModel() { strpath = string.Concat(currentdirectory, @"\files\XMLFileTest.xml") });
            Assert.IsNotNull(strresult);

            strresult = objread.getTextonFile(new clsModel() { strpath = string.Concat(currentdirectory, @"\files\TextFileTest.txt") });
            Assert.IsNotNull(strresult);
        }

        [TestMethod]
        public void TestMethod2()
        {
            byte[] strresult;
            pRichTextFile objread = new pRichTextFile();
            var currentdirectory = System.IO.Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;

            strresult = objread.getAllBytesfromFile(new clsModel() { strpath = string.Concat(currentdirectory, @"\files\XMLFileTest.xml") });
            Assert.IsNotNull(strresult);

            strresult = objread.getAllBytesfromFile(new clsModel() { strpath = string.Concat(currentdirectory, @"\files\TextFileTest.txt") });
            Assert.IsNotNull(strresult);
        }

        [TestMethod]
        public void TestMethod3()
        {
            personas strresult = new personas();
            pExternalFileProcess objread = new pExternalFileProcess();
            var currentdirectory = System.IO.Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;

            strresult = objread.getObjetctfromXML(new clsModel() { strpath = string.Concat(currentdirectory, @"\files\XMLFileTest.xml") });
            Assert.IsNotNull(strresult);
        }
    }
}