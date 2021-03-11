using System.Diagnostics.CodeAnalysis;

namespace QuickerWebTools.Entities
{
    /// <summary>
    /// 通用Post请求体，用于指定要处理的内容。
    /// </summary>
    public class CommonRequestVm
    {
        /// <summary>
        /// 待处理的内容
        /// </summary>
        [NotNull]
        public string Source { get; set; }
    }
}
