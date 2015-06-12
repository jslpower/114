using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Collections.Generic;
using System.Linq;
namespace IMFrame.TourAgency.TourOrder
{
    /// <summary>
    /// 获取线路区域
    ///MQ修改 luofx 2010-8-16
    /// </summary>
    public partial class AjaxAera : EyouSoft.ControlCommon.Control.MQPage
    {

        private string _pageNameLink = "";
        private int _currentAreaId = 0;   //当前的线路区域id
        protected string strAreaList = "";
        /// <summary>
        /// 获取或设置当前的分站id
        /// </summary>
        protected int SiteId = 0;
        protected int SalerId = 0;

        private bool _isSortByValidTour = false;
        /// <summary>
        /// 是否把有有效团队的线路区域显示在前面 默认为否
        /// </summary>
        public bool IsSortByValidTour
        {
            get { return _isSortByValidTour; }
            set { _isSortByValidTour = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            string strAreaId = Request.QueryString["AreaId"];

            if (EyouSoft.Common.Utils.GetInt(strAreaId) > 0)
            {
                _currentAreaId = Int32.Parse(strAreaId);
            }
            //初始值
            _pageNameLink = Request.ServerVariables["SCRIPT_NAME"].ToString();
            if (!IsPostBack)
            {
                InitRouteArea();
            }
        }
        #region 初始化区域
        /// <summary>
        /// 初始化线路区域
        /// </summary>
        private void InitRouteArea()
        {
            EyouSoft.SSOComponent.Entity.UserInfo operatorModel = this.SiteUserInfo;
            int[] AreaIds={};
            string UserId = EyouSoft.Common.Utils.GetQueryStringValue("UserId");            
            if (!string.IsNullOrEmpty(Request.QueryString["UserId"]))
            {
                EyouSoft.Model.CompanyStructure.CompanyUser UserModel = EyouSoft.BLL.CompanyStructure.CompanyUser.CreateInstance().GetModel(UserId);
                if (UserModel != null)
                {
                    List<EyouSoft.Model.SystemStructure.AreaBase> UserHasArea = UserModel.Area;
                    if (UserHasArea != null && UserHasArea.Count > 0)
                    {
                        AreaIds = UserHasArea.Select(p => p.AreaId).ToArray<int>();
                    }
                }
            }
            else {
                AreaIds = operatorModel.AreaId;
            }            
            if (operatorModel != null)
            {                
                List<EyouSoft.Model.SystemStructure.SysArea> AreaList = (List<EyouSoft.Model.SystemStructure.SysArea>)EyouSoft.BLL.SystemStructure.SysArea.CreateInstance().GetSysAreaList(AreaIds);
                StringBuilder strVal = new StringBuilder();
                if (AreaList != null && AreaList.Count > 0)
                {

                    if (_currentAreaId > 0)  //指定了显示哪个线路区域
                        strVal.Append("<a href=\"javascript:void(0)\" name=a_AreaId  AreaId='-1' class=\"red\">所 有</a>");
                    else
                        strVal.Append("<a href=\"javascript:void(0)\"  name=a_AreaId  AreaId='-1' class=\"red\">所 有</a>&nbsp;");
                    int rowCount = AreaList.Count;   //实际的记录数
                    for (int i = 0; i < rowCount; i++)
                    {
                        int AreaId = AreaList[i].AreaId;
                        string AreaName = AreaList[i].AreaName;
                        if (AreaId != _currentAreaId)
                        {
                            if ((i + 1) % 3 == 0)
                            {
                                strVal.AppendFormat("<a href=\"javascript:void(0)\" name='a_" + AreaId + "' AreaId='{1}'>{0}</a>&nbsp;</br>", AreaName, AreaId);
                            }
                            else
                            {
                                strVal.AppendFormat("<a href=\"javascript:void(0)\"  name='a_" + AreaId + "' AreaId='{1}'>{0}</a>&nbsp;", AreaName, AreaId);
                            }
                        }
                        else
                        {
                            if ((i + 1) % 3 == 0)
                            {
                                strVal.AppendFormat("<a href=\"javascript:void(0)\" name='a_" + AreaId + "' AreaId='{1}''>{0}</a>&nbsp;</br>", AreaName, AreaId);
                            }
                            else
                            {
                                strVal.AppendFormat("<a href=\"javascript:void(0)\" name='a_" + AreaId + "' AreaId='{1}'>{0}</a>&nbsp;", AreaName, AreaId);
                            }
                        }
                    }
                    AreaList = null;
                    Response.Write(strVal.ToString());
                }
                else
                {
                    Response.Clear();
                    Response.Write("[{isSuccess:false,Message:'你当前使用帐号没有经营任何线路区域！'}]");
                    Response.End();                    
                }                
            }
        }
        #endregion
    }
}
