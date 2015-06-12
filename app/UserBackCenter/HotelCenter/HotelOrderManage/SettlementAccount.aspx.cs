using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace UserBackCenter.HotelCenter.HotelOrderManage
{
    /// <summary>
    /// 运算账户设置 页面
    /// create:lixh
    /// datetime:2010-12-07
    /// </summary>
    public partial class SettlementAccount : EyouSoft.Common.Control.BackPage
    {
        protected string CompanyID = "0"; //当前登录公司的编号      

        #region 页面加载        
        protected void Page_Load(object sender, EventArgs e)
        {
            CompanyID = this.SiteUserInfo.CompanyID;

            if (Utils.GetInt(Request.QueryString["issave"]) == 1)
            {
                this.DataSave();
            }
            else
            {
                //页面初始化
                if (!this.Page.IsPostBack)
                {
                    //权限判断
                    if (this.SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.其他采购商) && this.SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.组团))
                    {
                        Response.Clear();
                        Response.Write("对不起，您没有权限");
                        Response.End();
                        return;
                    }
                    this.InitHotelAccountType();
                    this.InitHotelSettlementType();
                    if (EyouSoft.BLL.HotelStructure.HotelAccount.CreateInstance().GetModel(CompanyID) != null)
                        this.InitHotelAccount();
                }
            }
        }
        #endregion

        #region  结算账户类型       
        //绑定结算账户类型
        private void InitHotelAccountType()
        {
            //清空结算账户类型下拉列表
            this.sla_clearanceaccount.Items.Clear();
            // 结算账户类型 0：银行卡 1：中间账户财付通 enum
            this.sla_clearanceaccount.DataSource = EnumObj.GetList(typeof(EyouSoft.Model.HotelStructure.HotelAccountType));
            this.sla_clearanceaccount.DataTextField = "Text";
            this.sla_clearanceaccount.DataValueField = "Value";
            this.sla_clearanceaccount.DataBind();
        }
        #endregion

        #region 结算账户方式        
        //绑定结算账户方式
        private void InitHotelSettlementType()
        {
            //结算方式 0:一月一结 1:离店后5日结算    
            bool obj = false;
            foreach (string item in Enum.GetNames((typeof(EyouSoft.Model.HotelStructure.HotelSettlement))))
            {
                if (!obj)
                {
                    this.sla_balancetype1.Text = "<label for=\"sla_clearancetype\" style=\"cursor:pointer;\">" + item + "</label>"; //页面上标签显示方式 取不到枚举里面的值 点击文字 redio按钮选择
                    this.sla_balancetype2.Text = "<label for=\"sla_clearancetype1\" style=\"cursor: pointer;\">" + item + "</label>";
                    obj = true;
                }
                else
                    this.sla_balancetype3.Text = "<label for=\"sla_clearancetype2\" style=\"cursor:pointer;\">" + item + "</label>";
            }
        }
        #endregion

        #region 当前公司的结算账户信息       
        /// <summary>
        /// 获取当前公司的结算账户信息
        /// </summary>
        private void InitHotelAccount()
        {
            EyouSoft.BLL.HotelStructure.HotelAccount Bll_Account = new EyouSoft.BLL.HotelStructure.HotelAccount();  //实例化结算账户信息业务逻辑类
            EyouSoft.Model.HotelStructure.HotelAccount Model_Account = new EyouSoft.Model.HotelStructure.HotelAccount();  //实例化结算账户信息实体类
            Model_Account = Bll_Account.GetModel(CompanyID);  //当前公司的编号
            this.div_Img.Attributes.Add("style","display:none"); //隐藏提交按钮
            if (Convert.ToInt32(Model_Account.AccountType) == 0) //银行卡结算信息
            {                
                this.sla_IsShowDDL.Attributes.Add("style","display:none");  //隐藏结算账户下拉框
                this.sla_Lblclearanceaccount.Text = Model_Account.AccountType.ToString(); //取结算账户类型
                this.sla_spclearancetype.Attributes.Add("style","display:none");        //隐藏结算方式文本框
                this.lab_Sla_clearancetype.Text = Model_Account.Settlement.ToString();  //取结算方式
                this.sla_accountname.Attributes.Add("style", "display:none");        //隐藏开户姓名文本框
                this.lab_sla_accountname.Attributes.Add("style", "display:block");   //显示开户姓名lable标签
                this.lab_sla_accountname.Text = Model_Account.AccountName;          //取开户姓名             
                this.sla_cardnumber.Attributes.Add("style", "display:none");        //隐藏银行卡号文本框
                this.lab_sla_cardnumber.Attributes.Add("style", "display:block");   //显示银行卡号lable标签
                this.lab_sla_cardnumber.Text=Model_Account.BankNo;                  //银行卡号 
                this.sla_subbranch.Attributes.Add("style", "display:none");         //隐藏开户行以及支行文本框
                this.lab_sla_subbranch.Attributes.Add("style", "display:block");    //显示开户行以及支行lable标签
                this.lab_sla_subbranch.Text=Model_Account.BankName;                //开户行以及支行
                this.spn_SlaMailInvoice.Attributes.Add("style","display:none"); //隐藏邮寄发票单选按钮
                this.spn_SlaMailInvoice.Attributes.Add("style","display:none");
                if (Convert.ToInt32(Model_Account.IsMailInvoice) == 0)  //转换邮寄发票
                    this.lbl_SlaMailInvoice.Text = "否";
                else
                    this.lbl_SlaMailInvoice.Text = "是";
            }
            else  //财付通结算信息
            {
                this.div_1.Attributes.Add("style","display:none");  //隐藏银行卡结算信息注册表单
                this.div_2.Attributes.Add("style", "display:block"); //显示财付通结算信息
                this.sla_IsShowDDL.Attributes.Add("style","display:none"); //隐藏结算类型下拉列表框
                this.sla_Lblclearanceaccount.Text = Model_Account.AccountType.ToString();  //取结算账户类型
                this.sla_tenpaycard.Attributes.Add("style","display:none");  //隐藏财付通账号文本框
                this.lab_sla_tenpaycard.Attributes.Add("style","display:block");  //显示财付通账号lable标签
                this.lab_sla_tenpaycard.Text = Model_Account.BankNo;  //取财付通账号
                this.span_sla_clearancetype1.Attributes.Add("style","display:none");   //隐藏结算方式单选按钮
                if (Convert.ToInt32(Model_Account.Settlement) == 0) //转换结算方式
                    this.lbl_sla_clearancetype1.Text = "一月一结";
                else
                    this.lbl_sla_clearancetype1.Text = "离店后5日结算";

                this.span_slaMailInvoice1.Attributes.Add("style", "display:none");  //隐藏邮寄发票单选按钮
                if (Convert.ToInt32(Model_Account.IsMailInvoice) == 0)  //转换邮寄发票
                    this.lbl_slaMailInvoice1.Text = "否";
                else
                    this.lbl_slaMailInvoice1.Text = "是";
                
            }
            
            //释放资源
            Model_Account = null;
            Bll_Account = null;
        }
        #endregion

        #region 保存结算账户信息
        /// <summary>
        /// 保存数据
        /// </summary>
        private void DataSave()
        {
            bool Result = false; //验证是否保存成功
            EyouSoft.Model.HotelStructure.HotelAccount Model_HotelAccount = new EyouSoft.Model.HotelStructure.HotelAccount(); //结算账户实体类
            EyouSoft.BLL.HotelStructure.HotelAccount Bll_HotelAccount = new EyouSoft.BLL.HotelStructure.HotelAccount(); //结算账户业务逻辑类
            Model_HotelAccount.CompanyId = CompanyID; //当前登录公司的编号
            if (Utils.GetInt(Utils.GetFormValue(sla_clearanceaccount.UniqueID.Trim()))==0)  //银行卡结账
            {
                Model_HotelAccount.AccountType = (EyouSoft.Model.HotelStructure.HotelAccountType)Utils.GetInt(Utils.GetFormValue(sla_clearanceaccount.UniqueID.Trim())); //结算账户类型值
                Model_HotelAccount.AccountName = Utils.GetString(Utils.GetFormValue(sla_accountname.UniqueID.Trim()), ""); //开户姓名
                Model_HotelAccount.BankName = Utils.GetString(Utils.GetFormValue(sla_subbranch.UniqueID.Trim()), ""); //开户行以及支行
                Model_HotelAccount.BankNo = Utils.GetString(Utils.GetFormValue(sla_cardnumber.UniqueID.Trim()),""); //银行卡号
                Model_HotelAccount.Settlement = (EyouSoft.Model.HotelStructure.HotelSettlement)Utils.GetInt(Utils.GetFormValue("sla_clearancetype"));  //结算方式 
                if (Utils.GetInt(Utils.GetFormValue("MailInvoice")) == 1)  //邮寄发票
                    Model_HotelAccount.IsMailInvoice = true;
                else
                    Model_HotelAccount.IsMailInvoice = false;             //不邮寄发票
            }
            else  //中间账户财付通结账
            {
                Model_HotelAccount.AccountType = (EyouSoft.Model.HotelStructure.HotelAccountType)Utils.GetInt(Utils.GetFormValue(sla_clearanceaccount.UniqueID.Trim())); //结算账户类型值
                Model_HotelAccount.BankNo = Utils.GetString(Utils.GetFormValue(sla_tenpaycard.UniqueID.Trim()), ""); //财付通账号
                Model_HotelAccount.Settlement = (EyouSoft.Model.HotelStructure.HotelSettlement)Utils.GetInt(Utils.GetFormValue("sla_clearancetype1")); //离店后5日结算  一月一结 
                if (Utils.GetInt(Utils.GetFormValue("MailInvoice1")) == 1)   //邮寄发票
                    Model_HotelAccount.IsMailInvoice = true;
                else
                    Model_HotelAccount.IsMailInvoice = false;                //不邮寄发票
            }            

            Model_HotelAccount.IssueTime = DateTime.Now; //添加时间

            Result = Bll_HotelAccount.Add(Model_HotelAccount);  //添加方法
            if (Result)
                SetErrorMsg(true, "保存成功!");
            else
                SetErrorMsg(false, "保存失败!");
                        
            //释放资源
            Model_HotelAccount = null;
            Bll_HotelAccount = null;
        }
        #endregion

        #region 提示操作信息方法
        /// <summary>
        /// 提示提交信息
        /// </summary>
        /// <param name="IsSuccess">true 执行成功 flase 执行失败</param>
        /// <param name="Msg">提示信息</param>
        private void SetErrorMsg(bool isSuccess, string msg)
        {
            var s = "{{isSuccess:{0},errMsg:'{1}'}}";
            Response.Clear();
            Response.Write(string.Format(s, isSuccess.ToString().ToLower(), msg));
            Response.End();
        }
        #endregion
    }
}
