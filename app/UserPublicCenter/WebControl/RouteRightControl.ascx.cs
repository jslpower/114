using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EyouSoft.Common.Control;
using EyouSoft.Common;
namespace UserPublicCenter.WebControl
{
    /// <summary>
    /// 热门旅游城市/旅游景点/酒店
    /// 开发人：刘玉灵  时间：2010-6-24
    /// </summary>
    public partial class RouteRightControl : System.Web.UI.UserControl
    {
        /// <summary>
        /// 热门旅游景点
        /// </summary>
        protected string strHotSightSpot = "";
        /// <summary>
        /// 热门旅游酒店
        /// </summary>
        protected string strHotHouse = "";
        /// <summary>
        /// 当前销售城市ID
        /// </summary>
        protected int CityId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            FrontPage page = this.Page as FrontPage;
            CityId = page.CityId;
            if (!page.IsPostBack)
            {
                this.GetHotSightSpot();
                this.GetHotHouse();
            }

        }

        /// <summary>
        /// 热门旅游景点
        /// </summary>
        protected void GetHotSightSpot()
        {
           
            IList<EyouSoft.Model.AdvStructure.AdvInfo> AdvList = EyouSoft.BLL.AdvStructure.Adv.CreateInstance().GetAdvs(CityId, EyouSoft.Model.AdvStructure.AdvPosition.线路频道热门景点展示);
            if (AdvList != null && AdvList.Count > 0)
            {
                StringBuilder AllAdv = new StringBuilder();
                foreach (EyouSoft.Model.AdvStructure.AdvInfo item in AdvList)
                {
                    AllAdv.AppendFormat(" <a href=\"{0}\" target=\"_blank\">{1}</a><br/>", Utils.GeneratePublicCenterUrl( item.RedirectURL,CityId), Utils.GetText(item.Title, 16));
                }
                strHotSightSpot = AllAdv.ToString();
            }
            AdvList = null;

        }

        /// <summary>
        /// 热门酒店
        /// </summary>
        protected void GetHotHouse()
        {
            IList<EyouSoft.Model.AdvStructure.AdvInfo> AdvList = EyouSoft.BLL.AdvStructure.Adv.CreateInstance().GetAdvs(CityId, EyouSoft.Model.AdvStructure.AdvPosition.线路频道热闹酒店展示);
            if (AdvList != null && AdvList.Count > 0)
            {
                StringBuilder AllAdv = new StringBuilder();
                foreach (EyouSoft.Model.AdvStructure.AdvInfo item in AdvList)
                {
                    AllAdv.AppendFormat("<a href=\"{0}\" target=\"_blank\">{1}</a><br/>", Utils.GeneratePublicCenterUrl(item.RedirectURL, CityId), Utils.GetText(item.Title, 16));
                }
                strHotHouse = AllAdv.ToString();
            }
            AdvList = null;

        }
    }
}