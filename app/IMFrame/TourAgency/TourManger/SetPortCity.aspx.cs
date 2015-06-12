using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using Adpost.Common.Function;
using System.Collections.Generic;

namespace TourUnion.WEB.IM.TourAgency
{
    /// <summary>
    /// 页面功能: 每一次登陆时设置出港城市页面
    /// 开发人：刘玉灵  时间：2009-9-9
    /// </summary>
    public partial class SetPortCity : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.btnNext.Attributes.Add("onclick", " CheckIsNext();");
        }

        #region  更新出港城市
        protected void btnNext_Click(object sender, EventArgs e)
        {
            string strCityId = Request.Form["DropSiteAndCityList1$dropCity"].ToString();
            int CityId = 0;
            int SiteId = 0;
            if (strCityId == "0")
            {
                MessageBox.ShowAndRedirect("请选择出港城市!", "SetPortCity.aspx");
            }
            else
            {
                SiteId = int.Parse(strCityId.Split('|')[1]);
                CityId = int.Parse(strCityId.Split('|')[0]);
                if (WEB.ProceFlow.PortCityManage.UpdatePortCity(CityId, SiteId))
                {
                    Response.Redirect("SetAttentionCompany.aspx?IsFristLogin=1");
                }
            }
        }
        #endregion 


    }
}
