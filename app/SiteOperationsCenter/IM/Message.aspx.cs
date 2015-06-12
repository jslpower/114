using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace SiteOperationsCenter.IM
{
    /// <summary>
    /// 给客户发送MQ消息
    /// </summary>
    /// Author:汪奇志 2010-11-24
    public partial class Message : EyouSoft.Common.Control.YunYingPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utils.GetInt(Utils.GetFormValue("type")) == 1)//发送消息
            {
                this.SendMessage();
            }
            else
            {
                if (!this.CheckMasterGrant(YuYingPermission.给客户发送MQ消息))
                {
                    Utils.ResponseNoPermit(YuYingPermission.给客户发送MQ消息, false);
                    return;
                }
            }
        }

        /// <summary>
        ///  发送消息
        /// </summary>
        private void SendMessage()
        {
            
            int companyType = Utils.GetInt(Utils.GetFormValue("companyType"), 1);
            string companyTypes = Utils.GetFormValue("companyTypes");

            EyouSoft.Model.MQStructure.OneToManyUnspecifiedMQMessageInfo info = new EyouSoft.Model.MQStructure.OneToManyUnspecifiedMQMessageInfo();

            info.SendType = (EyouSoft.Model.MQStructure.SendType)Utils.GetInt(Utils.GetFormValue("sendType"), 1);
            info.SendMQId = Utils.GetInt(Utils.GetFormValue("sendMQId"), 0);
            info.AcceptProvinceId = Utils.GetInt(Utils.GetFormValue("provinceId"), 0);
            info.AcceptCityId = Utils.GetInt(Utils.GetFormValue("cityId"), 0);            
            info.OnlineState= (EyouSoft.Model.MQStructure.OnlineState)Utils.GetInt(Utils.GetFormValue("onlineState"), 0);
            info.Message = Utils.GetFormValue("message");

            info.IsAllCompanyType = true;
            info.AcceptCompanyType = new List<EyouSoft.Model.CompanyStructure.CompanyType>();

            #region 表单验证
            if (!this.CheckMasterGrant(YuYingPermission.给客户发送MQ消息))
            {
                this.SendMessageResponseWrite(false, "你没有给客户发送MQ消息权限，请联系管理员", 0);
            }

            if (companyType == 2)
            {
                info.IsAllCompanyType = false;

                if (string.IsNullOrEmpty(companyTypes))
                {
                    this.SendMessageResponseWrite(false, "请选择指定的客户类型", 0);
                }

                foreach (string s in companyTypes.Split(','))
                {
                    if (s == "-1") { info.IsContainsGuest = true; continue; }

                    info.AcceptCompanyType.Add((EyouSoft.Model.CompanyStructure.CompanyType)int.Parse(s));
                }
            }

            if (info.SendType == EyouSoft.Model.MQStructure.SendType.聊天窗口 && info.SendMQId == 0)
            {
                this.SendMessageResponseWrite(false, "请输入指定的用户MQID", 0);
            }

            if (string.IsNullOrEmpty(info.Message))
            {
                this.SendMessageResponseWrite(false, "请输入消息内容", 0);
            }
            #endregion

            int sendResult = EyouSoft.BLL.MQStructure.IMMessage.CreateInstance().SendOneToManyMessage(info);

            if (sendResult >= 0)
            {
                this.SendMessageResponseWrite(true, "", sendResult);
            }
            else
            {
                this.SendMessageResponseWrite(false, "发送错误", 0);
            }
        }

        /// <summary>
        /// 发送消息ResponseWrite
        /// </summary>
        /// <param name="isSuccess"></param>
        /// <param name="errorMsg"></param>
        /// <param name="successCount"></param>
        private void SendMessageResponseWrite(bool isSuccess, string errorMsg, int successCount)
        {
            var responseText = "{{\"isSuccess\":{0},\"errorMsg\":\"{1}\",\"successCount\":{2}}}";
            Response.Clear();
            Response.Write(string.Format(responseText, isSuccess ? "true" : "false", errorMsg, successCount));
            Response.End();
        }
    }
}
