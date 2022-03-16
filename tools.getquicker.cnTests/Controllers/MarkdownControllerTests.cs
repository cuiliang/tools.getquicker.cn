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
    public class MarkdownControllerTests
    {
        [TestMethod()]
        public void ConvertHtmlToMarkdownTest()
        {
            string html = @"<section>
                    <h4 class=""mt-5"" id=""action_changelog"">最近更新</h4>

                    <table class=""table table-bordered table-sm"">
                        <tbody><tr>
                            <th style=""width: 80px"">
                                更新时间
                            </th>
                            <th style=""width: 80px"">
                                修订版本
                            </th>
                            <th>
                                更新说明
                            </th>
                        </tr>
                            <tr>
                                <td class=""font14"">
                                    <span title=""20220309 12:39:18"">
                                        7天5小时前
                                    </span>
                                </td>
                                <td class=""font14"">
                                    22
                                </td>

                                <td class=""font12"">
                                    + 动态自动调整检测间隔，闲时检测次数更少
<br>+ 外部调用重开积累的历史数据，命令参数：getRecentList
                                </td>
                            </tr>
                            <tr>
                                <td class=""font14"">
                                    <span title=""20220302 19:10:33"">
                                        13天23小时前
                                    </span>
                                </td>
                                <td class=""font14"">
                                    21
                                </td>

                                <td class=""font12"">
                                    ~ 检测间隔默认值改为400ms
<br>+ 爆内存通知，请有过Quicker内存占用过高并使用「重开」的用户务必更新查看
                                </td>
                            </tr>
                            <tr>
                                <td class=""font14"">
                                    <span title=""20220223 17:38:59"">
                                        21天0小时前
                                    </span>
                                </td>
                                <td class=""font14"">
                                    20
                                </td>

                                <td class=""font12"">
                                    + 在设置中增加「检测间隔」参数以调整资源占用量
                                </td>
                            </tr>
                    </tbody></table>
                    <a class=""mt-2 mb-2"" title=""查看更多更新历史"" href=""/Share/Actions/Versions?code=e88dba21-983c-4ff4-9cbd-08d945c23a95"">
                        <i class=""fal fa-history text-primary ml-2 mr-0""></i> 更多...
                    </a>
                </section>";

            var md = MarkdownController.ConvertHtmlToMarkdown(html);

            
        }
    }
}