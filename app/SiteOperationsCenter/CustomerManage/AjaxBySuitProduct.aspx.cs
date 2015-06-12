using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Function;
using EyouSoft.Common;

namespace SiteOperationsCenter.CustomerManage
{
    public partial class AjaxBySuitProduct : EyouSoft.Common.Control.YunYingPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //处理页面删除请求
                if (!string.IsNullOrEmpty(Utils.InputText(Request.QueryString["DeletID"])))
                {
                    DelProcess(Utils.InputText(Request.QueryString["DeletID"]));
                }
                //处理页面修改请求
                if (!string.IsNullOrEmpty(Utils.InputText(Request.QueryString["EidtID"])))
                {
                    Eidt(Utils.InputText(Request.QueryString["EidtID"]), Utils.InputText(HttpContext.Current.Server.UrlDecode(Request.QueryString["SuitName"])));
                }
                else
                {
                    //处理页面添加请求
                    if (!string.IsNullOrEmpty(Utils.InputText(HttpContext.Current.Server.UrlDecode(Request.QueryString["SuitName"]))))
                    {
                        Add(Utils.InputText(HttpContext.Current.Server.UrlDecode(Request.QueryString["SuitName"])));
                    }
                }

            }
            //初始化数据绑定
            GetCustomerTypeList();
        }

        #region 绑定数据
        /// <summary>
        /// 绑定数据
        /// </summary>
        protected void GetCustomerTypeList()
        {

            IList<EyouSoft.Model.PoolStructure.SuitProductInfo> SuitList = EyouSoft.BLL.PoolStructure.SuitProduct.CreateInstance().GetSuitProductList();
            if (SuitList != null && SuitList.Count > 0)
            {
                this.dalList.DataSource = SuitList;
                this.dalList.DataBind();
            }
            //释放资源
            SuitList = null;
        }
        #endregion

        #region 新增事件
        private void Add(string SuitName)
        {
            //新增
            string strErr = "";
            if (SuitName == "")
            {
                strErr += "客户类型名称不能为空！\\n";
            }
            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }
            EyouSoft.Model.PoolStructure.SuitProductInfo ModelSuit = new EyouSoft.Model.PoolStructure.SuitProductInfo();
            ModelSuit.ProductName = SuitName;
            EyouSoft.BLL.PoolStructure.SuitProduct.CreateInstance().AddSuitProduct(ModelSuit);

            return;
        }
        #endregion

        #region 修改事件
        private void Eidt(string EidtID, string SuitName)
        {
            string strErr = "";
            if (SuitName == "")
            {
                strErr += "客户类型名称不能为空！\\n";
            }

            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }

            EyouSoft.Model.PoolStructure.SuitProductInfo ModelSuit = new EyouSoft.Model.PoolStructure.SuitProductInfo();
            ModelSuit.ProductName = SuitName;
            ModelSuit.ProuctId = EyouSoft.Common.Utils.GetInt(EidtID);
            EyouSoft.BLL.PoolStructure.SuitProduct.CreateInstance().UpdateSuitProduct(ModelSuit);
            
            return;
        }
        #endregion

        #region 删除事件
        /// <summary>
        /// 删除事件
        /// </summary>
        private void DelProcess(string DelID)
        {
            List<int> list=new List<int>();
            list.Add(EyouSoft.Common.Utils.GetInt(DelID));
            EyouSoft.BLL.PoolStructure.SuitProduct.CreateInstance().DeleteSuitProduct(list);
            
            return;
        }
        #endregion

        #region 处理项操作事件
        /// <summary>
        ///修改和删除
        /// </summary>
        /// <returns></returns>
        public string CreateOperation(string ID)
        {
            return string.Format("<a id='suit_{0}' href='javascript:;'  onclick='EidtSuitPro(\"suit_{0}\",\"{0}\");return false;'>修改</a> <a href='javascript:void(0)' onclick='DeleteSuitPro(\"{0}\")'>删除</a>", ID);
        }
        #endregion

        #region 项绑定事件
        protected void dalList_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemIndex != -1)
            {
                int itemNum = e.Item.ItemIndex + 1;
                Label lblItemID = (Label)e.Item.FindControl("lblAutoNumber");
                lblItemID.Text = Convert.ToString(itemNum);
            }
        }
        #endregion
    }
}
