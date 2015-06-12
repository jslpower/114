using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Function;
using EyouSoft.Common;

namespace SiteOperationsCenter.NewsCenterControl
{
    /// <summary>
    ///  Tag标签管理
    ///  author:zhengfj date:2011-4-1
    /// </summary>
    public partial class Tag : EyouSoft.Common.Control.YunYingPage
    {
        #region 成员变量
        protected const int PAGESIZE = 20; //每页显示记录数
        protected int pageIndex = 1;
        private int pageCount;
        private static EyouSoft.Model.NewsStructure.TagKeyInfo tagKeyInfoModel = null;
        private readonly EyouSoft.BLL.NewsStructure.TagKeyInfo tagKeyInfoBLL
            = new EyouSoft.BLL.NewsStructure.TagKeyInfo();
        #endregion
        #region page_load

        protected void Page_Load(object sender, EventArgs e)
        {
            YuYingPermission[] parms = { YuYingPermission.平台管理_管理该栏目, YuYingPermission.Tag管理_管理该栏目 };
            if (!CheckMasterGrant(parms))
            {
                Utils.ResponseNoPermit(YuYingPermission.Tag管理_管理该栏目, true);
                return;
            }
            if (!IsPostBack)
            {
                btnAdd.CommandName = "Add";
                //switch (Request.QueryString["type"])
                //{
                //    case "edit":
                //        EditTag();
                //        break;
                //    default:
                //        break;
                //}
                Databind();
            }
        }

        #endregion

        #region 编辑tag标签
        //private void EditTag()
        //{
        //    txtTag.Text = Request.QueryString["tag"];
        //    btnAdd.CommandName = "Edit";
        //}
        #endregion

        #region 获取所有tag标签
        public void Databind()
        {
            pageIndex = Utils.GetInt(Request.QueryString["Page"], 1);
            IList<EyouSoft.Model.NewsStructure.TagKeyInfo> list = tagKeyInfoBLL.GetTagList(PAGESIZE,
                pageIndex, ref pageCount, TagName);
            repList.DataSource = list;
            repList.DataBind();
            ExportPageInfo();
        }

        /// <summary>
        /// 绑定分页控件
        /// </summary>
        public void ExportPageInfo()
        {
            //绑定分页控件
            this.ExportPageInfo2.intPageSize = PAGESIZE;//每页显示记录数
            System.Collections.Specialized.NameValueCollection urlParams
                = new System.Collections.Specialized.NameValueCollection(2);
            urlParams.Add("tag1", TagName);
            this.ExportPageInfo2.UrlParams = urlParams;
            this.ExportPageInfo2.intRecordCount = pageCount;
            this.ExportPageInfo2.CurrencyPage = pageIndex;
            this.ExportPageInfo2.CurrencyPageCssClass = "RedFnt";
            this.ExportPageInfo2.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            this.ExportPageInfo2.LinkType = 3;
        }
        #endregion

        #region 查询条件
        public string TagName
        {
            get
            {
                return Request.QueryString["tag1"];
            }
        }
        #endregion
        /// <summary>
        /// 添加/修改 Tag标签
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string tag = txtTag.Text.Replace(" ", "");
            tagKeyInfoModel = new EyouSoft.Model.NewsStructure.TagKeyInfo()
            {
                Category = EyouSoft.Model.NewsStructure.ItemCategory.Tag,
                ItemName = EyouSoft.Common.Function.StringValidate.Encode(tag),
                OperatorId = base.MasterUserInfo.ID
            };
            bool result = false;
            try
            {
                if (btnAdd.CommandName.Equals("Add"))
                {
                    if (tagKeyInfoBLL.TagExists(tag, 0))  //检测tag是否存在
                    {
                        MessageBox.Show(this, "该tag已经存在，请重新输入!!!");
                        return;
                    }
                    result = tagKeyInfoBLL.Add(tagKeyInfoModel);
                }
                else if (btnAdd.CommandName.Equals("Edit"))
                {
                    int ID;
                    result = int.TryParse(hfid.Value, out ID); 
                    if (result)
                    {
                        if (tagKeyInfoBLL.TagExists(tag, ID))  //检测tag是否存在
                        {
                            MessageBox.Show(this, "该tag已经存在，请重新输入!!!");
                            return;
                        }
                        tagKeyInfoModel.Id = ID;
                        result = tagKeyInfoBLL.Update(tagKeyInfoModel);
                    }
                }
                if (result)
                {
                    hfid.Value = string.Empty;
                    MessageBox.ShowAndRedirect(this, "Tag标签操作成功", "Tag.aspx");
                }
                else
                {
                    MessageBox.Show(this, "操作失败");
                }
            }
            catch (Exception)
            {
                MessageBox.Show(this, "操作失败");
            }
        }

        /// <summary>
        /// Repeater操作
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void repList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int id = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName.Equals("Del"))
            {
                try
                {
                    if (tagKeyInfoBLL.Delete(new int[] { id }))
                    {
                        MessageBox.ShowAndRedirect(this, "删除成功", "Tag.aspx");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show(this, "删除失败");
                }
            }
            else if (e.CommandName.Equals("Edit"))
            {
                btnAdd.CommandName = "Edit";
                tagKeyInfoModel = tagKeyInfoBLL.GetModel(id, EyouSoft.Model.NewsStructure.ItemCategory.Tag);
                if (tagKeyInfoModel != null)
                {
                    txtTag.Text = tagKeyInfoModel.ItemName;
                    hfid.Value = id.ToString();
                }
            }
        }

    }
}
