using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using System.Text;
using EyouSoft.Common;
using EyouSoft.Common.Function;
using System.Linq;
using EyouSoft.Model.NewsStructure;

namespace IMFrame.ExchangeTopic
{
    /// <summary>
    /// IM端显示同业互动相关话题信息
    /// </summary>
    public partial class Default : EyouSoft.ControlCommon.Control.MQPage
    {
        protected string strAllClass = "";
        protected bool isTrue = false;
        protected string strUrl = "";
        protected string strCompanyTypeId = "1";
        protected void Page_Load(object sender, EventArgs e)
        {
            a_GL.Visible = CheckGrant(TravelPermission.营销工具_供求信息);
            a_GL.HRef = GetDesPlatformUrl(Domain.UserBackCenter + "/supplyinformation/allsupplymanage.aspx");

            InitRpInfo();
            InitDateControl();
            InitEachangTyps();
            InitExchangTags();
            InitProvinces();
        }

        #region [暂时关闭不使用的方法]

        #region 绑定话题类型
        /// <summary>
        /// 绑定话题类型
        /// </summary>
        protected void BindAllTopicClass()
        {
            int index = 0;
            foreach (string str in Enum.GetNames(typeof(EyouSoft.Model.CommunityStructure.ExchangeType)))
            {
                index++;
                strAllClass += string.Format("<li class=\"topnav2\" id='{0}' {2}><a href=\"javascript:void(0);\" onclick=\"ChangeCss({0});GetTopicList(1);return false;\"><span >{1}</span></a></li>", (int)Enum.Parse(typeof(EyouSoft.Model.CommunityStructure.ExchangeType), str), str,
                    index > 3 ? "style=\"display:none;\"" : string.Empty);
            }
        }
        #endregion

        #region 初始化搜索块
        /// <summary>
        /// 初始化搜索块
        /// </summary>
        public void InitSearchControl()
        {
            #region 初始化时间块
            StringBuilder StrControl = new StringBuilder();
            StrControl.Append("时间：");
            foreach (string str in Enum.GetNames(typeof(EyouSoft.Model.CommunityStructure.SearchDateType)))
            {
                int strValue = (int)Enum.Parse(typeof(EyouSoft.Model.CommunityStructure.SearchDateType), str);
                StrControl.AppendFormat("<a href=\"javascript:;\" style=\"{0}padding:3px;\" value=\"{1}\" state=\"{2}\">{3}</a>|",
                    strValue == 0 ? "background:#CCCCCC;" : "", strValue, strValue == 0 ? 1 : 0, str);
            }
            ltDateTypes.Text = StrControl.ToString().TrimEnd('|');
            StrControl = null;
            #endregion

            #region 初始化标签块
            StrControl = new StringBuilder();
            StrControl.Append("标签：");
            StrControl.AppendFormat("<a href=\"javascript:;\" style=\"{0}padding:3px;\" value=\"{1}\" state=\"{3}\">{2}</a>|", "background:#CCCCCC;",
                -1, "全部", 1);
            foreach (string str in Enum.GetNames(typeof(EyouSoft.Model.CommunityStructure.ExchangeTag)))
            {
                int strValue = (int)Enum.Parse(typeof(EyouSoft.Model.CommunityStructure.ExchangeTag), str);
                StrControl.AppendFormat("<a href=\"javascript:;\" style=\"{0}padding:3px;\" value=\"{1}\" state=\"{3}\">{2}</a>|",
                   "", strValue, str, 0);
            }
            ltTags.Text = StrControl.ToString().TrimEnd('|');
            StrControl = null;
            #endregion

            #region 初始化省份块
            //StrControl = new StringBuilder();
            //StrControl.AppendFormat("<a href=\"javascript:;\" style=\"{0}padding:3px;\" value=\"{1}\" state=\"{3}\">{2}</a>|","background:#CCCCCC;",
            //    0, "全部", 1);
            //foreach (EyouSoft.Model.SystemStructure.SysProvince model in EyouSoft.BLL.SystemStructure.SysProvince.CreateInstance().GetEnabledList())
            //{
            //    StrControl.AppendFormat("<a href=\"javascript:;\" style=\"{0}padding:3px;\" value=\"{1}\" state=\"{3}\"><nobr>{2}</nobr></a>|",
            //       string.Empty, model.ProvinceId, model.ProvinceName,0);
            //}
            //ltProvinces.Text = StrControl.ToString().TrimEnd('|');
            //StrControl = null;
            #endregion
        }
        #endregion

