using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using QuickerWebTools.Entities;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace QuickerWebTools.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Produces(typeof(string))]
    public class YamlController : ControllerBase
    {
        /// <summary>
        /// 将Yaml转换为Json
        /// </summary>
        /// <param name="source">Yaml内容</param>
        /// <returns>json内容</returns>
        [HttpGet]
        public string YamlToJson(string source)
        {
            if (String.IsNullOrEmpty(source))
            {
                return String.Empty;
            }

            var deserializer = new DeserializerBuilder().Build();
            using var r = new StringReader(source);

            var yamlObject = deserializer.Deserialize(r);

            var serializer = new SerializerBuilder()
                .JsonCompatible()
                .Build();

            var json = serializer.Serialize(yamlObject);

            return json;
        }

        /// <summary>
        /// 将Yaml转换为Json
        /// </summary>
        /// <param name="vm">请求体</param>
        /// <returns>json内容</returns>
        [HttpPost]
        public string YamlToJson([FromBody] CommonRequestVm vm)
        {
            return YamlToJson(vm.Source);
        }


        /// <summary>
        /// json数据转换为yaml数据
        /// </summary>
        /// <param name="source">json内容</param>
        /// <returns>yaml内容</returns>
        [HttpGet]
        public string JsonToYaml(string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return string.Empty;
            }

            var expConverter = new ExpandoObjectConverter();
            dynamic deserializedObject = JsonConvert.DeserializeObject<ExpandoObject>(source, expConverter);

            var serializer = new YamlDotNet.Serialization.Serializer();
            string yaml = serializer.Serialize(deserializedObject);

            return yaml;
        }

        /// <summary>
        /// json数据转换为yaml数据
        /// </summary>
        /// <param name="vm">请求体</param>
        /// <returns>yaml内容</returns>
        [HttpPost]
        public string JsonToYaml([FromBody] CommonRequestVm vm)
        {
            return JsonToYaml(vm.Source);
        }
    }
}
