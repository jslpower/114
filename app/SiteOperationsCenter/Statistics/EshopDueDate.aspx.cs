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
using EyouSoft.Common.Function;

namespace SiteOperationsCenter.Statistics
{
    /// <summary>
    /// 高级网店到期审核
    /// 创建时间：2010-9-19   袁惠
    /// </summary>
    public partial class EshopDueDate : EyouSoft.Common.Control.YunYingPage
    {
        public string NowDate = System.DateTime.Now.ToString("yyyy-MM-dd");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                #region 权限判断
                if (!CheckMasterGrant(YuYingPermission.统计分析_管理该栏目, YuYingPermission.统计分析_高级网店到期统计))
                {
                    Utils.ResponseNoPermit(YuYingPermission.统计分析_高级网店到期统计, true);
                    return;
                }
                #endregion
                if (Request.QueryString["Method"] == "method")
                {
                    AddMoneyOpear();    //续费操作
                }
                StartAndEndDate1.SetTitle = "到期时间";
                StartAndEndDate2. SetTitle = "到期时间";
            }
        }

        //续费
        private void AddMoneyOpear()
        {
            string startDate = Request.QueryString["StartDate"];
            string endDate = Request.QueryString["EndDate"];
            string applyId = Utils.InputText(Request.QueryString["ApplyId"]);
            if(!(StringValidate.IsDateTime(startDate) && StringValidate.IsDateTime(endDate)))
            {
                Utils.ResponseMeg(false, "到期时间格式错误");
                return;
            }
            if (Convert.ToDateTime(startDate)>Convert.ToDateTime(endDate))
            {
                Utils.ResponseMeg(false, "续费时间不对");
                return;
            }
            int result=EyouSoft.BLL.SystemStructure.SysApplyService.CreateInstance().EshopRenewed(applyId, Convert.ToDateTime(startDate), Convert.ToDateTime(endDate), this.MasterUserInfo.ID, DateTime.Now);
            if (result == 1)
                Utils.ResponseMeg(true, "操作成功");
            else if(result==2)
                Utils.ResponseMeg(false,"续费时间不对");
            else
                Utils.ResponseMeg(false, "操作失败");
        }
    }
}
