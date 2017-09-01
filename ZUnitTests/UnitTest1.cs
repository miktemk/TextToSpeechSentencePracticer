using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextToSpeechSentencePracticer.Controls;
using Miktemk.TextToSpeech.Core;

namespace ZUnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test_TextBlockWithHighlight_VM_1()
        {
            var vm = new TextBlockWithHighlight_VM();
            vm.Apply("0123456789", new WordHighlight(2, 1));
            Assert.AreEqual(vm.TextBefore, "01");
            Assert.AreEqual(vm.TextHighlight, "2");
            Assert.AreEqual(vm.TextAfter, "3456789");
        }

        [TestMethod]
        public void Test_TextBlockWithHighlight_VM_2()
        {
            var vm = new TextBlockWithHighlight_VM();
            vm.Apply("0123456789", new WordHighlight(2, 3));
            Assert.AreEqual(vm.TextBefore, "01");
            Assert.AreEqual(vm.TextHighlight, "234");
            Assert.AreEqual(vm.TextAfter, "56789");
        }

        [TestMethod]
        public void Test_TextBlockWithHighlight_VM_3()
        {
            var vm = new TextBlockWithHighlight_VM();
            vm.Apply("0123456789", new WordHighlight(7, 3));
            Assert.AreEqual(vm.TextBefore, "0123456");
            Assert.AreEqual(vm.TextHighlight, "789");
            Assert.AreEqual(vm.TextAfter, "");
        }

        [TestMethod]
        public void Test_TextBlockWithHighlight_VM_overflow()
        {
            var vm = new TextBlockWithHighlight_VM();
            vm.Apply("0123456789", new WordHighlight(7, 5));
            Assert.AreEqual(vm.TextBefore, "0123456");
            Assert.AreEqual(vm.TextHighlight, "789");
            Assert.AreEqual(vm.TextAfter, "");
        }

        [TestMethod]
        public void Test_TextBlockWithHighlight_VM_underflow()
        {
            var vm = new TextBlockWithHighlight_VM();
            vm.Apply("0123456789", new WordHighlight(-1, 5));
            Assert.AreEqual(vm.TextBefore, "");
            Assert.AreEqual(vm.TextHighlight, "0123");
            Assert.AreEqual(vm.TextAfter, "456789");
        }

        [TestMethod]
        public void Test_TextBlockWithHighlight_VM_all()
        {
            var vm = new TextBlockWithHighlight_VM();
            vm.Apply("0123456789", new WordHighlight(0, 10));
            Assert.AreEqual(vm.TextBefore, "");
            Assert.AreEqual(vm.TextHighlight, "0123456789");
            Assert.AreEqual(vm.TextAfter, "");
        }

        [TestMethod]
        public void Test_TextBlockWithHighlight_VM_alloverflow()
        {
            var vm = new TextBlockWithHighlight_VM();
            vm.Apply("0123456789", new WordHighlight(-10, 50));
            Assert.AreEqual(vm.TextBefore, "");
            Assert.AreEqual(vm.TextHighlight, "0123456789");
            Assert.AreEqual(vm.TextAfter, "");
        }
    }
}
