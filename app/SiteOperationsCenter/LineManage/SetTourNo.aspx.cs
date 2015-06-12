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
using EyouSoft.Common;
using EyouSoft.Model.NewTourStructure;

namespace SiteOperationsCenter.LineManage
{
    /// <summary>
    /// 设置团号前缀
    /// 罗丽娥   2010-06-28
    /// </summary>
    public partial class SetTourNo : EyouSoft.Common.Control.YunYingPage
    {
        private EyouSoft.SSOComponent.Entity.UserInfo UserInfoModel = null;
        protected string strPrefixText = string.Empty;
        protected string AreaId = "";
        protected bool IsCompanyCheck = true;
        protected string CompanyId = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            AreaId = Utils.GetQueryStringValue("AreaId");
            CompanyId = Utils.GetQueryStringValue("CompanyId");
            // 判断是否已经登录
            if (!IsLogin)
            {
                EyouSoft.Security.Membership.UserProvider.RedirectLoginOpenTopPage(Domain.UserBackCenter + "/Default.aspx");
            }
            if (!Page.IsPostBack)
            {
                InitRouteArea();
            }

            if (!String.IsNullOrEmpty(Request.QueryString["flag"]))
            {
                string flag = Request.QueryString["flag"].ToString();
                if (flag.Equals("save", StringComparison.OrdinalIgnoreCase))
                {
                    Response.Clear();
                    Response.Write(SaveData());
                    Response.End();
                }
            }
        }

        #region 初始化线路区域
        /// <summary>
        /// 初始化线路区域
        /// </summary>
        private void InitRouteArea()
        {
            int[] AreaIds = { 0 };
            AreaIds[0] = Utils.GetInt(AreaId);
            EyouSoft.IBLL.SystemStructure.ISysArea bll = EyouSoft.BLL.SystemStructure.SysArea.CreateInstance();
            IList<EyouSoft.Model.SystemStructure.SysArea> list = bll.GetSysAreaList(AreaIds);
            if (list != null && list.Count > 0)
            {
                this.rptRouteArea.DataSource = list;
                this.rptRouteArea.DataBind();
            }
            list = null;
            bll = null;
        }

        protected string InitData(string AreaID, string AreaName)
        {
            string tmpVal = string.Empty;
            EyouSoft.IBLL.CompanyStructure.ICompanyAreaSetting bll = EyouSoft.BLL.CompanyStructure.CompanyAreaSetting.CreateInstance();
            EyouSoft.Model.CompanyStructure.CompanyAreaSetting model = bll.GetModel(CompanyId, int.Parse(AreaID));
            if (model != null)
            {
                tmpVal = string.Format("<tr><td align=\"right\" style=\"line-height: 30px;\">{0}：</td><td align=\"left\" style=\"line-height: 30px;\"><input type=\"hidden\" name=\"hidAreaID\" name=\"hidAreaID\" value=\"{1}\" /><input type=\"text\" name=\"txtPrefixText\" id=\"txtPrefixText{1}\" value=\"{2}\" /></td></tr>", AreaName, AreaID, model.PrefixText);
            }
            else
            {
                tmpVal = string.Format("<tr><td align=\"right\" style=\"line-height: 30px;\">{0}：</td><td align=\"left\" style=\"line-height: 30px;\"><input type=\"hidden\" name=\"hidAreaID\" name=\"hidAreaID\" value=\"{1}\" /><input type=\"text\" name=\"txtPrefixText\" id=\"txtPrefixText{1}\" /></td></tr>", AreaName, AreaID);
            }
            return tmpVal;
        }
        #endregion

        #region 添加团号前缀规则
        /// <summary>
        /// 添加团号前缀规则
        /// </summary>
        private bool SaveData()
        {
            bool IsResult = true;
            string[] hidAreaID = (!String.IsNullOrEmpty(Request.QueryString["AreaIdList"])) ? Request.QueryString["AreaIdList"].ToString().Split(',') : null;
            string[] PrefixText = (!String.IsNullOrEmpty(Request.QueryString["PrefixText"])) ? Request.QueryString["PrefixText"].ToString().Split(',') : null;
            if (PrefixText != null)
            {
                EyouSoft.IBLL.CompanyStructure.ICompanyAreaSetting bll = EyouSoft.BLL.CompanyStructure.CompanyAreaSetting.CreateInstance();
                IList<EyouSoft.Model.CompanyStructure.CompanyAreaSetting> list = new List<EyouSoft.Model.CompanyStructure.CompanyAreaSetting>();
                for (int i = 0; i < PrefixText.Length; i++)
                {
                    if (!String.IsNullOrEmpty(PrefixText[i]))
                    {
                        EyouSoft.Model.CompanyStructure.CompanyAreaSetting model = new EyouSoft.Model.CompanyStructure.CompanyAreaSetting();
                        model.CompanyID = CompanyId;
                        model.AreaID = int.Parse(hidAreaID[i]);
                        model.PrefixText = PrefixText[i];
                        list.Add(model);
                        model = null;
                    }
                }
                IsResult = bll.Update(list);
                bll = null;
            }
            return IsResult;
        }
        #endregion
    }
}
