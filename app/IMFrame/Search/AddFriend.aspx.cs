using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using EyouSoft.Common;
using EyouSoft.Common.Function;
using System.Linq;

namespace IMFrame.Search
{
    /// <summary>
    /// 功能:添加好友的页面
    /// </summary>
    /// 编写人:zhangzy  时间:2009-9-10
    public partial class AddFriend : EyouSoft.ControlCommon.Control.MQPage
    {
        private int intPageSize = 8, CurrPageIndex = 1; //每页显示的记录数
        /// <summary>
        /// 已加为MQ好友的MQID集合
        /// </summary>
        private List<int> MyFriendIdList = new List<int>();
        /// <summary>
        /// 返回参数
        /// </summary>
        protected string urlPars;
        protected string TodayMaxFriendCout = EyouSoft.Common.ConfigModel.ConfigClass.GetConfigString("MQConfig", "TodayMaxFriendCout");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["action"]))
            {
                switch (Request["action"])
                {
                    case "CheckIsAdd":
                        IsAddFriend();
                        break;
                    case "SetCustomer":
                        SetCustomer();
                        break;
                    case "AddFriendCount":
                        AddFriendCount();
                        break;
                }
                return;
            }
            if (SiteUserInfo != null && SiteUserInfo.CompanyRole != null && SiteUserInfo.CompanyRole.RoleItems != null && SiteUserInfo.CompanyRole.RoleItems.Length > 0)
            {
                if (SiteUserInfo.CompanyRole.RoleItems.Contains(EyouSoft.Model.CompanyStructure.CompanyType.地接) && (!SiteUserInfo.CompanyRole.RoleItems.Contains(EyouSoft.Model.CompanyStructure.CompanyType.专线)) && (!SiteUserInfo.CompanyRole.RoleItems.Contains(EyouSoft.Model.CompanyStructure.CompanyType.组团)))
                {
                    foreach (ListItem item in ddlCompanyType.Items)
                    {
                        if (item.Value == "2")
                        {
                            item.Enabled = false;
                        }
                    }
                }
            }
            if (!IsPostBack)
            {
                MyFriendIdList = GetMyFriendIdList(MQLoginId);
                InitData();
            }
            this.ddlCompanyType.Attributes.Add("onchange", "ChangeType();");

        }

        /// <summary>
        /// 累加所添加的好友数量
        /// </summary>
        private void AddFriendCount()
        {
            int MQID = Utils.GetInt(Request["CurrUserMQId"]);
            if (MQID > 0)
            {
                EyouSoft.BLL.MQStructure.IMFriendList.CreateInstance().AddFriendCount(MQID);
            }
        }

        /// <summary>
        /// 是否允许添加MQ好友
        /// </summary>
        private void IsAddFriend()
        {
            bool isAdd = false;
            int MQID = Utils.GetInt(Request["CurrUserMQId"]);
            if (MQID > 0)
            {
                isAdd = EyouSoft.BLL.MQStructure.IMFriendList.CreateInstance().IsAddFriend(MQID);
            }

            Response.Write(isAdd.ToString());
            Response.Flush();
            Response.End();
        }

        #region 初始化数据

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitData()
        {
            int intRecordCount = 0;
            StringBuilder strUrlPara = new StringBuilder();
            EyouSoft.Model.MQStructure.MQFriendSearchType? MQFriendSearchType = null;
            //用户名
            string LookUserName = Utils.InputText(Server.UrlDecode(Request.QueryString["UserName"]));
            strUrlPara.AppendFormat("&UserName={0}", Server.UrlEncode(LookUserName));
            //公司名称
            string CompanyName = Utils.InputText(Server.UrlDecode(Request.QueryString["CompanyName"]));
            strUrlPara.AppendFormat("&CompanyName={0}", Server.UrlEncode(CompanyName));
            //联系人姓名
            string ContactName = Utils.InputText(Server.UrlDecode(Request.QueryString["ContactName"]));
            strUrlPara.AppendFormat("&ContactName={0}", Server.UrlEncode(ContactName));
            //城市名称
            string CityName = Utils.InputText(Request.QueryString["CityName"]);
            strUrlPara.AppendFormat("&CityName={0}", Server.UrlEncode(CityName));
            //查找公司类型：1，批发商  2，零售商  3:地接  4：其他  0：精确查找
            int TypeId = Utils.GetInt(Request.QueryString["TypeId"]);
            strUrlPara.AppendFormat("&TypeId={0}", TypeId);
            //省份ID
            int ProvinceId = Utils.GetInt(Request.QueryString["ProvinceId"]);
            strUrlPara.AppendFormat("&ProvinceId={0}", ProvinceId);
            int CityId = Utils.GetInt(Request.QueryString["CityId"]);
            strUrlPara.AppendFormat("&CityId={0}", CityId);
            //用户帐号
            int UserId = Utils.GetInt(Request.QueryString["UserId"]);
            strUrlPara.AppendFormat("&UserId={0}", UserId);
            CurrPageIndex = Utils.GetInt(Request.QueryString["Page"], 1);
            urlPars = "?IsBack=1" + strUrlPara.ToString();

            if (TypeId == 0 || TypeId == 1 || TypeId == 2 || TypeId == 3 || TypeId == 4)
                MQFriendSearchType = (EyouSoft.Model.MQStructure.MQFriendSearchType)TypeId;

            #region 查询条件

            if (!string.IsNullOrEmpty(CompanyName))
            {
                this.txtCompanyName.Value = CompanyName;
            }
            if (!string.IsNullOrEmpty(ContactName))
            {
                this.txtContactName.Value = ContactName;
            }
            if (this.ddlCompanyType.Items.FindByValue(TypeId.ToString()) != null)
            {
                this.ddlCompanyType.Items.FindByValue(TypeId.ToString()).Selected = true;
                MessageBox.ResponseScript(this, "ChangeType();");
            }
            if (!string.IsNullOrEmpty(CityName) || UserId > 0)
            {
                if (TypeId == 0)
                    this.txtCityName.Value = UserId <= 0 ? string.Empty : UserId.ToString();
                else
                    this.txtCityName.Value = CityName;
            }           
            #endregion

            //若分站/城市ID>0,则直接通过ID来查询,不通过分站/城市名称查询
            if (CityId > 0)
                CityName = "";

            //仅按省份城市查询时，若该城市下在线用户数少于8个，城市不做为查询条件
            if (ProvinceId > 0 && CityId > 0
               && string.IsNullOrEmpty(CompanyName)
               && string.IsNullOrEmpty(ContactName)
               && string.IsNullOrEmpty(LookUserName)
               && EyouSoft.BLL.MQStructure.IMMember.CreateInstance().GetCityOnlineNumber(ProvinceId, CityId) < 8)
            {
                CityId = 0;
            }

            IList<EyouSoft.Model.MQStructure.IMMemberAndUser> Friendedlist = new List<EyouSoft.Model.MQStructure.IMMemberAndUser>();
            if (SiteUserInfo != null && SiteUserInfo.CompanyRole != null && SiteUserInfo.CompanyRole.RoleItems != null && SiteUserInfo.CompanyRole.RoleItems.Length > 0)
                Friendedlist = EyouSoft.BLL.MQStructure.IMMember.CreateInstance().GetSearchFriend(intPageSize, CurrPageIndex
                    , ref intRecordCount, CompanyName, ContactName, LookUserName, ProvinceId, CityId, CityName, UserId, MQFriendSearchType,false
                    , SiteUserInfo.CompanyRole.RoleItems);
            else
                Friendedlist = EyouSoft.BLL.MQStructure.IMMember.CreateInstance().GetSearchFriend(intPageSize, CurrPageIndex, ref intRecordCount, CompanyName, ContactName, LookUserName, ProvinceId, CityId, CityName, UserId,  MQFriendSearchType, false);

            this.ExporPageInfoSelect1.IsInitJs = false;
            this.ExporPageInfoSelect1.IsInitBaseCssStyle = false;
            this.ExporPageInfoSelect1.intPageSize = intPageSize;
            this.ExporPageInfoSelect1.intRecordCount = intRecordCount;
            this.ExporPageInfoSelect1.CurrencyPage = CurrPageIndex;
            this.ExporPageInfoSelect1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"] + "?";
            this.ExporPageInfoSelect1.UrlParams = Request.QueryString;

            if (intRecordCount > 0)
            {
                this.NoData.Visible = false;
                this.Repeater1.DataSource = Friendedlist;
                this.Repeater1.DataBind();
            }
            else
            {
                this.NoData.Visible = true;
            }
            Friendedlist = null;
        }

        #endregion

        #region 获的己添加好友的ID字符串列表

        /// <summary>
        /// 获的己添加好友的ID字符串列表(前后不包括逗号)
        /// </summary>
        /// <param name="Im_Uid">当前用户的用户id</param>
        private List<int> GetMyFriendIdList(int Im_Uid)
        {
            List<int> IdList = null;
            IdList = EyouSoft.BLL.MQStructure.IMFriendList.CreateInstance().GetMyFriendIdList(Im_Uid);
            return IdList;
        }

        #endregion

        #region 获得每一行的用户信息

        /// <summary>
        /// 获得每一行的用户信息
        /// </summary>
        /// <param name="drv">数据实体</param>
        /// <param name="myFriendIdList">已加好友的列表</param>
        /// <param name="isCheckdCompany">当前公司有无审核通过</param>
        /// <param name="CompanyTypeId">公司类型 1：专线  2：组团  3：地接</param>
        /// <returns></returns>
        private string GetListTrValue(EyouSoft.Model.MQStructure.IMMemberAndUser drv, List<int> myFriendIdList)
        {
            StringBuilder strVal = new StringBuilder();
            if (drv == null)
                return strVal.ToString();

            string im_uid = drv.MQId.ToString();
            string contactName = string.IsNullOrEmpty(drv.ContactName) ? string.Empty : drv.ContactName.ToString().Trim();

            string im_displayname = string.IsNullOrEmpty(drv.MQDisplayName) ? string.Empty : drv.MQDisplayName.ToString().Trim();
            string userName = string.IsNullOrEmpty(drv.MQUserName) ? string.Empty : drv.MQUserName.ToString().Trim();
            string im_status = drv.MQStatus.ToString();
            string CompanyName = string.IsNullOrEmpty(drv.CompanyName) ? string.Empty : drv.CompanyName.ToString().Trim();
            string CityName = string.IsNullOrEmpty(drv.CityName) ? string.Empty : drv.CityName.ToString().Trim();
            string CompanyId = string.IsNullOrEmpty(drv.CompanyId) ? string.Empty : drv.CompanyId.ToString().Trim();

            //显示的用户名称
            string showName = contactName;
            if (contactName == "")
            {
                if (im_displayname != "")
                    showName = im_displayname;
                else
                    showName = userName;
            }
            //用户名,联系人,昵称
            string userNameContactName = string.Format("用户名:{0},联系人:{1},昵称:{2}", userName, contactName, im_displayname);

            string icoSrc = ImageServerUrl + "/IM/images/icounline.gif";   //头像src地址,默认不在线的

            if (Int32.Parse(im_status) > 11)   //表示用户不在线  >11在线  否则不在线
            {
                icoSrc = ImageServerUrl + "/IM/images/icoonline.gif";
            }

            #region 初始化tr

            string isLookUserInfo = string.Format(" href='/Card/LookUserInfo.aspx?im_username={0}'", im_uid);
            if (!IsCompanyCheck)
            {
                isLookUserInfo = "";
            }

            strVal.Append("<tr>");
            strVal.AppendFormat("<td align='left' bgcolor='#fbfbfb'><img src='{0}' title='{2}' width='14' height='17' /><span title='{2}'><a target=\"_blank\" {3}>{1}</a></span></td>", icoSrc, Utils.GetText(showName, 8), userNameContactName, isLookUserInfo);

            //公司名称
            strVal.AppendFormat("<td align='left' bgcolor='#fbfbfb'><span title='{1}'><a target=\"_blank\" {2}>{0}</a></span></td>", Utils.GetText(CompanyName, 17), CompanyName, isLookUserInfo);

            //城市
            strVal.AppendFormat("<td align='left' bgcolor='#fbfbfb'><span title='{1}'>{0}</span></td>", Utils.GetText(CityName, 6), CityName);
            string isCheckOnclick = string.Empty;
            if (IsCompanyCheck && SiteUserInfo != null && SiteUserInfo.CompanyRole != null && SiteUserInfo.CompanyRole.RoleItems != null && SiteUserInfo.CompanyRole.RoleItems.Length > 0)
            {
                if ((SiteUserInfo.CompanyRole.RoleItems.Contains(EyouSoft.Model.CompanyStructure.CompanyType.专线) || SiteUserInfo.CompanyRole.RoleItems.Contains(EyouSoft.Model.CompanyStructure.CompanyType.组团) || SiteUserInfo.CompanyRole.RoleItems.Contains(EyouSoft.Model.CompanyStructure.CompanyType.地接)) && drv.CompanyType == EyouSoft.Model.CompanyStructure.BusinessProperties.旅游社)
                {
                    isCheckOnclick = string.Format("onclick='CheckFormAdd(\"{0}\",\"{1}\",\"{2}\")'", im_uid, CompanyId, SiteUserInfo.ContactInfo.MQ);
                }
                else
                {
                    isCheckOnclick = string.Format("onclick='CheckFormAdd(\"{0}\",\"{1}\",\"{2}\")'", im_uid, string.Empty, SiteUserInfo.ContactInfo.MQ);
                }
            }

            if (myFriendIdList != null)
            {
                if (myFriendIdList.Contains(Convert.ToInt32(im_uid)))
                {
                    strVal.AppendFormat("<td align='left' bgcolor='#fbfbfb'><img style='cursor:pointer' src='{0}/IM/images/addedFriend.gif' /></td>", ImageServerUrl);
                }
                else
                {
                    strVal.AppendFormat("<td align='left' bgcolor='#fbfbfb'><img style='cursor:pointer' {0}  src='{1}/IM/images/addFriend.gif' /></td>", isCheckOnclick, ImageServerUrl);
                }
            }
            else
            {
                strVal.AppendFormat("<td align='left' bgcolor='#fbfbfb'><img style='cursor:pointer' {0} src='{1}/IM/images/addFriend.gif' /></td>", isCheckOnclick, ImageServerUrl);
            }

            strVal.Append("</tr>");

            #endregion


            return strVal.ToString();
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Literal LiteralTr = (Literal)e.Item.FindControl("LiteralTr");
                if (LiteralTr != null)
                    LiteralTr.Text = GetListTrValue((EyouSoft.Model.MQStructure.IMMemberAndUser)e.Item.DataItem, MyFriendIdList);
            }
        }

        #endregion

        #region 查询

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string TypeId = Utils.GetFormValue("ddlCompanyType");
            string CompanyName = Utils.InputText(txtCompanyName.Value);
            string ContactName = Utils.InputText(txtContactName.Value);
            string CityName = Utils.InputText(txtCityName.Value);
            string strPars = "?IsQuery=1";
            int ProvinceId = Utils.GetInt(Request.QueryString["ProvinceId"]);
            int CityId = Utils.GetInt(Request.QueryString["CityId"]);

            if (!string.IsNullOrEmpty(CompanyName))
            {
                strPars += "&CompanyName=" + Server.UrlEncode(CompanyName.Trim());
            }
            if (!string.IsNullOrEmpty(ContactName))
            {
                strPars += "&ContactName=" + Server.UrlEncode(ContactName.Trim());
            }
            if (!string.IsNullOrEmpty(CityName))
            {
                if (TypeId != "0")
                    strPars += "&CityName=" + Server.UrlEncode(CityName.Trim());
                else  //精确查找
                    strPars += "&UserId=" + Server.UrlEncode(CityName.Trim());
            }

            strPars += "&TypeId=" + TypeId;
            Response.Redirect(Request.ServerVariables["SCRIPT_NAME"] + strPars);
        }

        #endregion

        #region 添加客户

        /// <summary>
        /// 加好友时，设置我的客户
        /// </summary>
        private void SetCustomer()
        {
            if (SiteUserInfo != null && !string.IsNullOrEmpty(SiteUserInfo.ID))
            {
                string companyId = Utils.InputText(Request.QueryString["CompanyId"]);
                if (EyouSoft.BLL.CompanyStructure.MyCustomer.CreateInstance().SetMyCustomer(SiteUserInfo.ID, companyId))
                {
                    //得到需要发送的公司的信息
                    EyouSoft.Model.CompanyStructure.CompanyDetailInfo model = 
                        EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(companyId);
                    //得到需要发送的公司的管理员用户信息
                    EyouSoft.Model.CompanyStructure.CompanyUser companyUser =
                        EyouSoft.BLL.CompanyStructure.CompanyUser.CreateInstance().GetAdminModel(companyId);

                    //添加短信提示功能   add by dyz 2010-10-25
                    bool isSend = Utils.SendSMSForReminderAddFriend(model.ContactInfo.MQ, model.ContactInfo.Mobile, SiteUserInfo.CompanyName, SiteUserInfo.ContactInfo.ContactName, model.CityId);

                    if (isSend)
                    {
                        //发送短信记录
                        EyouSoft.Model.ToolStructure.MsgTipRecord tipModel = new EyouSoft.Model.ToolStructure.MsgTipRecord();
                        tipModel.Email = string.Empty;
                        tipModel.FromMQID = SiteUserInfo.ContactInfo.MQ;
                        tipModel.ToMQID = model.ContactInfo.MQ;//接收方MQ
                        tipModel.Mobile = model.ContactInfo.Mobile;//接收方手机
                        tipModel.MsgType = EyouSoft.Model.ToolStructure.MsgType.AddFriend;
                        tipModel.SendWay = EyouSoft.Model.ToolStructure.MsgSendWay.SMS;
                        EyouSoft.BLL.ToolStructure.MsgTipRecord msgTipBll = new EyouSoft.BLL.ToolStructure.MsgTipRecord();
                        msgTipBll.Add(tipModel);
                    }

                    //添加邮件提示功能   add by dyz 2010-10-26
                    EyouSoft.Common.Email.ReminderEmailHelper.SendAddFriendEmail(
                        model.AdminAccount.UserName,
                        model.ContactInfo.Email,
                        companyUser != null ? companyUser.PassWordInfo.NoEncryptPassword : "",
                        SiteUserInfo.ContactInfo.ContactName);
                }
            }
        }

        #endregion
    }
}
