using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Function;
using EyouSoft.Common;
using System.Text.RegularExpressions;

namespace SiteOperationsCenter.NewsCenterControl
{ 
    /// <summary>
    ///  关键字管理
    ///  author:zhengfj date:2011-4-1
    /// </summary>
    public partial class Keyword : EyouSoft.Common.Control.YunYingPage
    {
        #region 成员变量
        private const int PAGESIZE = 50; //每页显示记录数
        private int pageIndex = 1;
        private int pageCount;
        private static EyouSoft.Model.NewsStructure.TagKeyInfo tagKeyInfoModel = null;
        private readonly EyouSoft.BLL.NewsStructure.TagKeyInfo tagKeyInfoBLL = new EyouSoft.BLL.NewsStructure.TagKeyInfo();
        #endregion

        #region page_load
        protected void Page_Load(object sender, EventArgs e)
        {
            YuYingPermission[] parms = { YuYingPermission.平台管理_管理该栏目, YuYingPermission.关键字管理_管理该栏目 };
            if (!CheckMasterGrant(parms))
            {
                Utils.ResponseNoPermit(YuYingPermission.关键字管理_管理该栏目, true);
                return;
            }
            if (!IsPostBack)
            {
                btnAdd.CommandName = "Add";
                switch (Request.QueryString["type"])
                {
                    case "upsta":
                        EditKeywordAjax();
                        break;
                    default:
                        break;
                }
                Databind();
            }
        }
        #endregion

        #region ajax编辑
        private void EditKeywordAjax()
        {
            string id = Utils.GetFormValue("tid");
            string key = Utils.GetFormValue("k").Replace(" ", "");
            string url = Utils.GetFormValue("u").Replace(" ", ""); ;
            string msg = string.Empty;

            if (tagKeyInfoBLL.KeyWordExists(key, int.Parse(id)))
            {
                msg = "false";
            }
            else
            {
                EyouSoft.Model.NewsStructure.TagKeyInfo tagKeyInfoModel = new EyouSoft.Model.NewsStructure.TagKeyInfo()
                {
                    Category = EyouSoft.Model.NewsStructure.ItemCategory.KeyWord,
                    Id = int.Parse(id),
                    ItemName = key,
                    ItemUrl = url,
                    OperatorId = base.MasterUserInfo.ID
                };
                bool result = tagKeyInfoBLL.Update(tagKeyInfoModel);
                if (result)
                {
                    msg = "true";
                }
                else
                {
                    msg = "ftrue";
                }
                
            }

            Response.Clear();
            Response.Write("{\"msg\":\"" + msg + "\"}");
            Response.End();
        }

        #endregion

        #region 编辑
        //private void EditKeyword()
        //{
        //    txtKeyword.Text = Request.QueryString["key"];
        //    txtUrl.Text = Request.QueryString["url"];
        //    btnAdd.CommandName = "Edit";
        //}
        #endregion

        #region 获取所有关键字
        public void Databind()
        {
            pageIndex = Utils.GetInt(Request.QueryString["Page"], 1);
            IList<EyouSoft.Model.NewsStructure.TagKeyInfo> list = tagKeyInfoBLL.GetKeyWordList(PAGESIZE, pageIndex,
                ref pageCount, Key, Url);
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
            urlParams.Add("key1", Key);
            urlParams.Add("url1", Url);
            this.ExportPageInfo2.UrlParams = urlParams;
            this.ExportPageInfo2.intRecordCount = pageCount;
            this.ExportPageInfo2.CurrencyPage = pageIndex;
            this.ExportPageInfo2.CurrencyPageCssClass = "RedFnt";
            this.ExportPageInfo2.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            this.ExportPageInfo2.LinkType = 3;
        }

        #endregion

        #region 查询条件
        public string Key
        {
            get
            {
                return Server.UrlDecode(Request.QueryString["key1"]);
            }
        }

        public string Url
        {
            get
            {
                return Request.QueryString["url1"];
            }
        }
        #endregion

        /// <summary>
        /// 添加/编辑关键字组
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string key = txtKeyword.Text.Replace(" ", "");
            if (IsHasCHZN(txtUrl.Text.Trim()))
            {
                MessageBox.Show(this, "URL链接不能有中文字符.");
                return;
            }
            tagKeyInfoModel = new EyouSoft.Model.NewsStructure.TagKeyInfo()
            {
                Category = EyouSoft.Model.NewsStructure.ItemCategory.KeyWord,
                ItemName = EyouSoft.Common.Function.StringValidate.Encode(key),
                ItemUrl = txtUrl.Text.Trim(),
                OperatorId = base.MasterUserInfo.ID
            };
            bool result = false;
            try
            {
                if (btnAdd.CommandName.Equals("Add"))  //添加关键字
                {
                    if (tagKeyInfoBLL.KeyWordExists(key, 0))  //如果该关键字已经存在
                    {
                        MessageBox.Show(this, "关键字已经存在，请重新输入!!!");
                        return;
                    }
                    result = tagKeyInfoBLL.Add(tagKeyInfoModel);
                }
                else if (btnAdd.CommandName.Equals("Edit")) //编辑关键字
                {
                    int ID;
                    result = int.TryParse(hfID.Value, out ID);
                    if (result)
                    {
                        if (tagKeyInfoBLL.KeyWordExists(key, ID))  //如果该关键字已经存在
                        {
                            MessageBox.Show(this, "关键字已经存在，请重新输入!!!");
                            return;
                        }
                        tagKeyInfoModel.Id = ID;
                        result = tagKeyInfoBLL.Update(tagKeyInfoModel);
                    }
                }
                if (result)
                {
                    hfID.Value = string.Empty;
                    MessageBox.ShowAndRedirect(this, "关键字组操作成功", "Keyword.aspx");
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
        /// 处理Repeater操作
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
                        MessageBox.ShowAndRedirect(this, "删除成功", "Keyword.aspx");   
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
                tagKeyInfoModel = tagKeyInfoBLL.GetModel(id, EyouSoft.Model.NewsStructure.ItemCategory.KeyWord);
                txtKeyword.Text = tagKeyInfoModel.ItemName;
                txtUrl.Text = tagKeyInfoModel.ItemUrl;
                hfID.Value = id.ToString();
            }
        }
        /// <summary>
        /// 检测是否有中文字符 
        /// </summary>
        /// <param name="inputData"></param>  
        /// <returns></returns>   
        private bool IsHasCHZN(string inputData)
        {
            Regex RegCHZN = new Regex("[\u4e00-\u9fa5]");
            Match m = RegCHZN.Match(inputData);
            return m.Success;
        }
    }
}
