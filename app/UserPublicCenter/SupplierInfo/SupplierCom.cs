using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using EyouSoft.Common.Function;

namespace UserPublicCenter.SupplierInfo
{
    public class SupplierCom
    {


        /// <summary>
        /// 得到标签html
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="imgserverurl"></param>
        /// <returns></returns>
        public static string GetTagUrl(EyouSoft.Model.CommunityStructure.ExchangeTag tag,string imgserverurl,int cityid,int provid)
        {
            string s = ""; 
            string url = EyouSoft.Common.URLREWRITE.SupplierInfo.GetSuplierUrl("", 0, (int)tag, 0, provid, 0, cityid);
            switch (tag)
            {
                case EyouSoft.Model.CommunityStructure.ExchangeTag.急急急:
                    s = "<span><a href=\"" + url + "\" class=\"icon\"><img src=\"" + imgserverurl + "/images/new2011/suplly_76.gif\" /></a></span> ";
                    break;
                case EyouSoft.Model.CommunityStructure.ExchangeTag.品质:
                    s = "<span><a href=\"" + url + "\" class=\"icon\"><img src=\"" + imgserverurl + "/images/new2011/icons_14.gif\" /></a></span> ";
                    break;
                case EyouSoft.Model.CommunityStructure.ExchangeTag.热:
                    s = "<span><a href=\"" + url + "\" class=\"icon\"><img src=\"" + imgserverurl + "/images/new2011/icons_09.gif\" /></a></span> ";
                    break;
                case EyouSoft.Model.CommunityStructure.ExchangeTag.特价:
                    s = "<span><a href=\"" + url + "\" class=\"icon\"><img src=\"" + imgserverurl + "/images/new2011/suplly_83.gif\" /></a></span> ";
                    break;
                case EyouSoft.Model.CommunityStructure.ExchangeTag.无:
                    s = "";
                    //<span><a href=\"#\" class=\"icon\"><img src=\"" + imgserverurl + "/images/new2011/suplly_72.gif\" /></a></span>
                    break;
                case EyouSoft.Model.CommunityStructure.ExchangeTag.最新报价:
                    s = "<span><a href=\"" + url + "\" class=\"icon\"><img src=\"" + imgserverurl + "/images/new2011/icons_07.gif\" /></a></span> ";
                    break;
            }
            return s;
        }

        /// <summary>
        /// 时间枚举数组
        /// </summary>
        /// <returns></returns>
        public static string[] TimeEnum()
        {
            string[] s = new string[] { "全部", "今天", "昨天", "前天", "更早" };
            return s;
        }

        /// <summary>
        /// 标签枚举数组
        /// </summary>
        /// <returns></returns>
        public static string[] TagEnum()
        {
            string[] s = new string[] { "全部", "无", "品质", "特价", "急急急","最新报价", "热"};
            return s;
        }

        /// <summary>
        /// 分类枚举数组
        /// </summary>
        /// <returns></returns>
        public static string[] CatEnum()
        {
            string[] s = new string[] { "全部", "团队询价", "地接报价", "直通车", "车辆", "酒店", "导游/招聘", "票务", "其他", "找地接", "机票", "签证" };
            return s;
        }

        /// <summary>
        /// 供求枚举数组
        /// </summary>
        /// <returns></returns>
        public static string[] TypeEnum()
        {
            string[] s = new string[] { "全部", "供应","求购"};
            return s;
        }

        /// <summary>
        /// 给URL加上http
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string SetUrlHttp(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return "javascript:;";
            }

            if (s.IndexOf("http") == 0)
            {
                return s;
            }
            else
            {
                return "http://" + s;
            }
        }

        public static Regex MQRegex = new Regex(@"(\(|（)(\d{5})(）|\))", RegexOptions.Compiled);
        public static Regex QQRegex = new Regex(@"(\(|（)(\d{6,10})(）|\))", RegexOptions.Compiled);
        /// <summary>
        /// 内容中是否含有HTML
        /// </summary>
        public static Regex IsHaveHtmlRegex = new Regex("<(.|\\n)*?>",RegexOptions.Compiled);

