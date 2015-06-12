using System;
using System.Web.UI;
using System.Collections.Specialized;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Text;

namespace Adpost.Common.ExportPageSet
{
	/// <summary>
	/// 分页控件
	/// </summary>
	/// 修改人:陈志仁	修改时间:2009-04-08
	/// 修改内容:分页样式3:修改为带下拉的分页样式.
	/// ---------------------------------------------------------------
	/// 修改人:陈志仁	修改时间:2009-05-25
	/// 修改内容:分页样式3:修改为带下拉的分页样式 当无记录时下拉显示为第一页
	[DefaultProperty("Text"), 
		ToolboxData("<{0}:ExportPageInfo runat=server></{0}:ExportPageInfo>")]
	public class ExportPageInfo : System.Web.UI.WebControls.WebControl
	{
		private int _intRecordCount = 0,_CurrencyPage = 1,_intPageSize = 10,_LinkType = 3;
		private string _prePic = "/images/shangyici.gif",_nextPic = "/images/xiayici.gif";
		private string _PageLinkURL = "",_CurrencyPageCssClass = "RedFnt", _LinkCssClass = "";
		private NameValueCollection _UrlParams=new NameValueCollection();
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
		[Bindable(true), Category("Behavior"), DefaultValue("RedFnt")]
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
		}//链接显示的CSS
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
            string retval = "", retval2 = "",retval3 = "",tmpReturnValue = "";
            StringBuilder retdropval = new StringBuilder();
			int intPageCount = 0,BasePage = 0,pageNumber=0;
			string NumLinkClass = " class=\"" + _CurrencyPageCssClass + "\"";
			_PageLinkURL=_PageLinkURL+BuildUrlString(_UrlParams);
			if(_intRecordCount % _intPageSize == 0)
			{
				intPageCount = Convert.ToInt32(_intRecordCount / _intPageSize);
			}
			else
			{
				intPageCount = Convert.ToInt32(_intRecordCount / _intPageSize) + 1;
			}
			if(intPageCount>0)
			{
				retval = "第 " + _CurrencyPage.ToString() + " 页/总 " + intPageCount.ToString() + " 页 ";
				retdropval.Append("第<font color=\"green\">" + _CurrencyPage.ToString() + "</font>/<font color=\"red\">" + intPageCount.ToString() + "</font>页 ");
			}
			else
			{
				retval = "第 " + _CurrencyPage.ToString() + " 页/总 1 页 ";
				retdropval.Append( "第<font color=\"green\">" + _CurrencyPage.ToString() + "</font>页/总<font color=\"red\">1</font>页 ");
			}
			retval3 = retval;
			retval3 += " <a href=\"" + _PageLinkURL + "Page=1\">首页</a>  ";
            if (_LinkType == 3) //3的时候为下拉框
            {
                retdropval.Append(" |  <select id=\"epi_ddlPageOption\" name=\"epi_ddlPageOption\" onchange='javascript:window.location.href=\"" + _PageLinkURL + "Page=\"+this.options[this.selectedIndex].value;'>");
                //下拉分页开始
                if (intPageCount > 0)
                {
                    for (int i = 1; i <= intPageCount; i++)
                    {
                        if (_CurrencyPage != i)
                        {
                          retdropval.Append( "<option value=\"" + i.ToString() + "\">" + i.ToString() + "</option>");
                        }
                        else
                        {
                           retdropval.Append( "<option value=\"" + i.ToString() + "\" selected=\"selected\">" + i.ToString() + "</option>");
                        }
                    }
                }
                else
                {
                   retdropval.Append( "<option value=\"1\">1</option>");
                }
               retdropval.Append( "</select>");
                //下拉分页结束
            }
			if(_CurrencyPage <= 1)
			{
				retval = retval + "首页 上一页 ";
				retdropval.Append( "首页 上一页 ");
			}
			else
			{
				retval = retval + " <a href=\"" + _PageLinkURL + "Page=1\">首页</a>  <a href=\"" + _PageLinkURL + "Page=" + Convert.ToString(_CurrencyPage - 1) + "\">上一页</a>  ";
				retdropval.Append(" <a href=\"" + _PageLinkURL + "Page=1\">首页</a>  <a href=\"" + _PageLinkURL + "Page=" + Convert.ToString(_CurrencyPage - 1) + "\">上一页</a>  ");
			}
			if(_CurrencyPage >= intPageCount)
			{
				retval = retval + " 下一页 末页";
				retdropval.Append(" 下一页 末页");
			}
			else
			{
				retval = retval + " <a href=\"" + _PageLinkURL + "Page=" + Convert.ToString(_CurrencyPage + 1) + "\">下一页</a>  <a href=\"" + _PageLinkURL + "Page=" + intPageCount.ToString() + "\">末页</a> ";
				retdropval.Append(" <a href=\"" + _PageLinkURL + "Page=" + Convert.ToString(_CurrencyPage + 1) + "\">下一页</a>  <a href=\"" + _PageLinkURL + "Page=" + intPageCount.ToString() + "\">末页</a> ");
			}
			//添加数字分页
			BasePage = Convert.ToInt32((_CurrencyPage / 10) * 10);
			if(BasePage > 0)
			{
				retval2 += " <a href=\"" + _PageLinkURL + "Page=" + Convert.ToString((BasePage - 9)) + "\"" + NumLinkClass + ">&lt;&lt;</a>";
                retval3 += " <a href=\"" + _PageLinkURL + "Page=" + Convert.ToString((BasePage - 9)) + "\"" + NumLinkClass + "><<</a>";//luofuxian--<img src=\"" + _prePic + "\" border=\"0\"></a>
			}
			for(int i = 1; i<=10; i++)
			{
				pageNumber = BasePage + i;
				if(pageNumber > intPageCount)
				{
					i = 11;
				}
				else
				{
					if(pageNumber == _CurrencyPage)
					{
						retval2 += " <span class=\"RedFnt\">" + pageNumber.ToString() + "</span>";
						retval3 += " <span class=\"RedFnt\">" + pageNumber.ToString() + "</span>";
					}
					else
					{
						retval2 += " <a href=\"" + _PageLinkURL + "Page=" + pageNumber.ToString() + "\">" + pageNumber.ToString() + "</a>";
						retval3 += " <a href=\"" + _PageLinkURL + "Page=" + pageNumber.ToString() + "\">" + pageNumber.ToString() + "</a>";
					}
				}
			}
			if(intPageCount > pageNumber)
			{
				retval2 += " <a href=\"" + _PageLinkURL + "Page=" + Convert.ToString((BasePage + 11)) + "\">&gt;&gt;</a>";
                retval3 += " <a href=\"" + _PageLinkURL + "Page=" + Convert.ToString((BasePage + 11)) + "\">>></a>";//luofuxian---<img src=\"" + _nextPic + "\" border=\"0\">
			}
			retval3 += "  <a href=\"" + _PageLinkURL + "Page=" + intPageCount.ToString() + "\">末页</a> ";
			switch(_LinkType)
			{
				case 1:
					tmpReturnValue = retval;
					break;
				case 2:
					tmpReturnValue = retval2;
					break;
				case 3:
					tmpReturnValue = retdropval.ToString();
					break;
				case 4:
					tmpReturnValue = retval3;
					break;
				default:
					tmpReturnValue = retval + "<br>" + retval2;
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
				if (newColKeys[i] != "page" )
				{					
					sb.Append(newColKeys[i]);
					sb.Append("=");				
					sb.Append(Page.Server.UrlEncode(newCol[i]));
					sb.Append("&");
				}
			}
			return sb.ToString();
		}
	}
}
