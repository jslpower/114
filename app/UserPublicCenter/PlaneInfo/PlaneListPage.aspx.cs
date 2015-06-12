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
using EyouSoft.Common.ConfigModel;
using EyouSoft.SSOComponent.Entity;

namespace UserPublicCenter.PlaneInfo
{
    /// <summary>
    /// 页面功能：机票——机票列表页
    /// 开发人：杜桂云      开发时间：2010-07-23
    /// </summary>
    public partial class PlaneListPage : EyouSoft.Common.Control.FrontPage
    {
        #region 成员变量
        //两张横向大广告图片
        protected string BannerImgUrl1 = "";
        protected string BannerImgUrl2 = "";
        /// <summary>
        /// 固定分销账号[用户名,企业代码]
        /// </summary>
        protected Dictionary<string, string> FXUser = new Dictionary<string, string>();
        #endregion

        #region 页面加载
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (SiteUserInfo != null)
                {
                    FXUser.Add("!@#$test", "xxx");
                    Adpost.Ticket.Model.TicketModel model = new Adpost.Ticket.Model.TicketModel();
                    model.User = this.InitUserModel();
                    Adpost.Ticket.Model.TicketQueryResult result = QueryURL(model, InitSysModel());
                    if (result.IsSuccess)
                    {
                        //Response.Write(result.TicketGotoUrl);
                        //Response.End();
                        WLog();

                        Response.Redirect(result.TicketGotoUrl);
                    }
                    else
                    {
                        Response.Write(result.ErrorInfo.ToString());
                        Response.End();
                    }
                }
                else
                {
                    Response.Write("请先登录");
                    Response.End();
                }

