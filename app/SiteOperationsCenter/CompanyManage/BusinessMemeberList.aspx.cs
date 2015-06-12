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
using EyouSoft.Model.ScenicStructure;
using EyouSoft.Model.CompanyStructure;
using EyouSoft.IBLL.SystemStructure;
using System.Text;
using System.Collections.Generic;
using EyouSoft.Model.SystemStructure;

namespace SiteOperationsCenter.CompanyManage
{
    /// <summary>
    /// 功能：运营后台中的个人会员管理列标以及搜索查询
    /// 2011-12-14 蔡永辉
    /// </summary>
    public partial class BusinessMemeberList : EyouSoft.Common.Control.YunYingPage
    {
        protected int currentPage = 0;
        protected string PagePath = "";
        protected int pageSize = 15;
        protected bool IsUpdateGant = false;
        protected bool IsDeleteGant = true;
        protected bool IsAddGant = false;
        protected int recordcount = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            currentPage = Utils.GetInt(Request.QueryString["Page"]);
            string type = Utils.GetQueryStringValue("type");
            string action = Utils.GetQueryStringValue("action");
            string lineId = Utils.GetQueryStringValue("lineId");
            if (!IsPostBack)
            {
                BindSearchDropdownlist();
                if (type == "DeleteBusinessMem")
                {
                    DelpersonalMem();
                }


                if (!string.IsNullOrEmpty(action))
                {
                    switch (action)
                    {
                        case "GetLineByType"://获取线路根据类型
                            GetLineByType();
                            break;
                    }
                }
            }
        }

        #region 删除商家会员
        protected void DelpersonalMem()
        {
            bool boolresult = false;
            string deleteid = Utils.GetQueryStringValue("arg");
            string result = "";
            string[] deleteidlist = deleteid.Split('$');
            string[] newdeleteidlist = { "" };
            for (int i = 0; i < deleteidlist.Length; i++)
            {
                if (!string.IsNullOrEmpty(deleteidlist[i]))
                {
                    newdeleteidlist[i] = deleteidlist[i];
                }
            }
            if (newdeleteidlist.Length > 0)
            {
                boolresult = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().Remove(newdeleteidlist);
                if (boolresult)
                {
                    result = "1";
                }
                else
                    result = "0";
            }
            Response.Clear();
            Response.Write(result);
            Response.End();
        }
        #endregion


        #region 绑定下拉列表
        protected void BindSearchDropdownlist()
        {
            //经营
            //this.dropCompanyType.DataSource = EnumObj.GetList(typeof(CompanyType));
            this.dropCompanyType.DataSource = EnumObj.GetList(typeof(CompanyType));
            this.dropCompanyType.DataTextField = "Text";
            this.dropCompanyType.DataValueField = "Value";
            this.dropCompanyType.DataBind();
            this.dropCompanyType.Items.Insert(0, new ListItem("请选择", "-1"));

            //公司等级
            this.dropCompanyLev.DataSource = EnumObj.GetList(typeof(CompanyLev));
            this.dropCompanyLev.DataTextField = "Text";
            this.dropCompanyLev.DataValueField = "Value";
            this.dropCompanyLev.DataBind();
            this.dropCompanyLev.Items.Insert(0, new ListItem("请选择", "-1"));

            //B2B
            this.dropB2B.DataSource = EnumObj.GetList(typeof(CompanyB2BDisplay));
            this.dropB2B.DataTextField = "Text";
            this.dropB2B.DataValueField = "Value";
            this.dropB2B.DataBind();
            this.dropB2B.Items.Insert(0, new ListItem("请选择", "-1"));

            //B2C
            this.dropB2C.DataSource = EnumObj.GetList(typeof(CompanyB2CDisplay));
            this.dropB2C.DataTextField = "Text";
            this.dropB2C.DataValueField = "Value";
            this.dropB2C.DataBind();
            this.dropB2C.Items.Insert(0, new ListItem("请选择", "-1"));

            //国内国际周边
            this.dropLine.DataSource = EnumObj.GetList(typeof(AreaType));
            this.dropLine.DataTextField = "Text";
            this.dropLine.DataValueField = "Value";
            this.dropLine.DataBind();
            this.dropLine.Items.Insert(0, new ListItem("请选择", "-1"));

            //专线
            EyouSoft.IBLL.SystemStructure.ISysArea bll = EyouSoft.BLL.SystemStructure.SysArea.CreateInstance();
            IList<EyouSoft.Model.SystemStructure.SysArea> list = bll.GetSysAreaList();
            this.dropLine1.DataSource = list;
            this.dropLine1.DataTextField = "AreaName";
            this.dropLine1.DataValueField = "AreaId";
            this.dropLine1.DataBind();
            this.dropLine1.Items.Insert(0, new ListItem("请选择", "0"));
        }
        #endregion


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



    }
}
