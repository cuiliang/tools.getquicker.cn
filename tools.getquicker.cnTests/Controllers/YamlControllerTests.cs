using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickerWebTools.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickerWebTools.Controllers.Tests
{
    [TestClass()]
    public class YamlControllerTests
    {
        string yaml = @"scalar: a scalar
sequence:
- one
- two
";
        string json = "{\"scalar\": \"a scalar\", \"sequence\": [\"one\", \"two\"]}";

        YamlController controller = new YamlController();

        [TestMethod()]
        public void YamlToJsonTest()
        {
            Assert.AreEqual(json, controller.YamlToJson(yaml).TrimEnd());
        }

        [TestMethod()]
        public void YamlToJsonTest1()
        {
            Assert.AreEqual(yaml, controller.JsonToYaml(json));
        }
    }
}