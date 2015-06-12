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
using EyouSoft.ControlCommon;
using EyouSoft.Common.Function;
using System.Collections.Generic;
using System.Text;

namespace SiteOperationsCenter.NewsCenterControl
{
    /// <summary>
    /// 页面功能：平台管理——新闻中心新增修改管理页
    /// 开发人：杜桂云      开发时间：2010-07-22
    /// </summary>
    public partial class OperatorNewsPage : EyouSoft.Common.Control.YunYingPage
    {
        #region 成员变量
        protected string img_Path = string.Empty;
        protected int EditeID = 0;
        #endregion

        #region 页面加载
        protected void Page_Load(object sender, EventArgs e)
        {
           
            //初始化新闻类别
            InitTypeList();
            if (!string.IsNullOrEmpty(Utils.InputText(Context.Request.QueryString["EditID"])))
            {
                //修改操作
                YuYingPermission[] parms2 = { YuYingPermission.新闻中心_管理该栏目, YuYingPermission.新闻中心_修改 };
                //修改操作
               
                //权限验证
                if (!CheckMasterGrant(parms2))
                {
                    Utils.ResponseNoPermit(YuYingPermission.新闻中心_修改, true);
                    return;
                }
                EditeID = Utils.GetInt(Context.Request.QueryString["EditID"]);
                //初始化要修改的数据
                if (!IsPostBack)
                {
                    EyouSoft.Model.SystemStructure.Affiche Model = EyouSoft.BLL.SystemStructure.Affiche.CreateInstance().GetModel(EditeID);
                    if (Model != null)
                    {
                        this.txtIssuTime.Value = Model.IssueTime.ToString("yyyy-MM-dd hh:mm");
                        this.ddlType.SelectedValue = ((int)Model.AfficheClass).ToString();
                        this.txt_TitleName.Value = Model.AfficheTitle;
                        this.FCK_PlanTicketContent.Value = Model.AfficheInfo;
                        this.txtSendPeople.Value = Model.OperatorName;
                        if (!string.IsNullOrEmpty(Model.PicPath))
                        {
                            img_Path = string.Format("<a href=\"{0}\"target='_blank'  title=\"点击查看\">{1}</a>", Domain.FileSystem + Model.PicPath, Model.PicPath.Substring(Model.PicPath.LastIndexOf('/') + 1));
                            hdfAgoImgPath.Value = Model.PicPath;
                        }
                    }
                    //释放资源
                    Model = null;
                }
            }
            else
            {
                YuYingPermission[] parms = { YuYingPermission.新闻中心_管理该栏目, YuYingPermission.新闻中心_新增 };
                //权限验证
                if (!CheckMasterGrant(parms))
                {
                    Utils.ResponseNoPermit(YuYingPermission.新闻中心_新增, true);
                    return;
                }
                this.tr_SendPeople.Visible = false;
                this.tr_SendTime.Visible = false;
            }
        }
        #endregion

        #region 初始化新闻类别
        private void InitTypeList()
        {
            this.ddlType.Items.Add(new ListItem("-请选择-", ""));
            string[] typeList=Enum.GetNames(typeof(EyouSoft.Model.SystemStructure.AfficheType));
            if (typeList != null && typeList.Length > 0)
            {
                foreach (string str in typeList)
                {
                    this.ddlType.Items.Add(new ListItem(str,((int)Enum.Parse(typeof(EyouSoft.Model.SystemStructure.AfficheType),str)).ToString()));
               }
            }
            //释放资源
            typeList = null;
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
                picUrl = Utils.GetFormValue(hdfAgoImgPath.UniqueID);
            }
            int NewsType =int.Parse(Utils.GetFormValue("ddlType"));
            string NewTitle = Utils.GetFormValue("txt_TitleName", 80);
            string NewsContent = Utils.EditInputText(this.FCK_PlanTicketContent.Value);
            string SendNewsPeople =this.MasterUserInfo.UserName;
            //必填验证
            if (NewTitle == "")
            {
                strErr += "标题名称不能为空！\\n";
            }
            
            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }
            #endregion

            //实例化类的对象,保存数据
            EyouSoft.Model.SystemStructure.Affiche Model = new EyouSoft.Model.SystemStructure.Affiche ();
            Model.AfficheClass=(EyouSoft.Model.SystemStructure.AfficheType)NewsType;
            Model.AfficheInfo =NewsContent;
            Model.AfficheTitle = NewTitle;
            Model.Clicks = 0;
            Model.IssueTime = DateTime.Now;
            Model.OperatorID = this.MasterUserInfo.ID; //操作员编号
            Model.OperatorName = SendNewsPeople;
            Model.PicPath = picUrl;
            if (EditeID != 0)
            {
                Model.ID = EditeID;
                EyouSoft.BLL.SystemStructure.Affiche.CreateInstance().Update(Model);               
                MessageBox.ShowAndRedirect(this, "修改成功", "NewsInfoList.aspx");
            }
            else
            {
                YuYingPermission[] parms3 = { YuYingPermission.新闻中心_管理该栏目, YuYingPermission.新闻中心_修改 };
                YuYingPermission[] parms4 = { YuYingPermission.新闻中心_管理该栏目, YuYingPermission.新闻中心_删除 };
                EyouSoft.BLL.SystemStructure.Affiche.CreateInstance().Add(Model);
                if (!CheckMasterGrant(parms3) && !CheckMasterGrant(parms4))
                {
                    MessageBox.ShowAndRedirect(this, "新增成功", "OperatorNewsPage.aspx");
                    //Utils.ResponseNoPermit();
                    //return;
                }
                else
                {
                    MessageBox.ShowAndRedirect(this, "新增成功", "NewsInfoList.aspx");
                }
               
            }
            //释放资源
            Model = null;
        }
        #endregion

        #region 取消
        protected void btn_Cancel_Click(object sender, EventArgs e)
        {
            MessageBox.LocationUrl(this, "NewsInfoList.aspx");
        }
        #endregion
    }
}
