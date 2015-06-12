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
using System.Text;

namespace SiteOperationsCenter.PlatformManagement
{
    /// <summary>
    /// 页面功能：平台管理——战略合作伙伴的新增和修改
    /// 开发人：杜桂云      开发时间：2010-06-28
    /// </summary>
    public partial class OperatorPartners : EyouSoft.Common.Control.YunYingPage
    {
        #region 成员变量
        protected string img_Path = string.Empty;
        protected int EditeID = 0;
        #endregion

        #region 页面加载
        protected void Page_Load(object sender, EventArgs e)
        {
            //权限判断
            YuYingPermission[] parms = { YuYingPermission.平台管理_管理该栏目, YuYingPermission.平台管理_战略合作伙伴};
            if (!CheckMasterGrant(parms))
            {
                Utils.ResponseNoPermit(YuYingPermission.平台管理_战略合作伙伴, true);
                return;
            }
            if (!string.IsNullOrEmpty(Utils.InputText(Context.Request.QueryString["EditID"])))
            {
                //修改操作
                EditeID =Utils.GetInt(Context.Request.QueryString["EditID"]);
                //初始化要修改的数据
                if (!IsPostBack)
                {
                    EyouSoft.Model.SystemStructure.SysFriendLink LinkModel = EyouSoft.BLL.SystemStructure.SysFriendLink.CreateInstance().GetSysFriendLinkModel(EditeID);
                    if (LinkModel != null)
                    {
                        this.txtWordRemark.Value = LinkModel.LinkName;
                        this.txtAddress.Value = LinkModel.LinkAddress;
                        if (!string.IsNullOrEmpty(LinkModel.ImgPath))
                        {
                            img_Path = string.Format("<a href=\"{0}\"target='_blank'  title=\"点击查看\">{1}</a>", Domain.FileSystem + LinkModel.ImgPath, LinkModel.ImgPath.Substring(LinkModel.ImgPath.LastIndexOf('/') + 1));
                            hdfAgoImgPath.Value = LinkModel.ImgPath;
                        }
                    }
                    //释放资源
                    LinkModel = null;
                }
            }
            
        }
        #endregion

        #region 保存数据
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            #region 获取表单信息
            string picUrl = "";
            string strErr = "";
            if (!string.IsNullOrEmpty(Utils.GetFormValue("SingleFileUpload1$hidFileName"))) 
            {
                picUrl = Utils.GetFormValue("SingleFileUpload1$hidFileName");
            }
            else
            {
                picUrl= Utils.GetFormValue(hdfAgoImgPath.UniqueID);
            }
            string WordsRemark =Utils.GetFormValue("txtWordRemark",30);
            string PicAddress = Utils.GetFormValue("txtAddress", 245);

            //必填验证
            if (picUrl == "")
            {
                strErr += "请选择要上传的图片！\\n";
            }
            if (PicAddress == "")
            {
                strErr += "链接地址不能为空！\\n";
            }
            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }
            #endregion

            //实例化bll
            EyouSoft.Model.SystemStructure.SysFriendLink linkModel = new EyouSoft.Model.SystemStructure.SysFriendLink();
            linkModel.LinkName = WordsRemark;
            linkModel.LinkAddress = PicAddress;
            linkModel.IssueTime = DateTime.Now;
            linkModel.ImgPath = picUrl;
            linkModel.LinkType = EyouSoft.Model.SystemStructure.FriendLinkType.战略合作;
            if (EditeID != 0)
            {
                linkModel.ID = EditeID;
                EyouSoft.BLL.SystemStructure.SysFriendLink.CreateInstance().UpdateSysFriendLink(linkModel);
                MessageBox.ShowAndRedirect(this, "修改成功", "PartnersInfo.aspx");
            }
            else
            {
                EyouSoft.BLL.SystemStructure.SysFriendLink.CreateInstance().AddSysFriendLink(linkModel);
                MessageBox.ShowAndRedirect(this, "新增成功", "PartnersInfo.aspx");
            }
            //释放资源
            linkModel = null;
        }
        #endregion
    }
}
