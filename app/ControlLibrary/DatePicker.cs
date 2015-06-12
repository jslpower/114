using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Globalization;

namespace Adpost.Common.DatePicker
{
	/// <summary>
	/// 新版日期选择控件
	/// </summary>
	[DefaultProperty("Text"), 
	ToolboxData("<{0}:DatePicker runat=server></{0}:DatePicker>")]
	public class DatePicker : System.Web.UI.WebControls.WebControl ,INamingContainer
	{
		private TextBox _dateTextBox;
		private System.Web.UI.WebControls.Image _calendarImage;

		#region properties		
		[Bindable(true), 
		Category("Appearance"), 
		DefaultValue("")] 
		public string Text 
		{
			get 
			{
				EnsureChildControls();
				return this._dateTextBox.Text;
			}
			set 
			{ 
				EnsureChildControls();
				this._dateTextBox.Text = value;
			}

		}
		[Bindable(true), Category("Appearance"), DefaultValue("")]
		public void AttributesJSFunction(string AttributesName,string AttributesValue)
		{
			EnsureChildControls();
			this._dateTextBox.Attributes.Add(AttributesName,AttributesValue);
		}
		[Bindable(true), Category("Appearance"), DefaultValue("~/JsCalendar/cal.gif")]
		public string calPicPath
		{
			get { return (ViewState["calPicPath"] != null ? (string)ViewState["calPicPath"] : "~/JsCalendar/cal.gif"); }
			set { ViewState["calPicPath"] = value; } 
		}
		[Bindable(true), Category("Appearance"), DefaultValue(CalendarTheme.blue)]
		public CalendarTheme Theme
		{
			get { return (ViewState["Theme"] != null ? (CalendarTheme)ViewState["Theme"] : CalendarTheme.blue); }
			set { ViewState["Theme"] = value; } 
		}
		[Bindable(true), Category("Behavior"), DefaultValue(CalendarLanguage.en)]
		public CalendarLanguage Language
		{
			get { return (ViewState["Language"] != null ? (CalendarLanguage)ViewState["Language"] : CalendarLanguage.en); }
			set { ViewState["Language"] = value; }  
		}
		[Bindable(true), Category("Behavior"), DefaultValue("~/JsCalendar")]
		public string SupportDir
		{
			get { return (ViewState["SupportDir"] != null ? (string)ViewState["SupportDir"] : ""); }
			set { ViewState["SupportDir"] = value; }  
		}
		[Bindable(true), Category("Behavior"), DefaultValue(false)]
		public bool DisplayTime
		{
			get { return (ViewState["DisplayTime"] != null ? (bool)ViewState["DisplayTime"] : false); }
			set { ViewState["DisplayTime"] = value; }  
		}
		[Browsable(false), Bindable(false)]
		public string DateFormat
		{
			get { return (ViewState["DateFormat"] != null ? (string)ViewState["DateFormat"] : ""); }
			set { ViewState["DateFormat"] = value; }  
		}
		[Browsable(false), Bindable(false)]
		public string TimeFormat
		{
			get { return (ViewState["TimeFormat"] != null ? (string)ViewState["TimeFormat"] : ""); }
			set { ViewState["TimeFormat"] = value; }  
		}
		[Bindable(true), Category("Behavior")]
		public DateTime SelectedDate
		{
			get 
			{ 
				EnsureChildControls();
				if (this.Text.Length > 0)
				{
					try
					{
						return DateTime.Parse(this.Text);
					}
					catch (FormatException ex)
					{
						System.Diagnostics.Trace.WriteLine("Invalid datetime: " + this._dateTextBox.Text + " " + ex.Message, this.GetType().FullName);
						return DateTime.MinValue;
					}
				}
				else
				{
					return DateTime.MinValue;
				}
			}
			set 
			{ 
				EnsureChildControls();
				this.Text = value.ToShortDateString();
				if (this.DisplayTime)
				{
					this.Text += " " + value.ToShortTimeString();
				}
			}
		}
		public override ControlCollection Controls
		{
			get
			{
				EnsureChildControls();
				return base.Controls;
			}
		}
  
