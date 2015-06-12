using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using System.Text;
using EyouSoft.Common.Function;

namespace SiteOperationsCenter.CompanyManage
{
    /// <summary>
    /// 修改公司基本信息
    /// 开发人：刘玉灵  时间：2010-7-5
    /// </summary>
    public partial class EditCompany : EyouSoft.Common.Control.YunYingPage
    {
        /// <summary>
        /// 修改的公司ID
        /// </summary>
        protected string EditId = "";

        protected string returnUrl = "";
        protected string OtherSaleCity = "";
        protected string strtype = string.Empty;
        protected string meg = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            EditId = Request.QueryString["EditId"];//要修改的公司ID
            returnUrl = Request.QueryString["returnUrl"];//修改成功后，返回的地址
            if (string.IsNullOrEmpty(returnUrl))
            {
                returnUrl = "Default.aspx";
            }
            if (!Page.IsPostBack)
            {
                if (!string.IsNullOrEmpty(EditId))
                {
                    this.SingleFilelLicence.ToCompanyId = EditId;
                    this.SingleFilelLicence.SiteModule = SiteOperationsCenterModule.会员管理;
                    this.SingleFileBusinessCertImg.ToCompanyId = EditId;
                    this.SingleFileBusinessCertImg.SiteModule = SiteOperationsCenterModule.会员管理;
                    this.SingleFileTaxRegImg.ToCompanyId = EditId;
                    this.SingleFileTaxRegImg.SiteModule = SiteOperationsCenterModule.会员管理;
                    GetCompanyInfo();
                }
                else
                {
                    EyouSoft.Common.Function.MessageBox.ShowAndRedirect(this.Page, "未找到该会员信息!", returnUrl);
                    return;
                }
            }
            if (Request.QueryString["type"] != null)
            {
                string type = Request.QueryString["type"].ToString();
                //获的省份/城市下的线路区域
                if (type.Equals("GetTourArea", StringComparison.OrdinalIgnoreCase))
                {
                    Response.Clear();
                    Response.Write(GetTourAreaList());
                    Response.End();
                }
                else if (type.Equals("isEmail", StringComparison.OrdinalIgnoreCase))
                {
                    string Email = Request.QueryString["Email"].ToString();
                    Response.Clear();
                    Response.Write(IsExistEmail(Email));
                    Response.End();

                }
            }
        }
        #region 绑定省份和城市
        //绑定省份
        private void BindProvince()
        {
            //获取省份列表
            IList<EyouSoft.Model.SystemStructure.SysProvince> ProvinceList = EyouSoft.BLL.SystemStructure.SysProvince.CreateInstance().GetProvinceList();
            this.dropProvinceId.Items.Clear();
            if (ProvinceList != null && ProvinceList.Count > 0)
            {
                this.dropProvinceId.DataSource = ProvinceList;
                this.dropProvinceId.DataTextField = "ProvinceName";
                this.dropProvinceId.DataValueField = "ProvinceId";
                this.dropProvinceId.DataBind();
            }
            this.dropProvinceId.Items.Insert(0, new ListItem("请选择", "0"));
            this.dropProvinceId.Attributes.Add("onchange", string.Format("ChangeList('" + this.dropCityId.ClientID + "', this.options[this.selectedIndex].value)"));
        }

