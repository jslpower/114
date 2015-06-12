using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;


namespace tenpay
{
    public class ScriptClientResponseHandler : ClientResponseHandler
    {
       
        public ScriptClientResponseHandler()
        {
            
        }

        public override void setContent(string content)
        {
            this.content = content;

            Regex r = new Regex("window.location.href[ \t]*=[ \t]*[\'\"]([^\'\"]*)[\'\"]", RegexOptions.IgnoreCase);
            Match m = r.Match(content);
            if (m.Success)
            {
                string url = m.Groups[1].ToString();

                char[] seps1 = new char[] { '?' };
                char[] seps2 = new char[] { '&' };
                char[] seps3 = new char[] { '=' };

                string[] urls = url.Split(seps1);
                if (urls != null && urls.Length >= 2)
                {
                    string[] paras = urls[1].Split(seps2);
                    foreach (string para in paras)
                    {
                        string[] kav = para.Split(seps3);
                        if (kav.Length == 2)
                        {
                            this.setParameter(kav[0], TenpayUtil.UrlDecode(kav[1], this.getCharset()));
                        }

                    }
                }
                else
                {
                    this.setParameter("pay_result", "99");
                    this.setParameter("pay_info", "返回包格式错误，请检查协议是否改变！");
                }

                
            }
            else
            {
                this.setParameter("pay_result", "99");
                this.setParameter("pay_info", "返回包格式错误，请检查协议是否改变！");
            }

        }
    }
}
