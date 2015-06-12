using System;
using System.Web;
using System.Collections.Generic;

using EyouSoft.Common;
namespace IMFrame.SuperCluster
{
    /// <summary>
    /// MQ同业中心，聊天记录搜索页
    /// 2011-07-29 AM 曹胡生 创建
    /// </summary>
    public partial class MsgSearch : EyouSoft.ControlCommon.Control.MQPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 初始化搜索聊天记录
        /// </summary>
        /// <returns></returns>
        protected string InitRecord()
        {
            //日期分组
            System.Collections.Hashtable ht = new System.Collections.Hashtable();
            //日期(当天1，三天3，一周7,所有31)
            int days = Utils.GetInt(Utils.GetQueryStringValue("day"));
            //要搜索的内容
            System.Collections.Specialized.NameValueCollection gb2312 = HttpUtility.ParseQueryString(Request.Url.Query, System.Text.Encoding.GetEncoding("GB2312"));
            string content = gb2312["content"];
            //同业中心编号
            int superid = Utils.GetInt(Utils.GetQueryStringValue("SuperID"));
            //搜索的聊天记录列表
            IList<EyouSoft.Model.MQStructure.IMSuperClusterMsg> list = EyouSoft.BLL.MQStructure.IMSuperCluster.CreateInstance().GetSuperClusterMsg(days, content, superid);
            //所有聊天记录里的图片列表:[Pic]{XXX.jpg}[/Pic]
            List<string> plist = null;
            //所有聊天记录里的内置表情图片列表:/:X(字母)X(数字)
            List<string> syslist = null;
            //要输出的HTML代码
            System.Text.StringBuilder html = new System.Text.StringBuilder(string.Empty);
            try
            {
                if (list != null && list.Count > 0)
                {
                    html.Append("<ul>");
                    foreach (EyouSoft.Model.MQStructure.IMSuperClusterMsg item in list)
                    {
                        //日期分组
                        string date = item.DisplayDate.ToShortDateString();
                        if (!ht.ContainsKey(date))
                        {
                            ht.Add(date, "");
                            html.AppendFormat("<li class=\"Record-time\">日期:{0}</li>", date);
                        }
                        //发送者
                        html.AppendFormat("<li class=\"mq-name\">{0}</li>", item.MessageSender);
                        //获取该字符的总字节数。
                        int MessageContentLength = Utils.GetByteLength(item.MessageContent);
                        //截取前60个汉字长度的字符串
                        string substr = Utils.GetText2(item.MessageContent, 60, true);
                        //发送内容,不为空时进行搜索关键字变红
                        if (!string.IsNullOrEmpty(content) && content != "[")
                        {
                            //不能给图片路径的文件名加了红
                            string regstr = System.Text.RegularExpressions.Regex.Escape(content);
                            string reg = @"(?<!(/:)|(\[Pic\]{*[^]]*)|(\[P)|(\[Pi)|(\[Pic))" + regstr;
                            item.MessageContent = System.Text.RegularExpressions.Regex.Replace(item.MessageContent, reg, string.Format("<font color=\"red\">{0}</font>", content));
                            substr = System.Text.RegularExpressions.Regex.Replace(substr, reg, string.Format("<font color=\"red\">{0}</font>", content));
                        }
                        //转换聊天内容里的图片。
                        plist = parseHref(item.MessageContent, "\\[Pic\\]({.*?}\\..*?)\\[/Pic\\]");
                        if (plist != null && plist.Count > 0)
                        {
                            foreach (string str in plist)
                            {
                                string[] strs = str.Split('|');
                                string replaceText = "";
                                //如果是本地访问或者访问254，则MQ图片存放位置为http://192.168.1.254:10021
                                if (Request.Url.Host == "192.168.1.254" || Request.Url.Host == "localhost")
                                {
                                    replaceText = string.Format("<a href=\"{0}/{1}\" target=\"_blank\"><img id=\"{2}\" sourcewidth=\"0\" src=\"{0}/{1}\"/></a>", "http://192.168.1.254:10021", strs[1], System.Guid.NewGuid().ToString());
                                }
                                //如果外网访问，则MQ图片存放位置为http://files.tongye114.com
                                else
                                {
                                    replaceText = string.Format("<a href=\"{0}/{1}\" target=\"_blank\"><img id=\"{2}\" sourcewidth=\"0\" src=\"{0}/{1}\"/></a>", "http://files.tongye114.com", strs[1], System.Guid.NewGuid().ToString());
                                }
                                item.MessageContent = item.MessageContent.Replace(strs[0], replaceText);
                            }
                        }
                        //MQ内置的表情图片
                        syslist = parseHref(item.MessageContent, "/:[a-zA-Z][0-9]");
                        if (syslist != null && syslist.Count > 0)
                        {
                            foreach (string str in syslist)
                            {
                                string replaceText = "";
                                //如果是本地访问或者访问254，则MQ内置的表情图片存放位置为http://192.168.1.254:10021
                                if (Request.Url.Host == "192.168.1.254" || Request.Url.Host == "localhost")
                                {
                                    replaceText = string.Format("<a href=\"{0}/{1}\" target=\"_blank\"><img id=\"{2}\" sourcewidth=\"0\" src=\"{0}/{1}\"/></a>", "http://192.168.1.254:10021/face", GetMqPic(str), System.Guid.NewGuid().ToString());
                                }
                                //如果外网访问，则内置的表情图片存放位置为http://files.tongye114.com
                                else
                                {
                                    replaceText = string.Format("<a href=\"{0}/{1}\" target=\"_blank\"><img id=\"{2}\" sourcewidth=\"0\" src=\"{0}/{1}\"/></a>", "http://files.tongye114.com/face", GetMqPic(str), System.Guid.NewGuid().ToString());
                                }
                                item.MessageContent = item.MessageContent.Replace(str, replaceText);
                            }
                        }
                        //如果总字度大于60个汉字长度
                        if (MessageContentLength / 2 > 60)
                        {
                            //生成一个P标签的ID
                            string guid = System.Guid.NewGuid().ToString();
                            html.AppendFormat("<li class=\"mq-Record\"><p id=\"Records_short{0}\" style=\"display: block;word-wrap:break-word;\">{1}<a href=\"\" onclick=\"javascript:show('Records_short{0}','Records_full{0}');return false;\">【+展开】</a></p>", guid, substr);
                            html.AppendFormat("<p id=\"Records_full{0}\" sign=\"hide\" style=\"word-wrap:break-word;\">{1}<a href=\"\" onclick=\"javascript:show('Records_full{0}','Records_short{0}');return false;\">【-收起】</a></p></li>", guid, item.MessageContent);
                        }
                        else
                        {
                            html.AppendFormat("<li class=\"mq-Record\"><p style=\"display: block;word-wrap:break-word;\">{0}</p></li>", item.MessageContent);
                        }
                    }
                    html.Append("</ul>");
                }
                else
                {
                    html.Append("<center>没有搜索到相关记录!</center>");
                }
            }
            catch
            {

            }
            finally
            {
                //释放资源
                list = null;
                syslist = null;
                plist = null;
                ht = null;
            }
            return html.ToString();
        }