        //绑定城市
        private void BindCity()
        {
            //获取城市列表
            IList<EyouSoft.Model.SystemStructure.SysCity> cityList = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetCityList();
            this.dropCityId.Items.Clear();
            StringBuilder strCity = new StringBuilder();
            strCity.Append("\nvar CityCount;\n");
            strCity.Append("arrCity = new Array();\n");
            if (cityList != null && cityList.Count > 0)
            {
                int index = 0;
                foreach (EyouSoft.Model.SystemStructure.SysCity model in cityList)
                {
                    ListItem item = new ListItem(model.CityName, model.CityId.ToString());
                    //数组下标，城市名称，城市ID，该城市所属的省份ID
                    //每行new一个含3列的的数组
                    string str = string.Format("\narrCity[{0}]=new Array('{1}', {2}, {3});", index.ToString(), model.CityName, model.CityId.ToString(), model.ProvinceId.ToString());
                    strCity.Append(str);
                    index++;
                }
                strCity.Append("\nCityCount=" + index.ToString() + ";");
                strCity.Append("\nfunction ChangeList(CityTextId, ProvinceId)");
                strCity.Append("\n{var Obj = document.getElementById(CityTextId);"); //获得要被绑定小类的控件对象
                strCity.Append("\nObj.length = 0;");
                strCity.Append("\nObj.options[0] = new Option('请选择', 0);");
                strCity.Append("if(ProvinceId==0)");
                strCity.Append("{Obj.options[0].selected=true;return;}");
                strCity.Append("\nvar index=1;");  //smallListObj之后的options下标从1开始                
                strCity.Append("\nfor(var i=0; i<CityCount; i++)");
                //Array数组行和列的下标都是从0开始的
                //arrCity的第i行第3列(下标为2<从0开始>)，即为大类的ID，若选择的大类ID等于当前循环的，则取出该小类
                strCity.Append("\n{if(arrCity[i][2]==ProvinceId)");
                //根据该大类ID，得到小类名称和ID，复制给options的Text和Value值
                strCity.Append("\n{Obj.options[index]=new Option(arrCity[i][0],arrCity[i][1]);\nindex=index+1;}");
                strCity.Append("\n}");
                strCity.Append("\n}");

                //注册该JS
                MessageBox.ResponseScript(this.Page, strCity.ToString());
            }
            this.dropCityId.Items.Insert(0, new ListItem("请选择", "0"));
            this.dropCityId.Attributes.Add("onchange", string.Format("ChangeCountyList('" + this.dropCountyId.ClientID + "', this.options[this.selectedIndex].value,'" + this.dropProvinceId.ClientID + "')"));
        }
        //绑定县区
        private void BindCounty()
        {
            //获得县区列表
            IList<EyouSoft.Model.SystemStructure.SysDistrictCounty> districtcounty = EyouSoft.BLL.SystemStructure.SysDistrictCounty.CreateInstance().GetDistrictCounty();
            this.dropCountyId.Items.Clear();
            StringBuilder strCounty = new StringBuilder();
            strCounty.Append("\nvar CountyCount;\n");
            strCounty.Append("arrCounty = new Array();\n");
            if (districtcounty != null && districtcounty.Count > 0)
            {
                int index = 0;
                foreach (EyouSoft.Model.SystemStructure.SysDistrictCounty model in districtcounty)
                {
                    ListItem item = new ListItem(model.DistrictName, model.Id.ToString());
                    //数组下标，县区名称，县区ID，该县区所属的城市ID，该县区所属的省份ID
                    //每行new一个含4列的的数组
                    string str = string.Format("\narrCounty[{0}]=new Array('{1}', {2}, {3},{4});", index.ToString(), model.DistrictName, model.Id, model.CityId, model.ProvinceId);
                    strCounty.Append(str);
                    index++;
                }
                strCounty.Append("\nCountyCount=" + index.ToString() + ";");
                strCounty.Append("\nfunction ChangeCountyList(CountyTextId,CityTextId, ProvinceId)");
                strCounty.Append("\n{var ProvinceId = document.getElementById(ProvinceId).selectedIndex;");
                strCounty.Append("\nvar ObjCity = document.getElementById(CityTextId);"); //获得要被绑定小类的控件对象
                strCounty.Append("\nvar ObjCounty = document.getElementById(CountyTextId);");
                strCounty.Append("\nObjCounty.length = 0;");
                strCounty.Append("\nObjCounty.options[0] = new Option('请选择', 0);");
                strCounty.Append("if(ProvinceId==0)");
                strCounty.Append("{ObjCity.options[0].selected=true;return;\nObjCounty.options[0].selected=true;return;}");
                strCounty.Append("\nif(ObjCity==0){ObjCounty.options[0].selected=true;return;}");
                strCounty.Append("\nvar index=1;");  //smallListObj之后的options下标从1开始                
                strCounty.Append("\nfor(var i=0; i<CountyCount; i++)");
                //Array数组行和列的下标都是从0开始的
                //narrCounty的第i行第3列(下标为2<从0开始>)，即为大类的ID，若选择的大类ID等于当前循环的，则取出该小类
                strCounty.Append("\n{if(arrCounty[i][3]==ProvinceId&&arrCounty[i][2]==CityTextId)");
                //根据该大类ID，得到小类名称和ID，复制给options的Text和Value值
                strCounty.Append("\n{ObjCounty.options[index]=new Option(arrCounty[i][0],arrCounty[i][1]);\nindex=index+1;}");
                strCounty.Append("\n}");
                strCounty.Append("\n}");

                //注册该JS
                MessageBox.ResponseScript(this.Page, strCounty.ToString());
            }
            this.dropCountyId.Items.Insert(0, new ListItem("请选择", "0"));
        }
        #endregion

