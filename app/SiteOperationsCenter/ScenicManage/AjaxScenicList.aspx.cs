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
using EyouSoft.Model.ScenicStructure;
using EyouSoft.Common;
using System.Collections.Generic;
using System.Text;
using EyouSoft.Common.Function;

namespace SiteOperationsCenter.ScenicManage
{
    public partial class AjaxScenicList : EyouSoft.Common.Control.YunYingPage
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
            int CountyId = EyouSoft.Common.Function.StringValidate.GetIntValue(Request.QueryString["CountyId"]);
            int Status = EyouSoft.Common.Function.StringValidate.GetIntValue(Request.QueryString["Status"]);
            int B2B = -1;
            if (Request.QueryString["B2B"] != "")
                B2B = EyouSoft.Common.Function.StringValidate.GetIntValue(Request.QueryString["B2B"]);
            int B2C = -1;
            if (Request.QueryString["B2C"] != "")
                B2C = EyouSoft.Common.Function.StringValidate.GetIntValue(Request.QueryString["B2C"]);
            string ScenicName = Utils.InputText(Request.QueryString["ScenicName"]);
            EyouSoft.Model.ScenicStructure.MSearchSceniceArea SearchModel = new EyouSoft.Model.ScenicStructure.MSearchSceniceArea();
            if (ProvinceId > 0)
                SearchModel.ProvinceId = ProvinceId;
            if (CityId > 0)
                SearchModel.CityId = CityId;
            if (CountyId > 0)
                SearchModel.CountyId = CountyId;
            if (ScenicName != "")
                SearchModel.ScenicName = ScenicName;
            if (Status != 0)
                SearchModel.Status = (ExamineStatus)Status;
            if (B2B > 0 || B2B == 0)
                SearchModel.B2B = (ScenicB2BDisplay)B2B;
            if (B2C > 0 || B2C == 0)
                SearchModel.B2C = (ScenicB2CDisplay)B2C;
            IList<MScenicArea> listScenicArea = EyouSoft.BLL.ScenicStructure.BScenicArea.CreateInstance().GetList(PageSize, PageIndex, ref recordCount, SearchModel);
            if (listScenicArea.Count > 0)
            {
                this.ExporPageInfoSelect1.intPageSize = PageSize;
                this.ExporPageInfoSelect1.intRecordCount = recordCount;
                this.ExporPageInfoSelect1.CurrencyPage = PageIndex;
                this.ExporPageInfoSelect1.HrefType = Adpost.Common.ExporPage.HrefTypeEnum.JsHref;
                this.ExporPageInfoSelect1.AttributesEventAdd("onclick", "ScenicManage.LoadData(this);", 1);
                this.ExporPageInfoSelect1.AttributesEventAdd("onchange", "ScenicManage.LoadData(this);", 0);
                this.repList.DataSource = listScenicArea;
                this.repList.DataBind();
            }
            else
            {
                StringBuilder strEmptyText = new StringBuilder();
                strEmptyText.Append("<table width=\"100%\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"1\" class=\"kuang\">");
                strEmptyText.Append("<tr background=\"" + ImageServerUrl + "/images/yunying/hangbg.gif\" class=\"white\" height=\"23\">");
                strEmptyText.AppendFormat("<td width=\"5%\" height=\"23\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"> <strong>序号</strong> </td> <td width=\"7%\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"> <strong>经营范围</strong></td>", ImageServerUrl);
                strEmptyText.AppendFormat("<td width=\"15%\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"><strong>景区名</strong></td><td width=\"10%\" align=\"center\" background=\"{0}/images/yunying/hangbg.gif\"> <strong>地区</strong></td>", ImageServerUrl);
                strEmptyText.AppendFormat("<td width=\"12%\" align=\"center\" background=\"{0}/images/yunying/hangbg.gif\"><strong>管理公司</strong></td><td width=\"13%\" align=\"center\" background=\"{0}/images/yunying/hangbg.gif\">  <strong>联系方式</strong></td>", ImageServerUrl);
                strEmptyText.AppendFormat("<td align=\"center\" background=\"{0}/images/yunying/hangbg.gif\"> <strong>状态</strong></td><td width=\"11%\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"><strong>B2B</strong></td>", ImageServerUrl);
                strEmptyText.AppendFormat("<td width=\"7%\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"><strong>B2C</strong> </td><td width=\"8%\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"><strong>点击量</strong></td>", ImageServerUrl);
                strEmptyText.AppendFormat("<td width=\"7%\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"><strong>景区管理</strong> </td><td width=\"8%\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"><strong>门票管理</strong></td>", ImageServerUrl);
                strEmptyText.Append("<tr class=\"huanghui\" ><td  align='center' colspan='10' height='100px'>暂无景区信息</td></tr>");
                strEmptyText.Append("</tr>");
                strEmptyText.Append("<tr background=\"" + ImageServerUrl + "/images/yunying/hangbg.gif\" class=\"white\" height=\"23\">");
                strEmptyText.AppendFormat("<td width=\"5%\" height=\"23\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"> <strong>序号</strong> </td> <td width=\"7%\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"> <strong>经营范围</strong></td>", ImageServerUrl);
                strEmptyText.AppendFormat("<td width=\"15%\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"><strong>景区名</strong></td><td width=\"10%\" align=\"center\" background=\"{0}/images/yunying/hangbg.gif\"> <strong>地区</strong></td>", ImageServerUrl);
                strEmptyText.AppendFormat("<td width=\"12%\" align=\"center\" background=\"{0}/images/yunying/hangbg.gif\"><strong>管理公司</strong></td><td width=\"13%\" align=\"center\" background=\"{0}/images/yunying/hangbg.gif\">  <strong>联系方式</strong></td>", ImageServerUrl);
                strEmptyText.AppendFormat("<td align=\"center\" background=\"{0}/images/yunying/hangbg.gif\"> <strong>状态</strong></td><td width=\"11%\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"><strong>B2B</strong></td>", ImageServerUrl);
                strEmptyText.AppendFormat("<td width=\"7%\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"><strong>B2C</strong> </td><td width=\"8%\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"><strong>点击量</strong></td>", ImageServerUrl);
                strEmptyText.AppendFormat("<td width=\"7%\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"><strong>景区管理</strong> </td><td width=\"8%\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"><strong>门票管理</strong></td>", ImageServerUrl);
                strEmptyText.Append("</tr>");
                strEmptyText.Append("</table>");
                this.repList.EmptyText = strEmptyText.ToString();
            }
            SearchModel = null;
            listScenicArea = null;
        }




        /// <summary>
        /// 获取公司的详细信息
        /// </summary>
        /// <returns></returns>
        public string GetCompanyInfo(string ContactOperator)
        {
            string result = "";
            var model = EyouSoft.BLL.CompanyStructure.CompanyUser.CreateInstance().GetModel(ContactOperator);
            if (model != null)
                result = "<a href=" + "javascript:void(0)" + " onmouseout=" + "wsug(event,0)" + " onmouseover=" + "wsug(event,'联系人:" + model.UserName + "&lt;br/&gt;手机:" + model.ContactInfo.Mobile + "&lt;br/&gt;电话:" + model.ContactInfo.Tel + "&lt;br/&gt;传真:" + model.ContactInfo.Fax + "&lt;br/&gt;QQ:" + model.ContactInfo.QQ + "')" + ">"
                + "联系人：" + model.UserName + "<br />"
                + "联系电话：" + model.ContactInfo.Tel + "</a>";
            return result;
        }


        /// <summary>
        /// 获取公司地址根据相应id
        /// </summary>
        /// <param name="provinceid">省id</param>
        /// <param name="cityid">市id</param>
        /// <param name="countryid">县id</param>
        /// <returns></returns>
        public string GetCompanyAddress(string provinceid, string cityid, string countryid)
        {
            string result = string.Empty;
            int proid = Convert.ToInt32(provinceid);
            int ctid = Convert.ToInt32(cityid);
            int cyid = Convert.ToInt32(countryid);
            EyouSoft.Model.SystemStructure.SysProvince proviceModel = EyouSoft.BLL.SystemStructure.SysProvince.CreateInstance().GetProvinceModel(proid);
            EyouSoft.Model.SystemStructure.SysCity cityModel = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetSysCityModel(ctid);
            EyouSoft.Model.SystemStructure.SysDistrictCounty countyModel = EyouSoft.BLL.SystemStructure.SysDistrictCounty.CreateInstance().GetModel(cyid);
            if (proviceModel != null)
                result = proviceModel.ProvinceName + " ";
            if (cityModel != null)
                result += cityModel.CityName + " ";
            if (countyModel != null)
                result += countyModel.DistrictName;

            return result;
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
