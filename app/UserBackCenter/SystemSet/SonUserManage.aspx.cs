using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using EyouSoft.Common.Function;
using EyouSoft.Common.Control;
using EyouSoft.Common;

namespace UserBackCenter.SystemSet
{   
    /// <summary>
    /// 页面功能：子账户管理
    /// 开发人：xuty 开发时间2010-07-06
    /// </summary>
    public partial class SonUserManage : BackPage
    {
        protected int recordCount;
        protected int pageSize = 10;
        protected int pageIndex=1;
        protected bool isArea;
        protected int SonUserNumLimit;
        protected bool haveUpdate = true;
        protected int itemIndex = 1;
        protected string userName;
        protected EyouSoft.IBLL.CompanyStructure.ICompanyUser sonUserBll;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CheckGrant(TravelPermission.系统设置_权限管理))
            {
                Utils.ResponseNoPermit();
                return;
            }
            if (!CheckGrant(TravelPermission.系统设置_权限管理, TravelPermission.系统设置_子账户管理))
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
            EyouSoft.Model.CompanyStructure.CompanySetting comSeting = EyouSoft.BLL.CompanyStructure.CompanySetting.CreateInstance().GetModel(SiteUserInfo.CompanyID);
            if (comSeting != null)
            {
                SonUserNumLimit = comSeting.OperatorLimit;
            }
            //是否开通专线,如果开通则显示专线区域列
            if (SiteUserInfo.CompanyRole.RoleItems.Contains(EyouSoft.Model.CompanyStructure.CompanyType.专线))
            {
                isArea = true;
            }
            sonUserBll = EyouSoft.BLL.CompanyStructure.CompanyUser.CreateInstance();
            pageIndex = Utils.GetInt(Request.QueryString["Page"], 1);
            itemIndex = (pageIndex - 1) * pageSize + 1;
            string method =Utils.GetFormValue("method");
            if (method == "del")//删除子账户
            {
                if (!haveUpdate)
                {
                    Utils.ResponseMeg(false, "对不起，你没有该权限！");
                    return;
                }
                DelSonUser();
                return;
            }
            if (method == "setforbid")//设置用户状态
            {
                if (!haveUpdate)
                {
                    Utils.ResponseMeg(false, "对不起，你没有该权限！");
                    return;
                }
                SetForbid();
                return;
            }
            userName = SiteUserInfo.UserName;
            LoadSonUserList();//绑定子账户列表
        }

        #region 产生分页
        protected void PageBind()
        {
            this.sum_ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            this.sum_ExportPageInfo1.intPageSize = pageSize;
            this.sum_ExportPageInfo1.CurrencyPage = pageIndex;
            this.sum_ExportPageInfo1.intRecordCount = recordCount;
        }
        #endregion

        #region 初始化子账户列表
        protected void LoadSonUserList()
        {
            IList<EyouSoft.Model.CompanyStructure.CompanyUser> sonUserList = sonUserBll.GetList(SiteUserInfo.CompanyID, pageSize, pageIndex, ref recordCount);
            if (sonUserList.Count > 0)
            {
              
                sum_rpt_SonUserList.DataSource = sonUserList;
                sum_rpt_SonUserList.DataBind();
                PageBind();//设置分页
            }
            else
            {
                sum_noData.Style.Remove("display");
                this.sum_ExportPageInfo1.Visible = false;
            }

        }
        #endregion

        #region 删除子账户
        protected void DelSonUser()
        {
            if (!IsCompanyCheck)
            {
                Utils.ResponseMeg(false, "对不起,你尚未审核通过!");
                return;
            }
            string ids = Utils.GetFormValue("sonuserids").TrimEnd(',');
            if (ids != "")
            {
                string[] idArray = ids.Split(',');
                if (sonUserBll.Remove(idArray))
                {
                   
                    Utils.ResponseMeg(true, "删除成功!");
                }
                else
                {
                   
                    Utils.ResponseMeg(false, "删除失败!");
                }
            }
            else
            {
                Utils.ResponseMeg(false, "请选择要删除的账户!");
            }
           
        }
        #endregion

        #region 绑定子账户时返回经营区域
        protected string GetAreas(Object areas)
        {
            IList<EyouSoft.Model.SystemStructure.AreaBase> areaList = areas as IList<EyouSoft.Model.SystemStructure.AreaBase>;
            StringBuilder areasBuilder=new StringBuilder();
            if (areaList != null)
            {
                foreach (EyouSoft.Model.SystemStructure.AreaBase area in areaList)
                {
                    areasBuilder.Append(area.AreaName + ",");
                }
            }
            return areasBuilder.ToString().TrimEnd(',');
        }
        #endregion

        #region 设置是否启用账户
        protected void SetForbid()
        {
            if (!IsCompanyCheck)
            {
                Utils.ResponseMeg(false, "对不起,你尚未审核通过!");
                return;
            }
            string id = Utils.GetFormValue("sonuserid");
            if (id != "")
            {
                bool isEnable = Utils.GetFormValue("enable") == "forbid" ? false : true;
                if (sonUserBll.SetEnable(id,isEnable))
                {
                    if (isEnable)
                    {
                        Utils.ResponseMeg(true, "账户已开启!");
                    }
                    else
                    {
                        Utils.ResponseMeg(true, "账户已关闭!");
                    }
                }
                else
                {
                    Utils.ResponseMegError();
                }
            }
            else
            {
                Utils.ResponseMegError();
            }
        }
        #endregion

       
    }
}
