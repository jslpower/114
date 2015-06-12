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

namespace UserBackCenter.usercontrol
{
    public partial class MemoCalendar : System.Web.UI.UserControl
    {
        private IList<EyouSoft.Model.ToolStructure.CompanyDayMemo> list = null;
        protected string ImageServerPath = EyouSoft.Common.ImageManage.GetImagerServerUrl(1);
        protected string tblID = "MemoCalendar";
        private bool _isbackdefault = false;
        public bool IsBackDefault
        {
            get { return _isbackdefault; }
            set { _isbackdefault = value; }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            tblID = "MemoCalendar" + Guid.NewGuid().ToString();
            if (!Page.IsPostBack)
            {
                this.CalendarDate.NextPrevFormat = NextPrevFormat.CustomText;
                this.CalendarDate.VisibleDate = DateTime.Now;
                this.lblVisibleDate.Text = this.CalendarDate.VisibleDate.ToString("yyyy年MM月");
                this.CalendarDate.NextMonthText = "<a id='NextMonth' href='javascript:void(0);' onclick=\"ChangeMonth('next','" + this.CalendarDate.VisibleDate + "','"+ tblID +"');\">下一月</a>";
                this.CalendarDate.PrevMonthText = "<a id='PrevMonth' href='javascript:void(0);' onclick=\"ChangeMonth('prev','" + this.CalendarDate.VisibleDate + "','" + tblID + "');\">上一月</a>";
                if (IsBackDefault)
                {
                    this.CalendarDate.DayStyle.Height = Unit.Percentage(12);
                }
            }
        }

        protected void CalendarDate_DayRender(object sender, DayRenderEventArgs e)
        {
            if (!e.Day.IsOtherMonth)
            {
                if (e.Day.Date.ToString("yyyy-MM-dd") != DateTime.Now.ToString("yyyy-MM-dd"))
                {
                    e.Cell.Attributes.Add("onmouseover", "this.bgColor='#E4FFE4'");
                    e.Cell.Attributes.Add("onmouseout", "this.bgColor=''");
                }
                else
                {
                    e.Cell.Attributes.Add("onmouseover", "this.bgColor='#E3EFFF'");
                    e.Cell.Attributes.Add("onmouseout", "this.bgColor=''");
                }

                int year = e.Day.Date.Year;
                int month = e.Day.Date.Month;

                DateTime BeginDate = e.Day.Date.AddDays(-(e.Day.Date.Day) + 1);
                DateTime EndDate = e.Day.Date.AddMonths(1).AddDays(-(e.Day.Date.Day));
                if (list == null)
                {
                    EyouSoft.Common.Control.BasePage page = (EyouSoft.Common.Control.BasePage)Page;
                    list = EyouSoft.BLL.ToolStructure.CompanyDayMemo.CreateInstance().GetList(page.SiteUserInfo.CompanyID, BeginDate, EndDate);
                }
                if (list != null)
                {
                    string str = string.Empty;
                    int count = 0, UrgentCount = 0;
                    string tmp = string.Empty;
                    foreach (EyouSoft.Model.ToolStructure.CompanyDayMemo model in list)
                    {
                        if (e.Day.Date == model.MemoTime)
                        {
                            if (model.UrgentType == EyouSoft.Model.ToolStructure.MemoDetailType.UrgentType.Urgent)
                            {
                                UrgentCount++;
                            }
                            if (count < 3)
                            {
                                str += "•<a href='/Memorandum/AddMemorandum.aspx' onclick=\"topTab.open($(this).attr('href'),'修改备忘录',{isRefresh:false,data:{MemId:'" + model.ID + "',UpPage:0}});return false;\" title='" + model.MemoTitle + "' style='cursor:pointer;'>" + Utils.GetText(model.MemoTitle, 8, true) + "</a><br/>";
                            }
                            count++;
                        }
                    }
                    if (count > 0)
                    {
                        if (UrgentCount > 0)
                        {
                            tmp = "<a href='/Memorandum/MemorandumList.aspx' onclick=\"topTab.open($(this).attr('href'),'备忘录列表',{isRefresh:false,data:{FromDate:'" + e.Day.Date + "'}});return false;\" style='cursor:pointer;' class='urgent'><span style=\"color:#aaaaaa;\">" + e.Day.Date.Day + "(共" + count + "记录)</span>紧急" + UrgentCount + "</a>";
                        }
                        else
                        {
                            tmp = "<a href='/Memorandum/MemorandumList.aspx' onclick=\"topTab.open($(this).attr('href'),'备忘录列表',{isRefresh:false,data:{FromDate:'" + e.Day.Date + "'}});return false;\" style='cursor:pointer;' class='common'><span style=\"color:#aaaaaa;\">" + e.Day.Date.Day + "(共" + count + "记录)</span></a>";
                        }
                        if (IsBackDefault)
                        {
                            e.Cell.Text = tmp;
                        }
                        else
                        {
                            e.Cell.Text = tmp + str.ToString();
                        }
                        e.Cell.HorizontalAlign = HorizontalAlign.Left;
                        e.Cell.VerticalAlign = VerticalAlign.Top;
                    }
                    else
                    {
                        e.Cell.Attributes.Add("onclick", "topTab.open('/Memorandum/AddMemorandum.aspx','添加备忘录',{isRefresh:false,data:{UpPage:0,DefaultDay:'"+ e.Day.Date +"'}});return false;");
                    }
                }
            }
            else
            {
                e.Cell.Text = string.Empty;
            }
        }
    }
}