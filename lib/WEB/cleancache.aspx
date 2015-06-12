<%@ Page Language="C#" AutoEventWireup="true"  %>

<%@ Import Namespace="System.Collections.Generic" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    缓存KEY：
    <select name="drpCacheKey">
    <option value="PLATFORM/CompanyState/">单位状态信息</option>
    <option value="PLATFORM/COMPANY/SETTING/">单位设置</option>
    <option value="PLATFORM/COMPANY/CITYAREA/">公司销售城市</option>
    <option value="PLATFORM/COMPANY/SITEAREA/">公司常用出港城市</option>
    <option value="PLATFORM/COMPANY/PRICESTAND/">公司常用报价等级</option>
    <option value="PLATFORM/COMPANY/USER/">公司用户</option>
    <option value="PLATFORM/SYSTEM_DOMAIN/">公司域名</option>
    <option value="PLATFORM/SYSTEM/CUSTOMER_LEVEL">系统客户等级</option>
    </select>
    </div>
    公司GUID:<asp:TextBox runat=server ID="txtCacheName"></asp:TextBox>
    
    <asp:Button runat=server ID="btnCacheDo" Text="清除缓存" 
        onclick="btnCacheDo_Click" />

    <script runat="server"> 
            protected void btnCacheDo_Click(object sender, EventArgs e)
            {
                EyouSoft.Cache.Facade.EyouSoftCache.Remove(Request.Form["drpCacheKey"] + txtCacheName.Text);
                Response.Write("已清除");
                    
            }

            protected void btnGetUser_Click(object sender, EventArgs e)
            {
                
                EyouSoft.SSOComponent.Entity.UserInfo UserInfo = (EyouSoft.SSOComponent.Entity.UserInfo)EyouSoft.Cache.Facade.EyouSoftSSOCache.GetCache(EyouSoft.CacheTag.Company.CompanyUser + Request.Form["txtUserName"]);
                if (UserInfo != null)
                {
                    Response.Write(UserInfo.UserName);
                }

            }

            protected void Page_Load(object sender, EventArgs e)
            {
                CreateHotelJsonData();
            }

            /// <summary>
            /// 生成酒店json数据
            /// </summary>
            private void CreateHotelJsonData()
            {
                Response.Write(GetHotelLandMarksJson());
            }

            /// <summary>
            /// 生成酒店地标json数据
            /// </summary>
            /// <returns></returns>
            private string GetHotelLandMarksJson()
            {
                IList<EyouSoft.Model.HotelStructure.HotelLandMarks> items=EyouSoft.BLL.HotelStructure.HotelLandMarks.CreateInstance().GetList(null);
                if (items == null && items.Count < 1) return "var GeographyList=[];";
                
                StringBuilder s = new StringBuilder();
                s.Append("var GeographyList=");
                s.Append("[");
                                
                for (int i = 0; i < items.Count;i++ )
                {
                    //item json:{ID:"92",C:"WUX",P:"\u4e94\u7231\u5e7f\u573a"}
                    string itemJson = "{3}{{ID:\"{0}\",C:\"{1}\",P:\"{2}\"}}";

                    if (i == 0)
                        s.AppendFormat(itemJson, items[i].Id, items[i].CityCode.Trim(), GetUnicode(items[i].Por), "");
                    else
                        s.AppendFormat(itemJson, items[i].Id, items[i].CityCode.Trim(), GetUnicode(items[i].Por), ",");                    
                }
                s.Append("];");

                return s.ToString();
            }

            /// <summary>
            /// 将字符串转换为Unicode编码
            /// </summary>
            /// <param name="s">要转换编码的字符串</param>
            /// <returns></returns>
            public static string GetUnicode(string s)
            {
                Encoding encode = System.Text.Encoding.Unicode;
                byte[] bytes = encode.GetBytes(s);
                char[] chars = encode.GetChars(bytes);
                StringBuilder unicodeStr = new StringBuilder();

                foreach (char c in chars)
                {
                    unicodeStr.Append("\\u" + ((int)c).ToString("X4"));
                }
                
                return unicodeStr.ToString();
            }
            
    </script>

    <asp:TextBox runat=server ID="txtUserName"></asp:TextBox>
    <asp:Button runat=server ID="btnGetUser" Text="获取用户信息" 
        onclick="btnGetUser_Click" />
    </form>
</body>
</html>
