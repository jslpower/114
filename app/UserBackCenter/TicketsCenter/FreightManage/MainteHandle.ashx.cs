using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserBackCenter.TicketsCenter.FreightManage
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>

    public class MainteHandle : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string id = context.Request.QueryString["id"];
            string state = context.Request.QueryString["state"];
            string companyId = context.Request.QueryString["cid"];
            string buyId = context.Request.QueryString["BuyId"];
            if (id != null)
            {
                bool isEnabled = false;
                //是否过期
                bool isGq = false;
                if (state != null && companyId != null && companyId.ToString() != "")
                {
                    int count = EyouSoft.BLL.TicketStructure.FreightBuyLog.CreateInstance().GetAvailableCount(companyId, EyouSoft.Model.TicketStructure.RateType.团队散拼);

                    EyouSoft.Model.TicketStructure.TicketFreightInfo model = EyouSoft.BLL.TicketStructure.TicketFreightInfo.CreateInstance().GetModel(id);
                    if (state == "1")
                    {
                        isEnabled = true;
                        if (1 > count && model != null && Convert.ToInt32(model.BuyType) == 1)
                        {
                            context.Response.Write("当月无可用运价数!");
                            return;
                        }

                        isGq = EyouSoft.BLL.TicketStructure.TicketFreightInfo.CreateInstance().SetIsEnabled(model.Id, isEnabled);
                    }
                    else
                    {
                        isGq = EyouSoft.BLL.TicketStructure.TicketFreightInfo.CreateInstance().SetIsEnabled(model.Id, isEnabled);
                    }
                }
                else
                {
                    context.Response.Write("ERROR");
                    return;
                }

                if (!isGq)
                {
                    context.Response.Write("该运价已过期，不能启用!");
                    return;
                }

                bool result = EyouSoft.BLL.TicketStructure.TicketFreightInfo.CreateInstance().SetIsEnabled(id, isEnabled);
                if (result)
                {
                    context.Response.Write("OK");
                }
                else
                {
                    context.Response.Write("ERROR");
                }
            }

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
