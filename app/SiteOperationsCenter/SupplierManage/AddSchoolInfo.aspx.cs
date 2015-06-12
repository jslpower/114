using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Function;

namespace SiteOperationsCenter
{
    /// <summary>
    /// 学堂类别
    /// </summary>
    /// 周文超 2010-07-29
    public partial class AddSchoolInfo : EyouSoft.Common.Control.YunYingPage
    {
        private string SchoolInfoId = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            SchoolInfoId = Utils.InputText(Request.QueryString["ID"]);
            SingleFileUpload1.SiteModule = SiteOperationsCenterModule.供求管理;
            SingleFileUpload1.IsGenerateThumbnail = false;
            if (string.IsNullOrEmpty(SchoolInfoId))
            {
                ltrTitle.Text = "新增同业资讯";
            }
            else
            {
                ltrTitle.Text = "修改同业资讯";
            }
            if (!IsPostBack)
            {
                InitPageData();
            }
        }

        /// <summary>
        /// 初始化页面
        /// </summary>
        private void InitPageData()
        {
            BindBigType();
            BindSmallType();
            if (string.IsNullOrEmpty(SchoolInfoId))
            {
                this.txtIssuTime.Value = DateTime.Now.ToString("yyyy-MM-dd hh:mm");
                this.txtSendPeople.Value = MasterUserInfo.ContactName;
            }
            else
                InitSchoolInfo();
        }

        #region 绑定类别

        /// <summary>
        /// 绑定大类别
        /// </summary>
        private void BindBigType()
        {
            ddlBigType.Items.Clear();
            ddlBigType.DataSource = EnumHandle.GetListEnumValue(typeof(EyouSoft.Model.CommunityStructure.TopicClass));
            ddlBigType.DataTextField = "text";
            ddlBigType.DataValueField = "value";
            ddlBigType.DataBind();

            ddlBigType.Items.Insert(0, new ListItem("请选择", "0"));
        }

        /// <summary>
        /// 绑定小类别
        /// </summary>
        private void BindSmallType()
        {
            List<int[]> BigType = new List<int[]>();
            BigType.Add(new int[]{1,2,3,4,5,6,7,9});
            BigType.Add(new int[]{8,10,17});
            BigType.Add(new int[]{11,12,13});
            BigType.Add(new int[]{ 14, 15, 16 });         
            
            ddlSmallType.Items.Clear();
            IList<ListItem> SmallTypes= EnumHandle.GetListEnumValue(typeof(EyouSoft.Model.CommunityStructure.TopicAreas));
            foreach (ListItem item in SmallTypes)
            {
                for (int i = 0; i < BigType.Count; i++)
                {
                    if (BigType[i].Contains(int.Parse(item.Value)))
                    {
                        item.Value += "_" + (i + 1).ToString();
                        break;
                    }
                }
                if(item.Value!="0")
                    ddlSmallType.Items.Add(item);
            }

            ddlSmallType.Items.Insert(0, new ListItem("请选择", "0"));
        }

        #endregion

        #region 初始化同业资讯

        /// <summary>
        /// 初始化同业资讯
        /// </summary>
        private void InitSchoolInfo()
        {
            EyouSoft.Model.CommunityStructure.InfoArticle model = EyouSoft.BLL.CommunityStructure.InfoArticle.CreateInstance().GetModel(SchoolInfoId);
            if (model == null)
            {
                MessageBox.ShowAndRedirect(this, "没有找到要修改的信息！", "/SupplierManage/SchoolList.aspx");
                return;
            }

            this.txtIssuTime.Value = model.IssueTime.ToString("yyyy-MM-dd hh:mm");
            if (ddlBigType.Items.FindByValue(((int)model.TopicClassId).ToString()) != null)
                ddlBigType.Items.FindByValue(((int)model.TopicClassId).ToString()).Selected = true;
            if (ddlSmallType.Items.FindByValue(((int)model.AreaId).ToString()+"_"+((int)model.TopicClassId).ToString()) != null)
                ddlSmallType.Items.FindByValue(((int)model.AreaId).ToString() + "_" + ((int)model.TopicClassId).ToString()).Selected = true;
            if (ddlColor.Items.FindByValue(model.TitleColor) != null)
                ddlColor.Items.FindByValue(model.TitleColor).Selected = true;
            this.txt_TitleName.Value = model.ArticleTitle;
            this.FCK_PlanTicketContent.Value = model.ArticleText;
            this.txtSendPeople.Value = model.Editor;
            if (!string.IsNullOrEmpty(model.ImgPath)) //推荐图
            {
                ltrOldImgPath.Text = string.Format("推荐图片：<a href=\"{0}\"target='_blank'  title=\"点击查看\">{1}</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;", Domain.FileSystem + model.ImgPath, model.ImgPath.Substring(model.ImgPath.LastIndexOf('/') + 1));
                hdfImgPath.Value = model.ImgPath;
            }
            if (!string.IsNullOrEmpty(model.ImgThumb)) //焦点图
            {
                ltrOldImgThumb.Text = string.Format("焦点图片：<a href=\"{0}\"target='_blank'  title=\"点击查看\">{1}</a>", Domain.FileSystem + model.ImgThumb, model.ImgThumb.Substring(model.ImgThumb.LastIndexOf('/') + 1));
                hdfImgThumb.Value = model.ImgThumb;
            }
            ckbIsTop.Checked = model.IsTop;
            ckbIsFrontPage.Checked = model.IsFrontPage;
            txtTag.Value = model.ArticleTag;
            txtSource.Value = model.Source;
        }

