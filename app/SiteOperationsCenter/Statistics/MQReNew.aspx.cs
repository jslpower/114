using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
namespace SiteOperationsCenter.Statistics
{   
   /// <summary>
   /// 页面功能：MQ续费
   /// 开发时间：2010-09-20 开发人：xuty
   /// </summary>
    public partial class MQReNew : EyouSoft.Common.Control.YunYingPage
    {
     
        protected bool haveUpdate = true;//是否有权限续费
        protected void Page_Load(object sender, EventArgs e)
        {   
            //判断权限
            if (!CheckMasterGrant(YuYingPermission.统计分析_管理该栏目,YuYingPermission.统计分析_收费MQ到期统计))
            {   
                Utils.ResponseNoPermit(YuYingPermission.统计分析_收费MQ到期统计, true);
                return;
            }
            mq_date.SetTitle = "到期时间";//设置时间控件的标题
            string method = Utils.GetQueryStringValue("method");
            if (method == "reNew")//判断是否执行续费操作
            {
              ReNewMQ();//mq续费
            }
        }

        #region MQ续费
        protected void ReNewMQ()
        {
            
            EyouSoft.Model.SystemStructure.MQCheckInfo mqCheckModel = new EyouSoft.Model.SystemStructure.MQCheckInfo();
            mqCheckModel.ApplyId = Utils.GetQueryStringValue("id");
            mqCheckModel.ApplyState = EyouSoft.Model.SystemStructure.ApplyServiceState.审核通过;
            string enableDate=Request.QueryString["enableDate"];
            string expireDate=Request.QueryString["expireDate"];
            string sonUserNum=Request.QueryString["sonUserNum"];
            if (!(EyouSoft.Common.Function.StringValidate.IsDateTime(enableDate) && EyouSoft.Common.Function.StringValidate.IsDateTime(expireDate) && EyouSoft.Common.Function.StringValidate.IsInteger(sonUserNum)))
            {
                Utils.ResponseMeg(false, "子账户数或日期格式不对!");
                return;
            }
            mqCheckModel.EnableTime = Utils.GetDateTime(enableDate, DateTime.Now);
            mqCheckModel.ExpireTime = Utils.GetDateTime(expireDate, DateTime.Now);
            mqCheckModel.SubAccountNumber = Utils.GetInt(sonUserNum);
            mqCheckModel.OperatorId = MasterUserInfo.ID;
            mqCheckModel.CheckTime = DateTime.Now;
            EyouSoft.IBLL.SystemStructure.ISysApplyService serviceBll=EyouSoft.BLL.SystemStructure.SysApplyService.CreateInstance();
            //执行续费操作
            int result = serviceBll.MQRenewed(mqCheckModel.ApplyId, mqCheckModel.EnableTime, mqCheckModel.ExpireTime, mqCheckModel.OperatorId, mqCheckModel.CheckTime, mqCheckModel.SubAccountNumber);
            if(result==1)
             {
                 Utils.ResponseMegSuccess();
             }
             else if(result==2)
             {
                 Utils.ResponseMeg(false,"续费时间不对");
             }
             else
             {
                 Utils.ResponseMegError();
             }
           
        }
        #endregion
    }
}

