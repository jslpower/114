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
using System.Text;
using EyouSoft.Common;

namespace SiteOperationsCenter.Statistics
{
    /// <summary>
    /// 在线组团社列表
    /// Lym 2010-09-20
    /// </summary>
    public partial class AjaxLineTravelagencyList : EyouSoft.Common.Control.YunYingPage
    {
        /// <summary>
        /// 每页显示条数
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
    

        #region 页面加载
        protected void Page_Load(object sender, EventArgs e)
        {
            if (EyouSoft.Common.Utils.GetInt(Request.QueryString["Page"]) > 1)
            {
                PageIndex = EyouSoft.Common.Utils.GetInt(Request.QueryString["Page"]);
            }
            if (!Page.IsPostBack)
            {               
                BindTravelagencyList();
            }
        }
        #endregion

        #region 初始化组社团信息列表
        protected void BindTravelagencyList()
        {
            int recordCount = 0;
            int ProvinceId = EyouSoft.Common.Function.StringValidate.GetIntValue(Request.QueryString["ProvinceId"]);//省份
            int CityId = EyouSoft.Common.Function.StringValidate.GetIntValue(Request.QueryString["CityId"]);//城市

            string CompanyName = Utils.InputText(Request.QueryString["CompanyName"]);//单位名称
            string RecommendPerson = Utils.InputText(Request.QueryString["CommendName"]);//负责人
            EyouSoft.Model.CompanyStructure.QueryParamsAllCompany SearchModel = new EyouSoft.Model.CompanyStructure.QueryParamsAllCompany();
            SearchModel.PorvinceId = ProvinceId;
            SearchModel.CityId = CityId;
            SearchModel.CompanyName = CompanyName;
            SearchModel.ContactName = RecommendPerson;
            SearchModel.CompanyType = EyouSoft.Model.CompanyStructure.CompanyType.组团;
            SearchModel.IsQueryOnlineUser = true;      //是否在线组团     
         
            SearchModel.BusinessProperties = EyouSoft.Model.CompanyStructure.BusinessProperties.旅游社;

            IList<EyouSoft.Model.CompanyStructure.CompanyDetailInfo> CompanyList = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetListChecked(SearchModel, PageSize, PageIndex, ref recordCount);
    
            if (CompanyList != null && CompanyList.Count > 0)
            {
                this.ExporPageInfoSelect1.intPageSize = PageSize;
                this.ExporPageInfoSelect1.intRecordCount = recordCount;
                this.ExporPageInfoSelect1.CurrencyPage = PageIndex;
                this.ExporPageInfoSelect1.HrefType = Adpost.Common.ExporPage.HrefTypeEnum.JsHref;
                this.ExporPageInfoSelect1.AttributesEventAdd("onclick", "LineTravelagencyData.LoadData(this);", 1);
                this.ExporPageInfoSelect1.AttributesEventAdd("onchange", "LineTravelagencyData.LoadData(this);", 0);
                this.crpTravelagencyList.DataSource = CompanyList;
                this.crpTravelagencyList.DataBind();
            }
            else
            {
                StringBuilder strEmptyText = new StringBuilder();
                strEmptyText.Append("<table width=\"100%\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"1\" class=\"kuang\">");
                strEmptyText.Append("<tr background=\"" + ImageServerUrl + "/images/yunying/hangbg.gif\" class=\"white\" height=\"23\">");
                strEmptyText.AppendFormat("<td width=\"5%\" height=\"23\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"> <strong>序号</strong> </td> <td width=\"7%\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"> <strong>单位名称</strong></td>", ImageServerUrl);
                strEmptyText.AppendFormat("<td width=\"18%\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"><strong>联系人</strong></td><td width=\"10%\" align=\"center\" background=\"{0}/images/yunying/hangbg.gif\"> <strong>手机</strong></td>", ImageServerUrl);
                strEmptyText.AppendFormat("<td width=\"12%\" align=\"center\" background=\"{0}/images/yunying/hangbg.gif\"><strong>电话</strong></td><td width=\"13%\" align=\"center\" background=\"{0}/images/yunying/hangbg.gif\">  <strong>QQ/MSN</strong></td>", ImageServerUrl);
                strEmptyText.AppendFormat("<td align=\"center\" background=\"{0}/images/yunying/hangbg.gif\"> <strong>登录时间</strong></td><td width=\"11%\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"><strong>登录总次数</strong></td>", ImageServerUrl);
                strEmptyText.Append("<tr class=\"huanghui\" ><td  align='center' colspan='10' height='100px'>暂无组团社信息</td></tr>");
                strEmptyText.Append("</tr>");
                strEmptyText.Append("<tr background=\"" + ImageServerUrl + "/images/yunying/hangbg.gif\" class=\"white\" height=\"23\">");
                strEmptyText.AppendFormat("<td width=\"5%\" height=\"23\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"> <strong>序号</strong> </td> <td width=\"7%\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"> <strong>单位名称</strong></td>", ImageServerUrl);
                strEmptyText.AppendFormat("<td width=\"18%\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"><strong>联系人</strong></td><td width=\"10%\" align=\"center\" background=\"{0}/images/yunying/hangbg.gif\"> <strong>手机</strong></td>", ImageServerUrl);
                strEmptyText.AppendFormat("<td width=\"12%\" align=\"center\" background=\"{0}/images/yunying/hangbg.gif\"><strong>电话</strong></td><td width=\"13%\" align=\"center\" background=\"{0}/images/yunying/hangbg.gif\">  <strong>QQ/MSN</strong></td>", ImageServerUrl);
                strEmptyText.AppendFormat("<td align=\"center\" background=\"{0}/images/yunying/hangbg.gif\"> <strong>登录时间</strong></td><td width=\"11%\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"><strong>登录总次数</strong></td>", ImageServerUrl);
                strEmptyText.Append("</tr>");
                strEmptyText.Append("</table>");
                this.crpTravelagencyList.EmptyText = strEmptyText.ToString();
            }
            SearchModel = null;
            CompanyList = null;

        }
        #endregion

        /// <summary>
        /// 序号
        /// </summary>
        protected int GetCount()
        {
            return ++count + (PageIndex - 1) * PageSize;
        }       
    }
}