        #region 获的省份/城市下的线路区域
        /// <summary>
        /// 获的省份/城市下的线路区域
        /// </summary>
        /// <returns></returns>
        protected string GetTourAreaList()
        {
            StringBuilder strTourAreaLis = new StringBuilder();
            int ProvinceId = EyouSoft.Common.Utils.GetInt(Request.QueryString["ProvinceId"]);
            int CityId = EyouSoft.Common.Utils.GetInt(Request.QueryString["CityId"]);
            string CityName = EyouSoft.Common.Utils.InputText(Request.QueryString["CityName"]);
            strTourAreaLis.AppendFormat("<table  cellspacing=\"0\" cellpadding=\"5\" border=\"0\" align=\"center\" width=\"900\" style=\"border: 1px solid rgb(197, 197, 197); background: none repeat scroll 0% 0% rgb(249, 249, 249);\" class=\"margin10\">");
            strTourAreaLis.AppendFormat("<tr><td align=\"left\"><img src=\"{0}\"></td></tr>", ImageServerUrl + "/images/yunying/jingyingquyu.gif");
            strTourAreaLis.Append("<tr><td>");
            //国内长线
            //IList<EyouSoft.Model.SystemStructure.SysAreaSiteControl> AreaList1 = EyouSoft.BLL.SystemStructure.SysArea.CreateInstance().GetLongAreaSiteControl(ProvinceId);
            IList<EyouSoft.Model.SystemStructure.SysArea> AreaList1 = EyouSoft.BLL.SystemStructure.SysArea.CreateInstance().GetSysAreaList(EyouSoft.Model.SystemStructure.AreaType.国内长线);
            if (AreaList1 != null && AreaList1.Count > 0)
            {
                strTourAreaLis.AppendFormat("<table cellspacing=\"3\" cellpadding=\"2\" width=\"100%\" border=\"0\" style=\"border-bottom:1px dashed #cccccc;\" onmouseover=\"mouseovertr(this)\"   onmouseout=\"mouseouttr(this)\" > <tr ><td align=\"middle\" width=\"4%\" bgcolor=\"#D1E7F4\">国内长线</td><td width=\"96%\">");
                strTourAreaLis.Append("<table cellspacing=\"0\" width=\"100%\" border=\"0\">");
                strTourAreaLis.Append("<tr>");
                int Count = 0;
                foreach (EyouSoft.Model.SystemStructure.SysArea item in AreaList1)
                {
                    strTourAreaLis.AppendFormat("<td width=\"25%\" align=\"left\"><input id=\"Area_{0}\"  type=\"checkbox\"  value=\"{0}\" name=\"checkbox_Area\" /><label for=\"Area_{0}\">{1}</label></td>", item.AreaId, item.AreaName);
                    Count++;
                    if (Count != 0 && Count % 4 == 0)
                    {
                        strTourAreaLis.Append("</tr><tr>");
                    }
                }
                strTourAreaLis.Append("</tr>");
                strTourAreaLis.Append("</table>");
                strTourAreaLis.Append("</td></tr></table>");
            }
            AreaList1 = null;
            //国际线
            //IList<EyouSoft.Model.SystemStructure.SysArea> AreaList2 = EyouSoft.BLL.SystemStructure.SysArea.CreateInstance().GetSysAreaList(EyouSoft.Model.SystemStructure.AreaType.国际线);
            IList<EyouSoft.Model.SystemStructure.SysArea> AreaList2 = EyouSoft.BLL.SystemStructure.SysArea.CreateInstance().GetSysAreaList(EyouSoft.Model.SystemStructure.AreaType.国际线);
            if (AreaList2 != null && AreaList2.Count > 0)
            {
                strTourAreaLis.AppendFormat("<table  onmouseover=\"mouseovertr(this)\"   onmouseout=\"mouseouttr(this)\" cellspacing=\"3\" cellpadding=\"2\" width=\"100%\" border=\"0\" style=\"border-bottom:1px dashed #cccccc;\"> <tr ><td align=\"middle\" width=\"4%\" bgcolor=\"#D1E7F4\">国际线</td><td width=\"96%\">");
                strTourAreaLis.Append("<table cellspacing=\"0\" width=\"100%\" border=\"0\">");
                strTourAreaLis.Append("<tr>");
                int Count = 0;
                foreach (EyouSoft.Model.SystemStructure.SysArea item in AreaList2)
                {
                    strTourAreaLis.AppendFormat("<td width=\"25%\" align=\"left\"><input id=\"Area2_{0}\"  type=\"checkbox\"  value=\"{0}\" name=\"checkbox_Area\" /><label for=\"Area2_{0}\">{1}</label></td>", item.AreaId, item.AreaName);
                    Count++;
                    if (Count != 0 && Count % 4 == 0)
                    {
                        strTourAreaLis.Append("</tr><tr>");
                    }
                }
                strTourAreaLis.Append("</tr>");
                strTourAreaLis.Append("</table>");
                strTourAreaLis.Append("</td></tr></table>");
            }
            //国内短线
            //IList<EyouSoft.Model.SystemStructure.SysAreaSiteControl> AreaList3 = EyouSoft.BLL.SystemStructure.SysArea.CreateInstance().GetShortAreaSiteControl(CityId);
            IList<EyouSoft.Model.SystemStructure.SysArea> AreaList3 = EyouSoft.BLL.SystemStructure.SysArea.CreateInstance().GetSysAreaList(EyouSoft.Model.SystemStructure.AreaType.国内短线);
            if (AreaList3 != null && AreaList3.Count > 0)
            {
                strTourAreaLis.AppendFormat("<table onmouseover=\"mouseovertr(this)\"   onmouseout=\"mouseouttr(this)\" cellspacing=\"3\" cellpadding=\"2\" width=\"100%\" border=\"0\" style=\"border-bottom:1px dashed #cccccc;\"> <tr ><td align=\"middle\" width=\"4%\" bgcolor=\"#D1E7F4\">国内短线</td><td width=\"96%\">");
                strTourAreaLis.Append("<table cellspacing=\"0\" width=\"100%\" border=\"0\">");
                strTourAreaLis.Append("<tr>");
                int Count = 0;
                foreach (EyouSoft.Model.SystemStructure.SysArea item in AreaList3)
                {
                    strTourAreaLis.AppendFormat("<td width=\"25%\" align=\"left\"><input id=\"Area3_{0}\"  type=\"checkbox\"  value=\"{0}\" name=\"checkbox_Area\" /><label for=\"Area3_{0}\">{1}</label></td>", item.AreaId, item.AreaName);
                    Count++;
                    if (Count != 0 && Count % 4 == 0)
                    {
                        strTourAreaLis.Append("</tr><tr>");
                    }
                }
                strTourAreaLis.Append("</tr>");
                strTourAreaLis.Append("</table>");
                strTourAreaLis.Append("</td></tr></table>");
            }
            strTourAreaLis.Append("</td></tr></table>");
            AreaList3 = null;


            return strTourAreaLis.ToString();

        }
        #endregion

