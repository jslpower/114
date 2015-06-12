using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserPublicCenter.TravelManage
{

    /// <summary>
    /// 旅游用品子页
    /// 功能：显示旅游用品网店信息
    /// 创建人：戴银柱
    /// 创建时间： 2010-07-26  
    /// </summary>
    public partial class TravelDetails : EyouSoft.Common.Control.FrontPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //this.Title=string.Format(EyouSoft.Common.PageTitle.ScenicDetail_Title,CityModel.CityName,
                //设置导航栏
                this.CityAndMenu1.HeadMenuIndex = 7;
                //设置右侧用户控件的赋值
                this.TravelRightControl1.Cid = this.CityId;
                //旅游用品新货上架
                NewTravelInit();
                //获得当前普通旅游用品店的ID
                string agencyId = Request.QueryString["Cid"];
                if (agencyId != null)
                {
                    this.GeneralShopControl1.SetAgencyId = agencyId.ToString();
                }
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            //设置Title.....
            this.Title = string.Format(EyouSoft.Common.PageTitle.TravelDetail_Title, CityModel.CityName, GeneralShopControl1.CompanyName);
            AddMetaTag("description", string.Format(EyouSoft.Common.PageTitle.TravelDetail_Des, CityModel.CityName, GeneralShopControl1.CompanyName));
            AddMetaTag("keywords", EyouSoft.Common.PageTitle.TravelDetail_Keywords);
        }

        #region 旅游集合方法
        /// <summary>
        /// 旅游集合方法
        /// </summary>
        protected IList<EyouSoft.Model.AdvStructure.AdvInfo> GetList(int relationId, EyouSoft.Model.AdvStructure.AdvPosition advPosition)
        {
            IList<EyouSoft.Model.AdvStructure.AdvInfo> List = EyouSoft.BLL.AdvStructure.Adv.CreateInstance().GetAdvs(relationId, advPosition);
            if (List != null && List.Count > 0)
            {
                return List;
            }
            List = null;
            return null;
        }
        #endregion

        #region 旅游新货上架
        protected void NewTravelInit()
        {
            IList<EyouSoft.Model.AdvStructure.AdvInfo> list = GetList(this.CityId, EyouSoft.Model.AdvStructure.AdvPosition.旅游用品频道新货上架);
            if (list != null)
            {
                this.rptNewTravel.DataSource = list;
                this.rptNewTravel.DataBind();
                list = null;

            }
        }
        #endregion
    }
}
