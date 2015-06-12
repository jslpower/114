using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using EyouSoft.Common.Control;
using System.Text;
using EyouSoft.Common.Function;
using EyouSoft.Common;
using EyouSoft.Common.URLREWRITE;

namespace UserPublicCenter.MasterPage
{
    /// <summary>
    /// 描述：首页数据读取页面
    /// 添加人：HL
    /// 添加时间：2010-04-2
    /// </summary>
    public partial class Default : EyouSoft.Common.Control.FrontPage
    {

        #region 页面加载事件
        /// <summary>
        /// 页面加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //标题以及关键字
                this.Page.Title = string.Format(EyouSoft.Common.PageTitle.Information_Title);
                AddMetaTag("description", string.Format(EyouSoft.Common.PageTitle.Information_Des));
                AddMetaTag("keywords", EyouSoft.Common.PageTitle.Information_Keywords);

                //对友情链接进行类别判断[资讯首页]
                FrindLinkList1.LinkType = "LinkType";
            }
        }
        #endregion


    }
}
