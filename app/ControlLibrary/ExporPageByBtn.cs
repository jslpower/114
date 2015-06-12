using System;
using System.Web.UI;
using System.Collections.Specialized;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Text;

namespace Adpost.Common.ExporPageByBtn
{
    /// <summary>
    /// ExporPageByBtn 的摘要说明。
    /// </summary>
    [DefaultProperty("Text"),
      ToolboxData("<{0}:ExporPageByBtn runat=server></{0}:ExporPageByBtn>")]
    public class ExporPageByBtn : System.Web.UI.WebControls.WebControl
    {
        private int _intRecordCount = 0, _CurrencyPage = 1, _intPageSize = 10, _LinkType = 3, index = 1;
        private string _prePic = "/images/shangyici.gif", _nextPic = "/images/xiayici.gif";
        private string _PageLinkURL = "", _CurrencyPageCssClass = "current", _LinkCssClass = "", _DivCurrencyPageCssClass = "yahoo2", _NextBtnText = "下一页", _PrevBtnText = "上一页";
        private NameValueCollection _UrlParams = new NameValueCollection();
        #region 取得资源中的图片数据
        //Assembly ab = Assembly.GetExecutingAssembly();
        //string resName = 
        //_prePic = System.Drawing.

        #endregion 结束
        #region model
        //分页大小
        [Bindable(true), Category("Behavior"), DefaultValue(10)]
        public virtual int intPageSize
        {
            get
            {
                return _intPageSize;
            }

            set
            {
                _intPageSize = value;
            }
        }
        //总记录数
        [Bindable(true), Category("Behavior"), DefaultValue(0)]
        public virtual int intRecordCount
        {
            get
            {
                return _intRecordCount;
            }

            set
            {
                _intRecordCount = value;
            }
        }
        //当前页面数
        [Bindable(true), Category("Behavior"), DefaultValue(1)]
        public virtual int CurrencyPage
        {
            get
            {
                return _CurrencyPage;
            }

            set
            {
                _CurrencyPage = value;
            }
        }
        //向前翻图片
        [Bindable(true), Category("Behavior"), DefaultValue(1)]
        public virtual string PrePicture
        {
            get
            {
                return _prePic;
            }

            set
            {
                _prePic = value;
            }
        }
        //向后翻图片
        [Bindable(true), Category("Behavior"), DefaultValue(1)]
        public virtual string NextPictrue
        {
            get
            {
                return _nextPic;
            }

            set
            {
                _nextPic = value;
            }
        }
        //网页链接地址
        [Bindable(true), Category("Behavior"), DefaultValue("")]
        public virtual string PageLinkURL
        {
            get
            {
                return _PageLinkURL;
            }

            set
            {
                _PageLinkURL = value;
            }
        }
        //分页显示方式
        [Bindable(true), Category("Behavior"), DefaultValue(3)]
        public virtual int LinkType
        {
            get
            {
                return _LinkType;
            }

            set
            {
                _LinkType = value;
            }
        }
        //当前页数字显示CSS
        [Bindable(true), Category("Behavior"), DefaultValue("current")]
        public virtual string CurrencyPageCssClass
        {
            get
            {
                return _CurrencyPageCssClass;
            }

            set
            {
                _CurrencyPageCssClass = value;
            }
        }
        //数字按钮所在DIV的显示CSS
        [Bindable(true), Category("Behavior"), DefaultValue("yahoo2")]
        public virtual string DivCurrencyPageCssClass
        {
            get
            {
                return _DivCurrencyPageCssClass;
            }

            set
            {
                _DivCurrencyPageCssClass = value;
            }
        }

        //下一页(组)按钮的文字
        [Bindable(true), Category("Behavior"), DefaultValue("下一页")]
        ///下一页(组)按钮的文字
        public virtual string NextBtnText
        {
            get
            {
                return _NextBtnText;
            }

            set
            {
                _NextBtnText = value;
            }
        }

        //上一页(组)按钮的文字
        [Bindable(true), Category("Behavior"), DefaultValue("上一页")]
        public virtual string PrevBtnText
        {
            get
            {
                return _PrevBtnText;
            }

            set
            {
                _PrevBtnText = value;
            }
        }

        //链接显示的CSS
        [Bindable(true), Category("Behavior"), DefaultValue("")]
        public virtual string LinkCssClass
        {
            get
            {
                return _LinkCssClass;
            }

            set
            {
                _LinkCssClass = value;
            }
        }
        public virtual NameValueCollection UrlParams
        {
            get
            {
                return _UrlParams;
            }

            set
            {
                _UrlParams = value;
            }
        }
        #endregion

