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
using System.Collections.Generic;
using EyouSoft.Common;
namespace SiteOperationsCenter.HotelManagement
{
    /// <summary>
    /// Ajax团队订单回复列表
    /// 2010-12-10
    /// 袁惠
    /// </summary>
    public partial class AjaxRevertInfo : EyouSoft.Common.Control.YunYingPage
    {
        protected string tourId = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!CheckMasterGrant(YuYingPermission.酒店后台管理_团队订单管理))
                {
                    Utils.ResponseNoPermit(YuYingPermission.酒店后台管理_团队订单管理, true);
                    return;
                }
                int pageSize = 5;
                int intRecordCount = 0;
                int pageIndex = EyouSoft.Common.Utils.GetInt(Request.QueryString["Page"]);
                int id=EyouSoft.Common.Utils.GetInt(Request.QueryString["id"]);
                tourId = id.ToString();
                //获取回复列表
                IList<EyouSoft.Model.HotelStructure.HotelTourCustomsAsk> list = EyouSoft.BLL.HotelStructure.HotelTourCustoms.CreateInstance().GetListAsk(pageSize,pageIndex,ref intRecordCount,id);
                if (list != null && list.Count > 0)
                {              
                    this.ExporPageInfoSelect1.intPageSize = pageSize;
                    this.ExporPageInfoSelect1.intRecordCount = intRecordCount;
                    this.ExporPageInfoSelect1.CurrencyPage = pageIndex;
                    this.ExporPageInfoSelect1.HrefType = Adpost.Common.ExporPage.HrefTypeEnum.JsHref; 
                    this.ExporPageInfoSelect1.AttributesEventAdd("onclick", "GroupOrderSearch.LoadData(this,\"" + id + "\");", 1);
                    this.ExporPageInfoSelect1.AttributesEventAdd("onchange", "GroupOrderSearch.LoadData(this,\"" + id + "\");", 0);
                    crptRevers.DataSource = list;
                    crptRevers.DataBind();
                    list = null;
                }
                else
                {
                    Response.Clear();
                    Response.Write("<table width=\"100%\" border=\"1\" cellpadding=\"0\" cellspacing=\"0\" bordercolor=\"#BAD4F2\" ><tr><td colspan=\"16\" bgcolor=\"#E8F2FE\"><div style=\"text-align:center;  margin-top:20px; margin-bottom:20px;\">暂无团队订单回复信息！</span></div></td></tr></table>");
                    Response.End();
                }
            }
        }
    }
}
