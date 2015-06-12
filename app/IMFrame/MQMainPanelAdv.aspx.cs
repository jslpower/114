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
using EyouSoft.ControlCommon.Control;
using System.Text;
using System.Collections.Generic;
using EyouSoft.Common;

namespace TourUnion.WEB.IM
{
    /// <summary>
    /// 功能：MQ主面板的广告位
    /// 开发人：zhangzy  开发时间：2010-8-17
    /// </summary>
    public partial class MQMainPanelAdv : Page
    {
        /// <summary>
        /// 宽度,高度的样式说明:
        /// 在MQ主面板的广告位宽,高为:210,45
        /// 在MQ聊天窗口的广告位宽,高为:220,47
        /// </summary>
        protected string flashWidth = "220";
        protected string flashHeight = "55";
        //机票url地址
        protected string ticketurl = "";
        protected string imageServerUrl = EyouSoft.Common.ImageManage.GetImagerServerUrl(1);
        /// <summary>
        /// 供求信息
        /// </summary>
        protected string supplysHtml="";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {                
                if (Request.QueryString["chat"] == "1")  //表示为在聊天窗口时
                {
                    flashWidth = "230";
                    flashHeight = "52";
                }
                EyouSoft.Model.MQStructure.IMMember model = EyouSoft.BLL.MQStructure.IMMember.CreateInstance().GetModel(EyouSoft.Common.Function.StringValidate.GetIntValue(Request.QueryString["loginuid"]));
                if (model != null)
                {
                    ticketurl = EyouSoft.Common.Utils.GetDesPlatformUrlForMQMsg(EyouSoft.Common.Domain.UserPublicCenter + "/PlaneInfo/PlaneListPage.aspx", model.im_uid.ToString(), model.im_password);
                }
                model = null;
                BindSupply();
            }
        }

        #region 绑定供求列表
        /// <summary>
        /// 绑定供求列表
        /// </summary>
        protected void BindSupply()
        {
            StringBuilder strBuilder = new StringBuilder();
            IList<EyouSoft.Model.CommunityStructure.ExchangeList> excList = EyouSoft.BLL.CommunityStructure.ExchangeList.CreateInstance().GetTopList(20, null, -1, null);
            if (excList != null && excList.Count > 0)
            {
                foreach (EyouSoft.Model.CommunityStructure.ExchangeList exc in excList)
                {
                    if (exc.ExchangeCategory == EyouSoft.Model.CommunityStructure.ExchangeCategory.供)
                    {
                        strBuilder.AppendFormat("<li class=\"gong\"><s></s><a href=\"{0}\">{1}</a></li>", EyouSoft.Common.URLREWRITE.SupplierInfo.InfoUrlWrite(exc.ID, 0), Utils.GetText(exc.ExchangeTitle, 13));
                    }
                    else
                    {
                        strBuilder.AppendFormat(" <li class=\"qiu\"><a href=\"{0}\"><s></s>{1}</a></li>", EyouSoft.Common.URLREWRITE.SupplierInfo.InfoUrlWrite(exc.ID, 0), Utils.GetText(exc.ExchangeTitle, 13));
                    }
                }
            }
            supplysHtml = strBuilder.ToString();
        }
        #endregion
    }
}
