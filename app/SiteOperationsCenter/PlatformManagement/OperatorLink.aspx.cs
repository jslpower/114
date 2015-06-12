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
using System.Collections.Generic;
using EyouSoft.Common.Function;
using EyouSoft.Common;
using System.Text;

namespace SiteOperationsCenter.PlatformManagement
{
    /// <summary>
    /// 页面功能：平台管理——友情链接
    /// 开发人：杜桂云      开发时间：2010-06-24
    /// </summary>
    public partial class OperatorLink : EyouSoft.Common.Control.YunYingPage
    {
        #region 成员变量
        protected int EditeID = 0;

        /// <summary>
        /// 友情链接类型
        /// </summary>
        protected int LinkType
        {
            get
            {
                return Utils.GetInt(ViewState["LinkType"].ToString(), 1);
            }
            set
            {
                ViewState["LinkType"] = value;
            }
        }
        #endregion

        #region 页面加载
        protected void Page_Load(object sender, EventArgs e)
        {
            //权限判断
            YuYingPermission[] parms = { YuYingPermission.平台管理_管理该栏目, YuYingPermission.平台管理_友情链接 };
            if (!CheckMasterGrant(parms))
            {
                Utils.ResponseNoPermit(YuYingPermission.平台管理_友情链接, true);
                return;
            }

            if (Context.Request.QueryString["linkType"] != null)
                LinkType = Utils.GetInt(Context.Request.QueryString["linkType"]);

            if (!string.IsNullOrEmpty(Utils.InputText(Context.Request.QueryString["EditID"])))
            {
                //修改操作
                EditeID = Utils.GetInt(Context.Request.QueryString["EditID"]);
                if (!IsPostBack)
                {
                    //初始化要修改的数据
                    EyouSoft.Model.SystemStructure.SysFriendLink LinkModel = EyouSoft.BLL.SystemStructure.SysFriendLink.CreateInstance().GetSysFriendLinkModel(EditeID);
                    if (LinkModel != null)
                    {
                        this.txtLinkWords.Value = LinkModel.LinkName;
                        this.txtLinkAddress.Value = LinkModel.LinkAddress;
                        LinkType = (int)LinkModel.LinkType;
                    }
                    //释放资源
                    LinkModel = null;
                }
            }
        }
        #endregion

        #region 保存
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            string strErr = "";
            //获取表单数据
            string LinkWords = Utils.GetFormValue("txtLinkWords", 30);
            string LinkAddress = Utils.GetFormValue("txtLinkAddress", 245);
            //必填验证
            if (LinkWords == "")
            {
                strErr += "链接文字不能为空！\\n";
            }
            if (LinkAddress == "")
            {
                strErr += "链接地址不能为空！\\n";
            }
            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }
            EyouSoft.Model.SystemStructure.SysFriendLink linkModel = new EyouSoft.Model.SystemStructure.SysFriendLink();
            linkModel.LinkName = LinkWords;
            linkModel.LinkAddress = LinkAddress;
            linkModel.LinkType = (EyouSoft.Model.SystemStructure.FriendLinkType)Enum.Parse(typeof(EyouSoft.Model.SystemStructure.FriendLinkType), LinkType.ToString());
            linkModel.IssueTime = DateTime.Now;
            if (EditeID != 0)
            {
                linkModel.ID = EditeID;
                if (linkModel.LinkType == (EyouSoft.Model.SystemStructure.FriendLinkType)Enum.Parse(typeof(EyouSoft.Model.SystemStructure.FriendLinkType), LinkType.ToString()))
                {
                    linkModel.ImgPath = string.Empty;
                }
                EyouSoft.BLL.SystemStructure.SysFriendLink.CreateInstance().UpdateSysFriendLink(linkModel);
                MessageBox.ShowAndRedirect(this, "修改成功", string.Format("LinksInfo.aspx?linkType={0}", LinkType));
            }
            else
            {
                EyouSoft.BLL.SystemStructure.SysFriendLink.CreateInstance().AddSysFriendLink(linkModel);
                MessageBox.ShowAndRedirect(this, "新增成功", string.Format("LinksInfo.aspx?linkType={0}", LinkType));
            }
            //释放资源
            linkModel = null;
        }
        #endregion
    }
}
