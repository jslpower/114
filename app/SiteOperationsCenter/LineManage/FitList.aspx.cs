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
using EyouSoft.Model.SystemStructure;
using System.Text;
using EyouSoft.Model.NewTourStructure;
using System.Collections.Generic;
using EyouSoft.IBLL.SystemStructure;
using EyouSoft.Model.CompanyStructure;

namespace SiteOperationsCenter.LineManage
{
    /// <summary>
    /// 运营后台 散客订单列表查询
    /// 蔡永辉 2011-12-21
    /// </summary>
    public partial class FitList : EyouSoft.Common.Control.YunYingPage
    {
        /// <summary>
        /// 分页
        /// </summary>
        protected int currentPage = 0;
        /// <summary>
        /// 该页面的权限
        /// </summary>
        protected bool IsGrantUpdate = true;
        /// <summary>
        /// 团号id
        /// </summary>
        protected string tourid = "";
        /// <summary>
        /// 专线区域类型
        /// </summary>
        protected string AreaType = "";
        /// <summary>
        /// 专线区域id
        /// </summary>
        protected string Areaid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            currentPage = Utils.GetInt(Utils.GetQueryStringValue("Page"));
            string action = Utils.GetQueryStringValue("action");
            tourid = Utils.GetQueryStringValue("tourid");//团号
            Areaid = Utils.GetQueryStringValue("Line2");
            if (!IsPostBack)
            {
                BindDropDownList();
            }

            if (!string.IsNullOrEmpty(action))
            {
                switch (action)
                {
                    case "GetLineByType"://获取线路根据类型
                        GetLineByType();
                        break;
                    case "GetCompanyByLine"://获取专线商根据线路
                        GetCompanyByLine();
                        break;
                }
            }
        }

        #region 根据专线类型获取专线
        protected void GetLineByType()
        {
            string argument = Utils.GetQueryStringValue("argument");
            ISysArea bll = EyouSoft.BLL.SystemStructure.SysArea.CreateInstance();
            int type = Utils.GetInt(argument, 0);
            IList<SysArea> list = bll.GetSysAreaList((AreaType)type);
            StringBuilder strb1 = new StringBuilder("{tolist:[");
            foreach (SysArea item in list)
            {
                strb1.Append("{\"AreaId\":\"" + item.AreaId + "\",\"AreaName\":\"" + item.AreaName + "\"},");
            }
            Response.Clear();
            Response.Write(strb1.ToString().TrimEnd(',') + "]}");
            Response.End();
        }

        #endregion

        #region 获取专线商根据线路
        protected void GetCompanyByLine()
        {
            int areaid = Utils.GetInt(Utils.GetQueryStringValue("argument"));
            QueryNewCompany Searchmodel = new QueryNewCompany();
            if (areaid != 0)
                Searchmodel.AreaId = areaid;
            IList<CompanyDetailInfo> list = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetAllCompany(0, Searchmodel);
            StringBuilder strb = new StringBuilder("{tolist:[");
            foreach (Company item in list)
            {
                strb.Append("{\"ID\":\"" + item.ID + "\",\"CompanyName\":\"" + item.CompanyName + "\"},");
            }
            Response.Clear();
            Response.Write(strb.ToString().TrimEnd(',') + "]}");
            Response.End();
        }

        #endregion

        #region 绑定列表


        /// <summary>
        /// 绑定订单状态
        /// </summary>
        /// <returns></returns>
        protected string BindOrderStatus()
        {
            StringBuilder str = new StringBuilder("");
            foreach (EnumObj item in EnumObj.GetList(typeof(PowderOrderStatus)))
            {
                str.Append(string.Format("<a href=\"javascript:void(0)\" onclick=\"FitManage.OrderStatus(this,{0})\">{1}</a>&nbsp;&nbsp;", item.Value, item.Text));
            }
            return str.ToString();
        }


        /// <summary>
        /// 绑定支付状态
        /// </summary>
        /// <returns></returns>
        protected string BindPaymentStatus()
        {
            StringBuilder str = new StringBuilder("");
            foreach (EnumObj item1 in EnumObj.GetList(typeof(PaymentStatus)))
            {
                str.Append(string.Format("<a href=\"javascript:void(0)\" onclick=\"FitManage.PaymentStatus(this,{0})\">{1}</a>&nbsp;&nbsp;", item1.Value, item1.Text));
            }
            return str.ToString();
        }



        protected void BindDropDownList()
        {
            //线路类型
            foreach (EnumObj item in EnumObj.GetList(typeof(AreaType)))
            {
                if (Utils.GetInt(item.Value) < 3)
                {
                    this.DropSearchLineType.Items.Add(new ListItem(item.Text, item.Value));
                }
            }
            this.DropSearchLineType.Items.Insert(0, new ListItem("线路类型", "-1"));


            if (!string.IsNullOrEmpty(Areaid))
            {
                AreaType = "";
                SysArea modelsysArea = EyouSoft.BLL.SystemStructure.SysArea.CreateInstance().GetSysAreaModel(Utils.GetInt(Areaid));
                if (modelsysArea != null)
                    AreaType = ((int)modelsysArea.RouteType).ToString();
            }
        }

        #endregion
    }
}
