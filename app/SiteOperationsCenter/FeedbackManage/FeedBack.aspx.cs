using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace SiteOperationsCenter.FeedbackManage
{
    /// <summary>
    /// 反馈列表页面
    /// 功能：显示用户反馈列表
    /// 创建人：戴银柱
    /// 创建时间： 2010-08-03  
    public partial class FeedBack : EyouSoft.Common.Control.YunYingPage
    {
        protected int pageSize = 20;
        public int pageIndex = 1;
        protected int recordCount;
        protected string type = "1";


        protected void Page_Load(object sender, EventArgs e)
        {
            switch (EyouSoft.Common.Utils.GetQueryStringValue("type"))
            {
                case "1":
                    if (!CheckMasterGrant(YuYingPermission.高级网店反馈_管理该栏目))
                    {
                        Utils.ResponseNoPermit(YuYingPermission.高级网店反馈_管理该栏目, true);
                        return;
                    } break;
                case "2":
                    if (!CheckMasterGrant(YuYingPermission.MQ反馈_管理该栏目))
                    {
                        Utils.ResponseNoPermit(YuYingPermission.MQ反馈_管理该栏目, true);
                        return;
                    }
                    break;
                case "3":
                    if (!CheckMasterGrant(YuYingPermission.同业114平台反馈_管理该栏目))
                    {
                        Utils.ResponseNoPermit(YuYingPermission.同业114平台反馈_管理该栏目, true);
                        return;
                    }
                    break;
                case "4":
                    if (!CheckMasterGrant(YuYingPermission.旅行社后台反馈_管理该栏目))
                    {
                        Utils.ResponseNoPermit(YuYingPermission.旅行社后台反馈_管理该栏目, true);
                        return;
                    }
                    break;
                case "5":
                    if (!CheckMasterGrant(YuYingPermission.嘉宾申请反馈_管理该栏目))
                    {
                        Utils.ResponseNoPermit(YuYingPermission.嘉宾申请反馈_管理该栏目, true);
                        return;
                    }
                    break;
                default:
                    if (!CheckMasterGrant(YuYingPermission.高级网店反馈_管理该栏目))
                    {
                        Utils.ResponseNoPermit(YuYingPermission.高级网店反馈_管理该栏目, true);
                        return;
                    }
                    break;
            }
            if (!IsPostBack)
            {
                this.ImgBtn.ImageUrl = ImageServerUrl + "/images/yunying/chaxun.gif";
                this.ImgBtnDel.ImageUrl = ImageServerUrl + "/images/yunying/shanchu.gif";
                this.hiddenType.Value = EyouSoft.Common.Utils.GetQueryStringValue("type");
                //设置当前页
                pageIndex = Utils.GetInt(Request.QueryString["Page"], 1);
                DataListInit();
            }
        }


        #region 列表初始化
        protected void DataListInit()
        {
            EyouSoft.Model.SystemStructure.ProductSuggestionType pst = EyouSoft.Model.SystemStructure.ProductSuggestionType.商铺;
            switch (this.hiddenType.Value)
            {
                case "1": pst = EyouSoft.Model.SystemStructure.ProductSuggestionType.商铺; break;
                case "2": pst = EyouSoft.Model.SystemStructure.ProductSuggestionType.MQ; break;
                case "3": pst = EyouSoft.Model.SystemStructure.ProductSuggestionType.平台; break;
                case "4":
                    this.ddlType.Visible = true;
                    if (ddlType.SelectedValue == "1")
                    {
                        pst = EyouSoft.Model.SystemStructure.ProductSuggestionType.个人中心;
                    }
                    else
                    {
                        pst = EyouSoft.Model.SystemStructure.ProductSuggestionType.个人中心报价标准; break;
                    }
                     break;
                    
                default: pst = EyouSoft.Model.SystemStructure.ProductSuggestionType.商铺; break;
            }
            //嘉宾申请反馈列表
            if (this.hiddenType.Value == "5")
            {
                this.divListSecond.Visible = true;
                this.divList.Visible = false;
                this.lblMsgSecond.Visible = false;
                EyouSoft.IBLL.CommunityStructure.ICommunityAdvisor IBLL = EyouSoft.BLL.CommunityStructure.CommunityAdvisor.CreateInstance();
                IList<EyouSoft.Model.CommunityStructure.CommunityAdvisor> list = IBLL.GetCommunityAdvisorList(pageSize, pageIndex, ref recordCount,false);
                if (list != null && list.Count > 0)
                {
                    this.rptListScond.DataSource = list;
                    this.rptListScond.DataBind();
                    BindPage();
                    list = null;
                }
                else
                {
                    this.rptListScond.DataSource = null;
                    this.rptListScond.DataBind();
                    this.lblMsgSecond.Visible = true;
                }
                IBLL = null;
            }
            else
            {
                this.divListSecond.Visible = false;
                this.divList.Visible = true;
                this.lblFristMsg.Visible = false;
                EyouSoft.IBLL.SystemStructure.IProductSuggestion IBll = EyouSoft.BLL.SystemStructure.ProductSuggestion.CreateInstance();
                IList<EyouSoft.Model.SystemStructure.ProductSuggestionInfo> list = IBll.GetSuggestions(pageSize, pageIndex, ref   recordCount, Utils.EditInputText(this.cName.Value), Utils.EditInputText(this.uName.Value), Utils.EditInputText(this.searchVal.Value),pst);
                if (list != null && list.Count > 0)
                {
                    this.rptList.DataSource = list;
                    this.rptList.DataBind();
                    BindPage();
                }
                else
                {
                    this.rptList.DataSource = null;
                    this.rptList.DataBind();
                    this.lblFristMsg.Visible = true;
                }
                list = null;
                IBll = null;
            }
            
        }
        #endregion

        #region 设置分页
        protected void BindPage()
        {
            this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            this.ExportPageInfo1.UrlParams = Request.QueryString;
            this.ExportPageInfo1.intPageSize = pageSize;
            this.ExportPageInfo1.CurrencyPage = pageIndex;
            this.ExportPageInfo1.intRecordCount = recordCount;
        }
        #endregion

        #region 按钮点击事件
        protected void ImgBtn_Click(object sender, ImageClickEventArgs e)
        {
            DataListInit();
        }
        #endregion

        #region 根据不同类型删除选中的行
        protected void ImgBtnDel_Click(object sender, ImageClickEventArgs e)
        {
            if (this.hiddenType.Value == "5")
            {
                for (int i = 0; i < rptListScond.Items.Count; i++)
                {
                    CheckBox box = rptListScond.Items[i].FindControl("deleteBox") as CheckBox;
                    if (box.Checked)
                    {
                       new EyouSoft.BLL.CommunityStructure.CommunityAdvisor().DeleteCommunityAdvisor(Convert.ToInt32(box.CssClass));
                    }
                }
            }
            else
            {
                for (int i = 0; i < rptList.Items.Count; i++)
                {
                    CheckBox box = rptList.Items[i].FindControl("Delbox") as CheckBox;
                    if (box.Checked)
                    {
                        new EyouSoft.BLL.SystemStructure.ProductSuggestion().DeleteSuggestionInfo(box.CssClass);
                    }
                }  
            }
            Response.Redirect(Request.Url.ToString());
        }
        #endregion
    }
}
