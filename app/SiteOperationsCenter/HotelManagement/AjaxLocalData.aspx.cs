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
    /// 酒店管理：本地酒店数据列表Ajax
    /// 2010-12-02，袁惠
    /// </summary>
    public partial class AjaxLocalData :EyouSoft.Common.Control.YunYingPage
    {
        protected int pageSize = 20;
        protected int pageIndex = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!CheckMasterGrant(YuYingPermission.酒店后台管理_首页板块数据管理))
                {
                    Utils.ResponseNoPermit(YuYingPermission.酒店后台管理_首页板块数据管理, true);
                    return;
                }
                pageIndex = Utils.GetInt(Request.QueryString["page"], 1);
                int recordCount = 0;
                int dataType = Utils.GetInt(Request.QueryString["HotelShowType"], 0);  //数据类型
                IList<EyouSoft.Model.HotelStructure.HotelLocalInfo> list = EyouSoft.BLL.HotelStructure.HotelLocalInfo.CreateInstance().GetList(pageSize,pageIndex,ref recordCount,(EyouSoft.Model.HotelStructure.HotelShowType)dataType);
                if (list.Count > 0 && list != null)
                {
                    crptLocalList.DataSource = list;
                    crptLocalList.DataBind();
                    this.ExporPageInfoSelect1.intPageSize = pageSize;
                    this.ExporPageInfoSelect1.intRecordCount = recordCount;
                    this.ExporPageInfoSelect1.CurrencyPage = pageIndex;
                    this.ExporPageInfoSelect1.HrefType = Adpost.Common.ExporPage.HrefTypeEnum.JsHref;
                    this.ExporPageInfoSelect1.AttributesEventAdd("onclick", "FirstPageDataAdd.LoadData(this,\"AjaxLocalData.aspx\");", 1);
                    this.ExporPageInfoSelect1.AttributesEventAdd("onchange", "FirstPageDataAdd.LoadData(this,\"AjaxLocalData.aspx\");", 0);
                    list = null;
                }
                else
                {
                    crptLocalList.EmptyText = "<tr><td colspan=\"4\"><div style=\"text-align:center;  margin-top:75px; margin-bottom:75px;\">暂无数据！</span></div></td></tr>";
                }
            }
        }
    }
}
