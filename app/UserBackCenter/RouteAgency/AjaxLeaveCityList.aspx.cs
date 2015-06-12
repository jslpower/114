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

namespace UserBackCenter.RouteAgency
{
    /// <summary>
    /// 根据线路区域获取出港城市
    /// 罗丽娥   2010-06-25
    /// </summary>
    public partial class AjaxLeaveCityList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string AreaID = Utils.GetFormValue(Request.QueryString["AreaID"]);
                string LeaveCitys = Utils.GetFormValue(Request.QueryString["LeaveCitys"]);
                //string CompanyId = Utils.GetFormValue(Request.QueryString["CompanyID"]);
                GetLeaveCity("0");
            }
        }

        #region 获取出港城市
        /// <summary>
        /// 获取出港城市
        /// </summary>
        private void GetLeaveCity(string CompanyID)
        {
            // 根据专线商的ID获取公司下面的线路区域及出港城市
                        

            //string sitelist = "";
            //if (dssite != null && dssite.Tables.Count > 0 && dssite.Tables[0].Rows.Count > 0)
            //{
            //    for (int j = 0; j < dssite.Tables[0].Rows.Count; j++)
            //    {
            //        sitelist += dssite.Tables[0].Rows[j]["SiteId"].ToString() + ",";
            //    }
            //    sitelist = sitelist.Trim(",".ToCharArray());
            //}
            //if (dssite != null)
            //    dssite.Dispose();
            //dssite = null;
            //if (!string.IsNullOrEmpty(sitelist))
            //{
            //    TourUnion.BLL.TourUnion_SysCity Sbll = new TourUnion.BLL.TourUnion_SysCity();
            //    //修改条件 
            //    //章已泉 2009-6-16
            //    DataSet ds = Sbll.GetCityNew(" u.Id in (" + sitelist + ")");
            //    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //    {
            //        LeaveCity.DataSource = ds;
            //        LeaveCity.DataBind();
            //        ds.Dispose();
            //        this.table_NoData.Visible = false;
            //    }
            //    else
            //    {
            //        this.lblError.Text = "该线路区域暂无任何出港城市!";
            //        this.table_NoData.Visible = true;
            //    }

            //    ds = null;
            //    Sbll = null;
            //}
            //else
            //{
            //    this.table_NoData.Visible = true;
            //}
            //bll = null;
        }
        #endregion 
    }
}
