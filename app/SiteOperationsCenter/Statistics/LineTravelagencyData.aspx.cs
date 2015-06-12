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

namespace SiteOperationsCenter.Statistics
{
    /// <summary>
    /// 在线组团统计
    /// Lym 2010-09-20
    /// </summary>
    public partial class LineTravelagencyData : EyouSoft.Common.Control.YunYingPage
    {
        protected int intPage = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            intPage = Utils.GetInt(Request.QueryString["Page"]);
            if (!Page.IsPostBack)
            {
                #region 权限判断
                //查询
                if (!this.CheckMasterGrant(YuYingPermission.统计分析_在线组团社))
                {
                    Utils.ResponseNoPermit(YuYingPermission.统计分析_在线组团社, true);
                    return;
                }
                #endregion

                int ProvinceId = Utils.GetInt(Request.QueryString["ProvinceId"]);
                if (ProvinceId > 0)
                {
                    this.ProvinceAndCityList1.SetProvinceId = ProvinceId;
                    int CityId = Utils.GetInt(Request.QueryString["CityId"]);
                    if (CityId > 0)
                    {
                        this.ProvinceAndCityList1.SetCityId = CityId;
                    }
                }          

                string CompanyName = Server.UrlDecode(Utils.InputText(Request.QueryString["CompanyName"]));
                if (!string.IsNullOrEmpty(CompanyName))
                {
                    this.txtCompanyName.Value = CompanyName;
                }
                string CommendName = Server.UrlDecode(Utils.InputText(Request.QueryString["CommendName"]));
                if (!string.IsNullOrEmpty(CommendName))
                {
                    this.txtCommendName.Value = CommendName;
                }
                if (ProvinceId > 0  || !string.IsNullOrEmpty(CompanyName) || !string.IsNullOrEmpty(CommendName))
                {
                    EyouSoft.Common.Function.MessageBox.ResponseScript(this.Page, "LineTravelagencyData.OnSearch();");
                }
                
            }

        }
    }
}