		[TypeConverter(typeof(UnitConverter))]
		public override Unit Width
		{
			get
			{
				EnsureChildControls();
				return base.Width;
			}
			set
			{
				EnsureChildControls();
				base.Width = value;
				this._dateTextBox.Width = Unit.Pixel((int)value.Value);
			}
		}
		public override string CssClass
		{
			get
			{
				EnsureChildControls();
				return this._dateTextBox.CssClass;
			}
			set
			{
				EnsureChildControls();
				//base.CssClass = value;
				this._dateTextBox.CssClass = value;
			}
		}
		#endregion
		public event System.EventHandler DateChanged;
		protected virtual void OnDateChanged(object sender)
		{
			if (DateChanged != null)
			{
				DateChanged(sender, System.EventArgs.Empty);
			}
		}
		public DatePicker()
		{
			// Set defaults
			this.Theme = CalendarTheme.blue;
			this.SupportDir = "~/DatePicker";
			this.DisplayTime = false;
			this.DateFormat = ConvertDateFormat(CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern);
			this.TimeFormat = ConvertTimeFormat(CultureInfo.CurrentCulture.DateTimeFormat.ShortTimePattern);
			SetLanguage();
		}
		protected override void CreateChildControls()
		{
			this._dateTextBox = new TextBox();
			this._calendarImage = new Image();
			this._dateTextBox.EnableViewState = true;
			this._dateTextBox.ID = "dateTextBox";
			this._dateTextBox.CssClass = base.CssClass;
			//this._dateTextBox.ReadOnly = true;			
			this._dateTextBox.TextChanged += new EventHandler(DateTextBox_TextChanged);
			
			this._calendarImage.EnableViewState = false;
			this._calendarImage.ID = "trigger";
			this._calendarImage.ImageUrl = GetClientFileUrl("cal.gif");
			this._calendarImage.ImageUrl = calPicPath;
			this._calendarImage.Attributes["align"] = "top";
			this._calendarImage.Attributes["hspace"] = "4";					
			Controls.Add(this._dateTextBox);
			//Controls.Add(this._calendarImage);
		}
		/// <summary> 
		/// Render this control to the output parameter specified.
		/// </summary>
		/// <param name="output"> The HTML writer to write out to </param>
		protected override void Render(HtmlTextWriter output)
		{
			if (this.Site != null && this.Site.DesignMode)
			{
				this._dateTextBox.RenderControl(output);
				output.Write("[" + this.ID + "]");
			}
			else
			{
				base.Render(output);
			}
		}
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender (e);
			/*
			string themeCss = GetClientCssImport(String.Format("calendar-{0}.css", this.Theme.ToString().Replace("_", "-")));
			Page.RegisterClientScriptBlock(typeof(string),"calendarcss", themeCss);
			string calendarScripts = "";
			calendarScripts += GetClientScriptInclude("calendar_stripped.js",this.Language);
			string languageFile = String.Format("lang/calendar-{0}.js", this.Language.ToString());
			calendarScripts += GetClientScriptInclude(languageFile,this.Language);
			calendarScripts += GetClientScriptInclude("calendar-setup_stripped.js",this.Language);
			Page.RegisterClientScriptBlock(typeof(string),"calendarscripts", calendarScripts);
			string setupScript = GetCalendarSetupScript(this._dateTextBox.ClientID, GetFormatString(), this.ClientID);
			this._dateTextBox.Attributes["onclick"]="return ShowCalendar(this,'" + GetFormatString() + "'," + this.DisplayTime.ToString().ToLower() + ")";
			Page.RegisterStartupScript(this.ClientID + "script", setupScript);
			*/
			string calendarScripts = GetClientScriptInclude("WdatePicker.js?id=" + Guid.NewGuid().ToString() + "",this.Language);
            Page.ClientScript.RegisterClientScriptBlock(typeof(string),"calendarscripts", calendarScripts);
			if(this.DisplayTime)//显示时间
			{
				this._dateTextBox.Attributes["onfocus"] = "WdatePicker({dateFmt:'yyyy-MM-dd HH:mm'})";
			}
			else
			{
				this._dateTextBox.Attributes["onfocus"] = "WdatePicker()";
			}
			if(this.DisplayTime)//显示时间
			{
				this._calendarImage.Attributes["onclick"] = "WdatePicker({el:$dp.$('" + this._dateTextBox.ClientID + "',dateFmt:'yyyy-MM-dd HH:mm')})";
			}
			else
			{
				this._calendarImage.Attributes["onclick"] = "WdatePicker({el:$dp.$('" + this._dateTextBox.ClientID + "')})";
			}	
		}
		private void SetLanguage()
		{
			string currentLanguage = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
			try
			{
				CalendarLanguage cl = (CalendarLanguage)Enum.Parse(typeof(CalendarLanguage), currentLanguage);
				this.Language = cl;
			}
			catch
			{
				// Default is 'en'
				this.Language = CalendarLanguage.en;
			}
		}
		private string GetClientFileUrl(string fileName)
		{
			return ResolveUrl(this.SupportDir + "/" + fileName);
			//return this.SupportDir + "/" + fileName;
		}
		private string GetClientScriptInclude(string scriptFile,CalendarLanguage language) 
		{
			string strScript = string.Empty;
			if(language == CalendarLanguage.zh)
			{
				strScript = "<script language=\"JavaScript\" src=\"" +
					GetClientFileUrl(scriptFile) + "\" charset=\"gb2312\"></script>\n";
			}
			else
			{
				strScript = "<script language=\"JavaScript\" src=\"" +
					GetClientFileUrl(scriptFile) + "\"></script>\n";
			}
			return strScript;
		}
		private string GetClientCssImport(string fileName)
		{
			return "<style type=\"text/css\">@import url(" + GetClientFileUrl("css/" + fileName) + ");</style>";
		}
		private string GetCalendarSetupScript(string inputField, string format, string trigger)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("<script type=\"text/javascript\">\nCalendar.setup( {\ninputField : \"");
			sb.Append(inputField);
			sb.Append("\", \nifFormat : \"");
			sb.Append(format);			
			sb.Append("\", \nshowsTime : ");
			sb.Append(this.DisplayTime.ToString().ToLower());
			sb.Append(", \nbutton : \"");
			sb.Append(trigger + "_trigger");
			sb.Append("\",\nalign :\"Bl\"");
			//sb.Append("\",\nonUpdate:catcalc");
			sb.Append(",\nsingleClick : true\n");
			sb.Append(" } ); </script>");
			return sb.ToString();
		}
		public string GetFormatString()
		{
			if (this.DisplayTime)
			{
				return this.DateFormat + " " + this.TimeFormat;
			}
			else
			{
				return this.DateFormat;
			}
		}
		private void DateTextBox_TextChanged(object sender, EventArgs e)
		{
			OnDateChanged(sender);
		}
		private string ConvertDateFormat(string shortDateFormat)
		{
			string tempFormat = shortDateFormat.Replace("yyyy", "%Y");
			tempFormat = tempFormat.Replace("M", "%m");
			tempFormat = tempFormat.Replace("d", "%d");
			return tempFormat;
		}
		private string ConvertTimeFormat(string shortTimeFormat)
		{
			string tempFormat = shortTimeFormat.Replace("H", "%H");
			tempFormat = tempFormat.Replace("mm", "%M");
			tempFormat = tempFormat.Replace("h", "%I");
			tempFormat = tempFormat.Replace("tt", "%p");
			return tempFormat;
		}
	}
	public enum CalendarTheme
	{
		blue,
		blue2,
		brown,
		green,
		system
	}
	public enum CalendarLanguage
	{
		en,
		zh
	}
}
