using System;
using System.Collections.Generic;

namespace IMFrame.SupplierInfo
{
    /// <summary>
    /// MQ同业中心供求列表页
    /// 创建人：曹胡生
    /// </summary>
    public partial class SupplierInfoList : EyouSoft.ControlCommon.Control.MQPage
    {
        protected IList<EyouSoft.Model.CommunityStructure.ExchangeList> exList = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            Bind();
        }
        /// <summary>
        /// 绑定
        /// </summary>
        private void Bind()
        {
            //时间
            int Stime = EyouSoft.Common.Utils.GetInt(EyouSoft.Common.Utils.GetQueryStringValue("Stime"));
            //同中中心编号
            int SuperID = EyouSoft.Common.Utils.GetInt(EyouSoft.Common.Utils.GetQueryStringValue("SuperID"));
            //根据同业中心编号得到其省份
            string Province = "0";
            EyouSoft.Model.MQStructure.IMSuperCluster model = EyouSoft.BLL.MQStructure.IMSuperCluster.CreateInstance().GetSuperClusterByID(SuperID);
            if (model != null)
            {
                if (model.SelectType == EyouSoft.Model.MQStructure.SelectType.选择省市)
                {
                    Province = model.SelectValue;
                }
            }
            string SearchText = EyouSoft.Common.Utils.GetFormValue("textfield") == "请输入关键字" ? "" : EyouSoft.Common.Utils.GetFormValue("textfield");
            exList = EyouSoft.BLL.CommunityStructure.ExchangeList.CreateInstance().GetList(Province, Stime, SearchText);
        }

        /// <summary>
        /// 得到标签html
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="imgserverurl"></param>
        /// <returns></returns>
        public string GetTagUrl(EyouSoft.Model.CommunityStructure.ExchangeTag tag, string imgserverurl, int cityid, int provid)
        {
            string s = "";
            string url =EyouSoft.Common.Domain.UserPublicCenter+EyouSoft.Common.URLREWRITE.SupplierInfo.GetSuplierUrl("", 0, (int)tag, 0, provid, 0, cityid);
            switch (tag)
            {
                case EyouSoft.Model.CommunityStructure.ExchangeTag.急急急:
                    s = "<span><a target=\"_blank\" href=\"" + url + "\" class=\"icon\"><img src=\"" + imgserverurl + "/images/new2011/suplly_76.gif\" /></a></span> ";
                    break;
                case EyouSoft.Model.CommunityStructure.ExchangeTag.品质:
                    s = "<span><a target=\"_blank\" href=\"" + url + "\" class=\"icon\"><img src=\"" + imgserverurl + "/images/new2011/icons_14.gif\" /></a></span> ";
                    break;
                case EyouSoft.Model.CommunityStructure.ExchangeTag.热:
                    s = "<span><a target=\"_blank\" href=\"" + url + "\" class=\"icon\"><img src=\"" + imgserverurl + "/images/new2011/icons_09.gif\" /></a></span> ";
                    break;
                case EyouSoft.Model.CommunityStructure.ExchangeTag.特价:
                    s = "<span><a target=\"_blank\" href=\"" + url + "\" class=\"icon\"><img src=\"" + imgserverurl + "/images/new2011/suplly_83.gif\" /></a></span> ";
                    break;
                case EyouSoft.Model.CommunityStructure.ExchangeTag.无:
                    s = "";
                    //<span><a href=\"#\" class=\"icon\"><img src=\"" + imgserverurl + "/images/new2011/suplly_72.gif\" /></a></span>
                    break;
                case EyouSoft.Model.CommunityStructure.ExchangeTag.最新报价:
                    s = "<span><a  target=\"_blank\" href=\"" + url + "\" class=\"icon\"><img src=\"" + imgserverurl + "/images/new2011/icons_07.gif\" /></a></span> ";
                    break;
            }
            return s;
        }
    }
}