        /// <summary>
        /// 根据正则表达式，返回该表达式匹配的所有项
        /// </summary>
        /// <param name="html"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        private List<string> parseHref(string html, string pattern)
        {
            List<string> results = new List<string>();
            if (string.IsNullOrEmpty(html) || string.IsNullOrEmpty(pattern)) return results;
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(pattern);
            //开始编译
            System.Text.RegularExpressions.MatchCollection mc = reg.Matches(html);
            foreach (System.Text.RegularExpressions.Match ma in mc)
            {
                string temp = string.Empty;
                //返回多组数据Group[1]...
                for (int i = 0; i < ma.Groups.Count; i++)
                {
                    temp += ma.Groups[i].Value + "|";
                }
                temp = temp.TrimEnd('|');
                results.Add(temp);
            }
            return results;
        }

        /// <summary>
        /// 获得MQ系统图片的文件名
        /// </summary>
        /// <param name="code">MQ系统表情代码</param>
        /// <returns></returns>
        private string GetMqPic(string code)
        {
            /// /:a1：0.gif,/:a1:1.gif……
            string[] codes = @"/:a1,/:a2,/:a3,/:a4,/:a5,/:a6,/:a7,/:a8,/:a9,/:a0,/:b1,/:b2,/:b3,/:b4,/:b5,/:b6,/:b7,/:b8,/:b9,/:b0,/:c1,/:c2,/:c3,/:c4,/:c5,/:c6,/:c7,/:c8,/:c9,/:c0,/:d1,/:d2,/:d3,/:d4,/:d5,/:d6,/:d7,/:d8,/:d9,/:d0,/:e1,/:e2,/:e3,/:e4,/:e5,/:e6,/:e7,/:e8,/:e9,/:e0,/:f1,/:f2,/:f3,/:f4,/:f5,/:f6,/:f7,/:f8,/:f9,/:f0,/:g1,/:g2,/:g3,/:g4,/:g5,/:g6,/:g7,/:g8,/:g9,/:g0,/:h1,/:h2,/:h3,/:h4,/:h5,/:h6,/:h7,/:h8,/:h9,/:h0,/:i1,/:i2,/:i3,/:i4,/:i5,/:i6,/:i7,/:i8,/:i9,/:i0,/:j1,/:j2,/:j3,/:j4,/:j5,/:j6,/:j7,/:j8,/:j9,/:j0,/:k1,/:k2,/:k3,/:k4,/:k5,/:k6,/:k7,/:k8,/:k9,/:k0,/:l1,/:l2,/:l3,/:l4,/:l5,/:l6,/:l7,/:l8,/:l9,/:l0,/:m1,/:m2,/:m3,/:m4,/:m5,/:m6,/:m7,/:m8,/:m9,/:m0,/:n1,/:n2,/:n3,/:n4,/:n5,/:n6,/:n7,/:n8,/:n9,/:n0,/:o1,/:o2,/:o3,/:o4,/:o5,/:o6,/:o7,/:o8,/:o9,/:o0,/:p1,/:p2,/:p3,/:p4,/:p5,/:p6,/:p7,/:p8,/:p9,/:p0,/:q1,/:q2,/:q3,/:q4,/:q5,/:q6,/:q7,/:q8,/:q9,/:q0,/:r1,/:r2,/:r3,/:r4,/:r5,/:r6,/:r7,/:r8,/:r9,/:r0,/:s1,/:s2,/:s3,/:s4,/:s5,/:s6,/:s7,/:s8,/:s9,/:s0,/:t1,/:t2,/:t3,/:t4,/:t5,/:t6,/:t7,/:t8,/:t9,/:t0".Split(',');
            System.Collections.ArrayList list = new System.Collections.ArrayList(codes);
            return list.IndexOf(code).ToString() + ".gif";
        }
    }
}
