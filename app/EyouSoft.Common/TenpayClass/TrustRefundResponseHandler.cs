using System;
using System.Collections;
using System.Text;
using System.Web;

namespace tenpay
{
	/// <summary>
	/// TrustRefundResponseHandler 的摘要说明。
	/// </summary>
	/**
	* 委托退款签约应答类
	* ============================================================================
	* api说明：
	* getKey()/setKey(),获取/设置密钥
	* getParameter()/setParameter(),获取/设置参数值
	* getAllParameters(),获取所有参数
	* isTenpaySign(),是否财付通签名,true:是 false:否
	* doShow(),显示处理结果
	* getDebugInfo(),获取debug信息
	* 
	* ============================================================================
	*
	*/
	public class TrustRefundResponseHandler:ResponseHandler
	{
		public TrustRefundResponseHandler(HttpContext httpContext) : base(httpContext)
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		/**
	 * 是否财付通签名
	 * @Override
	 * @return boolean
	 */
		
		public override Boolean isTenpaySign() 
		{
		
			//获取参数
			string cmdno = getParameter("cmdno");
            string spid = getParameter("spid");
            string uin = getParameter("uin");
            string status = getParameter("status");
            string tenpaySign = getParameter("cftsign").ToUpper();
			string key = this.getKey();
			
			//组织签名串
			StringBuilder sb = new StringBuilder();
			sb.Append("cmdno=" + cmdno + "&");
            sb.Append("spid=" + spid + "&");
            sb.Append("uin=" + uin + "&");
            sb.Append("status=" + status );
			sb.Append(key);
		
			//算出摘要
			string sign = MD5Util.GetMD5(sb.ToString(),getCharset());	

			//debug信息
			setDebugInfo(sb.ToString() + " => sign:" + sign +
				" tenpaySign:" + tenpaySign);
		
			 return sign.Equals(tenpaySign);
		} 
	
	
	}
}
