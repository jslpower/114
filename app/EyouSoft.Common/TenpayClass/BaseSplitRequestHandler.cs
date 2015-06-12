using System;
using System.Text;
using System.Web;
using System.Web.UI;


namespace tenpay
{
	/// <summary>
	/// BaseSplitRequestHandler 的摘要说明。
	/// </summary>
	public class BaseSplitRequestHandler:RequestHandler
	{
		public BaseSplitRequestHandler(HttpContext httpContext) : base(httpContext)
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		protected override void createSign() 
		{
			base.createSign();
		
			this.setParameter("sign", this.getParameter("sign").ToUpper());

		}

	
	}
}
