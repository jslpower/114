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
using EyouSoft.Common.Control;
using System.Collections.Generic;
using EyouSoft.Common;
using System.Text.RegularExpressions;
using EyouSoft.Common.Function;

namespace UserBackCenter.HotelCenter.HotelOrderManage
{
    public partial class HotelVisitorsPage : BackPage
    {
        #region 成员变量
        protected string EditId = string.Empty;
        protected string FlageName = "添加常旅客";
        #endregion


        #region 页面加载
        protected void Page_Load(object sender, EventArgs e)
        {

            // 初始化旅客类型
            BindVisitorType();
            //绑定证件类型
            BindCardType();
            //初始化国家
            InitCountryList();
            string method = Utils.GetFormValue("method");
            if (!string.IsNullOrEmpty(Utils.InputText(Context.Request.QueryString["EditId"])))
            {
                EditId = Utils.GetString(Context.Request.QueryString["EditId"], "");
                this.hvp_hfEditId.Value = EditId;
            }    

            if (method == "save")
            {
                SaveHotelVisitors();
                return;
            }
           
            if (!Page.IsPostBack)
            {
                if (this.SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.其他采购商) && this.SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.组团))
                {
                    Response.Clear();
                    Response.Write("对不起，您没有权限");
                    Response.End();
                    return;
                }


                //根据旅客ID获得常旅客实体对象 
                EyouSoft.Model.TicketStructure.TicketVistorInfo HotelVisitorInfo = EyouSoft.BLL.TicketStructure.TicketVisitor.CreateInstance().GetTicketVisitorInfo(EditId);
               
