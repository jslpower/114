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
using EyouSoft.Common.Control;
namespace SiteOperationsCenter.TyProductManage
{
    public partial class AjaxMQAduit : EyouSoft.Common.Control.YunYingPage
    {
        protected int pageIndex;
        protected int pageSize=15;
        protected int recordCount;
        protected int itemIndex = 1;
        protected EyouSoft.IBLL.SystemStructure.ISystemUser userBll;
        protected void Page_Load(object sender, EventArgs e)
        {   
          
            if (!CheckMasterGrant(YuYingPermission.企业MQ申请审核_管理该栏目))
            {
                Utils.ResponseNoPermit(YuYingPermission.企业MQ申请审核_管理该栏目, true);
                return;
            }
            userBll = EyouSoft.BLL.SystemStructure.SystemUser.CreateInstance();
            pageIndex = Utils.GetInt(Request.QueryString["Page"], 1);
            itemIndex = (pageIndex - 1) * pageSize + 1;
            int pId = Utils.GetInt(Utils.GetQueryStringValue("province"));
            int cId = Utils.GetInt(Utils.GetQueryStringValue("city"));
            int? provinceId = pId == 0 ? new Nullable<int>() : pId;
            int? cityId=cId==0?new Nullable<int>():cId;
            string companyName=Utils.GetQueryStringValue("companyname");
            string state=Utils.GetQueryStringValue("state");
            DateTime? applyStartDate=Utils.GetDateTimeNullable(Utils.GetQueryStringValue("applysdate"));
            DateTime? applyFinishDate=Utils.GetDateTimeNullable(Utils.GetQueryStringValue("applyfdate"));
            EyouSoft.Model.SystemStructure.ApplyServiceState? applyState=state==""?new Nullable<EyouSoft.Model.SystemStructure.ApplyServiceState>():(EyouSoft.Model.SystemStructure.ApplyServiceState)(Utils.GetInt(state));
            IList<EyouSoft.Model.SystemStructure.SysApplyServiceInfo> serviceList = EyouSoft.BLL.SystemStructure.SysApplyService.CreateInstance().GetApplys(pageSize, pageIndex, ref recordCount, EyouSoft.Model.CompanyStructure.SysService.MQ, provinceId, cityId, companyName, applyStartDate, applyFinishDate, applyState);
            if (serviceList != null && serviceList.Count > 0)
            {
                ama_rpt_applyList.DataSource = serviceList;
                ama_rpt_applyList.DataBind();
                BindPage();
            }
            else
            {
                ama_rpt_applyList.EmptyText = "暂无MQ审核信息";
            }
           
        }
        protected void BindPage()
        {
            this.ExporPageInfoSelect1.intPageSize = pageSize;
            this.ExporPageInfoSelect1.intRecordCount = recordCount;
            this.ExporPageInfoSelect1.CurrencyPage = pageIndex;
            this.ExporPageInfoSelect1.HrefType = Adpost.Common.ExporPage.HrefTypeEnum.JsHref;
            this.ExporPageInfoSelect1.AttributesEventAdd("onclick", "MQAudit.loadData(this);", 1);
            this.ExporPageInfoSelect1.AttributesEventAdd("onchange", "MQAudit.loadData(this);", 0);
        }
        protected string GetSysUserName(int id)
        {
            string userName = MasterUserInfo.ContactName;
            if (id != 0)
            {
                EyouSoft.Model.SystemStructure.SystemUser userModel = userBll.GetSystemUserModel(id);
                if (userModel != null)
                {
                    userName = userModel.ContactName;
                }
                userModel = null;
            }
            return userName;
        }
      
    }
}
