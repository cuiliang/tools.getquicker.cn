using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickerWebTools.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickerWebTools.Entities;

namespace QuickerWebTools.Controllers.Tests
{
    [TestClass()]
    public class ChineseControllerTests
    {
        ChineseController controller = new ChineseController();


        [TestMethod()]
        public void ToDBCTest()
        {
            
            Assert.AreEqual("abcABC123", controller.ToDBC("ａｂｃＡＢＣ１２３"));
            Assert.AreEqual("abcABC123", controller.ToDBC(new CommonRequestVm()
            {
                Source = "ａｂｃＡＢＣ１２３"
            }));

            Assert.AreEqual("abcA\r\nBC123", controller.ToDBC("ａｂｃＡ\r\nＢＣ１２３"));
            Assert.AreEqual("abcA\r\nBC123", controller.ToDBC(new CommonRequestVm()
            {
                Source = "ａｂｃＡ\r\nＢＣ１２３"
            }));
        }

        [TestMethod()]
        public void ToSBCTest()
        {

            Assert.AreEqual("ａｂｃＡＢＣ１２３", controller.ToSBC("abcABC123"));
            Assert.AreEqual("ａｂｃＡＢＣ１２３", controller.ToSBC(new CommonRequestVm()
            {
                Source = "abcABC123"
            }));

            Assert.AreEqual("ａｂｃＡ\r\nＢＣ１２３", controller.ToSBC("abcA\r\nBC123"));
            Assert.AreEqual("ａｂｃＡ\r\nＢＣ１２３", controller.ToSBC(new CommonRequestVm()
            {
                Source = "abcA\r\nBC123"
            }));
        }

        [TestMethod()]
        public void ToRmbNumberTest()
        {
            Assert.AreEqual((decimal)12345678901.12, controller.ToRmbNumber("壹佰贰拾叁亿肆仟伍佰陆拾柒万捌仟玖佰零壹元壹角贰分"));
            Assert.AreEqual((decimal)12345678901.12, controller.ToRmbNumber(new CommonRequestVm()
            {
                Source = "壹佰贰拾叁亿肆仟伍佰陆拾柒万捌仟玖佰零壹元壹角贰分"
            }));
        }
    }
}