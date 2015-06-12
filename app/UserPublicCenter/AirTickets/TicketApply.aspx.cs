using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
namespace UserPublicCenter.AirTickets
{
    public partial class TicketApply : EyouSoft.Common.Control.FrontPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            (this.Master as UserPublicCenter.MasterPage.NewPublicCenter).HeadMenuIndex = 3;
            Utils.AddStylesheetInclude(CssManage.GetCssFilePath("index2011"));
            Utils.AddStylesheetInclude(CssManage.GetCssFilePath("InformationStyle"));

            if (Utils.GetFormValue("hidSave")=="save")
            {   
                EyouSoft.Model.TicketStructure.GroupTickets model=new EyouSoft.Model.TicketStructure.GroupTickets();
                int airType = Utils.GetInt(Request.Form["selAirCompany"]);//获取航空公司
                if (airType > 0)
                {
                    model.AirlinesType = (EyouSoft.Model.TicketStructure.AirlinesType)airType;
                }
                model.Contact = Utils.GetFormValue("txtContacter");
                model.ContactQQ = Utils.GetFormValue("txtQQ");
                model.Email = Utils.GetFormValue("txtEmail");
                model.EndCity = Utils.GetFormValue("txtEndCity");
                model.FlightNumber = Utils.GetFormValue("txtNo");
                model.GroupType = (EyouSoft.Model.TicketStructure.GroupType)Utils.GetInt(Request.Form["rdiType"], 1);
                model.HopesPrice = Utils.GetDecimal(Request.Form["txtPrice"]);
                model.IssueTime = DateTime.Now;
                model.Notes = Utils.GetFormValue("txtReamrk", 500);
                model.PelopeCount = Utils.GetInt(Request.Form["txtNum"]);
                if (model.PelopeCount < 10)
                {
                    Utils.ShowAndRedirect("预订失败，人数不正确！", this.Request.Url.ToString());
                }
                model.Phone = Utils.GetFormValue("txtTel");
                model.StartCity = Utils.GetFormValue("txtStartCity");
                model.StartTime = Utils.GetFormValue("txtStartDate");
                model.TimeRange = Utils.GetFormValue("selSTime") + "—" + Utils.GetFormValue("selETime");
                string[] names=Utils.GetFormValues("txtName");
                string[] pTypes = Utils.GetFormValues("selpType");
                string[] cTypes = Utils.GetFormValues("selcType");
                string[] cNos = Utils.GetFormValues("txtcNo");
                string[] mobiles = Utils.GetFormValues("txtMobile");
                if (names != null && names.Length > 0)
                {  
                    IList<EyouSoft.Model.TicketStructure.PassengerInformation> passengerList=new List<EyouSoft.Model.TicketStructure.PassengerInformation>();
                    for (int i = 0; i < names.Length; i++)
                    {
                        EyouSoft.Model.TicketStructure.PassengerInformation passenger=new EyouSoft.Model.TicketStructure.PassengerInformation();
                        passenger.DocumentNo=cNos[i];
                        passenger.DocumentType = (EyouSoft.Model.TicketStructure.DocumentType)Utils.GetInt(cTypes[i], 1);
                        passenger.Mobile = mobiles[i];
                        passenger.PassengerType = (EyouSoft.Model.TicketStructure.PassengerType)Utils.GetInt(pTypes[i], 1);
                        passenger.UName = names[i];
                        passengerList.Add(passenger);
                    }
                    model.PassengerInformationList = passengerList;
                }
                if (EyouSoft.BLL.TicketStructure.GroupTickets.CreateInstance().AddGroupTickets(model))
                {
                    Utils.ShowAndRedirect("预订成功！",this.Request.Url.ToString());
                   
                }
                else
                {
                    Utils.ShowAndRedirect("预订失败！", this.Request.Url.ToString());
                }
            }
        }
    }
}
