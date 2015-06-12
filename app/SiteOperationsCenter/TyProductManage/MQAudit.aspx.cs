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
namespace SiteOperationsCenter.TyProductManage
{   
    /// <summary>
    /// 页面功能：运营后台审核MQ
    /// 开发时间：2010-07-23 开发人：xuty
    /// </summary>
    public partial class MQAudit : EyouSoft.Common.Control.YunYingPage
    {  
        protected string checkPeople;
        protected string checkDate;
        protected bool haveUpdate = true;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CheckMasterGrant(YuYingPermission.企业MQ申请审核_管理该栏目))
            {
                Utils.ResponseNoPermit(YuYingPermission.企业MQ申请审核_管理该栏目, true);
                return;
            }
            if(!CheckMasterGrant(YuYingPermission.企业MQ申请审核_管理该栏目,YuYingPermission.企业MQ申请审核_审核))
            {
                haveUpdate = false;
            }
            string method = Utils.GetQueryStringValue("method");
            checkDate = DateTime.Now.ToString("yyyy-MM-dd");
            checkPeople = MasterUserInfo.ContactName;
            if (method == "audit")
            {
                if (!haveUpdate)
                {
                    Utils.ResponseNoPermit(YuYingPermission.企业MQ申请审核_审核, true);
                    return;
                }
                AuditMQ();//审核MQ
            }
        }

        #region 审核MQ
        protected void AuditMQ()
        {
            int applyState=Utils.GetInt(Utils.GetQueryStringValue("applystate"));
            EyouSoft.Model.SystemStructure.MQCheckInfo mqCheckModel=new EyouSoft.Model.SystemStructure.MQCheckInfo();
            mqCheckModel.ApplyId=Utils.GetQueryStringValue("id");
            if (applyState != 3)
            {
                mqCheckModel.ApplyState = (EyouSoft.Model.SystemStructure.ApplyServiceState)applyState;
                mqCheckModel.CheckTime = DateTime.Now;
                mqCheckModel.OperatorId = MasterUserInfo.ID;
            }
            else
            {
                mqCheckModel.ApplyState = EyouSoft.Model.SystemStructure.ApplyServiceState.审核通过;
                mqCheckModel.CheckTime = Utils.GetDateTime(Request.QueryString["checkDate"], DateTime.Now);
                mqCheckModel.OperatorId = Utils.GetInt(Request.QueryString["operatorId"]);
            }
            mqCheckModel.EnableTime=Utils.GetDateTime(Request.QueryString["enableDate"], DateTime.Now);
            mqCheckModel.ExpireTime=Utils.GetDateTime(Request.QueryString["expireDate"], DateTime.Now);
            mqCheckModel.SubAccountNumber=Utils.GetInt(Request.QueryString["sonUserNum"]);
            if (EyouSoft.BLL.SystemStructure.SysApplyService.CreateInstance().MQChecked(mqCheckModel) > 0)
            {
                Utils.ResponseMegSuccess();
            }
            else
            {
                Utils.ResponseMegError();
            }
        }
        #endregion 
    }
}
