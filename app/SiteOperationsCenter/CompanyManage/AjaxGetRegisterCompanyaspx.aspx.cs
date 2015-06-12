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
    /// 获的平台注册会员列表
    /// </summary>
    public partial class AjaxGetRegisterCompanyaspx : EyouSoft.Common.Control.YunYingPage
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
                BindCompanyList();
            }
        }
        /// <summary>
        /// 绑定会员列表
        /// </summary>
        protected void BindCompanyList()
        {
            int recordCount = 0;
            int ProvinceId = EyouSoft.Common.Function.StringValidate.GetIntValue(Request.QueryString["ProvinceId"]);
            int CityId = EyouSoft.Common.Function.StringValidate.GetIntValue(Request.QueryString["CityId"]);
            //int CountId = EyouSoft.Common.Function.StringValidate.GetIntValue(Request.QueryString["CountyId"]);
            string CompanyType = Request.QueryString["CompanyType"];

            EyouSoft.Model.CompanyStructure.CompanyType TypeEmnu = EyouSoft.Model.CompanyStructure.CompanyType.全部;
            if (!string.IsNullOrEmpty(CompanyType) && CompanyType != "0")
            {
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
                    case "4":
                        TypeEmnu = EyouSoft.Model.CompanyStructure.CompanyType.景区;
                        break;
                    case "5":
                        TypeEmnu = EyouSoft.Model.CompanyStructure.CompanyType.酒店;
                        break;
                    case "6":
                        TypeEmnu = EyouSoft.Model.CompanyStructure.CompanyType.车队;
                        break;
                    case "7":
                        TypeEmnu = EyouSoft.Model.CompanyStructure.CompanyType.旅游用品店;
                        break;
                    case "8":
                        TypeEmnu = EyouSoft.Model.CompanyStructure.CompanyType.购物店;
                        break;
                    case "9":
                        TypeEmnu = EyouSoft.Model.CompanyStructure.CompanyType.机票供应商;
                        break;
                    case "10":
                        TypeEmnu = EyouSoft.Model.CompanyStructure.CompanyType.其他采购商;
                        break;
                    case "11":
                        TypeEmnu = EyouSoft.Model.CompanyStructure.CompanyType.随便逛逛;
                        break;
                }
            }
            string CompanyName = Utils.InputText(Request.QueryString["CompanyName"]);
            EyouSoft.Model.CompanyStructure.QueryParamsAllCompany SearchModel = new EyouSoft.Model.CompanyStructure.QueryParamsAllCompany();
            SearchModel.PorvinceId = ProvinceId;
            SearchModel.CityId = CityId;
            
            SearchModel.CompanyName = CompanyName;
            SearchModel.CompanyType = TypeEmnu;
            SearchModel.Username = Utils.InputText(Request.QueryString["username"]);
            SearchModel.ContactName = Utils.InputText(Request.QueryString["contactName"]);

            IList<EyouSoft.Model.CompanyStructure.CompanyDetailInfo> CompanyList = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetListNoChecked(SearchModel, PageSize, PageIndex, ref recordCount);

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
                strEmptyText.AppendFormat("<td width=\"15%\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"><strong>单位名称</strong></td><td width=\"10%\" align=\"center\" background=\"{0}/images/yunying/hangbg.gif\"> <strong>单位名称</strong></td>", ImageServerUrl);
                strEmptyText.AppendFormat("<td width=\"12%\" align=\"center\" background=\"{0}/images/yunying/hangbg.gif\"><strong>地区</strong></td><td width=\"13%\" align=\"center\" background=\"{0}/images/yunying/hangbg.gif\">  <strong>产品区域</strong></td>", ImageServerUrl);
                strEmptyText.AppendFormat("<td align=\"center\" background=\"{0}/images/yunying/hangbg.gif\"> <strong>产品销售城市</strong></td><td width=\"11%\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"><strong>联系方式</strong></td>", ImageServerUrl);
                strEmptyText.AppendFormat("<td width=\"7%\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"><strong>加入时间</strong> </td><td width=\"8%\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"><strong>审核</strong></td>", ImageServerUrl);
                strEmptyText.Append("<tr class=\"huanghui\" ><td  align='center' colspan='10' height='100px'>暂无注册会员信息</td></tr>");
                strEmptyText.Append("</tr>");
                strEmptyText.Append("<tr background=\"" + ImageServerUrl + "/images/yunying/hangbg.gif\" class=\"white\" height=\"23\">");
                strEmptyText.AppendFormat("<td width=\"5%\" height=\"23\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"> <strong>序号</strong> </td> <td width=\"7%\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"> <strong>经营范围</strong></td>", ImageServerUrl);
                strEmptyText.AppendFormat("<td width=\"15%\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"><strong>单位名称</strong></td><td width=\"10%\" align=\"center\" background=\"{0}/images/yunying/hangbg.gif\"> <strong>单位名称</strong></td>", ImageServerUrl);
                strEmptyText.AppendFormat("<td width=\"12%\" align=\"center\" background=\"{0}/images/yunying/hangbg.gif\"><strong>地区</strong></td><td width=\"13%\" align=\"center\" background=\"{0}/images/yunying/hangbg.gif\">  <strong>产品区域</strong></td>", ImageServerUrl);
                strEmptyText.AppendFormat("<td align=\"center\" background=\"{0}/images/yunying/hangbg.gif\"> <strong>产品销售城市</strong></td><td width=\"11%\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"><strong>联系方式</strong></td>", ImageServerUrl);
                strEmptyText.AppendFormat("<td width=\"7%\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"><strong>加入时间</strong> </td><td width=\"8%\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"><strong>审核</strong></td>", ImageServerUrl);
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

        protected string GetisCheck(string CompanyId, EyouSoft.Model.CompanyStructure.CompanyType[] TypeItems)
        {
            string strVal = "";
            bool isManage = CheckMasterGrant(EyouSoft.Common.YuYingPermission.会员管理_管理该栏目);
            bool isTrue = false;
            for (int i = 0; i < TypeItems.Length; i++)
            {
                if (TypeItems[i] == EyouSoft.Model.CompanyStructure.CompanyType.专线)
                {
                    if (CheckMasterGrant(YuYingPermission.注册审核_专线商审核))
                    {
                        isTrue = true;
                        break;
                    }
                }
                else if (TypeItems[i] == EyouSoft.Model.CompanyStructure.CompanyType.组团)
                {
                    if (CheckMasterGrant(YuYingPermission.注册审核_组团社审核))
                    {
                        isTrue = true;
                        break;
                    }
                }
                else if (TypeItems[i] == EyouSoft.Model.CompanyStructure.CompanyType.地接)
                {
                    if (CheckMasterGrant(YuYingPermission.注册审核_地接社审核))
                    {
                        isTrue = true;
                        break;
                    }
                }
                else if (TypeItems[i] == EyouSoft.Model.CompanyStructure.CompanyType.景区)
                {
                    if (CheckMasterGrant(YuYingPermission.注册审核_景区审核))
                    {
                        isTrue = true;
                        break;
                    }
                }
                else if (TypeItems[i] == EyouSoft.Model.CompanyStructure.CompanyType.酒店)
                {
                    if (CheckMasterGrant(YuYingPermission.注册审核_酒店审核))
                    {
                        isTrue = true;
                        break;
                    }
                }
                else if (TypeItems[i] == EyouSoft.Model.CompanyStructure.CompanyType.车队)
                {
                    if (CheckMasterGrant(YuYingPermission.注册审核_车队审核))
                    {
                        isTrue = true;
                        break;
                    }
                }
                else if (TypeItems[i] == EyouSoft.Model.CompanyStructure.CompanyType.旅游用品店)
                {
                    if (CheckMasterGrant(YuYingPermission.注册审核_旅游用品店审核))
                    {
                        isTrue = true;
                        break;
                    }
                }
                else if (TypeItems[i] == EyouSoft.Model.CompanyStructure.CompanyType.购物店)
                {
                    if (CheckMasterGrant(YuYingPermission.注册审核_购物店审核))
                    {
                        isTrue = true;
                        break;
                    }
                }
                else if (TypeItems[i] == EyouSoft.Model.CompanyStructure.CompanyType.机票供应商)
                {
                    if (CheckMasterGrant(YuYingPermission.注册审核_机票供应商审核))
                    {
                        isTrue = true;
                        break;
                    }
                }
                else if (TypeItems[i] == EyouSoft.Model.CompanyStructure.CompanyType.其他采购商)
                {
                    if (CheckMasterGrant(YuYingPermission.注册审核_其他采购商审核))
                    {
                        isTrue = true;
                        break;
                    }
                }
                else if (TypeItems[i] == EyouSoft.Model.CompanyStructure.CompanyType.随便逛逛)
                {
                    if (CheckMasterGrant(YuYingPermission.注册审核_随便逛逛))
                    {
                        isTrue = true;
                        break;
                    }

                }
            }
            if (isManage && isTrue)
            {
                strVal = string.Format("<a href=\"javascript:void(0);\" class=\"nav4\" onclick=\"CompanyManage.CompanyCheck('{0}');\"> 【审核】</a>", CompanyId);
            }

            return strVal;
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
    }
}
