using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace SiteOperationsCenter.IM
{
    public partial class SetGroupMember : EyouSoft.Common.Control.YunYingPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int requestType = Utils.GetInt(Utils.GetFormValue("type"), 0);
            if (requestType == 1)
            {
                this.AdjustGroupMember();
            }
            else if (requestType == 2)
            {
                this.GetGroupMember();
            }
        }

        #region private members
        /// <summary>
        /// 设置MQ群人数 
        /// </summary>
        private void AdjustGroupMember()
        {
            //群号
            int groupId = Utils.GetInt(Request.Form["txtGroupId"], 0);
            //群人数
            int maxMember = Utils.GetInt(Request.Form["txtMaxMember"], 0);

            if (groupId < 100)
            {
                this.ResponseWriteMessage(false, "请输入正确的群号！");
            }

            if (maxMember < 100 || maxMember > 900)
            {
                this.ResponseWriteMessage(false, "请输入正确的群人数！群人数不能小于100，大于900。");
            }

            if (EyouSoft.BLL.MQStructure.IMMember.CreateInstance().SetGroupMember(groupId, maxMember) > 0)
            {
                this.ResponseWriteMessage(true, "设置成功！");
            }
            else
            {
                this.ResponseWriteMessage(true, "设置失败！");
            }

        }

        /// <summary>
        /// 获取MQ群人数 
        /// </summary>
        private void GetGroupMember()
        {
            //群号
            int groupId = Utils.GetInt(Request.Form["txtGroupId"], 0);

            if (groupId < 100)
            {
                this.ResponseWriteMessage(false, "请输入正确的群号！");
            }

            int number = EyouSoft.BLL.MQStructure.IMMember.CreateInstance().GetGroupMember(groupId);

            this.ResponseWriteMessage(true, number.ToString());
        }
        
        /// <summary>
        /// 输出提示信息
        /// </summary>
        /// <param name="isSuccess">是否成功</param>
        /// <param name="s">提示内容</param>
        private void ResponseWriteMessage(bool isSuccess,string s)
        {
            var responseText = "{{\"isSuccess\":{0},\"msg\":\"{1}\"}}";
            Response.Clear();
            Response.Write(string.Format(responseText, isSuccess ? "true" : "false", s));
            Response.End();
        }
        #endregion
    }
}
