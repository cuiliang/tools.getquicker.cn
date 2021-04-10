using System.IO;
using System.Text;
using ChoETL;
using Markdig;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QuickerWebTools.Entities;
using ReverseMarkdown;

namespace QuickerWebTools.Controllers
{
    /// <summary>
    /// Markdown相关处理
    /// </summary>
    [Route("api/[controller]/[action]")]
    [Produces(typeof(string))]
    [ApiController]
    public class JsonController : ControllerBase
    {
      

       
        /// <summary>
        /// 将Json代码片段转换为Csv代码
        /// </summary>
        /// <param name="vm">在请求体中传入要转换的json代码内容（应该为数组，每项的各属性值类型为简单类型）</param>
        /// <returns>Csv文本</returns>
        [HttpPost]
        public string ToCsv([FromBody] CommonRequestVm vm)
        {
            return ConvertJsonToCsv(vm.Source);
        }

        private string ConvertJsonToCsv(string vmSource)
        {
            using (var r = new ChoJSONReader(new JsonTextReader(new StringReader(vmSource))))
            {
                using var stream = new MemoryStream();
                
                using (var w = new ChoCSVWriter(stream).WithFirstLineHeader())
                {
                    w.Write(r);
                }

                return Encoding.UTF8.GetString(stream.ToArray());
            }
        }
    }
}
