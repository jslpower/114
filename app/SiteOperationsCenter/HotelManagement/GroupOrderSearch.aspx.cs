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
    /// 团队订单查询
    /// 2010-12-09
    /// 袁惠
    /// </summary>
    public partial class GroupOrderSearch : EyouSoft.Common.Control.YunYingPage
    {
        protected string imagePath = "";  //图片地址
        protected void Page_Load(object sender, EventArgs e)
        {
            imagePath = ImageServerUrl;
            if (!IsPostBack)
            {
                if (!CheckMasterGrant(YuYingPermission.酒店后台管理_团队订单管理))
                {
                    Utils.ResponseNoPermit(YuYingPermission.酒店后台管理_团队订单管理, true);
                    return;
                }
                BindControlData();     
            }
        }
        /// <summary>
        /// 绑定控件
        /// </summary>
        private void BindControlData()
        {
            int pageSize=20;
            int recordCount=0;
            int Pageindex=EyouSoft.Common.Utils.GetInt(Request.QueryString["page"],1);

            IList<EyouSoft.Model.HotelStructure.HotelTourCustoms> list = EyouSoft.BLL.HotelStructure.HotelTourCustoms.CreateInstance().GetList(pageSize, Pageindex, ref recordCount, new EyouSoft.Model.HotelStructure.SearchTourCustomsInfo());  //获取团队订单列表
            if (list != null && list.Count > 0)
            {
                crpOrderList.DataSource = list;
                crpOrderList.DataBind();
                //绑定分页控件
                this.ExportPageInfo1.intPageSize = pageSize;
                this.ExportPageInfo1.CurrencyPage = Pageindex;
                this.ExportPageInfo1.intRecordCount = recordCount;
                this.ExportPageInfo1.CurrencyPageCssClass = "RedFnt";
                this.ExportPageInfo1.UrlParams = Request.QueryString;
                this.ExportPageInfo1.PageLinkURL = "GroupOrderSearch.aspx?";
                this.ExportPageInfo1.LinkType = 3;
                list = null;
            }
        }
        /// <summary>
        /// 备注处理
        /// </summary>
        protected string GetRemark(string remark,string Id)
        {
            if (!string.IsNullOrEmpty(remark))
            {
                if (remark.Length > 9)
                {
                    return string.Format("{0}<span style=\"DISPLAY: none\" remark=\"remark{1}\" >{2}</span><a href=\"javascript:void(0)\" onclick=\"GroupOrderSearch.GetAllReamark('{1}')\"><img src=\"{3}/images/yunying/ns-expand.gif\"/></a>", remark.Substring(0, 8), Id, remark.Substring(8), ImageServerUrl);
                }
                else
                    return remark;
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 根据companyID获取公司信息
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <returns></returns>
        protected string GetCgsCompanyInfo(string companyid)
        {
            if (!string.IsNullOrEmpty(companyid))
            {
                EyouSoft.Model.CompanyStructure.CompanyDetailInfo detail = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(companyid);
                if (detail != null)
                {
                    return string.Format("<a href=\"javascript:void(0)\" onmouseover=\"wsug(this, '用户名:{0}<br/>真实名:{1}<br/>固定联系电话:{2}<br/>手机:{3}<br/>公司名称:{4}<br/>地址:{5}<br/>')\" onmouseout=\"wsug(this, 0)\">{6}</a>", detail.AdminAccount.UserName, detail.ContactInfo.ContactName, detail.ContactInfo.Tel, detail.ContactInfo.Mobile, detail.CompanyName, detail.CompanyAddress, detail.AdminAccount.UserName);
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 酒店星级初始化
        /// </summary>
        /// <param name="hotelLevel"></param>
        protected string HotelLevelInit(string hotelLevel)
        {
            string showtext = "";
            if (hotelLevel != null && hotelLevel.Trim() != "")
            {
                string str = "一二三四五";
                int i=(int)(EyouSoft.HotelBI.HotelRankEnum)Enum.Parse(typeof(EyouSoft.HotelBI.HotelRankEnum),hotelLevel);
                if (i > 0 && i < 6)
                {
                    showtext = str.Substring(i - 1, 1) + "星级";
                }
                if (i >= 6)
                {
                    showtext = "准" + str.Substring(i - 6, 1) + "星级";
                }
                   
            }
            return showtext;
        }
    }
}