                #region 初始化表单数据
                if (HotelVisitorInfo != null)
                {
                    FlageName = "修改常旅客";
                    this.hvp_ddlCardType.SelectedValue = ((int)HotelVisitorInfo.CardType).ToString();
                    this.hvp_ddlVisitorType.SelectedValue = ((int)HotelVisitorInfo.VistorType).ToString();
                    if (HotelVisitorInfo.NationInfo.NationId.ToString()!="")
                       this.hvp_ddlCountry.SelectedValue = ((int)HotelVisitorInfo.NationInfo.NationId).ToString();
                    this.hvp_txtChinaName.Value = HotelVisitorInfo.ChinaName.Trim();
                    this.hvp_txtEnglishName.Value = HotelVisitorInfo.EnglishName.Trim();
                    this.hvp_txtCardNo.Value = HotelVisitorInfo.CardNo.Trim();
                    this.hvp_txtMobilePhone.Value = HotelVisitorInfo.ContactTel.Trim();
                    this.hvp_txtReamrk.Value = HotelVisitorInfo.Remark.Trim();
                    EyouSoft.Model.CompanyStructure.Sex Gender = HotelVisitorInfo.ContactSex;
                    if (Gender == EyouSoft.Model.CompanyStructure.Sex.男)
                    {
                        this.hvp_rdoMale.Checked = true;
                        this.hvp_rdoFemale.Checked = false;
                    }
                    else
                    {
                        this.hvp_rdoFemale.Checked = true;
                        this.hvp_rdoMale.Checked = false;
                    }
                }
                //释放资源
                HotelVisitorInfo = null;
                #endregion
            }
        }
        #endregion

        #region 绑定旅客类型
        private void BindVisitorType()
        {
            this.hvp_ddlVisitorType.Items.Add(new ListItem("-请选择-", ""));
            string[] typeList = Enum.GetNames(typeof(EyouSoft.Model.TicketStructure.TicketVistorType));
            if (typeList != null && typeList.Length > 0)
            {
                foreach (string str in typeList)
                {
                    this.hvp_ddlVisitorType.Items.Add(new ListItem(str, ((int)Enum.Parse(typeof(EyouSoft.Model.TicketStructure.TicketVistorType), str)).ToString()));
                }
            }
            //释放资源
            typeList = null;
        }
        #endregion

        #region 绑定证件类型
        private void BindCardType()
        {  
             this.hvp_ddlCardType.Items.Clear();                       
            List<EnumObj> typeList = EnumObj.GetList(typeof(EyouSoft.Model.TicketStructure.TicketCardType));

            if (typeList != null && typeList.Count > 0)
            {
                for (int i = 0; i < typeList.Count; i++)
                {
                    ListItem list_CardType = new ListItem();
                    switch (typeList[i].Value)
                    {
                        case "-1":
                            list_CardType.Text = "-请选择-";
                            break;
                        case "0":
                            list_CardType.Text = "身份证";
                            break;
                        case "1":
                            list_CardType.Text = "护照";
                            break;
                        case "2":
                            list_CardType.Text = "军官证";
                            break;
                        case "3":
                            list_CardType.Text = "台胞证";
                            break;
                        case "4":
                            list_CardType.Text = "港澳通行证";
                            break;

                    }
                    list_CardType.Value = typeList[i].Value;
                    this.hvp_ddlCardType.Items.Add(list_CardType);
                }             
            }
            //释放资源
            typeList = null;
        }
        #endregion

        #region 初始化国家列表
        private void InitCountryList()
        {

            this.hvp_ddlCountry.Items.Add(new ListItem("-请选择-", ""));
            string countryInfo = this.hvp_ddlCountry.SelectedItem.Value;
            IList<EyouSoft.Model.TicketStructure.TicketNationInfo> list = EyouSoft.BLL.TicketStructure.TicketNationInfo.CreateInstance().GetList(countryInfo);
            for (int i = 0; i < list.Count; i++)
            {
                ListItem liCountry = new ListItem();
                liCountry.Text = list[i].CountryCode + "--" + list[i].CountryName;
                liCountry.Value = list[i].NationId.ToString();
                this.hvp_ddlCountry.Items.Add(liCountry);

            }
            this.hvp_ddlCountry.DataBind();
        }
        #endregion

        #region 保存常旅客信息
        private void SaveHotelVisitors()
        {
            string companyId = this.SiteUserInfo.CompanyID;//公司ID

            string chinaName = Utils.GetFormValue("ctl00$ContentPlaceHolder1$hvp_txtChinaName", 13);//中文名
            string englishName = Utils.GetFormValue("ctl00$ContentPlaceHolder1$hvp_txtEnglishName", 27);//英文名
            string cardNo = Utils.GetFormValue("ctl00$ContentPlaceHolder1$hvp_txtCardNo");//证件号码
            string cardType=string.Empty;
            if (Utils.GetFormValue("ctl00$ContentPlaceHolder1$hvp_ddlCardType").ToString()!="")
            {
                cardType = Utils.GetFormValue("ctl00$ContentPlaceHolder1$hvp_ddlCardType");//证件类型
            }
            string phone = Utils.GetFormValue("ctl00$ContentPlaceHolder1$hvp_txtMobilePhone");//联系电话
            string remark = Utils.GetFormValue("ctl00$ContentPlaceHolder1$hvp_txtReamrk", 450).Trim();//备注
            string sex = string.Empty;
            if (this.hvp_rdoFemale.Checked)//性别
                sex = this.hvp_rdoFemale.Value;
            if (this.hvp_rdoMale.Checked)
                sex = this.hvp_rdoMale.Value;
            string country = string.Empty;
            if (Utils.GetFormValue("ctl00$ContentPlaceHolder1$hvp_ddlCountry").ToString() != "")
            {
                country = Utils.GetFormValue("ctl00$ContentPlaceHolder1$hvp_ddlCountry").ToString();//国籍
            }
            string visitorType=string.Empty;
            if (Utils.GetFormValue("ctl00$ContentPlaceHolder1$hvp_ddlVisitorType").ToString()!="")
            {
                visitorType=Utils.GetFormValue("ctl00$ContentPlaceHolder1$hvp_ddlVisitorType").ToString();//旅客类型
            }
          
            bool result = true;
            //    //创建一个常旅客实体，并实例化该对象
            EyouSoft.Model.TicketStructure.TicketVistorInfo Model = new EyouSoft.Model.TicketStructure.TicketVistorInfo();

            Model.ChinaName = chinaName;  //中文名
            Model.EnglishName = englishName.ToUpper().ToString(); //英文名
            Model.ContactSex = (EyouSoft.Model.CompanyStructure.Sex)int.Parse(sex);//性别
            if (visitorType != "")
            {
                Model.VistorType = (EyouSoft.Model.TicketStructure.TicketVistorType)int.Parse(visitorType);//常旅客类型
            }
            Model.ContactTel = phone;//联系电话
            if (cardType != "")
            {
                Model.CardType = (EyouSoft.Model.TicketStructure.TicketCardType)int.Parse(cardType);//证件类型
            }
            Model.CardNo = cardNo.Trim();//证件号    
            if (country != "")
            {
                Model.NationInfo.NationId =int.Parse(country);   //所属国家   
            }
            Model.Remark = remark.Trim();//备注
            Model.IssueTime = DateTime.Now;//创建时间
            Model.CompanyId = companyId;//公司ID
            Model.DataType = EyouSoft.Model.TicketStructure.TicketDataType.酒店常旅客;
            EditId = this.hvp_hfEditId.Value.ToString();
            if(EditId!="")
                result = EyouSoft.BLL.TicketStructure.TicketVisitor.CreateInstance().HotelVistorIsExist(chinaName, englishName,phone, companyId,EditId);
            else
                result = EyouSoft.BLL.TicketStructure.TicketVisitor.CreateInstance().HotelVistorIsExist(chinaName, englishName, phone, companyId, null);
            if (result)
            {
                Response.Clear();
                Response.Write("-1");//已经存在
                Response.End();
            }
            else
            {//如果不存在，就执行修改/添加操作
                if (EditId != "")
                {
                    Model.Id = EditId;
                    if (EyouSoft.BLL.TicketStructure.TicketVisitor.CreateInstance().UpdateTicketVisitorInfo(Model))
                    {
                        Response.Clear();
                        Response.Write("1");//修改成功
                        Response.End();
                    }
                    else
                    {
                        Response.Clear();
                        Response.Write("0");//修改失败
                        Response.End();
                    }
                }
                else
                {
                    Model.Id = Guid.NewGuid().ToString();      
                        result=EyouSoft.BLL.TicketStructure.TicketVisitor.CreateInstance().AddTicketVisitorInfo(Model);
                        if (result)
                        {
                            Response.Clear();
                            Response.Write("2");//新增成功
                            Response.End();
                        }
                        else
                        {            
                            Response.Clear();
                            Response.Write("-2");
                            Response.End();
                        }
                  }
               }
        }
        #endregion
    }
}
