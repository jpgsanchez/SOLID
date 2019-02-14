using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using startup.examples.principles.ocp;

using startup.examples.transverseobjects.ocp;
using startup.examples.transverseobjects.common;

namespace startup.examples.Test.ocp
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string strresult = string.Empty;
            IReadFile objread = new pRichTextFile();
            var currentdirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;

            strresult = objread.getTextonFile(new clsModel() { strpath = string.Concat(currentdirectory, @"\files\XMLFileTest.xml") });
            Assert.IsNotNull(strresult);

            strresult = objread.getTextonFile(new clsModel() { strpath = string.Concat(currentdirectory, @"\files\TextFileTest.txt") });
            Assert.IsNotNull(strresult);
        }

        [TestMethod]
        public void TestMethod2()
        {
            byte[] strresult;
            IReadFile objread = new pRichTextFile();
            var currentdirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;

            strresult = objread.getAllBytesfromFile(new clsModel() { strpath = string.Concat(currentdirectory, @"\files\XMLFileTest.xml") });
            Assert.IsNotNull(strresult);

            strresult = objread.getAllBytesfromFile(new clsModel() { strpath = string.Concat(currentdirectory, @"\files\TextFileTest.txt") });
            Assert.IsNotNull(strresult);
        }

        [TestMethod]
        public void TestMethod3()
        {
            personas strresult = new personas();
            IExternalProcess objread = new pExternalFileProcess();
            var currentdirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;

            strresult = objread.getObjetctfromXML(new clsModel() { strpath = string.Concat(currentdirectory, @"\files\XMLFileTest.xml") });
            Assert.IsNotNull(strresult);
        }
    }
}