using System;
using System.Collections;
using System.Collections.Generic;
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
using EyouSoft.Common;
namespace UserBackCenter.SystemSet
{
    /// <summary>
    /// 页面功能：个人信息设置
    /// 开发人：xuty 开发时间：2010-7-6
    /// 修改人：徐从栎
    /// 修改时间：2011-12-19
    /// </summary>
    public partial class PersonInfoSet : BackPage
    {
        protected string userNo;//用户账号
        protected int itemIndex = 1;
        protected EyouSoft.IBLL.CompanyStructure.ICompanyUser userBll;
        protected EyouSoft.Model.CompanyStructure.CompanyUser userModel;
        protected int longCount;
        protected int shortCount;
        protected int exitCount;
        protected bool haveArea = true;
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!CheckGrant(TravelPermission.系统设置_管理首页))
            //{
            //    Utils.ResponseNoPermit();
            //    return;
            //}
            if (!IsPostBack)
            {
                this.InitData();
            }
            userBll = EyouSoft.BLL.CompanyStructure.CompanyUser.CreateInstance();
            userModel = userBll.GetModel(SiteUserInfo.ID);
            string method = Utils.GetFormValue("method");
            if (method == "save")
            {
                UpdatePersonInfo();//更新个人信息
                return;
            }
            LoadPersonInfo();//初始化个人信息
        }
        protected void InitData()
        {
            //EyouSoft.BLL.CompanyStructure.CompanyDepartment departBLL = new EyouSoft.BLL.CompanyStructure.CompanyDepartment();
            //IList<EyouSoft.Model.CompanyStructure.CompanyDepartment> lst = departBLL.GetList(this.SiteUserInfo.CompanyID);
            //if (null != lst && lst.Count > 0)
            //{
            //    this.ddlDepart.DataSource = lst;
            //    this.ddlDepart.DataTextField = "DepartName";
            //    this.ddlDepart.DataValueField = "ID";
            //    this.ddlDepart.DataBind();
            //}
            //this.ddlDepart.Items.Insert(0, new ListItem("--请选择--", "-1"));
        }
        #region 修改个人信息
        protected void UpdatePersonInfo()
        {
            if (!IsCompanyCheck)//是否审核
            {
                Utils.ResponseMeg(false, "对不起,你尚未审核通过!");
                return;
            }
            string userName = Utils.GetFormValue(pis_txtUserName.UniqueID, 20);//姓名
            string userEmail = Utils.GetFormValue(pis_txtEmail.UniqueID, 50);//邮箱
            if (userEmail == "" || userName == "")
            {
                Utils.ResponseMegNoComplete();
            }
            else
            {
                userModel.ContactInfo.ContactName = userName;
                if (this.pis_rdiSex.Enabled)
                {
                    userModel.ContactInfo.ContactSex = (EyouSoft.Model.CompanyStructure.Sex)Utils.GetInt(this.pis_rdiSex.SelectedValue, 2);
                }
                userModel.ContactInfo.Mobile = Utils.GetFormValue(pis_txtMobile.UniqueID, 50);
                userModel.ContactInfo.Fax = Utils.GetFormValue(pis_txtFax.UniqueID, 50);
                userModel.ContactInfo.Tel = Utils.GetFormValue(pis_txtTel.UniqueID, 20);
                userModel.ContactInfo.Email = userEmail;

                userModel.ContactInfo.QQ = Utils.GetFormValue(pis_txtQQ.UniqueID, 20);
                userModel.ContactInfo.MSN = Utils.GetFormValue(pis_txtMSN.UniqueID, 50);

                userModel.MqNickName = Utils.GetFormValue(this.txtMQNickName.UniqueID, 20);//MQ昵称
                userModel.Job = Utils.GetFormValue(this.txtDuty.UniqueID, 10);//公司职务
                //部门
                //string partID=Utils.GetFormValue(this.ddlDepart.UniqueID);
                //if(!string.IsNullOrEmpty(partID))
                //{
                //    userModel.DepartId =partID;
                //    userModel.DepartName = this.ddlDepart.Text;
                //}

                if (userBll.IsExistsEmail(userEmail, SiteUserInfo.ID))
                {
                    Utils.ResponseMeg(false, "该邮箱已经存在!");
                    return;
                }
                if (userBll.UpdatePersonal(userModel))
                {
                    Utils.ResponseMeg(true, "修改成功!");
                }
                else
                {
                    Utils.ResponseMegError();
                }
            }
        }
        #endregion

        #region 初始化个人信息
        protected void LoadPersonInfo()
        {
            haveArea = SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.专线);
            userNo = SiteUserInfo.UserName;//用户账号
            pis_txtUserName.Value = userModel.ContactInfo.ContactName;//姓名
            if (userModel.ContactInfo.ContactSex == EyouSoft.Model.CompanyStructure.Sex.未知)
            {
                pis_rdiSex.SelectedValue = "0";
            }
            else
            {
                pis_rdiSex.SelectedValue = Convert.ToInt32(userModel.ContactInfo.ContactSex).ToString();
            }
            pis_txtMobile.Value = userModel.ContactInfo.Mobile;
            pis_txtEmail.Value = userModel.ContactInfo.Email;
            pis_txtFax.Value = userModel.ContactInfo.Fax;
            pis_txtMSN.Value = userModel.ContactInfo.MSN;
            pis_txtQQ.Value = userModel.ContactInfo.QQ;
            pis_txtTel.Value = userModel.ContactInfo.Tel;

            this.pis_rdiSex.Enabled = true;
            this.spanMQName.InnerText = userModel.ContactInfo.MQ.Trim();//MQ用户名
            this.txtMQNickName.Value = userModel.MqNickName;//MQ昵称
            this.txtDuty.Value = userModel.Job;//公司职务
            //部门
            //this.ddlDepart.SelectedValue = userModel.DepartId;
            this.spanDepart.InnerText = string.IsNullOrEmpty(userModel.DepartName) ? "暂无部门" : userModel.DepartName;
            this.spanRegTime.InnerText = string.Format("{0:yyyy-MM-dd}", userModel.JoinTime);//注册时间
            //最近登录时间
            this.spanLoginTime.InnerText = string.Format("{0:yyyy-MM-dd}",
                                                         userModel.LastLoginTime.HasValue
                                                             ? userModel.LastLoginTime.Value
                                                             : DateTime.Now);

            this.spanLoginCount.InnerText = userModel.LoginCount.ToString();//最近登录次数
            //this.spanHandTime.InnerText = userModel.;//操作时间


            //加载经营范围
            if (haveArea)
            {
                IList<EyouSoft.Model.SystemStructure.AreaBase> areaList = EyouSoft.BLL.CompanyStructure.CompanyArea.CreateInstance().GetCompanyArea(SiteUserInfo.CompanyID);
                if (areaList != null && areaList.Count > 0)
                {
                    IList<EyouSoft.Model.SystemStructure.AreaBase> longAreaList = areaList.Where(i => i.RouteType == EyouSoft.Model.SystemStructure.AreaType.国内长线).ToList<EyouSoft.Model.SystemStructure.AreaBase>();
                    longCount = longAreaList.Count;
                    pis_rpt_LongAreaList.DataSource = longAreaList;
                    pis_rpt_LongAreaList.DataBind();
                    IList<EyouSoft.Model.SystemStructure.AreaBase> shortAreaList = areaList.Where(i => i.RouteType == EyouSoft.Model.SystemStructure.AreaType.国内短线).ToList<EyouSoft.Model.SystemStructure.AreaBase>();
                    shortCount = shortAreaList.Count;
                    pis_rpt_ShortAreaList.DataSource = shortAreaList;
                    pis_rpt_ShortAreaList.DataBind();
                    IList<EyouSoft.Model.SystemStructure.AreaBase> exitAreaList = areaList.Where(i => i.RouteType == EyouSoft.Model.SystemStructure.AreaType.国际线).ToList<EyouSoft.Model.SystemStructure.AreaBase>();
                    exitCount = exitAreaList.Count;
                    pis_rpt_ExitAreaList.DataSource = exitAreaList;
                    pis_rpt_ExitAreaList.DataBind();
                }
            }
        }
        #endregion

        #region 绑定经营范围项操作
        protected string GetItem()
        {
            string str = "";
            if (itemIndex % 4 == 0)
            {
                str = "</tr><tr>";
            }
            itemIndex++;
            return str;
        }
        #endregion

        #region 绑定项时判断是否经营该专线
        protected string GetChecked(int areaId)
        {
            string checkedHTML = "";
            if (userModel.Area != null)
            {
                if (userModel.Area.Where(i => i.AreaId == areaId).Count() > 0)
                {
                    checkedHTML = "checked='checked'";
                }
            }
            return checkedHTML;
        }
        #endregion


    }
}
