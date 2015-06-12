using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace Adpost.Common.ExportHtmlPageInfo
{
	/// <summary>
	/// ExportHtmlPageInfo 的摘要说明。
	/// </summary>
	[DefaultProperty("Text"), 
		ToolboxData("<{0}:ExportHtmlPageInfo runat=server></{0}:ExportHtmlPageInfo>")]
	public class ExportHtmlPageInfo : System.Web.UI.WebControls.WebControl
	{
		private int _intRecordCount = 0,_CurrencyPage = 1,_intPageSize = 10,_LinkType = 3;
		private string _PageLinkURL = "",_FileFxt = ".aspx",_CurrencyPageCssClass="RedFnt", _LinkCssClass="";
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
		//虚拟网页链接地址
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
		//虚拟文件后缀名
		[Bindable(true), Category("Behavior"), DefaultValue(".aspx")]
		public virtual string FileFxt 
		{
			get
			{
				return _FileFxt;
			}

			set
			{
				_FileFxt = value;
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
		#endregion

		/// <summary> 
		/// 将此控件呈现给指定的输出参数。
		/// </summary>
		/// <param name="output"> 输出分页内容 </param>
		protected override void Render(HtmlTextWriter output)
		{
			string  retval = "", retval2 = "",tmpReutrnValue = "";
			int intPageCount = 0,BasePage = 0,pageNumber=0;
			string NumLinkClass = " class=\"" + _CurrencyPageCssClass + "\"";
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
			}
			else
			{
				retval = "第 " + _CurrencyPage.ToString() + " 页/总 1 页 ";
			}
			if(_CurrencyPage <= 1)
			{
				retval = retval + "首页 前页 ";
			}
			else
			{
				retval = retval + " <a href=\"" + _PageLinkURL + "1" + _FileFxt + "\">首页</a>  <a href=\"" + _PageLinkURL + Convert.ToString(_CurrencyPage - 1) + _FileFxt + "\">前页</a>  ";
			}
			if(_CurrencyPage >= intPageCount)
			{
				retval = retval + " 后页 末页";
			}
			else
			{
				retval = retval + " <a href=\"" + _PageLinkURL + Convert.ToString(_CurrencyPage + 1) + _FileFxt + "\">后页</a>  <a href=\"" + _PageLinkURL + intPageCount.ToString() + _FileFxt + "\">末页</a> ";
			}
			//添加数字分页
			retval2 = retval2 + "<br>";
			BasePage = Convert.ToInt32((_CurrencyPage / 10) * 10);
			if(BasePage > 0)
			{
				retval2 = retval2 + " <a href=\"" + _PageLinkURL + Convert.ToString((BasePage - 9)) + _FileFxt + "\"" + NumLinkClass + ">&lt;&lt;</a>";
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
						retval2 = retval2 + " <span class=\"RedFnt\">" + pageNumber.ToString() + "</span>";
					}
					else
					{
						retval2 = retval2 + " <a href=\"" + _PageLinkURL + pageNumber.ToString() + _FileFxt + "\">" + pageNumber.ToString() + "</a>";
					}
				}
			}
			if(intPageCount > pageNumber)
			{
				retval2 = retval2 + " <a href=\"" + _PageLinkURL + Convert.ToString((BasePage + 11)) + _FileFxt + "\">&gt;&gt;</a><br>";
			}
			switch(_LinkType)
			{
				case 1:
					tmpReutrnValue = retval;
					break;
				case 2:
					tmpReutrnValue = retval2;
					break;
				default:
					tmpReutrnValue = retval + retval2;
					break;
			}
			output.Write(tmpReutrnValue);
		}
	}
}
