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
using EyouSoft.Model.CompanyStructure;
using System.Collections.Generic;
using EyouSoft.Common;
using System.Text;

namespace SiteOperationsCenter.CompanyManage
{
    /// <summary>
    /// 功能：运营后台中的个人会员管理列标以及搜索查询
    /// 2011-12-14 蔡永辉
    /// </summary>
    public partial class AjaxPersonalMemberList : EyouSoft.Common.Control.YunYingPage
    {
        /// <summary>
        /// 每页显示数
        /// </summary>
        protected int PageSize = 20;
        /// <summary>
        /// 当前页
        /// </summary>
        protected int PageIndex = 1;
        /// <summary>
        /// 序号
        /// </summary>
        private int count = 0;
        protected void Page_Load(object sender, EventArgs e)
        {

            PageIndex = EyouSoft.Common.Utils.GetInt(Request.QueryString["Page"], 1);//获取分页序号
            if (!Page.IsPostBack)
            {
                BindPersonalMemberList();
            }
        }
        /// <summary>
        /// 绑定会员列表
        /// </summary>
        protected void BindPersonalMemberList()
        {
            int recordCount = 0;
            string Companyid = Utils.GetQueryStringValue("Companyid");
            int ProvinceId = EyouSoft.Common.Function.StringValidate.GetIntValue(Request.QueryString["ProvinceId"]);
            int CityId = EyouSoft.Common.Function.StringValidate.GetIntValue(Request.QueryString["CityId"]);
            int CountyId = EyouSoft.Common.Function.StringValidate.GetIntValue(Request.QueryString["CountyId"]);
            string PersonalMemberName = Utils.InputText(Request.QueryString["PersonalMemName"]);
            int Sort = Utils.GetInt(Utils.GetQueryStringValue("Sort"));//排序
            MLBYNUserSearchInfo SearchModel = new MLBYNUserSearchInfo();

            if (ProvinceId > 0)
                SearchModel.ProvinceId = ProvinceId;
            if (CityId > 0)
                SearchModel.CityId = CityId;
            if (CountyId > 0)
                SearchModel.DistrictId = CountyId;
            if (PersonalMemberName != "单位名，地址，联系人，账户")
                SearchModel.Keyword = PersonalMemberName;
            if (Sort > -1)
                SearchModel.OrderingType = (EyouSoft.Model.CompanyStructure.MLBYNUserInfo.OrderingType)Sort;
            if (!string.IsNullOrEmpty(Companyid))
                SearchModel.CompanyId = Companyid;
            IList<MLBYNUserInfo> listUserInfo = EyouSoft.BLL.CompanyStructure.CompanyUser.CreateInstance().GetUsers(PageSize, PageIndex, ref recordCount, SearchModel);

            if (listUserInfo.Count > 0)
            {
                this.ExporPageInfoSelect1.intPageSize = PageSize;
                this.ExporPageInfoSelect1.intRecordCount = recordCount;
                this.ExporPageInfoSelect1.CurrencyPage = PageIndex;
                this.ExporPageInfoSelect1.HrefType = Adpost.Common.ExporPage.HrefTypeEnum.JsHref;
                this.ExporPageInfoSelect1.AttributesEventAdd("onclick", "CompanyUserManage.LoadData(this);", 1);
                this.ExporPageInfoSelect1.AttributesEventAdd("onchange", "CompanyUserManage.LoadData(this);", 0);
                this.repList.DataSource = listUserInfo;
                this.repList.DataBind();
            }
            else
            {
                StringBuilder strEmptyText = new StringBuilder();
                strEmptyText.Append("<table width=\"100%\" border=\"1\" align=\"center\" cellspacing=\"0\" bordercolor=\"#C7DEEB\" class=\"table_basic\">");
                strEmptyText.Append("<tr>");
                strEmptyText.Append("<th>序号</th>");
                strEmptyText.Append("<th>帐号</th>");
                strEmptyText.Append("<th>姓名</th><th>性别</th>");
                strEmptyText.Append("<th>单位名称</th><th>区域</th>");
                strEmptyText.Append("<th>手机</th><th>QQ</th>");
                strEmptyText.Append("<th>MQ</th><th>登录</th>");
                strEmptyText.Append("<th>注册时间</th><th>最近登录</th>");
                strEmptyText.Append("<th>功能</th>");
                strEmptyText.Append("<tr align='center'><td  align='center' colspan='20' height='100px'>暂无景区信息</td></tr>");
                strEmptyText.Append("</tr>");
                strEmptyText.Append("<tr>");
                strEmptyText.Append("<th>序号</th>");
                strEmptyText.Append("<th>帐号</th>");
                strEmptyText.Append("<th>姓名</th><th>性别</th>");
                strEmptyText.Append("<th>单位名称</th><th>区域</th>");
                strEmptyText.Append("<th>手机</th><th>QQ</th>");
                strEmptyText.Append("<th>MQ</th><th>登录</th>");
                strEmptyText.Append("<th>注册时间</th><th>最近登录</th>");
                strEmptyText.Append("<th>功能</th>");
                strEmptyText.Append("</tr>");
                strEmptyText.Append("</table>");
                this.repList.EmptyText = strEmptyText.ToString();
                SearchModel = null;
                listUserInfo = null;
            }
        }



        /// <summary>
        /// 序号
        /// </summary>
        protected int GetCount()
        {
            return ++count + (PageIndex - 1) * PageSize;
        }

    }
}
