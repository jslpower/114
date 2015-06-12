using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Function;

namespace SiteOperationsCenter.AirTicktManage
{
    /// <summary>
    /// 页面功能：运营后台---机票管理---特价、免票、K位管理新增修改页
    /// BuildDate:2011-05-18
    /// </summary>
    /// Author:liuym
    public partial class AirTicketItemInfo : EyouSoft.Common.Control.YunYingPage
    {
        #region 成员变量
        protected string EditId = string.Empty;

        #endregion

        #region 页面加载
        protected void Page_Load(object sender, EventArgs e)
        {
          
            if (!string.IsNullOrEmpty(Request.QueryString["EditId"]))
            {
                EditId = Request.QueryString["EditId"];
            }
            if (!IsPostBack)
            {
                if (!CheckMasterGrant(YuYingPermission.机票首页管理_特价机票管理))
                {
                    Utils.ResponseNoPermit(YuYingPermission.机票首页管理_特价机票管理, true);
                    return;
                }
                BindTypeList();
                if (!string.IsNullOrEmpty(EditId))
                {
                    EyouSoft.Model.TicketStructure.SpecialFares Model = EyouSoft.BLL.TicketStructure.SpecialFares.CreateInstance().GetSpecialFare(int.Parse(EditId));
                    //调用底层方法
                    int type = (int)Model.SpecialFaresType;
                    rdoTypeList.SelectedValue = type.ToString();
                    bool isGoSanke = Model.IsJump;
                    if (isGoSanke)
                        ckGoSan.Checked = true;
                    else
                        ckGoSan.Checked = false;

                    fckContent.Value = Model.ContentText;
                    txtContactName.Value = Model.Contact;
                    txtContactPhone.Value = Model.ContactWay;
                    txtQQ.Value = Model.QQ;
                    txtTitle.Value = Model.Title;
                    Model = null;
                }
            }
        }
        #endregion

        #region 保存
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //类别Id
            string typeId = this.rdoTypeList.SelectedValue;
            //是否跳转散客平台
            bool IsGoSanKe = false;
            if (this.ckGoSan.Checked)
                IsGoSanKe = true;
            else
                IsGoSanKe = false;
            //正文
            string content = fckContent.Value;
            //标题
            string title = Utils.GetText(txtTitle.Value.Trim(), 100);
            //联系人
            string contactName = Utils.GetText(txtContactName.Value.Trim(), 100);
            //联系电话
            string contactPhone = Utils.GetText(txtContactPhone.Value.Trim(), 255);
            //联系人qq
            string contactQQ = Utils.GetText(txtQQ.Value.Trim(), 100);

            EyouSoft.Model.TicketStructure.SpecialFares Model = new EyouSoft.Model.TicketStructure.SpecialFares();
            Model.QQ = contactQQ;
            Model.IsJump = IsGoSanKe;
            Model.ContactWay = contactPhone;
            Model.ContentText = content;
            Model.AddTime = DateTime.Now;
            Model.Contact = contactName;
            Model.Title = title;
            Model.SpecialFaresType = (EyouSoft.Model.TicketStructure.SpecialFaresType)int.Parse(typeId);
            bool result = false;
            if (!string.IsNullOrEmpty(EditId))
            {
                Model.ID = int.Parse(EditId);
                result = EyouSoft.BLL.TicketStructure.SpecialFares.CreateInstance().ModifySpecialFares(Model);
            }
            else
                result = EyouSoft.BLL.TicketStructure.SpecialFares.CreateInstance().AddSpecialFares(Model);

            #region 输出提醒信息
            if (result)
                MessageBox.ShowAndRedirect(this, Model.ID>0? "修改成功！" : "添加成功！",
                    "/AirTicktManage/AirTicketItemList.aspx");
            else
                MessageBox.ShowAndRedirect(this, Model.ID > 0 ? "修改失败！" : "添加失败！",
                    "/AirTicktManage/AirTicketItemList.aspx");
            #endregion
            Model = null;
        }
        #endregion

        #region 绑定类别
        private void BindTypeList()
        {
            string[] typeList = Enum.GetNames(typeof(EyouSoft.Model.TicketStructure.SpecialFaresType));
            if (typeList != null && typeList.Length > 0)
            {
                foreach (string str in typeList)
                    this.rdoTypeList.Items.Add(new ListItem(str, ((int)Enum.Parse(typeof(EyouSoft.Model.TicketStructure.SpecialFaresType), str)).ToString()));
            }
            typeList = null;
        }
        #endregion

    }
}
