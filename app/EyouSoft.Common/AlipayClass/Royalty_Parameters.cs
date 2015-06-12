using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// 分润接口使用的 提成信息集
/// </summary>
public class Royalty_Parameters
{
	public Royalty_Parameters()
	{
	}

    private List<Royalty_Parameter> Parameters = new List<Royalty_Parameter>();

    public void Add(Royalty_Parameter parameter)
    {
        Parameters.Add(parameter);
    }
    /// <summary>
    /// 把数组所有元素，按照“分润收款账号1^提成金额1^说明1”的模式用“|”字符拼接成字符串
    /// </summary>
    /// <returns></returns>
    public string CreateParametersString()
    {
        StringBuilder prestr = new StringBuilder();
        foreach (Royalty_Parameter parameter in this.Parameters)
        {
            prestr.Append(parameter.ToString() + "|");
        }

        //去掉最后一个"|"字符
        if (prestr.Length > 0)
        {
            prestr.Remove(prestr.Length - 1, 1);
        }

        return prestr.ToString();
    }
}
/// <summary>
/// 分润接口使用的  单条 提成信息集
/// </summary>
public class Royalty_Parameter
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="rece_account">分润收款帐号</param>
    /// <param name="rece_price">分润收款金额</param>
    /// <param name="remark">分润说明</param>
    public Royalty_Parameter(string rece_account, string rece_price, string remark)
    {
        Remark = remark;
        Rece_Account = rece_account;
        Rece_Price = rece_price;
    }
    /// <summary>
    /// 分润收款帐号
    /// </summary>
    public string Rece_Account { get; set; }

    public string Rece_Price { get; set; }
    /// <summary>
    /// 分润说明，说明不能超过30个字符，不能有^和|符号
    /// </summary>
    public string Remark { get; set; }

    public override string ToString()
    {
        if (Remark.Length > 30)
        {
            Remark = Remark.Substring(0, 30);
        }
        Remark = Remark.Replace('^',' ');
        Remark = Remark.Replace('|',' ');
        //格式：分润收款账号1^提成金额1^说明1
        return string.Format("{0}^{1}^{2}",Rece_Account,Rece_Price,Remark);
    }

}