        #endregion

        #region 初始化行业资讯
        /// <summary>
        /// 初始化行业资讯
        /// </summary>
        private void InitRpInfo()
        {
            var list = EyouSoft.BLL.NewsStructure.BPeerNews.CreateInstance().GetGetPeerNewsList(5,
                                                                                                   new MQueryPeerNews
                                                                                                       {
                                                                                                           IsShowHideNew
                                                                                                               = false,
                                                                                                           OrderIndex =
                                                                                                               4
                                                                                                       });
            if (list != null && list.Count > 0)
            {
                RpInfos.DataSource = list;
                RpInfos.DataBind();
            }
            list = null;
        }
        #endregion

        #region 初始化省份列表
        /// <summary>
        /// 初始化省份列表
        /// </summary>
        private void InitProvinces()
        {
            //初始化发布到的省份
            IList<EyouSoft.Model.SystemStructure.SysProvince> list = EyouSoft.BLL.SystemStructure.SysProvince.CreateInstance().GetEnabledList();
            rptProvince.DataSource = list;
            rptProvince.DataBind();
            if (list != null) list.Clear();
            list = null;
        }
        #endregion

        #region 初始化供求类别
        private void InitEachangTyps()
        {
            StringBuilder StrControl = new StringBuilder();
            StrControl.Append("类别：");
            StrControl.AppendFormat("<a href=\"javascript:;\" style=\"{0}padding:3px;\" value=\"{1}\" state=\"{3}\" alink=\"{4}\">{2}</a>|", "",
                -1, "全部", 0, GetDesPlatformUrl(EyouSoft.Common.Domain.UserPublicCenter + "/SupplierInfo/ExchangeList.aspx?type=-1"));
            ddlTypes.Items.Clear();
            foreach (string str in Enum.GetNames(typeof(EyouSoft.Model.CommunityStructure.ExchangeType)))
            {
                int strValue = (int)Enum.Parse(typeof(EyouSoft.Model.CommunityStructure.ExchangeType), str);
                StrControl.AppendFormat("<a href=\"javascript:;\" style=\"{0}padding:3px;\" value=\"{1}\" state=\"{3}\" alink=\"{4}\">{2}</a>|",
                   "", strValue, str, 0, GetDesPlatformUrl(EyouSoft.Common.Domain.UserPublicCenter + "/SupplierInfo/ExchangeList.aspx?stype=" + strValue.ToString()));
                ddlTypes.Items.Add(new ListItem(str, strValue.ToString()));
            }
            ddlTypes.Items.Insert(0, new ListItem("请选择类别", "-1"));
            ltTypes.Text = StrControl.ToString().TrimEnd('|');
            StrControl = null;
        }
        #endregion

        #region 初始化供求标签
        private void InitExchangTags()
        {
            StringBuilder StrControl = new StringBuilder();
            StrControl.Append("标签：");
            StrControl.AppendFormat("<a searchattr=\"tag\" href=\"javascript:;\" style=\"{0}padding:3px;\" value=\"{1}\" state=\"{3}\" alink=\"{4}\">{2}</a>|", "",
                -1, "全部", 0, GetDesPlatformUrl(EyouSoft.Common.Domain.UserPublicCenter + "/SupplierInfo/ExchangeList.aspx?stag=-1"));
            ddlTags.Items.Clear();
            foreach (string str in Enum.GetNames(typeof(EyouSoft.Model.CommunityStructure.ExchangeTag)))
            {
                int strValue = (int)Enum.Parse(typeof(EyouSoft.Model.CommunityStructure.ExchangeTag), str);
                StrControl.AppendFormat("<a searchattr=\"tag\" href=\"javascript:;\" style=\"{0}padding:3px;\" value=\"{1}\" state=\"{3}\" alink=\"{4}\">{2}</a>|",
                   "", strValue, str, 0, GetDesPlatformUrl(EyouSoft.Common.Domain.UserPublicCenter + "/SupplierInfo/ExchangeList.aspx?stag=" + strValue.ToString()));
                ddlTags.Items.Add(new ListItem(str, strValue.ToString()));
            }
            ddlTags.Items.Insert(0, new ListItem("请选择标签", "-1"));
            ltTags.Text = StrControl.ToString().TrimEnd('|');
            StrControl = null;
        }
        #endregion

