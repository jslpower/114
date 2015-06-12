using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EyouSoft.Common;
using EyouSoft.Common.Control;
using System.Text;
namespace UserPublicCenter.AirTickets.TeamBook
{
    /// <summary>
    /// 获取机场
    /// </summary>
     public class GetSeattles : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            EyouSoft.IBLL.TicketStructure.ITicketSeattle seattleBll=EyouSoft.BLL.TicketStructure.TicketSeattle.CreateInstance();
            int?seattleId=Utils.GetIntNull(Utils.GetQueryStringValue("seattleId"));
            bool isSelect = Utils.GetQueryStringValue("isSelect")=="isSelect";
            int selectId=Utils.GetInt(Utils.GetQueryStringValue("selectId"));

            IList<EyouSoft.Model.TicketStructure.TicketSeattle> seattleList = seattleBll.GetTicketSeattleByFreight(seattleId);
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append("<option value=\"0\" >请选择城市</option>");
            if (seattleList != null && seattleList.Count > 0)
            {
                foreach (EyouSoft.Model.TicketStructure.TicketSeattle seat in seattleList)
                {
                    strBuilder.AppendFormat("<option value=\"{0}\" {3}>{1}({2})</option>", seat.SeattleId, seat.Seattle,seat.LKE,(isSelect&&selectId==seat.SeattleId)?"selected=\"selected\"":"");
                }
            }
            context.Response.Clear();
            context.Response.Write(strBuilder.ToString());
            context.Response.End();
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
