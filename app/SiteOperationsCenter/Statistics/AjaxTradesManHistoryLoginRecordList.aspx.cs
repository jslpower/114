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
using EyouSoft.Common.Function;
using System.Collections.Generic;
using System.Text;
using EyouSoft.Common;

namespace SiteOperationsCenter.Statistics
{
    /// <summary>
    /// 零售商历史记录统计列表
    /// lym 2010-09-20
    /// </summary>
    public partial class AjaxTradesManHistoryLoginRecordList : EyouSoft.Common.Control.YunYingPage
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
      
        protected void Page_Load(object sender, EventArgs e)
        {
            if (EyouSoft.Common.Utils.GetInt(Request.QueryString["Page"]) > 1)
            {
                PageIndex = EyouSoft.Common.Utils.GetInt(Request.QueryString["Page"]);
            }
            if (!Page.IsPostBack)
            {           
                BindLoginRecordList();
            }
        }
        #region 初始化零售商历史登录记录
        private void BindLoginRecordList()
        {
            int recordCount = 0;     
             DateTime? startDate = null;
            DateTime? EndDate = null;
            string CompanyName = Utils.InputText(Request.QueryString["CompanyName"]);//单位名称
     
          
            string date = Request.QueryString["StartDate"];
            if (StringValidate.IsDateTime(date))
                startDate = Convert.ToDateTime(date);//开始时间

            date = Request.QueryString["EndDate"];//结束时间
            if (StringValidate.IsDateTime(date))
                EndDate = Convert.ToDateTime(date);

            EyouSoft.Model.CompanyStructure.QueryParamsAllCompany SearchModel = new EyouSoft.Model.CompanyStructure.QueryParamsAllCompany();
            
            SearchModel.CompanyName = CompanyName;          
            SearchModel.IsQueryOnlineUser = null;
            SearchModel.LoginStartDate = startDate;
            SearchModel.LoginEndDate = EndDate;                   
    
            IList<EyouSoft.Model.CompanyStructure.CompanyDetailInfo> LoginRecordList = 
                EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetListChecked(SearchModel, PageSize, PageIndex, ref recordCount);
            if (LoginRecordList != null && LoginRecordList.Count > 0)   
            {
                this.ExporPageInfoSelect1.intPageSize = PageSize;
                this.ExporPageInfoSelect1.intRecordCount = recordCount;
                this.ExporPageInfoSelect1.CurrencyPage = PageIndex;
                this.ExporPageInfoSelect1.HrefType = Adpost.Common.ExporPage.HrefTypeEnum.JsHref;
                this.ExporPageInfoSelect1.AttributesEventAdd("onclick", "LoginRecordData.LoadData(this);", 1);
                this.ExporPageInfoSelect1.AttributesEventAdd("onchange", "LoginRecordData.LoadData(this);", 0);
                if (startDate != null || EndDate != null)
                {
                    for (int i = 0; i < LoginRecordList.Count; i++)
                    {
                        //登录次数 
                        LoginRecordList[i].StateMore.LoginCount = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance()
                            .GetLoginCountByTime(LoginRecordList[i].ID, startDate, EndDate);
                    }
                }
                this.crpLoginRecordList.DataSource = LoginRecordList;
                this.crpLoginRecordList.DataBind();
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
                strEmptyText.Append("<tr class=\"huanghui\" ><td  align='center' colspan='10' height='100px'>暂无零售商历史登录记录</td></tr>");
                strEmptyText.Append("</tr>");
                strEmptyText.Append("<tr background=\"" + ImageServerUrl + "/images/yunying/hangbg.gif\" class=\"white\" height=\"23\">");
                strEmptyText.AppendFormat("<td width=\"5%\" height=\"23\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"> <strong>序号</strong> </td> <td width=\"7%\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"> <strong>单位名称</strong></td>", ImageServerUrl);
                strEmptyText.AppendFormat("<td width=\"18%\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"><strong>联系人</strong></td><td width=\"10%\" align=\"center\" background=\"{0}/images/yunying/hangbg.gif\"> <strong>手机</strong></td>", ImageServerUrl);
                strEmptyText.AppendFormat("<td width=\"12%\" align=\"center\" background=\"{0}/images/yunying/hangbg.gif\"><strong>电话</strong></td><td width=\"13%\" align=\"center\" background=\"{0}/images/yunying/hangbg.gif\">  <strong>QQ/MSN</strong></td>", ImageServerUrl);
                strEmptyText.AppendFormat("<td align=\"center\" background=\"{0}/images/yunying/hangbg.gif\"> <strong>最后登录时间</strong></td><td width=\"11%\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"><strong>登录次数</strong></td>", ImageServerUrl);
                strEmptyText.Append("</tr>");
                strEmptyText.Append("</table>");
                this.crpLoginRecordList.EmptyText = strEmptyText.ToString();
            }      
            LoginRecordList = null;
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
