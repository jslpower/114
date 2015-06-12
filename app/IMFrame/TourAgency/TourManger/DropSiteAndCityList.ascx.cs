using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using System.Collections.Generic;

namespace TourUnion.WEB.IM.TourAgency.TourManger
{
    public partial class DropSiteAndCityList : System.Web.UI.UserControl
    {
        //protected int UnionId = 0;
        //DataSet ProvinceSiteAllDs = null;
        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    TourUnion.Account.Model.Account account = TourUnion.Account.Factory.UserFactory.GetUserManage(TourUnion.Account.Enum.ChildSystemLocation.TourAgency, TourUnion.Account.Enum.SystemMedia.MQ).AccountUser;
        //    if (account != null)
        //    {
        //        UnionId = account.UnionId;
        //    }
        //    account = null;
        //    if (!Page.IsPostBack)
        //    {
        //        BindDropProvinceList();
        //        BindDropCity();
        //    }
        //   int SiteId = 0;
        //   int CityId = 0;
        //   TourUnion.WEB.ProceFlow.SiteInfo site = TourUnion.WEB.ProceFlow.SiteManage.GetIMSiteInfoByIp();
        //   if (site != null)
        //   {
        //       SiteId = site.SiteId;
        //       CityId = site.Cityid;
        //   }
        //   site = null;
        //   if (ProvinceSiteAllDs != null)
        //   {
        //       if (SiteId != 0 && CityId != 0)
        //       {
        //           DataRow[] rows = ProvinceSiteAllDs.Tables[0].Select(string.Format("SiteId={0}", SiteId));
        //           if (rows.Length > 0)
        //           {
        //               int ProvinceId = int.Parse(rows[0]["ProvinceId"].ToString());
        //               string strCityAndSite = CityId + "|" + SiteId;
        //               MessageBox.ResponseScript("SelectedDrop(1," + ProvinceId + ");ChangeList('" + this.dropCity.ClientID + "'," + ProvinceId + ");SelectedDrop(0,'" + strCityAndSite + "');");
        //           }
        //       }
        //   }
        //}

        //#region 绑定出港省份，城市
        //private void BindDropProvinceList()
        //{
        //    TourUnion.Cache.ProvinceSite cache = new TourUnion.Cache.ProvinceSite(UnionId, false);
        //    ProvinceSiteAllDs = cache.GetProvinceSiteCache();
        //    cache = null;
        //    //ProvinceSiteAllDs = WEB.ProceFlow.CacheManage.GetProvinceSiteCache(UnionId, false);
            
        //    if (ProvinceSiteAllDs != null && ProvinceSiteAllDs.Tables.Count > 0 && ProvinceSiteAllDs.Tables[0].Rows.Count > 0)
        //    {
        //        DataTable dt = ProvinceSiteAllDs.Tables[0];
        //        Dictionary<string, string> dic = new Dictionary<string, string>();
        //        foreach (DataRow row in dt.Rows)
        //        {
        //            if (!dic.ContainsKey(row["ProvinceId"].ToString()))
        //            {
        //                dic.Add(row["ProvinceId"].ToString(), row["ProvinceName"].ToString());
        //            }
        //        }

        //        if (dic.Count > 0)
        //        {
        //            foreach (KeyValuePair<string, string> pair in dic)
        //            {
        //                ListItem item = new ListItem(pair.Value, pair.Key);
        //                this.dropProvince.Items.Add(item);
        //            }
        //        }
        //    }
        //    this.dropProvince.Items.Insert(0, new ListItem("省份", "0"));
        //    this.dropProvince.Attributes.Add("onchange", string.Format("ChangeList('" + this.dropCity.ClientID + "', this.options[this.selectedIndex].value)"));
        //}


        //protected void BindDropCity()
        //{
        //    this.dropCity.Items.Clear();
        //    DataTable dt = ProvinceSiteAllDs.Tables[0];
        //    int index = 0;
        //    StringBuilder strCity = new StringBuilder();
        //    strCity.Append("\nvar CityCount;\n");
        //    strCity.Append("arrCity = new Array();\n");
        //    if (dt != null && dt.Rows.Count > 0)
        //    {
        //        foreach (DataRow row in dt.Rows)
        //        {
        //            ListItem item = new ListItem(row["CityName"].ToString(), row["cityid"].ToString() + "|" + row["siteid"].ToString());
        //            //数组下标，城市名称，城市ID，该城市所属的省份ID
        //            //每行new一个含3列的的数组
        //            string str = string.Format("\narrCity[{0}]=new Array('{1}', '{2}', {3});", index.ToString(), row["CityName"].ToString(), row["cityid"].ToString() + "|" + row["siteid"].ToString(), row["ProvinceId"].ToString());
        //            strCity.Append(str);
        //            index++;
        //        }
        //        strCity.Append("\nCityCount=" + index.ToString() + ";");
        //        strCity.Append("\nfunction ChangeList(CityTextId, ProvinceId)");
        //        strCity.Append("\n{var Obj = document.getElementById(CityTextId);"); //获得要被绑定小类的控件对象
        //        strCity.Append("\nObj.length = 0;");
        //        strCity.Append("\nObj.options[0] = new Option('市', 0);");
        //        strCity.Append("if(ProvinceId==0)");
        //        strCity.Append("{Obj.options[0].selected=true;return;}");
        //        strCity.Append("\nvar index=1;");  //smallListObj之后的options下标从1开始                
        //        strCity.Append("\nfor(var i=0; i<CityCount; i++)");
        //        //Array数组行和列的下标都是从0开始的
        //        //arrCity的第i行第3列(下标为2<从0开始>)，即为大类的ID，若选择的大类ID等于当前循环的，则取出该小类
        //        strCity.Append("\n{if(arrCity[i][2]==ProvinceId)");
        //        //根据该大类ID，得到小类名称和ID，复制给options的Text和Value值
        //        strCity.Append("\n{Obj.options[index]=new Option(arrCity[i][0],arrCity[i][1]);\nindex=index+1;}");
        //        strCity.Append("\n}");
        //        strCity.Append("\n}");
        //        //注册该JS
        //        MessageBox.ResponseScript(strCity.ToString());
        //    }
        //    this.dropCity.Items.Insert(0, new ListItem("市", "0"));
        //}
        //#endregion 
    }
}