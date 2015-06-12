using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SiteOperationsCenter.NewsCenterControl
{
    /// <summary>
    /// 描述：运营后台新闻关键字或Tag选择页面
    /// 修改记录：
    /// 1. 2010-03-31 AM 曹胡生 创建
    /// </summary>
    public partial class SelectKeys : EyouSoft.Common.Control.YunYingPage
    {
        //要输出的html
        public string Html = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            Databind();
        }

        public void Databind()
        {
            //要显示关键字(1),还是Tag(2)
            string keyOrTags = EyouSoft.Common.Utils.GetQueryStringValue("keyOrTags");
            //关键字Tag标题
            string keyOrTagsTitle = EyouSoft.Common.Utils.GetQueryStringValue("keyOrTagsTitle");
            //是否要异步请求数据
            bool isAjax = Convert.ToBoolean(EyouSoft.Common.Utils.GetQueryStringValue("isAjax"));
            EyouSoft.BLL.NewsStructure.TagKeyInfo tagKeyInfoBLL = new EyouSoft.BLL.NewsStructure.TagKeyInfo();
            IList<EyouSoft.Model.NewsStructure.TagKeyInfo> list = new List<EyouSoft.Model.NewsStructure.TagKeyInfo>();
            System.Text.StringBuilder str = new System.Text.StringBuilder();
            //是关键字
            if (keyOrTags == "1")
            {
                if (!string.IsNullOrEmpty(keyOrTagsTitle))
                {
                    list = tagKeyInfoBLL.GetAllKeyWord(keyOrTagsTitle);

                }
                else
                {
                    list = tagKeyInfoBLL.GetAllKeyWord();

                }
            }
                //Tag
            else if (keyOrTags == "2")
            {
                if (!string.IsNullOrEmpty(keyOrTagsTitle))
                {
                    list = tagKeyInfoBLL.GetAllTag(keyOrTagsTitle);

                }
                else
                {
                    list = tagKeyInfoBLL.GetAllTag();

                }
            }
            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    //每行显示8列,奇数列显示颜色:white,偶数列显示颜色:#f3f7ff
                    if (i % 8 == 0)
                    {
                        str.AppendFormat("<tr bgcolor=\"{0}\">", ((i / 8)) % 2 == 0 ? "#f3f7ff" : "white");
                    }
                    str.AppendFormat("<td><input id=\"{0}\" name=\"selKey\" type=\"checkbox\" /><label for=\"{0}\">{1}</label></td>", list[i].Id, list[i].ItemName);
                    if (i + 1 % 10 == 0 && i > 0)
                    {
                        str.Append("</tr>");
                    }
                }
            }
            //是否异步请求
            if (isAjax)
            {
                Response.Clear();
                Response.Write(str);
                Response.End();
            }
            list = null;
            tagKeyInfoBLL = null;
            Html = str.ToString();
        }
    }
}
