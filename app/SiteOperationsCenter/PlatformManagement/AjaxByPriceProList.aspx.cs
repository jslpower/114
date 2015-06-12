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
using System.Collections.Generic;
using System.Text;

namespace SiteOperationsCenter.PlatformManagement
{
    /// <summary>
    /// 页面功能：平台管理—基础数据维护—报价等级列表
    /// 开发人：杜桂云      开发时间：2010-07-01
    /// </summary>
    public partial class AjaxByPriceProList : EyouSoft.Common.Control.YunYingPage
    {
        protected bool EditFlag = false; //操作权限
        #region 页面加载
        protected void Page_Load(object sender, EventArgs e)
        {
            //权限验证
            YuYingPermission[] parms = { YuYingPermission.平台管理_管理该栏目, YuYingPermission.平台管理_基础数据维护 };
            EditFlag = CheckMasterGrant(parms);
            if (!EditFlag)
            {
                Utils.ResponseNoPermit(YuYingPermission.平台管理_基础数据维护, true);
                return;
              //  this.btnPriceAdd.Visible = false;
            }
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
                    Eidt(Utils.InputText(Request.QueryString["EidtID"]),Utils.InputText(HttpContext.Current.Server.UrlDecode(Request.QueryString["PriceName"]),10));
                }
                else
                {
                    //处理页面添加请求
                    if (!string.IsNullOrEmpty(Utils.InputText(HttpContext.Current.Server.UrlDecode(Request.QueryString["PriceName"]),10)))
                    {
                        Add(Utils.InputText(HttpContext.Current.Server.UrlDecode(Request.QueryString["PriceName"]), 10));
                    }
                }

            }
            //初始化数据绑定
            GetAreaList();
        }
        #endregion

        #region 绑定数据
        /// <summary>
        /// 绑定数据
        /// </summary>
        protected void GetAreaList()
        {

            IList<EyouSoft.Model.CompanyStructure.CommonPriceStand> priceList = EyouSoft.BLL.CompanyStructure.CommonPriceStand.CreateInstance().GetList();
            if (priceList != null && priceList.Count > 0)
            {
                this.dalList.DataSource = priceList;
                this.dalList.DataBind();
            }
            //释放资源
            priceList = null;
        }
        #endregion

        #region 新增事件
        private void Add(string PriceName)
        {
            //新增
            string strErr = "";
            if (PriceName == "")
            {
                strErr += "价格等级名称不能为空！\\n";
            }
            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }
            EyouSoft.BLL.CompanyStructure.CommonPriceStand.CreateInstance().Add(PriceName);

            return;
        }
        #endregion

        #region 修改事件
        private void Eidt(string EidtID, string PriceName)
        {
            string strErr = "";
            if (PriceName == "")
            {
                strErr += "价格等级名称不能为空！\\n";
            }

            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }

            EyouSoft.BLL.CompanyStructure.CommonPriceStand.CreateInstance().Update(PriceName,EidtID);

            return;
        }
        #endregion

        #region 删除事件
        /// <summary>
        /// 删除事件
        /// </summary>
        private void DelProcess(string DelID)
        {
            EyouSoft.BLL.CompanyStructure.CommonPriceStand.CreateInstance().Delete(DelID);
            return;
        }
        #endregion

        #region 处理项操作事件
        /// <summary>
        ///修改和删除操作
        /// </summary>
        /// <returns></returns>
        public string CreateOperation(string ID)
        {
            if (EditFlag)
            {
                return string.Format("<a id='aprice_{0}' href='javascript:;'  onclick='EidtPricePro(\"aprice_{0}\",\"{0}\");return false;'>修改</a> <a href='javascript:void(0)' onclick='DeletePricePro(\"{0}\")'>删除</a>", ID);
            }
            else 
            {
                return "";
            }
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