        #region 初始化公司信息
        /// <summary>
        /// 初始化公司信息
        /// </summary>
        protected void GetCompanyInfo()
        {
            EyouSoft.Model.CompanyStructure.CompanyDetailInfo Model = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(EditId);
            if (Model != null)
            {
                BindProvince();
                BindCity();
                BindCounty();
                //省份 城市
                if (Model.ProvinceId > 0)
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "SetProvince('" + Model.ProvinceId + "');ChangeList('" + this.dropCityId.ClientID + "','" + Model.ProvinceId + "');", true);
                    if (Model.CityId > 0)
                    {
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "SetCity('" + Model.CityId + "');ChangeCountyList('"+this.dropCountyId.ClientID+"','"+Model.CityId+"','"+this.dropProvinceId.ClientID+"');", true);
                        if (Model.CountyId > 0)
                        {
                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "SetCounty('" + Model.CountyId + "');", true);
                        }
                    }
                }

                this.txtCompanyName.Value = Model.CompanyName;
                this.txtLicenseNumber.Value = Model.License;
                this.txtUserName.Value = Model.AdminAccount.UserName;

                this.txtContactName.Value = Model.ContactInfo.ContactName;
                //引荐单位
                this.labCommendCompany.InnerText = Model.CommendPeople;

                this.txtContactTel.Value = Model.ContactInfo.Tel;
                this.txtContactMobile.Value = Model.ContactInfo.Mobile;
                this.txtContactFax.Value = Model.ContactInfo.Fax;
                this.txtContactEmail.Value = Model.ContactInfo.Email;
                this.hidContactEmail.Value = Model.ContactInfo.Email;
                this.txtBrandName.Value = Model.CompanyBrand;
                this.txtOfficeAddress.Value = Model.CompanyAddress;
                this.txtContactQQ.Value = Model.ContactInfo.QQ;
                this.txtContactMSN.Value = Model.ContactInfo.MSN;
                this.txtCompanyInfo.Value = Model.Remark;
                this.txtBusinessSuperior.Value = Model.ShortRemark;
                //经营范围

                EyouSoft.Model.CompanyStructure.CompanyType[] TypeItems = Model.CompanyRole.RoleItems;
                bool isManage = CheckMasterGrant(EyouSoft.Common.YuYingPermission.会员管理_管理该栏目);
                string TypeId = "";
                #region 公司类型
                for (int i = 0; i < TypeItems.Length; i++)
                {
                    if (TypeItems[i] == EyouSoft.Model.CompanyStructure.CompanyType.专线)
                    {

                        if (!isManage || !CheckMasterGrant(EyouSoft.Common.YuYingPermission.旅行社汇总管理_修改))
                        {
                            Utils.ResponseNoPermit(EyouSoft.Common.YuYingPermission.旅行社汇总管理_修改, true);
                            return;
                        }
                        TypeId = "1";
                    }
                    else if (TypeItems[i] == EyouSoft.Model.CompanyStructure.CompanyType.组团)
                    {
                        if (!isManage || !CheckMasterGrant(EyouSoft.Common.YuYingPermission.旅行社汇总管理_修改))
                        {
                            Utils.ResponseNoPermit(EyouSoft.Common.YuYingPermission.旅行社汇总管理_修改, true);
                            return;
                        }
                        if (TypeId != "")
                        {
                            TypeId = TypeId + ",2";
                        }
                        else
                        {
                            TypeId = "2";
                        }
                    }
                    else if (TypeItems[i] == EyouSoft.Model.CompanyStructure.CompanyType.地接)
                    {
                        if (!isManage || !CheckMasterGrant(EyouSoft.Common.YuYingPermission.旅行社汇总管理_修改))
                        {
                            Utils.ResponseNoPermit(EyouSoft.Common.YuYingPermission.旅行社汇总管理_修改, true);
                            return;
                        }
                        if (TypeId != "")
                        {
                            TypeId = TypeId + ",3";
                        }
                        else
                        {
                            TypeId = "3";
                        }
                    }
                    else if (TypeItems[i] == EyouSoft.Model.CompanyStructure.CompanyType.景区)
                    {
                        if (!isManage || !CheckMasterGrant(EyouSoft.Common.YuYingPermission.景区汇总管理_修改))
                        {
                            Utils.ResponseNoPermit(EyouSoft.Common.YuYingPermission.景区汇总管理_修改, true);
                            return;
                        }

                        TypeId = "4";
                    }
                    else if (TypeItems[i] == EyouSoft.Model.CompanyStructure.CompanyType.酒店)
                    {
                        if (!isManage || !CheckMasterGrant(EyouSoft.Common.YuYingPermission.酒店汇总管理_修改))
                        {
                            Utils.ResponseNoPermit(EyouSoft.Common.YuYingPermission.酒店汇总管理_修改, true);
                            return;
                        }
                        TypeId = "5";
                    }
                    else if (TypeItems[i] == EyouSoft.Model.CompanyStructure.CompanyType.车队)
                    {
                        if (!isManage || !CheckMasterGrant(EyouSoft.Common.YuYingPermission.车队汇总管理_修改))
                        {
                            Utils.ResponseNoPermit(EyouSoft.Common.YuYingPermission.车队汇总管理_修改, true);
                            return;
                        }
                        TypeId = "6";
                    }
                    else if (TypeItems[i] == EyouSoft.Model.CompanyStructure.CompanyType.旅游用品店)
                    {
                        if (!isManage || !CheckMasterGrant(EyouSoft.Common.YuYingPermission.旅游用品店汇总管理_修改))
                        {
                            Utils.ResponseNoPermit(EyouSoft.Common.YuYingPermission.旅游用品店汇总管理_修改, true);
                            return;
                        }
                        TypeId = "7";
                    }
                    else if (TypeItems[i] == EyouSoft.Model.CompanyStructure.CompanyType.购物店)
                    {
                        if (!isManage || !CheckMasterGrant(EyouSoft.Common.YuYingPermission.购物店汇总管理_修改))
                        {
                            Utils.ResponseNoPermit(EyouSoft.Common.YuYingPermission.购物店汇总管理_修改, true);
                            return;
                        }
                        TypeId = "8";
                    }
                    else if (TypeItems[i] == EyouSoft.Model.CompanyStructure.CompanyType.机票供应商)
                    {
                        if (!isManage || !CheckMasterGrant(EyouSoft.Common.YuYingPermission.机票供应商管理_修改))
                        {
                            Utils.ResponseNoPermit(EyouSoft.Common.YuYingPermission.机票供应商管理_修改, true);
                            return;
                        }
                        TypeId = "9";
                    }
                    else if (TypeItems[i] == EyouSoft.Model.CompanyStructure.CompanyType.其他采购商)
                    {
                        if (!isManage || !CheckMasterGrant(EyouSoft.Common.YuYingPermission.其他采购商管理_修改))
                        {
                            Utils.ResponseNoPermit(EyouSoft.Common.YuYingPermission.其他采购商管理_修改, true);
                            return;
                        }
                        TypeId = "10";
                    }
                    else if (TypeItems[i] == EyouSoft.Model.CompanyStructure.CompanyType.随便逛逛)
                    {
                        if (!isManage || !CheckMasterGrant(EyouSoft.Common.YuYingPermission.随便逛逛_修改))
                        {
                            Utils.ResponseNoPermit(EyouSoft.Common.YuYingPermission.随便逛逛_修改, true);
                            return;
                        }
                        TypeId = "11";
                    }
                }
                #endregion

                EyouSoft.Common.Function.MessageBox.ResponseScript(this.Page, string.Format("Edit.SetCompanyType('{0}');", TypeId));

                if (TypeId.Contains("1")) //专线商
                {
                    //销售城市
                    IList<EyouSoft.Model.SystemStructure.CityBase> SaleCityList = Model.SaleCity;
                    StringBuilder MySaleCity = new StringBuilder();
                    if (SaleCityList != null && SaleCityList.Count > 0)
                    {
                        int Count = 0;
                        foreach (EyouSoft.Model.SystemStructure.CityBase item in SaleCityList)
                        {
                            Count++;
                            if (Count % 13 == 0)
                            {
                                MySaleCity.Append("<br/>");
                            }
                            MySaleCity.AppendFormat("<input type='checkbox' id='SaleCity_{0}' value='{0}' checked='checked' name='ckSellCity'/><label for='SaleCity_{0}'>{1}</label>", item.CityId, item.CityName);
                        }
                    }
                    SaleCityList = null;
                    //输入的销售城市
                    string strSaleCity = EyouSoft.BLL.CompanyStructure.CompanyUnCheckedCity.CreateInstance().GetSaleCity(EditId);
                    if (!string.IsNullOrEmpty(strSaleCity))
                    {
                        OtherSaleCity = string.Format("<tr  class=\"lr_hangbg\"><td align=\"right\" class=\"lr_shangbg\">待增加的销售城市：</td><td colspan=\"2\">{0}</td></tr>", strSaleCity);
                    }

                    //经营线路区域
                    IList<EyouSoft.Model.SystemStructure.AreaBase> AreaList = Model.Area;
                    StringBuilder MyAreaList = new StringBuilder();
                    if (AreaList != null && AreaList.Count > 0)
                    {
                        foreach (EyouSoft.Model.SystemStructure.AreaBase item in AreaList)
                        {
                            MyAreaList.Append(item.AreaId + ",");
                        }

                    }
                    EyouSoft.Common.Function.MessageBox.ResponseScript(this.Page, string.Format("Edit.GetSellCity(\"{0}\",'{1}');", MySaleCity.ToString(), MyAreaList.ToString()));
                    AreaList = null;

                }
                //证书
                EyouSoft.Model.CompanyStructure.BusinessCertif Buiness = Model.AttachInfo.BusinessCertif;
                if (Buiness != null)
                {
                    string OldLicenceImg = Buiness.LicenceImg;
                    string OldBusinessCertImg = Buiness.BusinessCertImg;
                    string OldTaxRegImg = Buiness.TaxRegImg;
                    if (!string.IsNullOrEmpty(OldLicenceImg))
                    {
                        this.ltrLicenceImg.Text = string.Format("<a href='{0}' target='_blank'>己上传</a>", EyouSoft.Common.Domain.FileSystem + OldLicenceImg, OldLicenceImg);
                        this.hidOldLicenceImg.Value = OldLicenceImg;
                    }
                    if (!string.IsNullOrEmpty(OldBusinessCertImg))
                    {
                        this.ltrBusinessCertImg.Text = string.Format("<a href='{0}' target='_blank'>己上传</a>", EyouSoft.Common.Domain.FileSystem + OldBusinessCertImg, OldBusinessCertImg);
                        this.hidOldBusinessCertImg.Value = OldBusinessCertImg;
                    }
                    if (!string.IsNullOrEmpty(OldTaxRegImg))
                    {
                        this.ltrTaxRegImg.Text = string.Format("<a href='{0}' target='_blank'>己上传</a>", EyouSoft.Common.Domain.FileSystem + OldTaxRegImg, OldTaxRegImg);
                        this.hidOldTaxRegImg.Value = OldTaxRegImg;
                    }
                }
                Buiness = null;
                //公司银行帐户
                List<EyouSoft.Model.CompanyStructure.BankAccount> BankList = new List<EyouSoft.Model.CompanyStructure.BankAccount>();
                BankList = Model.BankAccounts;
                IList<EyouSoft.Model.CompanyStructure.BankAccount> oneselfBank = new List<EyouSoft.Model.CompanyStructure.BankAccount>();
                if (BankList != null && BankList.Count > 0)
                {
                    foreach (EyouSoft.Model.CompanyStructure.BankAccount Bank in BankList)
                    {
                        if (Bank.AccountType == EyouSoft.Model.CompanyStructure.BankAccountType.公司)
                        {
                            this.txtCompanyBackName.Value = Bank.BankAccountName;
                            this.txtCompanyBack.Value = Bank.BankName;
                            this.txtCompanyBackNumber.Value = Bank.AccountNumber;
                        }
                        else
                        {
                            oneselfBank.Add(Bank);
                        }
                    }

                }
                if (oneselfBank != null && oneselfBank.Count > 0)
                {
                    this.RepeaterBank.DataSource = oneselfBank;
                    this.RepeaterBank.DataBind();
                }
                oneselfBank = null;
                BankList = null;


            }
            else
            {
                EyouSoft.Common.Function.MessageBox.ShowAndRedirect(this.Page, "未找到该会员信息!", returnUrl);
                return;
            }

            Model = null;
        }
        #endregion

        #region 验证邮箱是否存在
        /// <summary>
        /// 邮箱是否存在
        /// </summary>
        private bool IsExistEmail(string Email)
        {
            bool isTrue = false;
            isTrue = EyouSoft.BLL.CompanyStructure.CompanyUser.CreateInstance().IsExistsEmail(Email);
            return isTrue;
        }
        #endregion

        #region 修改公司信息
        /// <summary>
        /// 修改公司信息
        /// </summary>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            //省份ID
            int ProvinceId = EyouSoft.Common.Utils.GetInt(Request.Form["dropProvinceId"]);
            //城市ID
            int CityId = EyouSoft.Common.Utils.GetInt(Request.Form["dropCityId"]);
            //县区ID
            int CountyId = EyouSoft.Common.Utils.GetInt(Request.Form["dropCountyId"]);
            //公司名称
            string txtCompanyName = Utils.InputText(this.txtCompanyName.Value.Trim());
            //许可证号
            string txtLicenseNumber = Utils.InputText(this.txtLicenseNumber.Value.Trim());
            //名牌名称
            string txtBrandName = Utils.InputText(this.txtBrandName.Value.Trim());
            //用户名
            string txtUserName = Utils.InputText(this.txtUserName.Value.Trim());
            //密码
            string NewPassWord = "";
            if (!string.IsNullOrEmpty(this.txtPassWord.Value.Trim()))
            {
                NewPassWord = Utils.InputText(this.txtPassWord.Value.Trim());
            }
            //联系人名称
            string txtContactName = Utils.InputText(this.txtContactName.Value.Trim());

            //联系电话
            string txtContactTel = Utils.InputText(this.txtContactTel.Value.Trim());
            //联系手机
            string txtContactMobile = Utils.InputText(this.txtContactMobile.Value.Trim());
            //传真
            string txtContactFax = Utils.InputText(this.txtContactFax.Value.Trim());
            //办公地点
            string txtOfficeAddress = Utils.InputText(this.txtOfficeAddress.Value.Trim());
            //MQ
            string txtContactQQ = Utils.InputText(this.txtContactQQ.Value.Trim(),100);
            //MSN
            string txtContactMSN = Utils.InputText(this.txtContactMSN.Value.Trim());
            //Email
            string txtContactEmail = Utils.InputText(this.txtContactEmail.Value.Trim());
            //经营范围
            string CompanyType = Utils.InputText(Request.Form["radManageArea"]);
            if (CompanyType == "")
            {
                //旅行社
                CompanyType = Utils.InputText(Request.Form["ckCompanyType"]);

                //专线商时有销售城市及经营线路区域
            }
            //公司介绍
            string txtCompanyInfo = Utils.EditInputText(this.txtCompanyInfo.Value.Trim());
            //业务优势
            string txtBusinessSuperior = Utils.InputText(this.txtBusinessSuperior.Value.Trim());

            #region  公司银行账户
            string txtCompanyBackName = Utils.InputText(this.txtCompanyBackName.Value.Trim());
            string txtCompanyBack = Utils.InputText(this.txtCompanyBack.Value.Trim());
            string txtCompanyBackNumber = Utils.InputText(this.txtCompanyBackNumber.Value.Trim());
            #endregion

            #region 个人银行账户
            string[] txtBankAccountName = Request.Form.GetValues("txtBankAccountName");
            string[] txtBankName = Request.Form.GetValues("txtBankName");
            string[] txtAccountNumber = Request.Form.GetValues("txtAccountNumber");
            #endregion

            EyouSoft.Model.CompanyStructure.CompanyArchiveInfo model = new EyouSoft.Model.CompanyStructure.CompanyArchiveInfo();

            /*公司信息*/
            model.ID = EditId;
            model.CompanyName = txtCompanyName;
            model.ProvinceId = ProvinceId;
            model.CityId = CityId;
            model.CountyId = CountyId;
            model.CompanyAddress = txtOfficeAddress;
            model.CompanyBrand = txtBrandName;
            model.License = txtLicenseNumber;
            model.Remark = txtCompanyInfo;
            model.ShortRemark = txtBusinessSuperior;

            /*证书管理 */

            EyouSoft.Model.CompanyStructure.CompanyAttachInfo AttachInfo = new EyouSoft.Model.CompanyStructure.CompanyAttachInfo();
            EyouSoft.Model.CompanyStructure.BusinessCertif Buiness = new EyouSoft.Model.CompanyStructure.BusinessCertif();
            string LicenceImg = Request.Form["SingleFilelLicence$hidFileName"];
            if (string.IsNullOrEmpty(LicenceImg) && this.hidOldLicenceImg.Value != "")
            {
                LicenceImg = this.hidOldLicenceImg.Value;
            }
            string BusinessCertImg = Request.Form["SingleFileBusinessCertImg$hidFileName"];
            if (string.IsNullOrEmpty(BusinessCertImg) && this.hidOldBusinessCertImg.Value != "")
            {
                BusinessCertImg = this.hidOldBusinessCertImg.Value;
            }
            string TaxRegImg = Request.Form["SingleFileTaxRegImg$hidFileName"];
            if (string.IsNullOrEmpty(TaxRegImg) && this.hidOldTaxRegImg.Value != "")
            {
                TaxRegImg = this.hidOldTaxRegImg.Value;
            }
            Buiness.LicenceImg = LicenceImg;
            Buiness.BusinessCertImg = BusinessCertImg;
            Buiness.TaxRegImg = TaxRegImg;

            AttachInfo.BusinessCertif = Buiness;
            Buiness = null;

            model.AttachInfo = AttachInfo;
            AttachInfo = null;


            EyouSoft.Model.CompanyStructure.UserAccount UserModel = new EyouSoft.Model.CompanyStructure.UserAccount();
            #region 用户信息
            /*用户信息*/
            UserModel.UserName = txtUserName;
            UserModel.PassWordInfo.NoEncryptPassword = NewPassWord;
            #endregion
            model.AdminAccount = UserModel;
            UserModel = null;

            EyouSoft.Model.CompanyStructure.ContactPersonInfo ContactModel = new EyouSoft.Model.CompanyStructure.ContactPersonInfo();
            #region 公司联系人信息
            /* 公司联系人信息 */
            ContactModel.ContactName = txtContactName;
            ContactModel.Tel = txtContactTel;
            ContactModel.Mobile = txtContactMobile;
            ContactModel.Fax = txtContactFax;
            ContactModel.QQ = txtContactQQ;
            ContactModel.MSN = txtContactMSN;
            ContactModel.Email = txtContactEmail;
            #endregion
            model.ContactInfo = ContactModel;
            ContactModel = null;

            #region 公司身份
            /* 公司身份 */
            EyouSoft.Model.CompanyStructure.CompanyRole RoleMode = new EyouSoft.Model.CompanyStructure.CompanyRole();
            EyouSoft.Model.CompanyStructure.CompanyType TypeEmnu = EyouSoft.Model.CompanyStructure.CompanyType.全部;
            //旅行社
            if (CompanyType == "0")
            {
                string[] strType = Request.Form.GetValues("ckCompanyType");
                for (int i = 0; i < strType.Length; i++)
                {
                    if (strType[i] == "1")
                    {
                        TypeEmnu = EyouSoft.Model.CompanyStructure.CompanyType.专线;

                        string[] strSalelist = Request.Form.GetValues("ckSellCity");
                        if (strSalelist != null)
                        {
                            //设置销售城市
                            List<EyouSoft.Model.SystemStructure.CityBase> SaleCity = new List<EyouSoft.Model.SystemStructure.CityBase>();
                            for (int j = 0; j < strSalelist.Length; j++)
                            {
                                EyouSoft.Model.SystemStructure.CityBase item = new EyouSoft.Model.SystemStructure.CityBase();
                                item.ProvinceId = ProvinceId;
                                item.CityId = EyouSoft.Common.Function.StringValidate.GetIntValue(strSalelist[j]);
                                SaleCity.Add(item);
                                item = null;
                            }
                            model.SaleCity = SaleCity;
                            SaleCity = null;
                        }
                        //设置经营线路区域
                        string[] strAreaList = Request.Form.GetValues("checkbox_Area");

                        if (strAreaList != null)
                        {
                            List<EyouSoft.Model.SystemStructure.AreaBase> AreaList = new List<EyouSoft.Model.SystemStructure.AreaBase>();
                            for (int j = 0; j < strAreaList.Length; j++)
                            {
                                EyouSoft.Model.SystemStructure.AreaBase item = new EyouSoft.Model.SystemStructure.AreaBase();
                                item.AreaId = Convert.ToInt32(strAreaList[j]);
                                AreaList.Add(item);
                                item = null;
                            }
                            model.Area = AreaList;
                            AreaList = null;
                        }

                    }
                    if (strType[i] == "2")
                    {
                        TypeEmnu = EyouSoft.Model.CompanyStructure.CompanyType.组团;
                    }
                    if (strType[i] == "3")
                    {
                        TypeEmnu = EyouSoft.Model.CompanyStructure.CompanyType.地接;
                    }
                    RoleMode.SetRole(TypeEmnu);
                }

            }
            else
            {
                switch (CompanyType)
                {
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
                RoleMode.SetRole(TypeEmnu);
            }
            #endregion
            model.CompanyRole = RoleMode;
            RoleMode = null;

            List<EyouSoft.Model.CompanyStructure.BankAccount> BankList = new List<EyouSoft.Model.CompanyStructure.BankAccount>();
            #region 银行账号
            /* 公司银行账户信息 */
            EyouSoft.Model.CompanyStructure.BankAccount BankCompanyModel = new EyouSoft.Model.CompanyStructure.BankAccount();
            BankCompanyModel.AccountType = EyouSoft.Model.CompanyStructure.BankAccountType.公司;
            BankCompanyModel.BankAccountName = txtCompanyBackName;
            BankCompanyModel.BankName = txtCompanyBack;
            BankCompanyModel.AccountNumber = txtCompanyBackNumber;
            BankCompanyModel.CompanyID = EditId;
            BankList.Add(BankCompanyModel);
            BankCompanyModel = null;
            if (!string.IsNullOrEmpty(txtBankAccountName.ToString()))
            {
                /* 个人银行帐户信息 */
                for (int i = 0; i < txtBankAccountName.Length; i++)
                {
                    if (!string.IsNullOrEmpty(txtBankAccountName[i]))
                    {
                        EyouSoft.Model.CompanyStructure.BankAccount BankModel = new EyouSoft.Model.CompanyStructure.BankAccount();
                        BankModel.AccountType = EyouSoft.Model.CompanyStructure.BankAccountType.个人;
                        BankModel.BankAccountName = txtBankAccountName[i];
                        BankModel.BankName = txtBankName[i];
                        BankModel.AccountNumber = txtAccountNumber[i];
                        BankModel.CompanyID = EditId;
                        BankList.Add(BankModel);
                        BankModel = null;
                    }
                }
            }
            #endregion
            model.BankAccounts = BankList;
            BankList = null;

            bool Result = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().Update(model);
            if (Result)
            {
                if (EyouSoft.Common.Utils.GetQueryStringValue("type") == "list")
                {
                    Response.Write("<script language='javascript'>alert('修改成功!');parent.Boxy.getIframeDialog('" + Request.QueryString["iframeid"] + "').hide();</script>");
                }
                else
                {
                    EyouSoft.Common.Function.MessageBox.ShowAndRedirect(this.Page, "修改成功!", returnUrl);
                }
            }
            else
            {
                EyouSoft.Common.Function.MessageBox.Show(this.Page, "修改失败!");
            }
            model = null;

        }
        #endregion

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (EyouSoft.Common.Utils.GetQueryStringValue("type") == "list")
            {
                Response.Write("<script language='javascript'>parent.Boxy.getIframeDialog('" + Request.QueryString["iframeid"] + "').hide();</script>");
            }
            else
            {
                Response.Redirect(returnUrl);
            }
        }
    }
}
