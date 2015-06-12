using System;
using System.Collections;
using System.Collections.Generic;
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
using EyouSoft.Common.Control;
using EyouSoft.Common;
namespace UserBackCenter.SystemSet
{  
    /// <summary>
    /// 页面功能：部门管理
    /// 开发人：xuty 开发时间：2010-07-06
    /// </summary>
    public partial class DepartManage : BackPage
    {
        protected int pageSize = 10000;
        protected int pageIndex;
        protected int recordCount;
        protected EyouSoft.IBLL.CompanyStructure.ICompanyDepartment companyDepartBll;
        protected EyouSoft.Model.CompanyStructure.CompanyDepartment companyDepartModel;
        protected bool haveUpdate = true;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CheckGrant( TravelPermission.系统设置_部门设置))
            {
                haveUpdate = false;
            }
            EyouSoft.Model.CompanyStructure.CompanyDetailInfo companyModel = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(SiteUserInfo.CompanyID);
            //是否开通收费MQ
            //if (!companyModel.StateMore.CompanyService.IsServiceAvailable(EyouSoft.Model.CompanyStructure.SysService.MQ))
            //{
            //    Server.Transfer("/SystemSet/ApplyMQ.aspx");
            //    return;
            //}
            companyDepartBll = EyouSoft.BLL.CompanyStructure.CompanyDepartment.CreateInstance();
            string method = Request.QueryString["method"];
            if (method == "save")
            {
                if (!haveUpdate)
                {
                    Utils.ResponseMeg(false, "对不起，你没有该权限！");
                    return;
                }
                UpadateDepart();//修改部门
                return;
            }
            if (method == "del")
            {
                if (!haveUpdate)
                {
                    Utils.ResponseMeg(false, "对不起，你没有该权限！");
                    return;
                }
                DelDepart();//删除部门
                return;
            }
            if (method == "add")
            {
                if (!haveUpdate)
                {
                    Utils.ResponseMeg(false, "对不起，你没有改权限！");
                    return;
                }
                AddDepart();//添加部门
                return;
            }
            
            LoadDepartList();
        }

        #region 初始化部门列表
        protected void LoadDepartList()
        {
            dm_rpt_DepartList.DataSource = companyDepartBll.GetList(SiteUserInfo.CompanyID, pageSize, pageIndex, ref recordCount);
            dm_rpt_DepartList.DataBind();
        }
         #endregion

        #region 添加部门
        protected void AddDepart()
        {
            if (!IsCompanyCheck)
            {
                Utils.ResponseMeg(false, "对不起,你尚未审核通过!");
                return;
            }
            string departName = Utils.InputText(Request.QueryString["departname"], 20);
            if (departName == "")
            {
                Utils.ResponseMegNoComplete();
            }
            else
            {
                if (companyDepartBll.Exists(SiteUserInfo.CompanyID, departName, ""))
                {
                    Utils.ResponseMeg(false, "该部门已经存在!");
                    return;
                }
                companyDepartModel = new EyouSoft.Model.CompanyStructure.CompanyDepartment();
                companyDepartModel.CompanyID = SiteUserInfo.CompanyID;
                companyDepartModel.DepartName = departName;
                companyDepartModel.OperatorID = SiteUserInfo.ID;
               
                if (companyDepartBll.Add(companyDepartModel))
                {  
                    string departId=companyDepartBll.GetList(SiteUserInfo.CompanyID,1,1,ref recordCount)[0].ID;
                    Utils.ResponseMeg(true, departId);
                    }
                else
                    Utils.ResponseMegError();
            }
        }
        #endregion

        #region 修改部门
        protected void UpadateDepart()
        {
            if (!IsCompanyCheck)
            {
                Utils.ResponseMeg(false, "对不起,你尚未审核通过!");
                return;
            }
            string departId=Utils.GetQueryStringValue("departid");
            string departName= Utils.InputText(Request.QueryString["departname"],20);
            if (departId == "" || departName == "")
            {
                Utils.ResponseMegNoComplete();
            }
            else
            {
                if (companyDepartBll.Exists(SiteUserInfo.CompanyID, departName, departId))
                {
                    Utils.ResponseMeg(false, "改部门已经存在!");
                    return;
                }
                companyDepartModel = companyDepartBll.GetModel(departId);
                companyDepartModel.DepartName = departName;
                companyDepartModel.OperatorID = SiteUserInfo.ID;
                if (companyDepartBll.Update(companyDepartModel))
                    Utils.ResponseMeg(true, "修改成功!");
                else
                    Utils.ResponseMegError();
            }
        }
        #endregion

        #region 删除部门
        protected void DelDepart()
        {
            if (!IsCompanyCheck)
            {
                Utils.ResponseMeg(false, "对不起,你尚未审核通过!");
                return;
            }
            string departId = Utils.GetQueryStringValue("departid");
            if (departId == "")
            {
                Utils.ResponseMegNoComplete();
            }
            else
            {
                if (companyDepartBll.Delete(departId))
                    Utils.ResponseMeg(true,"删除成功!");
                else
                    Utils.ResponseMegError();
            }
        }
        #endregion


    }
}
