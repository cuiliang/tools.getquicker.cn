using Microsoft.AspNetCore.Mvc;
using QuickerWebTools.Entities;

namespace QuickerWebTools.Controllers
{
    /// <summary>
    /// Markdown相关处理
    /// </summary>
    [Route("api/[controller]/[action]")]
    [Produces(typeof(string))]
    [ApiController]
    public class MarkdownController : ControllerBase
    {
        /// <summary>
        /// 将Html代码片段转换为Markdown代码（Get方式，只适合少量数据）
        /// </summary>
        /// <param name="source">经过URL编码的HTML代码片段</param>
        /// <returns>Markdown文本</returns>
        [HttpGet]
        public string Html2MarkDown(string source)
        {
            return ConvertHtmlToMarkdown(source);
        }

        /// <summary>
        /// 将Html代码片段转换为Markdown代码
        /// </summary>
        /// <param name="vm">在请求体中传入要转换的html代码内容</param>
        /// <returns>Markdown文本</returns>
        [HttpPost]
        [Route("/api/[controller]/Html2MarkDown")]
        public string Html2MarkDownPost([FromBody]CommonRequestVm vm)
        {
            return ConvertHtmlToMarkdown(vm.Source);
        }


        private static string ConvertHtmlToMarkdown(string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return "";
            }

            var converter = new ReverseMarkdown.Converter();
            var markdown = converter.Convert(source);
            return markdown;
        }
    }
}
