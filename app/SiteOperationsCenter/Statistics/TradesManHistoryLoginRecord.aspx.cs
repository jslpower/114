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
using System.Collections.Generic;
using System.Text;

namespace SiteOperationsCenter.Statistics
{
    /// <summary>
    /// 零售商历史登录记录列表
    /// lym 2010-09-19
    /// </summary>
    public partial class TradesManHistoryLoginRecord : System.Web.UI.Page
    {
        /// <summary>
        /// 每页显示条数
        /// </summary>
        protected int PageSize = 15;
        /// <summary>
        /// 当前页
        /// </summary>
        protected int PageIndex = 1;
        /// <summary>
        /// 序号
        /// </summary>
        private int count = 0;
        protected string ImageServerUrl = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (EyouSoft.Common.Utils.GetInt(Request.QueryString["Page"]) > 1)
            {
                PageIndex = EyouSoft.Common.Utils.GetInt(Request.QueryString["Page"]);
            }
            if (!Page.IsPostBack)
            {
                this.StartAndEndDate1.SetTitle = "登录时间段：";               
                this.ImgBtn.ImageUrl = ImageServerUrl + "/images/chaxun.gif";
                BindTravelagencyList();
            }
        }
        #region 初始化零售商历史登录记录列表
        private void BindTravelagencyList()
        {
            int recordCount = 0;

            string CompanyType = Request.QueryString["CompanyType"];

            EyouSoft.Model.CompanyStructure.CompanyType TypeEmnu = EyouSoft.Model.CompanyStructure.CompanyType.全部;

            switch (CompanyType)
            {
                case "1":
                    TypeEmnu = EyouSoft.Model.CompanyStructure.CompanyType.专线;
                    break;
                case "2":
                    TypeEmnu = EyouSoft.Model.CompanyStructure.CompanyType.组团;
                    break;
                case "3":
                    TypeEmnu = EyouSoft.Model.CompanyStructure.CompanyType.地接;
                    break;
            }
            string CompanyName = Utils.InputText(Request.QueryString["CompanyName"]);           
            
            EyouSoft.Model.CompanyStructure.QueryParamsAllCompany SearchModel = new EyouSoft.Model.CompanyStructure.QueryParamsAllCompany();
            SearchModel.CompanyName = CompanyName;           
            SearchModel.CompanyType = TypeEmnu;
            SearchModel.IsQueryOnlineUser = null;
            

            SearchModel.BusinessProperties = EyouSoft.Model.CompanyStructure.BusinessProperties.旅游社;

            IList<EyouSoft.Model.CompanyStructure.CompanyDetailInfo> CompanyList = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetListChecked(SearchModel, PageSize, PageIndex, ref recordCount);

            if (CompanyList != null && CompanyList.Count > 0)
            {
                this.ExporPageInfoSelect1.intPageSize = PageSize;
                this.ExporPageInfoSelect1.intRecordCount = recordCount;
                this.ExporPageInfoSelect1.CurrencyPage = PageIndex;
                this.ExporPageInfoSelect1.HrefType = Adpost.Common.ExporPage.HrefTypeEnum.JsHref;
                this.ExporPageInfoSelect1.AttributesEventAdd("onclick", "CompanyManage.LoadData(this);", 1);
                this.ExporPageInfoSelect1.AttributesEventAdd("onchange", "CompanyManage.LoadData(this);", 0);
                this.repCompanyList.DataSource = CompanyList;
                this.repCompanyList.DataBind();
            }
            else
            {
                StringBuilder strEmptyText = new StringBuilder();
                strEmptyText.Append("<table width=\"100%\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"1\" class=\"kuang\">");
                strEmptyText.Append("<tr background=\"" + ImageServerUrl + "/images/yunying/hangbg.gif\" class=\"white\" height=\"23\">");
                strEmptyText.AppendFormat("<td width=\"5%\" height=\"23\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"> <strong>序号</strong> </td> <td width=\"7%\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"> <strong>单位名称</strong></td>", ImageServerUrl);
                strEmptyText.AppendFormat("<td width=\"18%\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"><strong>联系人</strong></td><td width=\"10%\" align=\"center\" background=\"{0}/images/yunying/hangbg.gif\"> <strong>手机</strong></td>", ImageServerUrl);
                strEmptyText.AppendFormat("<td width=\"12%\" align=\"center\" background=\"{0}/images/yunying/hangbg.gif\"><strong>电话</strong></td><td width=\"13%\" align=\"center\" background=\"{0}/images/yunying/hangbg.gif\">  <strong>QQ/MSN</strong></td>", ImageServerUrl);
                strEmptyText.AppendFormat("<td align=\"center\" background=\"{0}/images/yunying/hangbg.gif\"> <strong>最后登录时间</strong></td><td width=\"11%\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"><strong>登录次数</strong></td>", ImageServerUrl);
                strEmptyText.Append("<tr class=\"huanghui\" ><td  align='center' colspan='10' height='100px'>暂无零售商历史登录记录信息</td></tr>");
                strEmptyText.Append("</tr>");
                strEmptyText.Append("<tr background=\"" + ImageServerUrl + "/images/yunying/hangbg.gif\" class=\"white\" height=\"23\">");
                strEmptyText.AppendFormat("<td width=\"5%\" height=\"23\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"> <strong>序号</strong> </td> <td width=\"7%\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"> <strong>单位名称</strong></td>", ImageServerUrl);
                strEmptyText.AppendFormat("<td width=\"18%\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"><strong>联系人</strong></td><td width=\"10%\" align=\"center\" background=\"{0}/images/yunying/hangbg.gif\"> <strong>手机</strong></td>", ImageServerUrl);
                strEmptyText.AppendFormat("<td width=\"12%\" align=\"center\" background=\"{0}/images/yunying/hangbg.gif\"><strong>电话</strong></td><td width=\"13%\" align=\"center\" background=\"{0}/images/yunying/hangbg.gif\">  <strong>QQ/MSN</strong></td>", ImageServerUrl);
                strEmptyText.AppendFormat("<td align=\"center\" background=\"{0}/images/yunying/hangbg.gif\"> <strong>最后登录时间</strong></td><td width=\"11%\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"><strong>登录次数</strong></td>", ImageServerUrl);
                strEmptyText.Append("</tr>");
                strEmptyText.Append("</table>");
                this.repCompanyList.EmptyText = strEmptyText.ToString();
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
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ImgBtn_Click(object sender, ImageClickEventArgs e)
        {
            BindTravelagencyList();
        }
    }
}
