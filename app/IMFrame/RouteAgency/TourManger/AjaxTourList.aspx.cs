using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace TourUnion.WEB.IM.RouteAgency.TourManger
{
    /// <summary>
    /// 页面功能:ajax调用批发商的团队列表
    /// 章已泉 2009-9-10
    /// </summary>
    public partial class AjaxTourList : System.Web.UI.Page
    {
        private int UnionId = 0;
        private int CompanyId = 0;
        private int OperatorId = 0;
        private int AreaId = 0;
        private bool isLookAll = false;
        private bool isUpdate = false;   //修改权限
        private bool isDelete = false;   //删除权限
        protected string strValue = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            TourUnion.Account.Model.UserManage usermanage = TourUnion.Account.Factory.UserFactory.GetUserManage(TourUnion.Account.Enum.ChildSystemLocation.RouteAgency, TourUnion.Account.Enum.SystemMedia.MQ);
            TourUnion.Account.Model.Account operatorModel = usermanage.AccountUser;
            if (operatorModel != null)
            {
                UnionId = operatorModel.UnionId;
                CompanyId = operatorModel.CompanyId;
            }


            if (usermanage.IsGrant("271"))  //查看所有
                isLookAll = true;
            if (usermanage.IsGrant("244"))  //修改
                isUpdate = true;
            if (usermanage.IsGrant("245"))  //删除
                isDelete = true;
            usermanage = null;
            AreaId = WEB.ProceFlow.ValidatorValueManage.GetIntValue(Request.QueryString["AreaId"]);


            if (isLookAll)
            {
                OperatorId = WEB.ProceFlow.ValidatorValueManage.GetIntValue(Request.QueryString["OperatorId"]);
            }
            else
            {
                OperatorId = operatorModel.Id;
            }
            operatorModel = null;
            if (!IsPostBack)
            {
                InitData();
            }
        }

        private void InitData()
        {
            TourUnion.BLL.TourUnion_TourList bll = new TourUnion.BLL.TourUnion_TourList();
            DataSet ds = bll.GetTourInAreaCS(UnionId, CompanyId, OperatorId, AreaId, isLookAll);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    #region 判断checkbox
                    string Id = row["Id"].ToString();
                    string isStopCheck = "  disabled='disabled' ";  //是否禁用checkbox
                    int createOperatorId = Convert.ToInt32(row["OperatorId"]);  //获得创建该团的创建人
                    int createCompanyId = Convert.ToInt32(row["CompanyId"]);  //获得创建该团的公司
                    //是自己公司以及自己添加的则可以进行操作
                    //待确认  有修改删除权限是否允许操作 任何团队  对应到已出团 未出团 的子团查看 模版团查看
                    //修改删除自己的团队是否需要权限控制
                    if ((OperatorId == createOperatorId || isDelete || isUpdate) && CompanyId == createCompanyId)
                    {
                        isStopCheck = "";
                    }

                    string strCheckBox = "<input " + isStopCheck + "  name='cbkModelTour' type='checkbox' value='TourId_" + Id + "'/>";
                    #endregion

                    strValue += string.Format("<li><a href='javascript:void(0)' title='{0}'>{1}{2}</a></li>", row["RouteName"].ToString(), strCheckBox, TourUnion.WEB.ProceFlow.StringManage.Substring(row["RouteName"].ToString(), 28));
                }
            }
            bll = null;
        }
    }
}
