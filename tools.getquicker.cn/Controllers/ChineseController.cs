using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuickerWebTools.Entities;
using ToolGood.Words;

namespace QuickerWebTools.Controllers
{
    /// <summary>
    /// 中文及汉字拼音相关处理
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Produces(typeof(string))]
    public class ChineseController : ControllerBase
    {
        #region 获取拼音

        /// <summary>
        /// 获取汉字句子的拼音
        /// </summary>
        /// <param name="source">汉字句子</param>
        /// <param name="separator">拼音之间的分隔字符（可选）</param>
        /// <param name="tone">是否输出音标</param>
        /// <param name="forName">是否获取姓名的拼音</param>
        /// <returns>拼音字符串</returns>
        [HttpGet]
        public string GetPinyin(string source, string separator = "", bool tone = false, bool forName = false)
        {
            return GetPinyinInternal(source, separator, tone, forName);
        }


        /// <summary>
        /// 获取汉字句子的拼音
        /// </summary>
        /// <param name="vm">请求体</param>
        /// <param name="separator">拼音之间的分隔字符（可选）</param>
        /// <param name="tone">是否输出音标</param>
        /// <param name="forName">是否获取姓名的拼音</param>
        /// <returns>拼音字符串</returns>
        [HttpPost]
        public string GetPinyin([FromBody] CommonRequestVm vm, string separator = "", bool tone = false, bool forName = false)
        {
            return GetPinyinInternal(vm.Source, separator, tone, forName);
        }

        /// <summary>
        /// 获取拼音
        /// </summary>
        /// <param name="source"></param>
        /// <param name="separator"></param>
        /// <param name="tone"></param>
        /// <param name="forName"></param>
        /// <returns></returns>
        private static string GetPinyinInternal(string source, string separator, bool tone, bool forName)
        {
            if (forName)
            {
                return WordsHelper.GetPinyinForName(source, separator, tone);
            }
            else
            {
                return WordsHelper.GetPinyin(source, separator, tone);
            }
        }
        #endregion


        #region 拼音首字母

        /// <summary>
        /// 获取句子的拼音首字母。
        /// </summary>
        /// <remarks>
        /// “我爱中国” => WAZG
        /// </remarks>
        /// <param name="source">汉字句子</param>
        /// <returns>拼音首字母字符串</returns>
        [HttpGet]
        public string GetFirstPinyin(string source)
        {
            return WordsHelper.GetFirstPinyin(source);
        }

        /// <summary>
        /// 获取句子的拼音首字母。“我爱中国” => WAZG
        /// </summary>
        /// <param name="vm">请求体</param>
        /// <returns>拼音首字母字符串</returns>
        [HttpPost]
        public string GetFirstPinyin([FromBody] CommonRequestVm vm)
        {
            return WordsHelper.GetFirstPinyin(vm.Source);
        }

        #endregion


        #region 一个汉字的所有拼音

        /// <summary>
        /// 获取一个汉字的所有拼音
        /// </summary>
        /// <param name="source">一个汉字</param>
        /// <param name="separator">分隔符</param>
        /// <param name="tone">是否输出音调</param>
        /// <returns>这个汉字的所有拼音，多音字使用逗号分隔</returns>
        [HttpGet]
        public string GetAllPinyin(string source, string separator = ",", bool tone = false)
        {
            return GetAllPinyinInternal(source, separator, tone);
        }

        private static string GetAllPinyinInternal(string source, string separator, bool tone)
        {
            if (string.IsNullOrEmpty(source))
            {
                return "";
            }

            return string.Join(separator, WordsHelper.GetAllPinyin(source[0], tone));
        }

        /// <summary>
        /// 获取一个汉字的所有拼音
        /// </summary>
        /// <param name="vm">请求体</param>
        /// <param name="separator">分隔符</param>
        /// <param name="tone">是否输出音调</param>
        /// <returns>这个汉字的所有拼音，多音字使用逗号分隔</returns>
        [HttpPost]
        public string GetAllPinyin([FromBody] CommonRequestVm vm, string separator = ",", bool tone = false)
        {
            return GetAllPinyinInternal(vm.Source, separator, tone);
        }
        #endregion



        #region 繁体转简体
        /// <summary>
        /// 繁体转简体
        /// </summary>
        /// <param name="source">繁体汉字内容</param>
        /// <param name="type">0、繁体中文，1、港澳繁体，2、台湾正体</param>
        /// <returns>简体中文结果</returns>
        [HttpGet]
        public string ToSimplifiedChinese(string source, int type = 0)
        {
            return ToSimplifiedChineseInternal(source, type);
        }

        /// <summary>
        /// 繁体转简体
        /// </summary>
        /// <param name="vm">请求体。繁体汉字内容</param>
        /// <param name="type">0、繁体中文，1、港澳繁体，2、台湾正体</param>
        /// <returns>简体中文结果</returns>
        [HttpPost]
        public string ToSimplifiedChinese([FromBody] CommonRequestVm vm, int type = 0)
        {
            return ToSimplifiedChineseInternal(vm.Source, type);
        }


        /// <summary>
        /// 转成简体
        /// </summary>
        /// <param name="source">繁体汉字</param>
        /// <param name="type">0、繁体中文，1、港澳繁体，2、台湾正体</param>
        /// <returns></returns>
        private string ToSimplifiedChineseInternal(string source, int type)
        {
            return WordsHelper.ToSimplifiedChinese(source, type);
        }

        #endregion

        #region 简体转繁体
        /// <summary>
        /// 简体转繁体
        /// </summary>
        /// <param name="source">简体中文内容</param>
        /// <param name="type">0、繁体中文，1、港澳繁体，2、台湾正体</param>
        /// <returns>转换结果</returns>
        [HttpGet]
        public string ToTraditionalChinese(string source, int type = 0)
        {
            return ToTraditionalChineseInternal(source, type);
        }

        /// <summary>
        /// 简体转繁体
        /// </summary>
        /// <param name="vm">请求体。简体中文内容</param>
        /// <param name="type">0、繁体中文，1、港澳繁体，2、台湾正体</param>
        /// <returns>转换结果</returns>
        [HttpPost]
        public string ToTraditionalChinese([FromBody] CommonRequestVm vm, int type = 0)
        {
            return ToTraditionalChineseInternal(vm.Source, type);
        }


        /// <summary>
        /// 转成繁体中文
        /// </summary>
        /// <param name="source">简体中文</param>
        /// <param name="type">0、繁体中文，1、港澳繁体，2、台湾正体</param>
        /// <returns></returns>
        private string ToTraditionalChineseInternal(string source, int type)
        {
            return WordsHelper.ToTraditionalChinese(source, type);
        }

        #endregion


        #region 半角与全角转换

        /// <summary>
        /// 转换为全角
        /// </summary>
        /// <remarks>
        /// abcABC123 => ａｂｃＡＢＣ１２３
        /// </remarks>
        /// <param name="source">半角内容</param>
        /// <returns>全角内容</returns>
        [HttpGet]
        public string ToSBC(string source)
        {
            return WordsHelper.ToSBC(source);
        }

        /// <summary>
        /// 转换为全角
        /// </summary>
        /// <remarks>
        /// abcABC123 => ａｂｃＡＢＣ１２３
        /// </remarks>
        /// <param name="vm">请求体。半角内容</param>
        /// <returns>全角内容</returns>
        [HttpPost]
        public string ToSBC([FromBody] CommonRequestVm vm)
        {
            return WordsHelper.ToSBC(vm.Source);
        }


        /// <summary>
        /// 全角转换为半角
        /// </summary>
        /// <remarks>
        /// ａｂｃＡＢＣ１２３=》abcABC123
        /// </remarks>
        /// <param name="source">全角内容</param>
        /// <returns>半角内容</returns>
        [HttpGet]
        public string ToDBC(string source)
        {
            return WordsHelper.ToDBC(source);
        }

        /// <summary>
        /// 全角转换为半角
        /// </summary>
        /// <remarks>
        /// ａｂｃＡＢＣ１２３=》abcABC123
        /// </remarks>
        /// <param name="vm">请求体。全角内容</param>
        /// <returns>半角内容</returns>
        [HttpPost]
        public string ToDBC([FromBody] CommonRequestVm vm)
        {
            return WordsHelper.ToDBC(vm.Source);
        }
        #endregion

        #region RMB

        /// <summary>
        /// 中文金额转换为数字
        /// </summary>
        /// <param name="source">中文金额，如壹佰贰拾叁亿肆仟伍佰陆拾柒万捌仟玖佰零壹元壹角贰分</param>
        /// <returns>数字金额</returns>
        [HttpGet]
        public decimal ToRmbNumber(string source)
        {
            
            return WordsHelper.ToNumber(source);
        }

        /// <summary>
        /// 中文金额转换为数字
        /// </summary>
        /// <param name="vm">请求体。中文金额，如壹佰贰拾叁亿肆仟伍佰陆拾柒万捌仟玖佰零壹元壹角贰分</param>
        /// <returns>数字金额</returns>
        [HttpPost]
        public decimal ToRmbNumber([FromBody] CommonRequestVm vm)
        {
            return ToRmbNumber(vm.Source);
        }

        #endregion
    }
}
