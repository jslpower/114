using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Newtonsoft.Json;
using EyouSoft.Common;
using System.Text;
using EyouSoft.Common.ConfigModel;

namespace UserPublicCenter.PlaneInfo
{
    /// <summary>
    /// 功能：机票处理页面
    /// 开发人：刘玉灵   时间：2010-8-12
    /// </summary>
    public class DefaultPost : EyouSoft.ControlCommon.Control.BaseAshx, IHttpHandler
    {
        /// <summary>
        /// 固定分销账号[用户名,企业代码]
        /// </summary>
        protected Dictionary<string, string> FXUser = new Dictionary<string, string>();

        #region ProcessRequest
        public void ProcessRequest(HttpContext context)
        {
            base.BaseInit(context);

            FXUser.Add("!@#$test", "xxx");

            PostResult pResult = new PostResult(true, string.Empty, string.Empty);
            string isFromMQ = context.Request["isFromMQ"];
            string isFromCenter = context.Request["isFromCenter"];
            EyouSoft.Model.TicketStructure.MLogTicketInfo logInfo = null;

            if (pResult.IsSucceed)
            {
                if (IsLogin) //是否登录
                {
                    //出发城市三字码
                    string fromCity = context.Request["flke"];
                    //目的地三字码
                    string toCity = context.Request["tlke"];
                    //出发日期
                    string startDate = context.Request["startTime"];
                    //返程日期
                    string EndDate = context.Request["endTime"];

                    Adpost.Ticket.Model.VoyageType VoyageType = Adpost.Ticket.Model.VoyageType.单程;
                    int intVoyageType = EyouSoft.Common.Utils.GetInt(context.Request["VoyageType"]);
                    switch (intVoyageType)
                    {
                        case 2:
                            VoyageType = Adpost.Ticket.Model.VoyageType.往返;
                            break;
                        case 3:
                            VoyageType = Adpost.Ticket.Model.VoyageType.联程;
                            break;
                    }

                    Adpost.Ticket.Model.TicketModel model = new Adpost.Ticket.Model.TicketModel();
                    //model.System = this.InitSysModel();
                    model.User = this.InitUserModel();
                    //出发城市
                    model.Flight.FromCity = fromCity;
                    //到达城市
                    model.Flight.ToCity = toCity;
                    //出发时间
                    model.Flight.TakeOffDate = EyouSoft.Common.Utils.GetDateTimeNullable(startDate);
                   
                    //航程类型
                    model.Flight.VoyageSet = VoyageType;
                    if (VoyageType == Adpost.Ticket.Model.VoyageType.往返)
                    {
                        //返回时间
                        model.Flight.ReturnDate = EyouSoft.Common.Utils.GetDateTimeNullable(EndDate);
                    }

                    //验证表单是否已填写完整
                    Adpost.Ticket.Model.ValidateResult validate = Adpost.Ticket.BLL.Ticket.ValidateForm(model.Flight);
                    if (!validate.IsSuccess)
                    {
                        pResult.IsSucceed = false;
                        pResult.ErrorMsg = validate.ErrorInfo.ToString();
                    }
                    else
                    {
                        Adpost.Ticket.Model.TicketQueryResult result = QueryURL(model,InitSysModel());
                        if (result.IsSuccess)
                        {
                            pResult.IsSucceed = true;
                            pResult.RedirectUrl = result.TicketGotoUrl;
                        }
                        else
                        {
                            pResult.IsSucceed = false;
                            pResult.ErrorMsg = result.ErrorInfo.ToString();
                        }

                        logInfo = new EyouSoft.Model.TicketStructure.MLogTicketInfo()
                        {
                            CDate = DateTime.Now,
                            CompanyId = SiteUserInfo.CompanyID,
                            LCity = model.Flight.FromCity,
                            LDate = model.Flight.TakeOffDate,
                            RCity = model.Flight.ToCity,
                            RDate = model.Flight.ReturnDate,
                            UserId = SiteUserInfo.ID
                        };
                    }
                    model = null;
                }
                else
                {
                    pResult.IsSucceed = false;
                    pResult.ErrorMsg = "请您先登录";
                }
            }

            if (!string.IsNullOrEmpty(isFromMQ) || !string.IsNullOrEmpty(isFromCenter))
            {
                if(logInfo!=null)
                    WLog(logInfo);
                context.Response.Redirect(pResult.RedirectUrl);
                context.Response.End();
            }
            else
            {
                string output = JsonConvert.SerializeObject(pResult);
                string callback = Utils.InputText(context.Request.QueryString["callback"]);
                context.Response.Write(";" + callback + "(" + output + ")");
                context.Response.End();
            }

        }
        #endregion

        #region 提交返回的数据实体
        /// <summary>
        /// 提交返回的数据实体
        /// </summary>
        public class PostResult
        {
            /// <summary>
            /// constructor with specified initial value
            /// </summary>
            /// <param name="isSucceed">是否成功</param>
            /// <param name="errorMsg">错误信息</param>
            /// <param name="redirectUrl">定向到的URL</param>
            public PostResult(bool isSucceed, string errorMsg, string redirectUrl)
            {
                this.IsSucceed = isSucceed;
                this.ErrorMsg = errorMsg;
                this.RedirectUrl = redirectUrl;
            }

            /// <summary>
            /// 是否成功
            /// </summary>
            public bool IsSucceed { get; set; }
            /// <summary>
            /// 错误信息
            /// </summary>
            public string ErrorMsg { get; set; }
            /// <summary>
            /// 定向到的URL
            /// </summary>
            public string RedirectUrl { get; set; }
        }
        #endregion

        public bool IsReusable
        {
            get { return false; }

        }

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

            parameters.Add("DoubleTrip", Convert.ToInt32(model.Flight.VoyageSet).ToString());

            if (!String.IsNullOrEmpty(model.Flight.FromCity))
            {
                parameters.Add("FromCityCode", model.Flight.FromCity);
            }

            if (!String.IsNullOrEmpty(model.Flight.ToCity))
            {
                parameters.Add("DestCityCode", model.Flight.ToCity);
            }

            if (model.Flight.TakeOffDate.HasValue)
            {
                parameters.Add("LeaveDate",model.Flight.TakeOffDate.Value.ToString("yyyy-MM-dd"));
            }

            if (model.Flight.ReturnDate.HasValue)
            {
                parameters.Add("ReturnDate", model.Flight.ReturnDate.Value.ToString("yyyy-MM-dd"));
            }
            else {
                parameters.Add("ReturnDate", string.Empty);
            }

            str.AppendFormat("&action={0}", HttpUtility.UrlEncode("/sales/selectair_1.asp" + "?" + Sign(parameters) + "seattype=012&isback=0&LimitSupplier=&airline="));
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
        /// <param name="log"></param>
        private void WLog(EyouSoft.Model.TicketStructure.MLogTicketInfo log)
        {
            EyouSoft.BLL.TicketStructure.BLogTicket.CreateInstance().WLog(log);
        }
    }

    #region 机票系统信息实体
    public class TicketSystemModel
    {
        /// <summary>
        /// 机票接口网址[必填]
        /// 必填
        /// </summary>
        public string TicketInterfaceUrl { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        public string Sign { get; set; }

        public string CompanyCode { get; set; }

        /// <summary>
        /// 企业代码
        /// </summary>
        public string cpcode { get; set; }

        public string sysPath { get; set; }
    }
    #endregion
}
