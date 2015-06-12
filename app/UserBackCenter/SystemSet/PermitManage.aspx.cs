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
    /// 页面功能：权限管理
    /// 开发人：xuty 开发时间：2010-07-06
    /// </summary>
    public partial class PermitManage : BackPage
    {
        protected EyouSoft.IBLL.CompanyStructure.ICompanyUserRoles roleBll;
        protected int sortId=1;
        protected int pageSize = 1000;
        protected int pageIndex = 1;
        protected int recordCount;
        protected bool haveUpdate = true;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CheckGrant(TravelPermission.系统设置_权限管理))
            {
                Utils.ResponseNoPermit();
                haveUpdate = false;
                return;
            }
            EyouSoft.Model.CompanyStructure.CompanyDetailInfo companyModel = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(SiteUserInfo.CompanyID);
            //是否开通收费MQ
            //if (!companyModel.StateMore.CompanyService.IsServiceAvailable(EyouSoft.Model.CompanyStructure.SysService.MQ))
            //{
            //    Server.Transfer("/SystemSet/ApplyMQ.aspx");
            //    return;
            //}
            roleBll = EyouSoft.BLL.CompanyStructure.CompanyUserRoles.CreateInstance();
            string method = Utils.GetFormValue("method");
            if (method == "del")
            {
                DelRole();//删除角色
                return;
            }
            LoadRoleList();
        }

        #region 初始化角色列表
        protected void LoadRoleList()
        {
            IList<EyouSoft.Model.CompanyStructure.CompanyUserRoles> userRoleList = roleBll.GetList(SiteUserInfo.CompanyID, pageSize, pageIndex, ref recordCount);
            if (userRoleList!=null&&userRoleList.Count > 0)
            {
                pm_rpt_RoleList.DataSource = userRoleList;
                pm_rpt_RoleList.DataBind();
            }
            else
            {
                pm_noData.Style.Remove("display");
            }
        }
        #endregion

        #region 删除角色
        protected void DelRole()
        {
            if (!IsCompanyCheck)
            {
                Utils.ResponseMeg(false, "对不起,你尚未审核通过!");
                return;
            }
            string ids = Utils.GetFormValue("roleids");
            if (ids == "")
            {
                Utils.ResponseMeg(false, "请选择要删除的角色!");
            }
            else
            {
                string[] idArray = ids.Split(',');
                bool isOk = true;
                foreach (string id in idArray)
                {
                    if (!roleBll.Delete(id))
                        isOk = false;
                }
                if (isOk)
                {
                    int count = roleBll.GetList(SiteUserInfo.CompanyID, pageSize, pageIndex, ref recordCount).Count;
                    if (count == 0)
                    {
                        Utils.ResponseMeg(true, "删除成功0");
                    }
                    else
                    {
                        Utils.ResponseMeg(true, "删除成功!");
                    }
                }
                else
                {
                    Utils.ResponseMegError();
                }
            }
        }
        #endregion
    }
}
