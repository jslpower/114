using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Control;
namespace SiteOperationsCenter.Statistics
{   
    /// <summary>
    /// 页面功能：ajax获取MQ续费的列表
    /// 开发时间：2010-09-20 开发人:xuty
    /// </summary>
    public partial class AjaxMqReNew : EyouSoft.Common.Control.YunYingPage
    {
        protected int pageIndex;
        protected int pageSize = 20;
        protected int recordCount;
        protected int itemIndex = 1;
        protected EyouSoft.IBLL.SystemStructure.ISystemUser userBll;
        protected void Page_Load(object sender, EventArgs e)
        {
            //判断权限
            if (!CheckMasterGrant(YuYingPermission.统计分析_管理该栏目,YuYingPermission.统计分析_收费MQ到期统计))
            {
                Utils.ResponseNoPermit(YuYingPermission.统计分析_收费MQ到期统计, true);
                return;
            }
            userBll = EyouSoft.BLL.SystemStructure.SystemUser.CreateInstance();
            pageIndex = Utils.GetInt(Request.QueryString["Page"], 1);//获取当前页
            itemIndex = (pageIndex - 1) * pageSize + 1;
            int pId = Utils.GetInt(Request.QueryString["province"]);//省份
            int cId = Utils.GetInt(Request.QueryString["city"]);//城市
            int? provinceId = pId == 0 ? new Nullable<int>() : pId;
            int? cityId = cId == 0 ? new Nullable<int>() : cId;
            string companyName = Utils.GetQueryStringValue("companyname");//公司名称
            DateTime? overSDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("oversdate"));//到期开始时间
            DateTime? overEDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("overedate"));//到期结束时间
            //绑定数据
            IList<EyouSoft.Model.SystemStructure.SysApplyServiceInfo> serviceList = EyouSoft.BLL.SystemStructure.SysApplyService.CreateInstance().GetComingExpireApplys(pageSize, pageIndex, ref recordCount, EyouSoft.Model.CompanyStructure.SysService.MQ, provinceId, cityId, companyName, overSDate, overEDate);
            if (serviceList != null && serviceList.Count > 0)
            {
                ama_rpt_applyList.DataSource = serviceList;
                ama_rpt_applyList.DataBind();
                BindPage();
            }
            else
            {
                this.ExporPageInfoSelect1.Visible = false;
                ama_rpt_applyList.EmptyText = "暂无MQ到期信息";
            }

        }
        //设置分页控件
        protected void BindPage()
        {
            this.ExporPageInfoSelect1.intPageSize = pageSize;
            this.ExporPageInfoSelect1.intRecordCount = recordCount;
            this.ExporPageInfoSelect1.CurrencyPage = pageIndex;
            this.ExporPageInfoSelect1.HrefType = Adpost.Common.ExporPage.HrefTypeEnum.JsHref;
            this.ExporPageInfoSelect1.AttributesEventAdd("onclick", "MQReNew.loadData(this);", 1);
            this.ExporPageInfoSelect1.AttributesEventAdd("onchange", "MQReNew.loadData(this);", 0);
        }
        /// <summary>
        /// 绑定列表的时候，根据ID编号获取审核人姓名
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
             }
            return userName;
        }
      
        
    }
}
