using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using EyouSoft.Common;
using System.Collections.Generic;

namespace UserPublicCenter.AirTickets
{
    /// <summary>
    /// 机票采购商首页
    /// 张新兵，20101019
    /// </summary>
    public partial class Default : EyouSoft.Common.Control.FrontPage
    {
        protected string FiveAdvImages = "";

        protected void Page_Load(object sender, EventArgs e)
        {

            AddMetaTag("description", PageTitle.Plane_Des);
            AddMetaTag("keywords", PageTitle.Plane_Keywords);
            this.Page.Title = PageTitle.Plane_Title;

            InitAdv();
        }

        /// <summary>
        /// 广告列表初始化
        /// </summary>
        protected void InitAdv()
        {
            IList<EyouSoft.Model.AdvStructure.AdvInfo> AdvList = EyouSoft.BLL.AdvStructure.Adv.CreateInstance().GetAdvs(CityId, EyouSoft.Model.AdvStructure.AdvPosition.机票频道焦点广告翻屏);
            if (AdvList != null && AdvList.Count > 0)
            {

                foreach (EyouSoft.Model.AdvStructure.AdvInfo item in AdvList)
                {
                    FiveAdvImages += string.Format("<li><a href=\"{1}\" {2}><img src=\"{0}\" height=\"73\" width=\"182\" /></a></li>", EyouSoft.Common.Domain.FileSystem + item.ImgPath, item.RedirectURL, item.RedirectURL == Utils.EmptyLinkCode ? "" : "target=\"_blank\"");
                }
            }
            AdvList = null;
        }
    }
}
