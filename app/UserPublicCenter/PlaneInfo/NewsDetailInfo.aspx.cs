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
using EyouSoft.Common.DataProtection;
using System.Text;
using System.Collections.Generic;
using EyouSoft.Common.Function;

namespace UserPublicCenter.PlaneInfo
{
    /// <summary>
    /// 页面功能：机票——机票详细信息页
    /// 开发人：杜桂云      开发时间：2010-07-23
    /// </summary>
    public partial class NewsDetailInfo : EyouSoft.Common.Control.FrontPage
    {
        #region 成员变量
        protected string titleName = "";   //页面头标题前缀
        protected int TypeID = 0;//0-运价参考；1-帮助信息；2-合作供应商
        protected int NewsID = 0;
        #endregion

        #region 页面加载
        protected void Page_Load(object sender, EventArgs e)
        {
            string titleInfo = "";
            string url = "";
            string url2 = "";

            if (Utils.GetFormValue("method") == "save")   //航班折扣申请
            {
                SavePlaneInfo();
                return;
            }

            if (!string.IsNullOrEmpty(Utils.InputText(Request.QueryString["TypeID"])))
            {
                TypeID = Utils.GetInt(Request.QueryString["TypeID"]);
                if (!string.IsNullOrEmpty(Utils.InputText(Request.QueryString["NewsID"])))
                {
                    NewsID = Utils.GetInt(Request.QueryString["NewsID"]);
                    //实例化类的对象,更新浏览次数
                    EyouSoft.BLL.SystemStructure.Affiche.CreateInstance().UpdateReadCount(NewsID);
                    //绑定数据
                    BindListInfo();
                }
                else
                {
                    EyouSoft.Common.Function.MessageBox.ShowAndRedirect(this.Page, "暂无该新闻信息!", "PlaneListPage.aspx?CityId=" + CityId);
                    return;
                }
                url = Utils.GeneratePublicCenterUrl("PlaneListPage.aspx", CityId);                
                switch (TypeID)
                {
                    case 0:
                        titleName += "_运价参考";
                        url2 = Utils.GeneratePublicCenterUrl("PlaneNewsList.aspx?TypeID=0", CityId);
                        titleInfo =string.Format("<a href=\"{0}\">机票首页</a> <a href=\"{1}\"> >运价参考</a> >正文",url,url2);
                        #region 加载航班折扣申请用到的js,css,控件
                        this.AddStylesheetInclude(CssManage.GetCssFilePath("boxy"));
                        this.AddJavaScriptInclude(JsManage.GetJsFilePath("boxy"), true, false);
                        this.AddJavaScriptInclude(JsManage.GetJsFilePath("validatorform"), true, false);
                        divPlaneAppl.Visible = true;
                        #endregion
                        break;
                    case 1:
                        titleName += "_帮助信息";
                        url2 = Utils.GeneratePublicCenterUrl("PlaneNewsList.aspx?TypeID=1", CityId);
                        titleInfo = string.Format("<a href=\"{0}\">机票首页</a> <a href=\"{1}\"> >帮助信息</a> >正文", url, url2);
                        break;
                    default:
                        titleName += "_合作供应商";
                        url2 = Utils.GeneratePublicCenterUrl("PlaneNewsList.aspx?TypeID=2", CityId);
                        titleInfo = string.Format("<a href=\"{0}\">机票首页</a> <a href=\"{1}\"> >合作供应商</a> >正文", url, url2);
                        break;
                }
                this.div_Title.InnerHtml = titleInfo;
                this.CityAndMenu1.HeadMenuIndex = 3;
                this.Page.Title = titleName;
                this.hidNewId.Value = NewsID.ToString();  //文章ID  (用于机票)
                this.hidTitle.Value =titleName;
            }
            else
            {
                //广告连接过来
                if (!string.IsNullOrEmpty(Utils.InputText(Request.QueryString["NewsID"])))
                {
                    int advID = Utils.GetInt(Request.QueryString["NewsID"]);
                    EyouSoft.Model.AdvStructure.AdvInfo advModel = EyouSoft.BLL.AdvStructure.Adv.CreateInstance().GetAdvInfo(advID);
                    if (advModel != null) 
                    {
                        //初始化信息：时间，标题，内容
                        this.lbl_Time.Text = advModel.IssueTime.ToString("yyyy年MM月dd日 hh:mm");
                        this.lbl_Title.Text = advModel.Title;
                        this.lit_Content.Text = advModel.Remark;
                        this.divShow.Visible = false;
                        this.Page.Title = advModel.Title;
                        this.hidNewId.Value = advID.ToString();     //文章ID  (用于机票)
                        this.hidTitle.Value = advModel.Title;
                    }
                    advModel = null;
                }
                else
                {
                    EyouSoft.Common.Function.MessageBox.ShowAndRedirect(this.Page, "暂无该广告信息!", "/Default.aspx?CityId="+CityId);
                    return;
                }
            }
        }
        #endregion

        #region 转换时间显示格式
        protected string GetTime(DateTime times)
        {
            return times.ToString("yyyy年MM月dd日 hh:mm");
        }
        #endregion