        /// <summary> 
        /// 将此控件呈现给指定的输出参数。
        /// </summary>
        /// <param name="output"> 要写出到的 HTML 编写器 </param>
        protected override void Render(HtmlTextWriter output)
        {
            string retval = "", retval2 = "", retval3 = "", retval4 = "", tmpReturnValue = "";
            int intPageCount = 0, BasePage = 0, pageNumber = 0;
            string NumLinkClass = " class=\"" + _CurrencyPageCssClass + "\"";
            string NumDivLinkClass = " class=\"" + _DivCurrencyPageCssClass + "\"";
            _PageLinkURL = _PageLinkURL + BuildUrlString(_UrlParams);
            if (_intRecordCount % _intPageSize == 0)
            {
                intPageCount = Convert.ToInt32(_intRecordCount / _intPageSize);
            }
            else
            {
                intPageCount = Convert.ToInt32(_intRecordCount / _intPageSize) + 1;
            }

            if (intPageCount <= 1)
                return;

            //添加数字分页
            BasePage = Convert.ToInt32((_CurrencyPage / 10) * 10);
            retval += "<div" + NumDivLinkClass + ">";
            retval2 += "<div" + NumDivLinkClass + ">";
            retval3 += "<div" + NumDivLinkClass + ">";
            retval4 += "<div" + NumDivLinkClass + ">";

            retval2 += " <span " + NumLinkClass + " >共" + intPageCount + "页</span>";
            retval4 += "每页" + _intPageSize + "条 共" + _intRecordCount + "条信息 共" + intPageCount + "页";

            if (CurrencyPage > 1)
            {
                retval += " <a href=\"" + _PageLinkURL + "Page=" + Convert.ToString((CurrencyPage - 1)) + "\">" + SafeRequest(PrevBtnText) + "</a>";
                retval2 += " <a href=\"" + _PageLinkURL + "Page=" + Convert.ToString((CurrencyPage - 1)) + "\">" + SafeRequest(PrevBtnText) + "</a>";
                retval4 += " <a href=\"" + _PageLinkURL + "Page=" + Convert.ToString((CurrencyPage - 1)) + "\">" + SafeRequest(PrevBtnText) + "</a>";
            }

            //在后一组按钮前面添加前一组按钮的最后两个按钮
            if (BasePage > 0)
            {
                index = -1;
                retval3 += " <a href=\"" + _PageLinkURL + "Page=" + Convert.ToString((BasePage - 9)) + "\">" + PrevBtnText + "</a>";
            }

            for (int i = index; i <= 10; i++)
            {
                pageNumber = BasePage + i;
                if (pageNumber > intPageCount)
                {
                    i = 11;
                }
                else
                {

                    if (pageNumber == _CurrencyPage)
                    {
                        retval += " <span " + NumLinkClass + " >" + pageNumber.ToString() + "</span>";
                        retval2 += " <span " + NumLinkClass + " >" + pageNumber.ToString() + "</span>";
                        retval3 += " <span " + NumLinkClass + ">" + pageNumber.ToString() + "</span>";
                        retval4 += " <span " + NumLinkClass + ">" + pageNumber.ToString() + "</span>";
                        //retval2 += "<span class=\"current\">" + pageNumber + "</span>";
                        //retval3 += "<span class=\"current\">" + pageNumber + "</span>";
                    }
                    else
                    {
                        retval += " <a href=\"" + _PageLinkURL + "Page=" + pageNumber.ToString() + "\">" + pageNumber.ToString() + "</a>";
                        retval2 += " <a href=\"" + _PageLinkURL + "Page=" + pageNumber.ToString() + "\">" + pageNumber.ToString() + "</a>";
                        retval3 += " <a href=\"" + _PageLinkURL + "Page=" + pageNumber.ToString() + "\">" + pageNumber.ToString() + "</a>";
                        retval4 += " <a href=\"" + _PageLinkURL + "Page=" + pageNumber.ToString() + "\">" + pageNumber.ToString() + "</a>";
                    }
                }
            }

            if (intPageCount > CurrencyPage)
            {
                retval += " <a href=\"" + _PageLinkURL + "Page=" + Convert.ToString((CurrencyPage + 1)) + "\">" + SafeRequest(NextBtnText) + "</a>";
                retval2 += " <a href=\"" + _PageLinkURL + "Page=" + Convert.ToString((CurrencyPage + 1)) + "\">" + SafeRequest(NextBtnText) + "</a>";
                retval4 += " <a href=\"" + _PageLinkURL + "Page=" + Convert.ToString((CurrencyPage + 1)) + "\">" + SafeRequest(NextBtnText) + "</a>";
            }

            if (intPageCount > pageNumber)
            {
                retval3 += " <a href=\"" + _PageLinkURL + "Page=" + Convert.ToString((BasePage + 11)) + "\">" + SafeRequest(NextBtnText) + "</a>";
            }

            retval2 += "</div>";
            retval3 += "</div>";
            retval4 += "</div>";

            switch (_LinkType)
            {
                case 1:
                    tmpReturnValue = retval;
                    break;
                case 2:
                    tmpReturnValue = retval2;
                    break;
                case 3:
                    tmpReturnValue = retval3;
                    break;
                case 4:
                    tmpReturnValue = retval4;
                    break;
                default:
                    tmpReturnValue = retval3;
                    break;
            }
            output.Write(tmpReturnValue);
        }

        //处理URL参数
        private string BuildUrlString(NameValueCollection urlParams)
        {
            NameValueCollection newCol = new NameValueCollection(urlParams);
            NameValueCollection col = new NameValueCollection();
            string[] newColKeys = newCol.AllKeys;
            int i;
            for (i = 0; i < newColKeys.Length; i++)
            {
                if (newColKeys[i] != null)
                {
                    newColKeys[i] = newColKeys[i].ToLower();
                }
            }
            StringBuilder sb = new StringBuilder();
            for (i = 0; i < newCol.Count; i++)
            {
                if (newColKeys[i] != "page")
                {
                    sb.Append(newColKeys[i]);
                    sb.Append("=");
                    sb.Append(Page.Server.UrlEncode(newCol[i]));
                    sb.Append("&");
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 处理">"
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string SafeRequest(string str)
        {
            if (str != null && str != string.Empty)
            {
                //str = str.Replace("'", "&#39");
                str = str.Replace("<", "&lt;");
                str = str.Replace(">", "&gt;");
            }
            else
            {
                str = "";
            }
            return str;
        }
    }
}