        #region 初始化时间块
        private void InitDateControl()
        {
            StringBuilder StrControl = new StringBuilder();
            StrControl.Append("时间：");
            foreach (string str in Enum.GetNames(typeof(EyouSoft.Model.CommunityStructure.SearchDateType)))
            {
                int strValue = (int)Enum.Parse(typeof(EyouSoft.Model.CommunityStructure.SearchDateType), str);
                StrControl.AppendFormat("<a searchattr=\"time\" href=\"javascript:;\" style=\"{0}padding:3px;\" value=\"{1}\" state=\"{2}\" alink=\"{4}\">{3}</a>|",
                    "", strValue, 0, str, GetDesPlatformUrl(EyouSoft.Common.Domain.UserPublicCenter + "/SupplierInfo/ExchangeList.aspx?stime=" + strValue.ToString()));
            }
            ltDateTypes.Text = StrControl.ToString().TrimEnd('|');
            StrControl = null;
        }
        #endregion


        protected void btnSave_Click(object sender, EventArgs e)
        {
            #region 数据验证
            StringBuilder StrErr = new StringBuilder();
            if (Utils.GetFormValue(ddlTypes.UniqueID) == "-1")
            {
                StrErr.Append("请选择类别\n");
            }
            if (Utils.GetFormValue(ddlTags.UniqueID) == "-1")
            {
                StrErr.Append("请选择标签\n");
            }
            if (txtContent.Text.Trim().Length == 0)
            {
                StrErr.Append("请输入供求内容");
            }
            if (StrErr.Length > 0)
            {
                MessageBox.Show(this.Page, StrErr.ToString());
                return;
            }
            StrErr = null;
            #endregion

            EyouSoft.Model.CommunityStructure.ExchangeList model = new EyouSoft.Model.CommunityStructure.ExchangeList();
            model.AttatchPath = string.Empty;
            model.CityId = SiteUserInfo.CityId;
            model.CompanyId = SiteUserInfo.CompanyID;
            model.CompanyName = SiteUserInfo.CompanyName;
            model.ContactName = SiteUserInfo.ContactInfo.ContactName;
            model.ContactTel = SiteUserInfo.ContactInfo.Tel;
            model.ExchangeTag = (EyouSoft.Model.CommunityStructure.ExchangeTag)int.Parse(Utils.GetFormValue(ddlTags.UniqueID));
            model.ExchangeText = Utils.GetText(EyouSoft.Common.Utils.InputText(txtContent.Text), 250);
            model.ExchangeTitle = Utils.GetText(model.ExchangeText, 26);
            model.ID = Guid.NewGuid().ToString();
            model.IssueTime = DateTime.Now;
            model.OperatorId = SiteUserInfo.ID;
            model.OperatorMQ = SiteUserInfo.ContactInfo.MQ;
            model.OperatorName = SiteUserInfo.ContactInfo.ContactName;
            model.ProvinceId = SiteUserInfo.ProvinceId;
            model.TopicClassID = (EyouSoft.Model.CommunityStructure.ExchangeType)int.Parse(Utils.GetFormValue(ddlTypes.UniqueID));
            model.IsCheck = IsCompanyCheck;
            model.ExchangeCategory = Utils.GetFormValue("dllCategory") == "1" ? EyouSoft.Model.CommunityStructure.ExchangeCategory.供 : EyouSoft.Model.CommunityStructure.ExchangeCategory.求;
            IList<int> ProvinceIds = null;
            string[] strProvinceIds = Utils.GetFormValues("ckbProvince");
            if (strProvinceIds != null && strProvinceIds.Length > 0)
            {
                ProvinceIds = new List<int>();
                for (int i = 0; i < strProvinceIds.Length; i++)
                {
                    if (!string.IsNullOrEmpty(strProvinceIds[i]) && StringValidate.IsInteger(strProvinceIds[i]))
                        ProvinceIds.Add(int.Parse(strProvinceIds[i]));
                }
            }

            bool Result = EyouSoft.BLL.CommunityStructure.ExchangeList.CreateInstance().AddExchangeList(model, ProvinceIds == null ? null : ProvinceIds.ToArray());
            model = null;
            if (Result)
            {
                MessageBox.ShowAndRedirect(this.Page, "供求发布成功！", Request.RawUrl);
            }
            else
            {
                MessageBox.ShowAndRedirect(this.Page, "供求发布失败！", Request.RawUrl);
            }
        }

    }
}
