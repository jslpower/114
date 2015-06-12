using System;
using System.Data;
using EyouSoft.Common;
using System.Collections.Generic;
using EyouSoft.Common.Function;
using System.Text;

namespace SiteOperationsCenter.PlatformManagement
{
    /// <summary>
    /// 页面功能：平台管理——基本信息
    /// 开发人：杜桂云      开发时间：2010-06-24
    /// </summary>
    public partial class BasicInfoManagement : EyouSoft.Common.Control.YunYingPage
    {
        #region 成员变量
        protected string img_Path = string.Empty;
        protected int EditeID=0;
        protected bool EditeFlag = true;
        #endregion

        #region 页面加载
        protected void Page_Load(object sender, EventArgs e)
        {
            //权限验证
            YuYingPermission[] parms = { YuYingPermission.平台管理_管理该栏目, YuYingPermission.平台管理_基本信息 };
            if (!CheckMasterGrant(parms))
            {
                Utils.ResponseNoPermit(YuYingPermission.平台管理_基本信息, true);
                return;
                //EditeFlag = false;
                //this.btnAdd.Visible = false;
            }
            else
            {
                //初始化数据绑定
                InitAreaContentInfo();
            }
        }
        #endregion

        #region 初始化信息
        private void InitAreaContentInfo()
        {
            EyouSoft.Model.SystemStructure.SystemInfo areaModel = EyouSoft.BLL.SystemStructure.SystemInfo.CreateInstance().GetSystemInfoModel();
            if (areaModel != null )
            {
                EditeID = areaModel.ID;
                this.txt_ContentName.Value = areaModel.ContactName;
                this.txt_CMQ.Value =areaModel.MQ;
                this.txt_CMSN.Value = areaModel.Msn;
                this.txt_ContentPhoneNum.Value = areaModel.ContactMobile;
                this.txt_ContentTelNum.Value = areaModel.ContactTel;
                this.txt_CQQ.Value = areaModel.QQ1;
                this.txt_IndexInfo.Value = areaModel.AllRight;
                this.txt_SiteOpeName.Value = areaModel.SystemName;
                this.txt_UnionInfo.Value = areaModel.SystemRemark;
                this.rpt_ContentPeople.DataSource = areaModel.SysAreaContact;
                this.rpt_ContentPeople.DataBind();

                if (!string.IsNullOrEmpty(areaModel.UnionLog))
                {
                    img_Path = string.Format("<a href=\"{0}\"target='_blank'  title=\"点击查看\">{1}</a>", Domain.FileSystem + areaModel.UnionLog, areaModel.UnionLog.Substring(areaModel.UnionLog.LastIndexOf('/') + 1));
                    hdfAgoImgPath.Value = areaModel.UnionLog;
                }
            }
            //释放资源
            areaModel = null;
        }
        #endregion
        
        #region 销售类别
        protected string GetSalType(string SalType)
        {
            string reVal = "";
            if (SalType == "0")
            {
                reVal = string.Format(" 类别<select name='sel_ContentType'><option value='0' selected>销售</option><option value='1'>客服</option></select>");
            }
            else
            {
                reVal = string.Format(" 类别<select name='sel_ContentType'><option value='0'>销售</option><option value='1' selected>客服</option></select>");
            }
            return reVal;
        }
        #endregion