        /// <summary>
        /// 替换文本 (12345)括号带五位数字替换成MQ,(123456..10)括号带10位数字替换成QQ;
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ConvertExchangeListContent(string s)
        {
            //判断供求内容是否包含HTML
            if (IsHaveHtmlRegex.IsMatch(s) == false)//不包含
            {
                //如果不包含，则是TextArea快速发布
                s = s.Replace("\n", "<br />");
            }
            s = MQRegex.Replace(s, EyouSoft.Common.Utils.GetMQ("$2"));
            s = QQRegex.Replace(s, EyouSoft.Common.Utils.GetForceQQ("$2",""));
            return s;
        }

        /// <summary>
        /// 供求类别健值对列表
        /// </summary>
        /// <param name="cat"></param>
        /// <returns></returns>
        public static IList<EyouSoft.Common.EnumObj> GetExchangeTypeListByCat( EyouSoft.Model.CommunityStructure.ExchangeCategory cat)
        {
            IList<EyouSoft.Common.EnumObj> exchangeTypelist = new List<EyouSoft.Common.EnumObj>();
            exchangeTypelist = EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.CommunityStructure.ExchangeType));
            if(cat == EyouSoft.Model.CommunityStructure.ExchangeCategory.供)
            {
                exchangeTypelist.RemoveAt(8);
                exchangeTypelist.RemoveAt(0);
            }
            else
            {
                exchangeTypelist.RemoveAt(2);
                exchangeTypelist.RemoveAt(1);
            }
            return exchangeTypelist;
        }
        /// <summary>
        /// 供应类型EnumObj列表
        /// </summary>
        public static readonly List<EyouSoft.Common.EnumObj> ExchangeTypeList_Gong = new List<EyouSoft.Common.EnumObj>()
        {
            new EyouSoft.Common.EnumObj("地接报价","2"),
            new EyouSoft.Common.EnumObj("直通车","3"),
            new EyouSoft.Common.EnumObj("车辆","4"),
            new EyouSoft.Common.EnumObj("机票","10"),
            new EyouSoft.Common.EnumObj("酒店","5"),
            new EyouSoft.Common.EnumObj("票务","7"),
            new EyouSoft.Common.EnumObj("签证","11"),
            new EyouSoft.Common.EnumObj("招聘","6"),
            new EyouSoft.Common.EnumObj("其他","8")
        };


        /// <summary>
        /// 供应类型Select Option列表
        /// </summary>
        public const string ExchangeTypeOptionList_Gong = @"<option value='2'>地接报价</option>
                            <option value='3'>直通车</option>  
                            <option value='4'>车辆</option>  
                            <option value='10'>机票</option>
                            <option value='5'>酒店</option>  
			                <option value='7'>票务</option>
                            <option value='11'>签证</option> 
			                <option value='6'>招聘</option>
                            <option value='8'>其他</option>";


        public static readonly List<EyouSoft.Common.EnumObj> ExchangeTypeList_Qiu = new List<EyouSoft.Common.EnumObj>()
        {
            new EyouSoft.Common.EnumObj("团队询价","1"),
            new EyouSoft.Common.EnumObj("找地接","9"),
            new EyouSoft.Common.EnumObj("车辆","4"),
            new EyouSoft.Common.EnumObj("机票","10"),
            new EyouSoft.Common.EnumObj("酒店","5"),
            new EyouSoft.Common.EnumObj("票务","7"),
            new EyouSoft.Common.EnumObj("签证","11"),
            new EyouSoft.Common.EnumObj("招聘","6"),
            new EyouSoft.Common.EnumObj("其他","8")
        };

        /// <summary>
        /// 求购类型Select Option列表
        /// </summary>
        public const string ExchangeTypeOptionList_Qiu = @"<option value='1'>团队询价</option>
			    <option value='9'>找地接</option>
                            <option value='4'>车辆</option>
			    <option value='10'>机票</option>
                            <option value='5'>酒店</option>
			    <option value='7'>票务</option>
			    <option value='11'>签证</option>
                            <option value='6'>招聘</option>
                            <option value='8'>其他</option>";
    }
}