                //AddMetaTag("description", PageTitle.Plane_Des);
                //AddMetaTag("keywords", PageTitle.Plane_Keywords);
                //this.Page.Title =PageTitle.Plane_Title;
                ////调用相关绑定初始化数据
                //BindTicketInfo();
                //BindTicketBook();
                //BindTicketAgu();
                ////图片广告绑定
                //GetBannerImg();
            }
            this.CityAndMenu1.HeadMenuIndex = 3;
        }
        #endregion

        #region 初始化列表绑定
        /// <summary>
        /// 获取横幅广告的地址和连接
        /// </summary>
        protected void GetBannerImg() 
        {
            EyouSoft.IBLL.AdvStructure.IAdv iBll = EyouSoft.BLL.AdvStructure.Adv.CreateInstance();
            IList<EyouSoft.Model.AdvStructure.AdvInfo> AdvListBanner = null;
            //第一张横向大广告
            AdvListBanner = iBll.GetAdvs(CityId, EyouSoft.Model.AdvStructure.AdvPosition.机票频道通栏banner1);
            if (AdvListBanner != null && AdvListBanner.Count > 0)
            {
                BannerImgUrl1 = Utils.GetImgOrFalash(AdvListBanner[0].ImgPath,AdvListBanner[0].RedirectURL);
            }
            //第二张横向大广告
            AdvListBanner = iBll.GetAdvs(CityId, EyouSoft.Model.AdvStructure.AdvPosition.机票频道通栏banner2);
            if (AdvListBanner != null && AdvListBanner.Count > 0)
            {
                BannerImgUrl2 = Utils.GetImgOrFalash(AdvListBanner[0].ImgPath, AdvListBanner[0].RedirectURL); 
            }
            //释放资源
            AdvListBanner = null;
            iBll = null;
        }
        /// <summary>
        /// 运价参考绑定
        /// </summary>
        protected void BindTicketInfo() 
        {
            IList<EyouSoft.Model.SystemStructure.Affiche> ModelList = EyouSoft.BLL.SystemStructure.Affiche.CreateInstance().GetTopList(20,EyouSoft.Model.SystemStructure.AfficheType.运价参考);
            if (ModelList != null && ModelList.Count > 0) 
            {
                this.dl_TicketbookList.DataSource = ModelList;
                this.dl_TicketbookList.DataBind();
            }
            //释放资源
            ModelList = null;
        }
        /// <summary>
        /// 帮助信息绑定
        /// </summary>
        protected void BindTicketBook()
        {
            IList<EyouSoft.Model.SystemStructure.Affiche> ModelList = EyouSoft.BLL.SystemStructure.Affiche.CreateInstance().GetTopList(10, EyouSoft.Model.SystemStructure.AfficheType.帮助信息);
            if (ModelList != null && ModelList.Count > 0)
            {
                this.dal_BookTichetList.DataSource = ModelList;
                this.dal_BookTichetList.DataBind();
            }
            //释放资源
            ModelList = null;
        }
        /// <summary>
        /// 合作供应商信息绑定
        /// </summary>
        protected void BindTicketAgu()
        {
            IList<EyouSoft.Model.SystemStructure.Affiche> ModelList = EyouSoft.BLL.SystemStructure.Affiche.CreateInstance().GetTopList(10, EyouSoft.Model.SystemStructure.AfficheType.合作供应商);
            if (ModelList != null && ModelList.Count > 0)
            {
                this.dal_PlaneAgu.DataSource = ModelList;
                this.dal_PlaneAgu.DataBind();
            }
            //释放资源
            ModelList = null;
        }
        #endregion

        #region 获取列表标题信息
        protected string ShowTicketInfo(int AffiID, string AffiName, int typeID)
        {
            string returnVal = "";
            string linkUrl = Utils.GetWordAdvLinkUrl(0, AffiID, typeID,CityId);
            switch (typeID)
            {
                case 0://运价参考
                    returnVal = string.Format("<li><a href='{0}' title='{2}'  target='_blank'>{1}</a></li>", linkUrl, Utils.GetText(AffiName, 22, true), AffiName);
                    break;
                case 1://帮助信息
                    returnVal = string.Format("<li>·<a href='{0}' title='{2}' target='_blank'>{1}</a></li>", linkUrl, Utils.GetText(AffiName, 13, true), AffiName);
                    break;
                default://合作供应商信息
                    returnVal = string.Format("<li>·<a href='{0}' title='{2}' target='_blank'>{1}</a></li>", linkUrl, Utils.GetText(AffiName, 13, true), AffiName);
                    break;
            }
            return returnVal;
        }
        #endregion

        #region 初始化系统信息实体
        /// <summary>
        /// 初始化系统信息实体
        /// </summary>
        /// <returns></returns>
        private TicketSystemModel InitSysModel()
        {
            TicketSystemModel sysModel = new TicketSystemModel();
            sysModel.TicketInterfaceUrl = ConfigClass.GetConfigString("Ticket", "TicketInterfaceUrl");
            sysModel.Sign = ConfigClass.GetConfigString("Ticket", "Sign");
            sysModel.CompanyCode = ConfigClass.GetConfigString("Ticket", "CompanyCode");
            sysModel.cpcode = ConfigClass.GetConfigString("Ticket", "cpcode");
            sysModel.sysPath = ConfigClass.GetConfigString("Ticket", "sysPath");
            return sysModel;
        }
        #endregion

        #region 初始化用户信息实体
        /// <summary>
        /// 初始化用户信息实体
        /// </summary>
        /// <returns></returns>
        private Adpost.Ticket.Model.TicketUserModel InitUserModel()
        {
            Adpost.Ticket.Model.TicketUserModel userModel = new Adpost.Ticket.Model.TicketUserModel();
            userModel.UserFlag = false;

            ////test 
            //userModel.UserName = "2183";
            //userModel.UserPassword = Adpost.Ticket.BLL.Ticket.MD5Encrypt("000000").ToUpper();

            //userModel.UserName = SiteUserInfo.OpUserId.ToString(); 
            //2011-1-5  zhangzy  修改成调用用户名
            userModel.UserName = SiteUserInfo.UserName;
            userModel.UserPassword = SiteUserInfo.PassWordInfo.MD5Password;
            userModel.TicketGroupGuid = EyouSoft.Common.Domain.TicketDefaultGroupGUID;
            userModel.TrueName = SiteUserInfo.ContactInfo.ContactName;
            userModel.CorpName = SiteUserInfo.CompanyName;
            userModel.Email = SiteUserInfo.ContactInfo.Email;
            userModel.Mobile = SiteUserInfo.ContactInfo.Mobile;
            userModel.QQ = SiteUserInfo.ContactInfo.QQ;
            return userModel;
        }
        #endregion

        #region 构造要跳转的URL
        /// <summary>
        /// 构造要跳转的URL
        /// </summary>
        /// <param name="model"></param>
        /// <param name="sysModel"></param>
        /// <returns></returns>
        private Adpost.Ticket.Model.TicketQueryResult QueryURL(Adpost.Ticket.Model.TicketModel model, TicketSystemModel sysModel)
        {
            Adpost.Ticket.Model.TicketQueryResult result = new Adpost.Ticket.Model.TicketQueryResult();
            result.IsSuccess = true;
            result.TicketGotoUrl = "";

            #region 表单验证
            if (String.IsNullOrEmpty(sysModel.TicketInterfaceUrl))
            {
                result.ErrorInfo.Append("请填写机票接口网址。\n");
                result.IsSuccess = false;
            }
            if (!result.IsSuccess)
                return result;
            #endregion

            #region 构造URL
            StringBuilder str = new StringBuilder();
            Hashtable parameters = new Hashtable();

            if (FXUser.ContainsKey(model.User.UserName))
            {
                sysModel.cpcode = FXUser[model.User.UserName].ToString();
            }
            str.AppendFormat("&CompanyCode={0}", sysModel.CompanyCode);
            str.AppendFormat("&cpcode={0}", sysModel.cpcode);
            str.AppendFormat("&sysPath={0}", sysModel.sysPath);
            str.AppendFormat("&sign={0}", sysModel.Sign);

            if (!String.IsNullOrEmpty(model.User.UserName))
            {
                str.AppendFormat("&UserAccount={0}", HttpUtility.UrlEncodeUnicode(model.User.UserName));
                str.AppendFormat("&UserID={0}", HttpUtility.UrlEncodeUnicode(sysModel.cpcode + "." + model.User.UserName));
                str.AppendFormat("&UserName={0}", HttpUtility.UrlEncodeUnicode(model.User.UserName));
            }
            str.AppendFormat("&MobilePhone={0}", "15356126700");
            str.AppendFormat("&CompanyName={0}", HttpUtility.UrlEncodeUnicode(model.User.CorpName));
            str.AppendFormat("&Email={0}", "xus@enowinfo.com");
            str.AppendFormat("&LinkMan={0}", HttpUtility.UrlEncodeUnicode("徐晟"));
            //parameters.Add("DoubleTrip", Convert.ToInt32(model.Flight.VoyageSet).ToString());

            model.Flight.FromCity = "HGH";
            model.Flight.ToCity = "PEK";
            model.Flight.TakeOffDate = DateTime.Now;

            //if (!String.IsNullOrEmpty(model.Flight.FromCity))
            //{
            //    parameters.Add("FromCityCode", model.Flight.FromCity);
            //}

            //if (!String.IsNullOrEmpty(model.Flight.ToCity))
            //{
            //    parameters.Add("DestCityCode", model.Flight.ToCity);
            //}

            //if (model.Flight.TakeOffDate.HasValue)
            //{
            //    parameters.Add("LeaveDate", model.Flight.TakeOffDate.Value.ToString("yyyy-MM-dd"));
            //}

            //if (model.Flight.ReturnDate.HasValue)
            //{
            //    parameters.Add("ReturnDate", model.Flight.ReturnDate.Value.ToString("yyyy-MM-dd"));
            //}
            //else
            //{
            //    parameters.Add("ReturnDate", string.Empty);
            //}
            if(string.IsNullOrEmpty(Request.QueryString["url"]))
                str.AppendFormat("&action={0}", HttpUtility.UrlEncode(ConfigClass.GetConfigString("Ticket", "QueryURL") + "?" + Sign(parameters) + ""));
            else
                str.AppendFormat("&action={0}", HttpUtility.UrlEncode(Request.QueryString["url"] + "?" + Sign(parameters) + ""));       
            string strURL = str.ToString();
            if (strURL.StartsWith("&"))
            {
                strURL = strURL.Substring(1);
            }
            #endregion

            result.IsSuccess = true;
            result.TicketGotoUrl = sysModel.TicketInterfaceUrl + "?" + strURL;
            return result;
        }
        #endregion

        /// <summary>
        /// 返回转向地址参数信息
        /// </summary>
        /// <param name="parameters">Hashtable参数集合</param>
        /// <returns></returns>
        private static string Sign(Hashtable parameters)
        {
            StringBuilder sb = new StringBuilder();
            ArrayList akeys = new ArrayList(parameters.Keys);
            akeys.Sort();
            foreach (string k in akeys)
            {
                string v = (string)parameters[k];
                if (null != v && "sign".CompareTo(k) != 0 && "key".CompareTo(k) != 0)
                {
                    sb.Append(k + "=" + v + "&");
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 写日志
        /// </summary>
        private void WLog()
        {
            if (SiteUserInfo == null) return;

            EyouSoft.Model.TicketStructure.MLogTicketInfo log = new EyouSoft.Model.TicketStructure.MLogTicketInfo()
            {
                CompanyId = SiteUserInfo.CompanyID,
                UserId = SiteUserInfo.ID
            };

            EyouSoft.BLL.TicketStructure.BLogTicket.CreateInstance().WLog(log);
        }
    }
    
}