        #region 添加按钮操作
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (EditeFlag)
            {
                string strErr = "";
                #region 获取联盟表单信息
                string siteOpeName = Utils.GetFormValue("txt_SiteOpeName",250);
                //验证平台名称不为空
                if (siteOpeName == "")
                {
                    strErr += "平台名称不能为空！\\n";
                }
                if (strErr != "")
                {
                    MessageBox.Show(this, strErr);
                    return;
                }
                string ContentName = Utils.GetFormValue("txt_ContentName",50);
                string ContentPhoneNum = Utils.GetFormValue("txt_ContentPhoneNum",50);
                string ContentTelNum = Utils.GetFormValue("txt_ContentTelNum",50);
                string CMQ = Utils.GetFormValue("txt_CMQ",30);
                string CQQ = Utils.GetFormValue("txt_CQQ",30);
                string CMSN = Utils.GetFormValue("txt_CMSN",40);
                string UnionInfo = Server.HtmlDecode(Request.Form["txt_UnionInfo"]);
                string IndexInfo = Server.HtmlDecode(Request.Form["txt_IndexInfo"]);
                #endregion

                #region 获取区域联系人表单信息
                //区域联系人信息
                string[] txt_AreaList = Utils.GetFormValues("txt_Area");
                string[] sel_ContentTypeList = Utils.GetFormValues("sel_ContentType");
                string[] txt_ContentPeopleList = Utils.GetFormValues("txt_ContentPeople");
                string[] txt_ContentPhoneList = Utils.GetFormValues("txt_ContentPhone");
                string[] txt_ContentTelList = Utils.GetFormValues("txt_ContentTel");
                string[] txt_ContentQQList = Utils.GetFormValues("txt_ContentQQ");
                string[] txt_ContentMQList = Utils.GetFormValues("txt_ContentMQ");

                #region 保存区域联系人信息
                IList<EyouSoft.Model.SystemStructure.SysAreaContact> areaList = new List<EyouSoft.Model.SystemStructure.SysAreaContact>();
                if (txt_AreaList != null)
                {
                    EyouSoft.Model.SystemStructure.SysAreaContact areaModel = null;
                    for (int index = 0; index < txt_AreaList.Length; index++)
                    {
                        if (!string.IsNullOrEmpty(txt_AreaList[index].Trim()) || !string.IsNullOrEmpty(txt_ContentPeopleList[index].Trim()) || !string.IsNullOrEmpty(txt_ContentPhoneList[index].Trim()) || !string.IsNullOrEmpty(txt_ContentTelList[index].Trim()) || !string.IsNullOrEmpty(txt_ContentQQList[index].Trim()) || !string.IsNullOrEmpty(txt_ContentMQList[index].Trim()))
                        {
                            areaModel = new EyouSoft.Model.SystemStructure.SysAreaContact();
                            areaModel.ContactMobile = txt_ContentTelList[index];
                            areaModel.ContactName = txt_ContentPeopleList[index];
                            areaModel.ContactTel = txt_ContentPhoneList[index];
                            areaModel.MQ = txt_ContentMQList[index];
                            areaModel.QQ = txt_ContentQQList[index];
                            areaModel.SaleArea = txt_AreaList[index];
                            areaModel.SaleType = int.Parse(sel_ContentTypeList[index]);
                            areaList.Add(areaModel);
                            //释放资源
                            areaModel = null;
                        }
                    }
                }
                #endregion
                #endregion

                #region 保存联盟基本信息
                EyouSoft.Model.SystemStructure.SystemInfo sysModel = new EyouSoft.Model.SystemStructure.SystemInfo();
                sysModel.ID = EditeID;
                sysModel.SystemName = siteOpeName;
                sysModel.ContactName = ContentName;
                sysModel.ContactTel = ContentTelNum;
                sysModel.ContactMobile = ContentPhoneNum;
                sysModel.QQ1 = CQQ;
                sysModel.Msn = CMSN;
                sysModel.SystemRemark =UnionInfo;
                sysModel.AllRight = IndexInfo;
                sysModel.SysAreaContact = areaList;
                sysModel.MQ = CMQ;
                if (!string.IsNullOrEmpty(Utils.GetFormValue("SingleFileUpload1$hidFileName")))
                {
                    sysModel.UnionLog = Utils.GetFormValue("SingleFileUpload1$hidFileName");
                }
                else
                {
                    sysModel.UnionLog = Utils.GetFormValue(hdfAgoImgPath.UniqueID);
                }

                int retInt = EyouSoft.BLL.SystemStructure.SystemInfo.CreateInstance().UpdateSystemInfo(sysModel);
                if (retInt > 0)
                {
                    MessageBox.ShowAndRedirect(this, "修改成功", "BasicInfoManagement.aspx");
                }
                //释放资源
                areaList = null;
                sysModel = null;
                #endregion
            }
            else 
            {
                Utils.ResponseNoPermit(YuYingPermission.平台管理_基本信息,true);
                return;
            }
        }
        #endregion
    }
}
