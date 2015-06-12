using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Control;
using System.Text;
namespace UserPublicCenter.HomeControl
{
    /// <summary>
    /// 首页公告/爆料区
    /// </summary>
    public partial class PostList : System.Web.UI.UserControl
    {
        private bool _isDisclose = false;

        /// <summary>
        /// 是否是爆料区 默认为公告
        /// </summary>
        public bool IsDisclose
        {
            get { return _isDisclose; }
            set { _isDisclose = value; }
        }
        protected string strAllPostList = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GetPostList();
            }
        }

        /// <summary>
        /// 获的广告列表
        /// </summary>
        protected void GetPostList()
        {
            FrontPage page = this.Page as FrontPage;
            int CityId = page.CityId;
            EyouSoft.Model.AdvStructure.AdvPosition PositionType = EyouSoft.Model.AdvStructure.AdvPosition.首页广告公告;
            if (IsDisclose)
            {
                PositionType = EyouSoft.Model.AdvStructure.AdvPosition.首页广告爆料区;
            }
            IList<EyouSoft.Model.AdvStructure.AdvInfo> AdvList = EyouSoft.BLL.AdvStructure.Adv.CreateInstance().GetAdvs(CityId,PositionType);
            if (AdvList != null && AdvList.Count > 0)
            {
                StringBuilder strAdvList = new StringBuilder();
                strAdvList.Append("<ul>");
                foreach (EyouSoft.Model.AdvStructure.AdvInfo item in AdvList)
                {
                    strAdvList.AppendFormat("<li style='text-align:left;'><a href='/PlaneInfo/NewsDetailInfo.aspx?NewsID={0}&CityId={2}' target=\"_blank\">{1}</a></li>", item.AdvId, EyouSoft.Common.Utils.GetText(item.Title, 16), CityId);
                }
                strAdvList.Append("</ul>");
                strAllPostList = strAdvList.ToString();
            }
            else
            {
                strAllPostList = "<ul><li></li><li></li><li></li><li></li><li>暂无信息</li><li></li><li></li><li></li><li></li></ul>";
            }
        }
    }
}