        #endregion

        #region 保存按钮事件

        /// <summary>
        /// 保存按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            int BigType = Utils.GetInt(ddlBigType.SelectedValue.Trim());
            string SmallType = Utils.GetString(ddlSmallType.SelectedValue.Trim(),"0");
            string strTitle = Utils.InputText(txt_TitleName.Value);
            string strTitleColor = Utils.InputText(ddlColor.SelectedValue);
            string strTag = Utils.InputText(txtTag.Value.Trim());
            string strSource = Utils.InputText(txtSource.Value.Trim());
            string NewImgThumb = Utils.GetFormValue("SingleFileUpload1$hidFileName");
            string OldImgThumb = Utils.InputText(hdfImgThumb.Value.Trim());
            string NewImgPath = Utils.GetFormValue("SingleFileUpload2$hidFileName");
            string OldImgPath = Utils.InputText(hdfImgPath.Value.Trim());
            bool IsTop = ckbIsTop.Checked;
            string strInfo = FCK_PlanTicketContent.Value.Trim();

            System.Text.StringBuilder strErr = new System.Text.StringBuilder();

            if (BigType <= 0)
                strErr.Append("请选择大类别！\\n");
            if (SmallType == "0" && BigType!=5)
                strErr.Append("请选择小类别！\\n");
            if (string.IsNullOrEmpty(strTitle))
                strErr.Append("请填写标题！\\n");

            if (!string.IsNullOrEmpty(strErr.ToString()))
            {
                MessageBox.ShowAndReturnBack(this, strErr.ToString(), 1);
                return;
            }

            EyouSoft.Model.CommunityStructure.InfoArticle model = new EyouSoft.Model.CommunityStructure.InfoArticle();
            model.AreaId = SmallType.Contains("_") ? (EyouSoft.Model.CommunityStructure.TopicAreas)int.Parse(SmallType.Split('_')[0].ToString()) : EyouSoft.Model.CommunityStructure.TopicAreas.未知;
            model.ArticleTag = strTag;
            model.ArticleText = strInfo;
            model.ArticleTitle = strTitle;
            model.Editor = MasterUserInfo.ContactName;
            model.IssueTime = DateTime.Now;
            model.IsTop = IsTop;
            model.IsFrontPage = ckbIsFrontPage.Checked;
            model.OperatorId = MasterUserInfo.ID;
            model.Source = strSource;
            model.TitleColor = strTitleColor;
            model.TopicClassId = (EyouSoft.Model.CommunityStructure.TopicClass)BigType;

            bool Result = false;
            if (string.IsNullOrEmpty(SchoolInfoId))
            {
                model.ImgPath = NewImgPath;
                model.ImgThumb = NewImgThumb;
                Result = EyouSoft.BLL.CommunityStructure.InfoArticle.CreateInstance().Add(model);
            }
            else
            {
                model.ImgPath = string.IsNullOrEmpty(NewImgPath) ? OldImgPath : NewImgPath;
                model.ImgThumb = string.IsNullOrEmpty(NewImgThumb) ? OldImgThumb : NewImgThumb;
                model.ID = SchoolInfoId;
                Result = EyouSoft.BLL.CommunityStructure.InfoArticle.CreateInstance().Update(model);
            }
            if (Result)
                MessageBox.ShowAndRedirect(this, "保存成功！", "/SupplierManage/SchoolList.aspx");
            else
                MessageBox.ShowAndRedirect(this, "保存失败！", "/SupplierManage/SchoolList.aspx");
        }

        #endregion
    }
}
