using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EyouSoft.Common;
using EyouSoft.Common.Control;
namespace SiteOperationsCenter.usercontrol
{
    /// <summary>
    /// 公司列表控件(景区 酒店 3车队 旅游用品 购物店 机票供应商)
    /// </summary>
    public partial class CompanyList : System.Web.UI.UserControl
    {
        private int _companyType;

        /// <summary>
        /// 公司类型: 1:景区 2:酒店 3:车队 4:旅游用品 5:购物店 6:机票供应商
        /// </summary>
        public int CompanyType
        {
            get { return _companyType; }
            set { _companyType = value; }
        }
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
        protected string ImageServerUrl = "";
        protected void Page_Load(object sender, EventArgs e)    
        {
            if (EyouSoft.Common.Utils.GetInt(Request.QueryString["Page"]) > 1)
            {
                PageIndex = EyouSoft.Common.Utils.GetInt(Request.QueryString["Page"]);
            }
            YunYingPage page = this.Page as YunYingPage;
            ImageServerUrl = page.ImageServerUrl;
            if (!Page.IsPostBack)
            {
                this.BindList();
            }
        }

        /// <summary>
        /// 绑定公司列表
        /// </summary>
        protected void BindList()
        {
            int recordCount = 0;
            int ProvinceId = Utils.GetInt(Request.QueryString["ProvinceId"]);
            int CityId = Utils.GetInt(Request.QueryString["CityId"]);

            EyouSoft.Model.CompanyStructure.CompanyType TypeEmnu = EyouSoft.Model.CompanyStructure.CompanyType.景区;
            string strMessage = "暂无景区会员信息";
            switch (CompanyType)
            {
                case 2:
                    TypeEmnu = EyouSoft.Model.CompanyStructure.CompanyType.酒店;
                    strMessage = "暂无酒店会员信息";
                    break;
                case 3:
                    TypeEmnu = EyouSoft.Model.CompanyStructure.CompanyType.车队;
                    strMessage = "暂无车队会员信息";
                    break;
                case 4:
                    TypeEmnu = EyouSoft.Model.CompanyStructure.CompanyType.旅游用品店;
                    strMessage = "暂无旅游用品会员信息";
                    break;
                case 5:
                    TypeEmnu = EyouSoft.Model.CompanyStructure.CompanyType.购物店;
                    strMessage = "暂无购物点会员信息";
                    break;
                case 6:
                    TypeEmnu = EyouSoft.Model.CompanyStructure.CompanyType.机票供应商;
                    strMessage = "暂无机票供应商会员信息";
                    break;
                case 7:
                    TypeEmnu = EyouSoft.Model.CompanyStructure.CompanyType.其他采购商;
                    strMessage = "暂无其他采购商会员信息";
                    break;
                case 11:
                    TypeEmnu = EyouSoft.Model.CompanyStructure.CompanyType.随便逛逛;
                    strMessage = "暂无随便逛逛会员信息";
                    break;
            }
            string CompanyName =Server.UrlDecode(Utils.InputText(Request.QueryString["CompanyName"]));
  
            EyouSoft.Model.CompanyStructure.QueryParamsAllCompany SearchModel = new EyouSoft.Model.CompanyStructure.QueryParamsAllCompany();
            SearchModel.PorvinceId = ProvinceId;
            SearchModel.CityId = CityId;
            SearchModel.CompanyName = CompanyName;
            SearchModel.CompanyType = TypeEmnu;
            SearchModel.Username = Utils.InputText(Request.QueryString["username"]);
            SearchModel.ContactName = Utils.InputText(Request.QueryString["contactName"]);

            IList<EyouSoft.Model.CompanyStructure.CompanyDetailInfo> CompanyList = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetListChecked(SearchModel,PageSize, PageIndex, ref recordCount);
            if (CompanyList != null && CompanyList.Count > 0)
            {
                this.ExportPageInfo1.intPageSize = PageSize;
                this.ExportPageInfo1.intRecordCount = recordCount;
                this.ExportPageInfo1.CurrencyPage = PageIndex;
                this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString()+"?";
                this.ExportPageInfo1.UrlParams = Request.QueryString;
                this.repCompanyList.DataSource = CompanyList;
                this.repCompanyList.DataBind();
            }
            else
            {
                StringBuilder strEmptyText = new StringBuilder();
                strEmptyText.Append("<table width=\"100%\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"1\" class=\"kuang\">");
                strEmptyText.Append("<tr background=\"" + ImageServerUrl + "/images/yunying/hangbg.gif\" class=\"white\" height=\"23\">");
                strEmptyText.AppendFormat("<td width=\"7%\" height=\"23\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"> <strong>序号</strong> </td> ", ImageServerUrl);
                strEmptyText.AppendFormat("<td width=\"18%\" align=\"center\" background=\"{0}/images/yunying/hangbg.gif\"><strong>单位名称</strong></td><td width=\"10%\" align=\"center\" background=\"{0}/images/yunying/hangbg.gif\">  <strong>地区</strong></td>", ImageServerUrl);
                strEmptyText.AppendFormat("<td align=\"center\"  width=\"13%\"  background=\"{0}/images/yunying/hangbg.gif\"> <strong>联系方式</strong></td><td width=\"11%\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"><strong>收费项目</strong></td>", ImageServerUrl);
                strEmptyText.AppendFormat("<td width=\"4%\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"><strong>登录次数</strong> </td><td width=\"8%\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"><strong>加入时间</strong></td>", ImageServerUrl);
                strEmptyText.AppendFormat("<tr class=\"huanghui\" ><td  align='center' colspan='10' height='100px'>{0}</td></tr>", strMessage);
                strEmptyText.Append("</tr>");
                strEmptyText.Append("<tr background=\"" + ImageServerUrl + "/images/yunying/hangbg.gif\" class=\"white\" height=\"23\">");
                strEmptyText.AppendFormat("<td width=\"7%\" height=\"23\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"> <strong>序号</strong> </td> ", ImageServerUrl);
                strEmptyText.AppendFormat("<td width=\"18%\" align=\"center\" background=\"{0}/images/yunying/hangbg.gif\"><strong>单位名称</strong></td><td width=\"10%\" align=\"center\" background=\"{0}/images/yunying/hangbg.gif\">  <strong>地区</strong></td>", ImageServerUrl);
                strEmptyText.AppendFormat("<td align=\"center\"  width=\"13%\"  background=\"{0}/images/yunying/hangbg.gif\"> <strong>联系方式</strong></td><td width=\"11%\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"><strong>收费项目</strong></td>", ImageServerUrl);
                strEmptyText.AppendFormat("<td width=\"4%\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"><strong>登录次数</strong> </td><td width=\"8%\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"><strong>加入时间</strong></td>", ImageServerUrl);
                strEmptyText.Append("</tr>");
                strEmptyText.Append("</table>");
                this.repCompanyList.EmptyText = strEmptyText.ToString();
            }
            SearchModel = null;
            CompanyList = null;

        }

        /// <summary>
        /// 获的省份-城市名称
        /// </summary>
        /// <param name="ProvinceId">省份ID</param>
        /// <param name="CityId">城市ID</param>
        protected string GetCityName(int ProvinceId, int CityId)
        {
            string strVal = "";

            IList<EyouSoft.Model.SystemStructure.SysCity> CityList = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetCityList(ProvinceId, 0, null, null, null);
            if (CityList != null)
            {
                CityList = (from c in CityList where c.CityId == CityId select c).ToList();
                if (CityList != null && CityList.Count > 0)
                {
                    strVal = CityList[0].ProvinceName + " " + CityList[0].CityName;
                }
            }
            CityList = null;

            return strVal;
        }

        /// <summary>
        /// 序号
        /// </summary>
        protected int GetCount()
        {
            return ++count + (PageIndex - 1) * PageSize;
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