using System;
using System.IO;
using AngleSharp.Html;
using AngleSharp.Html.Parser;
using Markdig;
using Microsoft.AspNetCore.Mvc;
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
    public class MarkdownController : ControllerBase
    {
        #region Html2Markdown

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
        public string Html2MarkDownPost([FromBody] CommonRequestVm vm)
        {
            return ConvertHtmlToMarkdown(vm.Source);
        }



        #endregion


        #region Markdown2Html

        /// <summary>
        /// 将Markdown代码转换为html
        /// </summary>
        /// <param name="source">待转换的Markdown代码</param>
        /// <returns>html内容</returns>
        [HttpGet]
        public string Markdown2Html(string source)
        {
            var html = ConvertMarkdownToHtml(source);

            return html;
        }


        /// <summary>
        /// 将Markdown代码转换为html
        /// </summary>
        /// <param name="vm">在请求体中传入要转换的markdown代码内容</param>
        /// <returns>HTML代码</returns>
        [HttpPost]
        [Route("/api/[controller]/Markdown2Html")]
        public string Markdown2Html([FromBody] CommonRequestVm vm)
        {
            return ConvertMarkdownToHtml(vm.Source);
        }


        #endregion



        #region 内部代码


        /// <summary>
        /// 将html转换为markdown
        /// </summary>
        /// <param name="source">待转换的html代码</param>
        /// <returns>生成的markdown代码</returns>
        public static string ConvertHtmlToMarkdown(string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return "";
            }



            var config = new ReverseMarkdown.Config
            {
                UnknownTags = Config.UnknownTagsOption.PassThrough,

                // generate GitHub flavoured markdown, supported for BR, PRE and table tags
                GithubFlavored = true,
                // will ignore all comments
                RemoveComments = true,
                // remove markdown output for links where appropriate
                SmartHrefHandling = true
            };

            var converter = new ReverseMarkdown.Converter(config);

            //var markdown = converter.Convert(ClearHtml(source));
            
            var markdown = converter.Convert(source);

            return markdown;
        }

        private static string ClearHtml(string html)
        {
            try
            {
                var parser = new HtmlParser();
                var document = parser.ParseDocument(html);
                

                using (var writer = new StringWriter())
                {
                    document.ToHtml(writer, new MinifyMarkupFormatter());
                    return writer.ToString();
                }
            }
            catch (Exception ex)
            {
                return html;
            }
        }



        /// <summary>
        /// 将Markdown转换为HTML
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private static string ConvertMarkdownToHtml(string source)
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                return source;
            }

            // Configure the pipeline with all advanced extensions active
            var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
            var html = Markdown.ToHtml(source, pipeline);
            return html;
        }

        #endregion


    }
}
