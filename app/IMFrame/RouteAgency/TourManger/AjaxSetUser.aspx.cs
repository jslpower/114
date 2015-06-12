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

namespace IMFrame.RouteAgency.TourManger
{
    /// <summary>
    /// AJAX设置MQ消息提醒的用户
    /// zhangzy   2010-8-14
    /// </summary>
    public partial class AjaxSetUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string CompanyId = Request.QueryString["CompanyId"];
                int AreaId = EyouSoft.Common.Function.StringValidate.GetIntValue(Request.QueryString["AreaId"]);
                string UserId = Request.QueryString["UserId"];
                string IsCheck = Request.QueryString["IsCheck"];       //0:取消设置   1:设置           

                if (AreaId == 0 || string.IsNullOrEmpty(CompanyId) || string.IsNullOrEmpty(UserId) || string.IsNullOrEmpty(IsCheck))
                {
                    Response.Write("0");  //0:操作失败  1:操作成功
                    Response.Flush();
                    Response.End();
                    return;
                }
                EyouSoft.Model.MQStructure.IMAcceptMsgPeople model = new EyouSoft.Model.MQStructure.IMAcceptMsgPeople();
                model.CompanyId = CompanyId.Trim();
                model.OperatorId = UserId.Trim();
                model.TourAreaId = AreaId;

                string returnVal = "0";
                if (IsCheck == "1")  //表示为设置
                    returnVal = EyouSoft.BLL.MQStructure.IMAcceptMsgPeople.CreateInstance().Add(model) == true ? "1" : "0";
                else  //取消设置
                    returnVal = EyouSoft.BLL.MQStructure.IMAcceptMsgPeople.CreateInstance().Delete(CompanyId, UserId, AreaId) == true ? "1" : "0";

                Response.Write(returnVal);
                Response.Flush();
                Response.End();
            }
        }
    }

}
