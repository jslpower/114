using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using System.Net;
using EyouSoft.Model.ScenicStructure;
using EyouSoft.Model.NewsStructure;
using EyouSoft.BLL.NewsStructure;
using EyouSoft.Model.CompanyStructure;
using EyouSoft.Model.SystemStructure;

namespace SiteOperationsCenter.NewsCenterControl
{
    /// <summary>
    /// 编辑资讯信息
    /// 创建时间：2011-12-15 方琪
    /// </summary>
    public partial class EditInformation : EyouSoft.Common.Control.YunYingPage
    {
        protected string EditId = string.Empty;
        protected string returnUrl = string.Empty;
        protected int intPage = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            EditId = Utils.GetQueryStringValue("EditId");
            returnUrl = Utils.GetQueryStringValue("returnUrl");
            intPage = Utils.GetInt(Utils.GetQueryStringValue("Page"), 1);
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(EditId))
                {
                    initPage();
                }
            }
        }

        protected void Bind()
        {
            //B2B显示绑定
            this.DdlB2B.DataSource = EnumObj.GetList(typeof(NewsB2BDisplay));
            this.DdlB2B.DataTextField = "Text";
            this.DdlB2B.DataValueField = "Value";
            this.DdlB2B.DataBind();

            //类别绑定
            this.DdlType.DataSource = EnumObj.GetList(typeof(PeerNewType));
            this.DdlType.DataTextField = "Text";
            this.DdlType.DataValueField = "Value";
            this.DdlType.DataBind();
        }

        protected void initPage()
        {
            Bind();

            EyouSoft.Model.NewsStructure.MPeerNews PeerNewsModel =
                EyouSoft.BLL.NewsStructure.BPeerNews.CreateInstance().GetPeerNews(EditId);
            //标题
            this.Title.Value = PeerNewsModel.Title;
            
            CompanyInfo companyModel = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(PeerNewsModel.CompanyId);
            if(companyModel.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.专线)|| companyModel.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.地接))
            {
                //相关专线
                this.ltname.Text="相关区域";
                this.RelevantLine.Text = PeerNewsModel.AreaName;
            }
            if(companyModel.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.景区))
            {
                this.ltname.Text="相关景区";
                this.RelevantLine.Text =EyouSoft.BLL.ScenicStructure.BScenicArea.CreateInstance().GetModel(PeerNewsModel.ScenicId).ScenicName;
            }
            //发布公司名称
            this.CompanyName.Text = PeerNewsModel.CompanyName;
            //发布个人帐号
            this.PersonAcount.Text = PeerNewsModel.OperatorName;
            //分类
            this.DdlType.SelectedValue = ((int)PeerNewsModel.TypeId).ToString();
            //资讯内容
            this.txtDescription.Text = PeerNewsModel.Content;
            //B2B显示控制
            this.DdlB2B.SelectedValue = ((int)PeerNewsModel.B2BDisplay).ToString();
            //B2B排序
            this.txt_B2BOrder.Value = PeerNewsModel.SortId.ToString() == "0" ? "50" : PeerNewsModel.SortId.ToString();
            //点击量
            this.ClickMount.Text = PeerNewsModel.ClickNum.ToString();
            //发布时间
            this.Time.Text = PeerNewsModel.IssueTime.ToString("f");
            //发布IP
            this.IP.Text = PeerNewsModel.Ip.ToString();

            if (PeerNewsModel.AttachInfo != null && PeerNewsModel.AttachInfo.Count > 0)
            {
                foreach (var item in PeerNewsModel.AttachInfo)
                {
                    if (item.Type == AttachInfoType.图片)
                    {
                        this.LabImgupLoad.Text = string.Format("<a href='{0}' target='_blank'>查看</a>", Utils.GetNewImgUrl(item.Path, 3));
                    }
                    if (item.Type == AttachInfoType.文件)
                    {
                        this.LabDocUpload.Text = string.Format("<a href='{0}' target='_blank'>查看</a>", Domain.FileSystem + item.Path);
                    }
                }
            }
            //this.Time.Text = DateTime.Now.ToString("f");
            //this.IP.Text = Dns.GetHostAddresses(Dns.GetHostName())[0].ToString();
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            MPeerNews PeerNewsModel = BPeerNews.CreateInstance().GetPeerNews(EditId);
            PeerNewsModel.Content = Utils.GetFormValue(txtDescription.UniqueID);
            PeerNewsModel.Title = Utils.GetFormValue(this.Title.UniqueID);
            PeerNewsModel.AttachInfo = GetAttachInfo(PeerNewsModel.AttachInfo);
            PeerNewsModel.B2BDisplay =
                (NewsB2BDisplay)Utils.GetInt(Utils.GetFormValue(this.DdlB2B.UniqueID));
            PeerNewsModel.TypeId =
                (PeerNewType)Utils.GetInt(Utils.GetFormValue(this.DdlType.UniqueID));
            PeerNewsModel.SortId = Utils.GetInt(Utils.GetFormValue(this.txt_B2BOrder.UniqueID));
            int result = EyouSoft.BLL.NewsStructure.BPeerNews.CreateInstance().ManageUpdatePeerNews(PeerNewsModel);
            if (result == 1)
            {
                if (EyouSoft.Common.Utils.GetQueryStringValue("type") == "list")// 弹窗的话就hide()
                {
                    Response.Write("<script language='javascript'>alert('修改成功!');parent.Boxy.getIframeDialog('" + Request.QueryString["iframeid"] + "').hide();parent.Informationindustry.GetInformationList()</script>");
                }
                else
                {
                    //这里跳转回去就翻页翻不动了。。
                    EyouSoft.Common.Function.MessageBox.ShowAndRedirect(this.Page, "修改成功!", returnUrl);
                }
            }
            else
            {
                EyouSoft.Common.Function.MessageBox.Show(this.Page, "修改失败!");
            }
            PeerNewsModel = null;
        }
        #region 获取资讯附件信息集合
        /// <summary>
        /// 获取资讯附件信息集合
        /// </summary>
        /// <param name="listInfo"></param>
        /// <returns></returns>
        protected IList<MPeerNewsAttachInfo> GetAttachInfo(IList<MPeerNewsAttachInfo> listInfo)
        {
            IList<MPeerNewsAttachInfo> list = new List<MPeerNewsAttachInfo>();
            MPeerNewsAttachInfo AttachInfoModel = new MPeerNewsAttachInfo();
            string FilePath = Utils.GetFormValue("ImgupLoad$hidFileName");
            if (FilePath != "")
            {

                AttachInfoModel.FileName = "";
                AttachInfoModel.Id = Guid.NewGuid().ToString();
                AttachInfoModel.Path = FilePath;
                AttachInfoModel.Remark = "图片";
                AttachInfoModel.Type = AttachInfoType.图片;
                list.Add(AttachInfoModel);
            }
            else
            {
                if (listInfo.Where(Item => (Item.Type == AttachInfoType.图片)).Count() > 0)
                {
                    list.Add(listInfo.First(Item => (Item.Type == AttachInfoType.图片)));
                }

            }
            FilePath = Utils.GetFormValue("DocUpLoad$hidFileName");
            if (FilePath != "")
            {
                string[] Path = FilePath.Split('|');
                AttachInfoModel.FileName = Path[0];
                AttachInfoModel.Id = Guid.NewGuid().ToString();
                AttachInfoModel.Path = Path[1];
                AttachInfoModel.Remark = "文件";
                AttachInfoModel.Type = AttachInfoType.文件;
                list.Add(AttachInfoModel);
            }
            else
            {
                if (listInfo.Where(Item => (Item.Type == AttachInfoType.文件)).Count() > 0)
                {
                    list.Add(listInfo.First(Item => (Item.Type == AttachInfoType.文件)));
                }
            }
            return list;
        }
        #endregion


        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClose_Click(object sender, EventArgs e)
        {
            if (Utils.GetQueryStringValue("type") == "list")
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
