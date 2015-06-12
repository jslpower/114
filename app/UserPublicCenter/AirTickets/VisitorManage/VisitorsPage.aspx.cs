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
using EyouSoft.Common.Function;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace UserPublicCenter.AirTickets.VisitorManage
{
    /// <summary>
    /// 页面功能：机票常旅客——机票常旅客添加/修改页
    /// 开发人：刘咏梅     
    /// 开发时间：2010-10-19
    /// </summary>
    public partial class VisitorsPage : EyouSoft.Common.Control.FrontPage
    {
        #region 成员变量
        protected string EditId=string.Empty;
        protected string CompanyId = string.Empty; //公司id
        #endregion

        #region 页面加载
        protected void Page_Load(object sender, EventArgs e)
        {
            CompanyId = this.SiteUserInfo.CompanyID;          
            this.ImgSave.ImageUrl = ImageManage.GetImagerServerUrl(1)+"/images/jipiao/bc_btn.jpg";
           // 初始化旅客类型
            BindVisitorType(); 
            //绑定证件类型
            BindCardType();
            //初始化国家
            InitCountryList();
           
            if (!string.IsNullOrEmpty(Utils.InputText(Context.Request.QueryString["EditId"])))
            {
                EditId = Utils.GetString(Context.Request.QueryString["EditId"],"");
            }
                if (!Page.IsPostBack)
                {
                    //根据旅客ID获得常旅客实体对象 
                    EyouSoft.Model.TicketStructure.TicketVistorInfo TicketVisitorInfo =EyouSoft.BLL.TicketStructure.TicketVisitor.CreateInstance().GetTicketVisitorInfo(EditId.ToString());
                   
                    this.Master.Naviagtion = AirTicketNavigation.机票常旅客管理;
                    this.Title = "常旅客添加_机票";

                    #region 初始化表单数据
                    if (TicketVisitorInfo != null)
                    {
                        this.ddlCardType.SelectedValue = ((int)TicketVisitorInfo.CardType).ToString();
                        this.ddlVisitoryType.SelectedValue = ((int)TicketVisitorInfo.VistorType).ToString();
                        this.ddlCountry.SelectedValue = ((int)TicketVisitorInfo.NationInfo.NationId).ToString();
                        this.txtCardNo.Value = TicketVisitorInfo.CardNo.Trim();
                        this.txtCname.Value = TicketVisitorInfo.ChinaName.Trim();
                        this.txtEname.Value = TicketVisitorInfo.EnglishName.Trim();
                        this.txtPhone.Value = TicketVisitorInfo.ContactTel.Trim();
                        this.txtRemark.Value = TicketVisitorInfo.Remark.Trim();
                        EyouSoft.Model.CompanyStructure.Sex Gender = TicketVisitorInfo.ContactSex;
                        if (Gender == EyouSoft.Model.CompanyStructure.Sex.男)
                            this.gender_male.Checked = true;
                        else
                            this.gender_female.Checked = true;
                    }
                    //释放资源
                    TicketVisitorInfo = null;                 
                    #endregion                
            }
        }      
        #endregion

        #region 绑定旅客类型
        private void BindVisitorType()
        {
            this.ddlVisitoryType.Items.Add(new ListItem("请选择", ""));
            string[] typeList = Enum.GetNames(typeof(EyouSoft.Model.TicketStructure.TicketVistorType));
            if (typeList != null && typeList.Length > 0)
            {
                foreach (string str in typeList)
                {
                    this.ddlVisitoryType.Items.Add(new ListItem(str, ((int)Enum.Parse(typeof(EyouSoft.Model.TicketStructure.TicketVistorType), str)).ToString()));
                }
            }
            //释放资源
            typeList = null;
        }
        #endregion

        #region 绑定证件类型
        private void BindCardType()
        {
            this.ddlCardType.Items.Clear();
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
                    this.ddlCardType.Items.Add(list_CardType);
                }
            }
            //释放资源
            typeList = null;
            //this.ddlCardType.Items.Add(new ListItem("请选择", ""));
            //string[] typeList = Enum.GetNames(typeof(EyouSoft.Model.TicketStructure.TicketCardType));
            //if (typeList != null && typeList.Length > 0)
            //{
            //    foreach (string str in typeList)
            //    {
            //        this.ddlCardType.Items.Add(new ListItem(str, ((int)Enum.Parse(typeof(EyouSoft.Model.TicketStructure.TicketCardType), str)).ToString()));
            //    }
            //}
            ////释放资源
            //typeList = null;
        }
        #endregion

        #region 初始化国家列表
        private void InitCountryList()
        {
           
            this.ddlCountry.Items.Add(new ListItem("--请选择--", ""));
            string countryInfo = this.ddlCountry.SelectedItem.Value;
            IList<EyouSoft.Model.TicketStructure.TicketNationInfo> list = EyouSoft.BLL.TicketStructure.TicketNationInfo.CreateInstance().GetList(countryInfo);
            for (int i = 0; i < list.Count;i++ )
            {
                ListItem liCountry = new ListItem();
                liCountry.Text =list[i].CountryCode+"--"+list[i].CountryName;
                liCountry.Value = list[i].NationId.ToString();
                this.ddlCountry.Items.Add(liCountry);
                
            }            
                this.ddlCountry.DataBind();  
        }       
        #endregion

        #region 常旅客保存
        protected void ImgSave_Click(object sender, ImageClickEventArgs e)
        {
            string strError=string.Empty;
            string sex=string.Empty;
            string companyId=this.SiteUserInfo.CompanyID;//公司ID
            string cName = Utils.GetFormValue("ctl00$c1$txtCname",13);//中文名
            string eName = Utils.GetFormValue("ctl00$c1$txtEname",27);//英文名
            string cardNo = Utils.GetFormValue("ctl00$c1$txtCardNo");//证件号码
            int cardType = int.Parse(Utils.GetFormValue("ctl00$c1$ddlCardType"));//证件类型
            string phone = Utils.GetFormValue("ctl00$c1$txtPhone");//联系电话
            string remark =Utils.GetFormValue("ctl00$c1$txtRemark",450).Trim();//备注
            if (this.gender_female.Checked)//性别
                sex = this.gender_female.Value;
            if (this.gender_male.Checked)
                sex = this.gender_male.Value;
            int country =int.Parse(Utils.GetFormValue("ctl00$c1$ddlCountry").ToString());//国籍
            int visitorType = int.Parse(Utils.GetFormValue("ctl00$c1$ddlVisitoryType").ToString());//旅客类型
            if (cName.Trim() == "" && eName.Trim()=="")
            {                
                strError += "常旅客中文名和英文名不能都为空!\\n";
            }
            if(cName.Trim()!=""&&eName.Trim()!="")
            {
                strError += "常旅客中文名和英文名只能填一个!\\n";
            }
            if (cName.Trim() != "")
            {
                bool count=Regex.IsMatch(cName,@"(^[\u0391-\uFFE5]+$)");
                if (!count)
                    strError += "请输入正确的中文名!\\n";
                else
                {
                    if (cName.Length > 13)
                    {
                        strError += "中文名的长度不能超过13位!\\n";
                    }
                }
            }
            if (eName != "")
            {
                bool count = Regex.IsMatch(eName, @"^[a-zA-Z]+\/[a-zA-Z]+$");
                if (!count)
                    strError += "请输入正确的英文名!\\n";    
                else
                {
                    if(eName.Length>27)
                        strError += "英文名的长度不能超过27位!\\n"; 
                }
            }
            if(visitorType.ToString()=="")
            {
                strError += "请选择旅客类型!\\n";
            }            
            if (sex == "")
            {
                strError += "请选择性别!\\n";
            }
            if (cardType.ToString() == "")
            {
                strError += "证件类型不能为空!\\n";
            }
            if (cardNo == "")
            {
                strError += "证件号码不能为空!\\n";
            }  
            if(cardType.ToString()=="0")//假如是身份证
            {
                bool count = Regex.IsMatch(cardNo, @"(^\d{15}$)|(^\d{17}[0-9Xx]$)");
                if (!count)
                    strError += "请输入正确的身份证号码!\\n";
            }
       
            if(phone=="")
            {
                strError += "联系电话不能为空!\\n";
            }
            if(phone!="")
            {
                bool count = Regex.IsMatch(phone, @"(^((\(\d{2,3}\))|(\d{3}\-))?(\(0\d{2,3}\)|0\d{2,3}-?)?[1-9]\d{6,7}(\-\d{1,4})?$)");
                if (!count)
                    strError += "请输入正确的联系电话!\\n";
            }
            if (country.ToString() == "")
            {
                strError += "请选择国家!\\n";
            }
            if (remark == "")
            {
                strError += "备注不能为空!\\n";
            }
            if (remark.Length > 500)
            {
                strError += "备注长度过长!\\n";
            }
            if(strError != "")
            {
                MessageBox.Show(this, strError);
                return;
            }
            //创建一个常旅客实体，并实例化该对象
            EyouSoft.Model.TicketStructure.TicketVistorInfo Model = new EyouSoft.Model.TicketStructure.TicketVistorInfo();
            
            Model.ChinaName = cName.Trim();//中文名
            Model.EnglishName = eName.Trim().ToUpper();//英文名
            Model.ContactSex = (EyouSoft.Model.CompanyStructure.Sex)int.Parse(sex);//性别
            Model.VistorType = (EyouSoft.Model.TicketStructure.TicketVistorType)visitorType;//常旅客类型
            Model.ContactTel = phone;//联系电话
            Model.CardType = (EyouSoft.Model.TicketStructure.TicketCardType)cardType;//证件类型
            Model.CardNo = cardNo.Trim();//证件号           
            Model.NationInfo.NationId = country;   //所属国家     
            Model.Remark = remark.Trim();//备注
            Model.IssueTime = DateTime.Now;//创建时间
            Model.CompanyId = this.SiteUserInfo.CompanyID;//公司ID
          
            bool result = true;
            if (EditId != "")
            {
                result = EyouSoft.BLL.TicketStructure.TicketVisitor.CreateInstance().VisitorIsExists(cardNo,companyId, EditId);
            }
            else
            {
                result = EyouSoft.BLL.TicketStructure.TicketVisitor.CreateInstance().VisitorIsExists(cardNo,companyId,null);
            }
            if (result)
            {
                MessageBox.ShowAndRedirect(this, "该常旅客已存在！", "VisitorsPage.aspx");
            }
            else
            {//如果不存在，就执行修改/添加操作
                if (EditId != "")
                {
                    Model.Id = EditId;
                    if (EyouSoft.BLL.TicketStructure.TicketVisitor.CreateInstance().UpdateTicketVisitorInfo(Model))
                        MessageBox.ShowAndRedirect(this, "修改成功", "VisitorList.aspx");
                    else
                        MessageBox.ShowAndRedirect(this, "修改失败", "VisitorsPage.aspx");
                }
                else
                {
                    Model.Id = Guid.NewGuid().ToString();
                    if (EyouSoft.BLL.TicketStructure.TicketVisitor.CreateInstance().AddTicketVisitorInfo(Model))
                        MessageBox.ShowAndRedirect(this, "新增成功", "VisitorList.aspx");
                    else
                        MessageBox.ShowAndRedirect(this, "新增失败", "VisitorsPage.aspx");
                }
            }
        }
        #endregion
    }
}