        #region 数据绑定
        protected void BindListInfo() 
        {
            string imgUrl = Domain.FileSystem;
            EyouSoft.Model.SystemStructure.Affiche Model = EyouSoft.BLL.SystemStructure.Affiche.CreateInstance().GetModel(NewsID);
            if (Model != null) 
            {
                this.lbl_Time.Text = Model.IssueTime.ToString("yyyy年MM月dd日 hh:mm");
                this.lbl_Title.Text = Model.AfficheTitle;
                this.lit_Content.Text = Model.AfficheInfo;
                if (Model.PicPath != "")
                {
                    this.img_NewsPic.Src = imgUrl + Model.PicPath;
                }
                else 
                {
                    this.divShow.Visible = false;
                }
                titleName = Model.AfficheTitle;
            }
            Model = null;

            #region 绑定航班类型,乘客类型
            Array arr = Enum.GetValues(typeof(EyouSoft.Model.TicketStructure.VoyageType));
            for (int i = 1; i < arr.Length; i++)
            {
                rdoType.Items.Add(new ListItem(Enum.GetName(typeof(EyouSoft.Model.TicketStructure.VoyageType), arr.GetValue(i)), arr.GetValue(i).ToString()));
            }
            arr = Enum.GetValues(typeof(EyouSoft.Model.TicketStructure.PeopleCountryType));
            for (int i = 0; i < arr.Length; i++)
            {
                string showtext = arr.GetValue(i).ToString() == EyouSoft.Model.TicketStructure.PeopleCountryType.Foreign.ToString() ? "外宾" : "内宾";
                rdoPassengerType.Items.Add(new ListItem(showtext, arr.GetValue(i).ToString()));
            }
            rdoType.Items[0].Selected = true;
            rdoPassengerType.Items[0].Selected = true;
            arr = null;
            #endregion
        }
        #endregion

        #region 保存操作
        private void SavePlaneInfo()
        {
            string voyageType=Utils.GetFormValue(rdoType.UniqueID);
            string peopleCountType=Utils.GetFormValue(rdoPassengerType.UniqueID);
            int personCount =Utils.GetInt(Utils.GetFormValue(txtPersonCount.UniqueID),0);
            string begionTime = Utils.GetFormValue(DatePicker1.UniqueID);
            string endTime = Utils.GetFormValue(DatePicker2.UniqueID);
            string companyName = Utils.GetFormValue(txtStockCompanyName.UniqueID);
            string linkName = Utils.GetFormValue(txtStockLinkName.UniqueID);
            string phone = Utils.GetFormValue(txtStockPhone.UniqueID);
            string address = Utils.GetFormValue(txtStockAddress.UniqueID);
            string remark=Utils.GetFormValue(txarRemark.UniqueID);
            if (personCount < 1)
            {
                Response.Write("{success:'3',message:'乘机人数填写错误'}");
                Response.End();
                return;
            }
            //时间判断
            DateTime? begionTime1 = null;
            DateTime? endTime1 = null;
            if ((EyouSoft.Model.TicketStructure.VoyageType)Enum.Parse(typeof(EyouSoft.Model.TicketStructure.VoyageType), voyageType) != EyouSoft.Model.TicketStructure.VoyageType.单程)
            {
                
                if (!StringValidate.IsDateTime(begionTime) && StringValidate.IsDateTime(endTime))
                {
                    Response.Write("{success:'3',message:'往返时间格式填写错误'}");
                    Response.End();
                    return;
                }
                begionTime1 = Convert.ToDateTime(begionTime);
                endTime1 = Convert.ToDateTime(endTime);                
                if (begionTime1 > endTime1)
                {
                    Response.Write("{success:'3',message:'出发时间不能大于返程时间'}");
                    Response.End();
                    return;
                }
            }
            else
            {;
                if (!StringValidate.IsDateTime(begionTime))
                {
                    Response.Write("{success:'3',message:'出发时间格式填写错误'}");
                    Response.End();
                    return;
                }
                begionTime1 = Convert.ToDateTime(begionTime);
            }
            if (!Utils.IsPhone(phone))
            {
                Response.Write("{success:'3',message:'采购商电话号码填写错误'}");
                Response.End();
                return;
            }
            //航班信息
            EyouSoft.Model.TicketStructure.TicketFlight flight=new EyouSoft.Model.TicketStructure.TicketFlight ();
            flight.PeopleNumber=personCount;
            flight.ReturnDate=endTime1;
            flight.TakeOffDate=begionTime1;
            flight.VoyageSet = (EyouSoft.Model.TicketStructure.VoyageType)Enum.Parse(typeof(EyouSoft.Model.TicketStructure.VoyageType), voyageType);
            flight.PeopleCountryType = (EyouSoft.Model.TicketStructure.PeopleCountryType)Enum.Parse(typeof(EyouSoft.Model.TicketStructure.PeopleCountryType), peopleCountType);

            //航班折扣申请
            EyouSoft.Model.TicketStructure.TicketApply ticketapply=new EyouSoft.Model.TicketStructure.TicketApply ();
            ticketapply.CompanyAddress=address;
            ticketapply.CompanyName=companyName;
            ticketapply.CompanyTel=phone;
            ticketapply.ContactName=linkName;
            ticketapply.Remark = remark;
            ticketapply.TicketArticleID = Utils.GetFormValue(hidNewId.UniqueID);
            ticketapply.TicketFlight=flight;
            ticketapply.TicketArticleTitle = Utils.GetFormValue(hidTitle.UniqueID);

            if (EyouSoft.BLL.TicketStructure.TicketApply.CreateInstance().Add(ticketapply))
            {
                Utils.ResponseMeg(true, "保存成功");
            }
            else
            {
                Utils.ResponseMeg(false, "保存失败");
            }
        }
        #endregion
    }
}
