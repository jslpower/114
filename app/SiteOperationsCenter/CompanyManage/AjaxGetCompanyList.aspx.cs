using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using System.Text;
namespace SiteOperationsCenter.CompanyManage
{
    /// <summary>
    /// 获的旅行社列表
    /// </summary>
    public partial class AjaxGetCompanyList : EyouSoft.Common.Control.YunYingPage
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
            PageIndex = EyouSoft.Common.Utils.GetInt(Request.QueryString["Page"],1);//获取分页序号
            if (!Page.IsPostBack)
            {
                BindCompanyList();
            }

        }
        /// <summary>
        /// 初始化旅行社列表 
        /// </summary>
        protected void BindCompanyList()
        {
            int recordCount = 0;
            int ProvinceId = EyouSoft.Common.Function.StringValidate.GetIntValue(Request.QueryString["ProvinceId"]);
            int CityId = EyouSoft.Common.Function.StringValidate.GetIntValue(Request.QueryString["CityId"]);
            
            string CompanyType = Request.QueryString["CompanyType"];

            EyouSoft.Model.CompanyStructure.CompanyType TypeEmnu = EyouSoft.Model.CompanyStructure.CompanyType.全部;

            switch (CompanyType)
            {
                case "1":
                    TypeEmnu =  EyouSoft.Model.CompanyStructure.CompanyType.专线;
                    break;
                case "2":
                    TypeEmnu = EyouSoft.Model.CompanyStructure.CompanyType.组团;
                    break;
                case "3":
                    TypeEmnu =  EyouSoft.Model.CompanyStructure.CompanyType.地接;
                    break;
            }
            string CompanyName = Utils.InputText(Request.QueryString["CompanyName"]);
            string RecommendPerson = Utils.InputText(Request.QueryString["RecommendPerson"]);
            EyouSoft.Model.CompanyStructure.QueryParamsAllCompany SearchModel = new EyouSoft.Model.CompanyStructure.QueryParamsAllCompany();
            SearchModel.PorvinceId = ProvinceId;
            SearchModel.CityId = CityId;
            SearchModel.CompanyName = CompanyName;
            SearchModel.CommendName = RecommendPerson;
            SearchModel.CompanyType = TypeEmnu;
            SearchModel.BusinessProperties = EyouSoft.Model.CompanyStructure.BusinessProperties.旅游社;
            SearchModel.Username = Utils.InputText(Request.QueryString["username"]);
            SearchModel.ContactName = Utils.InputText(Request.QueryString["contactName"]);

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
                strEmptyText.AppendFormat("<td width=\"5%\" height=\"23\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"> <strong>序号</strong> </td> <td width=\"7%\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"> <strong>经营范围</strong></td>", ImageServerUrl);
                strEmptyText.AppendFormat("<td width=\"18%\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"><strong>经营范围</strong></td><td width=\"10%\" align=\"center\" background=\"{0}/images/yunying/hangbg.gif\"> <strong>产品销售城市</strong></td>", ImageServerUrl);
                strEmptyText.AppendFormat("<td width=\"12%\" align=\"center\" background=\"{0}/images/yunying/hangbg.gif\"><strong>产品区域</strong></td><td width=\"13%\" align=\"center\" background=\"{0}/images/yunying/hangbg.gif\">  <strong>联系方式</strong></td>", ImageServerUrl);
                strEmptyText.AppendFormat("<td align=\"center\" background=\"{0}/images/yunying/hangbg.gif\"> <strong>证书</strong></td><td width=\"11%\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"><strong>收费项目</strong></td>", ImageServerUrl);
                strEmptyText.AppendFormat("<td width=\"4%\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"><strong>登录</strong> </td><td width=\"8%\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"><strong>加入时间</strong></td>", ImageServerUrl);
                strEmptyText.Append("<tr class=\"huanghui\" ><td  align='center' colspan='10' height='100px'>暂无旅行社信息</td></tr>");
                strEmptyText.Append("</tr>");
                strEmptyText.Append("<tr background=\"" + ImageServerUrl + "/images/yunying/hangbg.gif\" class=\"white\" height=\"23\">");
                strEmptyText.AppendFormat("<td width=\"5%\" height=\"23\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"> <strong>序号</strong> </td> <td width=\"7%\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"> <strong>经营范围</strong></td>", ImageServerUrl);
                strEmptyText.AppendFormat("<td width=\"18%\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"><strong>单位名称</strong></td><td width=\"10%\" align=\"center\" background=\"{0}/images/yunying/hangbg.gif\"> <strong>产品销售城市</strong></td>", ImageServerUrl);
                strEmptyText.AppendFormat("<td width=\"12%\" align=\"center\" background=\"{0}/images/yunying/hangbg.gif\"><strong>产品区域</strong></td><td width=\"13%\" align=\"center\" background=\"{0}/images/yunying/hangbg.gif\">  <strong>联系方式</strong></td>", ImageServerUrl);
                strEmptyText.AppendFormat("<td align=\"center\" background=\"{0}/images/yunying/hangbg.gif\"> <strong>证书</strong></td><td width=\"11%\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"><strong>收费项目</strong></td>", ImageServerUrl);
                strEmptyText.AppendFormat("<td width=\"4%\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"><strong>登录</strong> </td><td width=\"8%\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"><strong>加入时间</strong></td>", ImageServerUrl);
                strEmptyText.Append("</tr>");
                strEmptyText.Append("</table>");
                this.repCompanyList.EmptyText = strEmptyText.ToString();
            }
            SearchModel = null;
            CompanyList = null;

        }

        /// <summary>
        /// 序号
        /// </summary>
        protected int GetCount()
        {
            return ++count + (PageIndex - 1) * PageSize;
        }
        /// <summary>
        /// 获的公司身份
        /// </summary>
        protected string GetCompanyRole(EyouSoft.Model.CompanyStructure.CompanyType[] TypeItems)
        {
            string strAllRole = "";
            for (int i = 0; i < TypeItems.Length; i++)
            {
                strAllRole += TypeItems[i].ToString() + ",";
            }
            if (strAllRole.EndsWith(","))
            {
                strAllRole = strAllRole.Substring(0, strAllRole.Length - 1);
            }
            return strAllRole;
        }
        /// <summary>
        /// 获的公司已经开通的收费项目
        /// </summary>
        protected string GetCompanyServiceItem(string CompanyId)
        {
            string strCompanyItem = "暂无";
            EyouSoft.Model.CompanyStructure.CompanyState CompanyStateModel = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetCompanyState(CompanyId);
            if (CompanyStateModel != null)
            {
                EyouSoft.Model.CompanyStructure.CompanyService Model = CompanyStateModel.CompanyService;
                if (Model != null)
                {
                    EyouSoft.Model.CompanyStructure.CompanyServiceItem[] CompanyItem = Model.ServiceItems;
                    if (CompanyItem != null && CompanyItem.Length > 0)
                    {
                        strCompanyItem = "";
                        for (int i = 0; i < CompanyItem.Length; i++)
                        {
                            strCompanyItem += CompanyItem[i].Service + "|";
                        }
                    }
                }
                Model = null;
                if (strCompanyItem.EndsWith("|"))
                {
                    strCompanyItem = strCompanyItem.Substring(0, strCompanyItem.Length - 1);
                }
            }
            CompanyStateModel = null;
            return strCompanyItem;
        }


    }
}
