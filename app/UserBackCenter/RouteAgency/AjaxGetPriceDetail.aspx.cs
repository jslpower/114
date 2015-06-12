using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace UserBackCenter.RouteAgency
{
    /// <summary>
    /// 报价等级信息
    /// 罗丽娥   2010-06-24
    /// </summary>
    public partial class AjaxGetPriceDetail : System.Web.UI.Page
    {
        private int UnionId = 0;
        protected int CompanyId = 0;
        private string RouteId = "";

        //IList<TourUnion.Model.LocalAgency.CustomerLevelInfo> CustomerLevellist = new List<TourUnion.Model.LocalAgency.CustomerLevelInfo>();

        protected string strTourPriceDetail = "";
        protected bool NoFast = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            //UnionId = Adpost.Common.Function.ValidatorValueManage.GetIntValue(Request.QueryString["UnionId"]);
            //CompanyId = Adpost.Common.Function.ValidatorValueManage.GetIntValue(Request.QueryString["CompanyId"]);
            //RouteId = Request.QueryString["RouteId"];
            //if (!string.IsNullOrEmpty(Request.QueryString["NoFast"]))
            //{
            //    NoFast = true;
            //}
            //if (!Page.IsPostBack)
            //{
            //    TourUnion.BLL.LocalAgency.CustomerLevel cbll = new TourUnion.BLL.LocalAgency.CustomerLevel();
            //    CustomerLevellist = cbll.GetCommonLevels();

            //    cbll = null;

            //    BindrptLevel(CustomerLevellist);
            //    IList<TourUnion.Model.LocalAgency.RoutePriceDetailInfo> PriceDetaillist = new List<TourUnion.Model.LocalAgency.RoutePriceDetailInfo>();
            //    if (!string.IsNullOrEmpty(RouteId))
            //    {
            //        TourUnion.BLL.LocalAgency.Route Tbll = new TourUnion.BLL.LocalAgency.Route();
            //        PriceDetaillist = Tbll.GetRoutePriceDetails(RouteId);
            //        Tbll = null;
            //    }
            //    strTourPriceDetail = InitTourPriceDetail(PriceDetaillist);

            //    PriceDetaillist = null;


            //}
        }


        /// <summary>
        /// 初始价格列表
        /// </summary>
        //private string InitTourPriceDetail(IList<TourUnion.Model.LocalAgency.RoutePriceDetailInfo> RoutePriceDetail)
        //{
        //    string strTemp = "";

        //    TourUnion.BLL.LocalAgency.PriceStand bll = new TourUnion.BLL.LocalAgency.PriceStand();
        //    IList<TourUnion.Model.LocalAgency.PriceStandInfo> list = bll.GetCompanyPriceStands(CompanyId);


        //    string str = "<option value='{0}' {1}>{2}</option>\n";

        //    if (RoutePriceDetail != null && RoutePriceDetail.Count > 0)//表示修改或者复制
        //    {
        //        Dictionary<string, Dictionary<string, string[]>> PriceDetailDic = this.GetDic(RoutePriceDetail);

        //        foreach (KeyValuePair<string, Dictionary<string, string[]>> pair in PriceDetailDic)
        //        {
        //            strTemp += "<tr class='zhonghui'>";
        //            strTemp += "<td align='center' style=\"border:1px solid #93B5D7;\"><select name='drpPriceRank' onchange='AjaxGetPriceDetail.isSamePrice(this)'>";
        //            StringBuilder sb = new StringBuilder();
        //            foreach (TourUnion.Model.LocalAgency.PriceStandInfo price in list)
        //            {
        //                if (price.PriceStandId == pair.Key)
        //                {
        //                    sb.Append(string.Format(str, price.PriceStandId, "selected", price.PriceStandName));

        //                }
        //                else
        //                {
        //                    sb.Append(string.Format(str, price.PriceStandId, "", price.PriceStandName));
        //                }
        //            }
        //            strTemp += sb.ToString();
        //            strTemp += "</select>";

        //            Dictionary<string, string[]> dicTemp = pair.Value;

        //            if (dicTemp.Count != CustomerLevellist.Count)
        //            {
        //                string differentId = CustomerLevellist[CustomerLevellist.Count - 1].CustomerLevelId.ToString();
        //                string[] dufferentArray = null;

        //                if (dicTemp.ContainsKey(differentId))
        //                    dufferentArray = dicTemp[differentId];

        //                dicTemp.Remove(differentId);

        //                for (int i = 0; i < CustomerLevellist.Count - 1; i++)
        //                {
        //                    if (!dicTemp.ContainsKey(CustomerLevellist[i].CustomerLevelId.ToString()))
        //                    {
        //                        dicTemp.Add(CustomerLevellist[i].CustomerLevelId, new string[] { "0", "0", "0" });
        //                    }
        //                }

        //                if (dufferentArray != null)
        //                    dicTemp.Add(differentId, dufferentArray);
        //            }

        //            int index = 0;
        //            string strClassName = "adultorchildren";
        //            foreach (KeyValuePair<string, string[]> var in dicTemp)
        //            {
        //                if (index == 2)
        //                {
        //                    strClassName = "room";

        //                }
        //                if (index == 0 || index == 1)
        //                {
        //                    strTemp += string.Format("<TD align='center' style=\"border:1px solid #93B5D7;\" class='" + strClassName + "'><input name='PeoplePrice{0}' size='7' class='bitiansm'  type='text' id='PeoplePrice{0}' value='{1}'></TD>", var.Key, var.Value[0], var.Value[2]);
        //                }
        //                else
        //                {
        //                    strTemp += string.Format("<TD align='center' style=\"border:1px solid #93B5D7;\" class='" + strClassName + "'><input name='PeoplePrice{0}' size='7' class='Newinput'  type='text' id='PeoplePrice{0}' value='{1}'></TD>", var.Key, var.Value[0], var.Value[2]);
        //                }
        //                strTemp += string.Format("<TD align='center' style=\"border:1px solid #93B5D7;\" class='" + strClassName + "'><input name='ChildPrice{0}' size='7' class='Newinput'   type='text' id='ChildPrice{0}' value='{1}'></TD>", var.Key, var.Value[1]);
        //                index++;
        //            }
        //            strTemp += "<td align='center' style=\"border:1px solid #93B5D7;\"><a onclick='AjaxGetPriceDetail.addthis(this)' href='javascript:void(0)'>增加一行</a>&nbsp;<a onclick='AjaxGetPriceDetail.delthis(this)' href='javascript:void(0)'>删除</a></td>";
        //            strTemp += "</tr>";
        //        }
        //    }
        //    else//表示新增
        //    {
        //        strTemp += "<tr >";
        //        strTemp += "<td align='center' style=\"border:1px solid #93B5D7;\"><select name='drpPriceRank' onchange='AjaxGetPriceDetail.isSamePrice(this)'>";
        //        StringBuilder strdropPriceList = new StringBuilder();
        //        foreach (TourUnion.Model.LocalAgency.PriceStandInfo model in list)
        //        {
        //            strdropPriceList.Append(string.Format(str, model.PriceStandId, "", model.PriceStandName));
        //        }
        //        strTemp += strdropPriceList.ToString();
        //        strTemp += "</select></td>";
        //        int index = 0;
        //        string strName1 = "成人价";
        //        string strName2 = "儿童价";
        //        string strClassName = "adultorchildren";
        //        foreach (TourUnion.Model.LocalAgency.CustomerLevelInfo model in CustomerLevellist)
        //        {
        //            if (Convert.ToInt32(model.CustomerLevelBasicType) == 2)
        //            {
        //                strClassName = "room";
        //                strName1 = "结算价";
        //                strName2 = "门市价";
        //            }
        //            if (index == 0 || index == 1)
        //            {
        //                strTemp += "<TD align='center' style=\"border:1px solid #93B5D7;\" class='" + strClassName + "'><input name='PeoplePrice" + model.CustomerLevelId + "' size='7' value='" + strName1 + "' class='bitiansm'  type='text' id='PeoplePrice" + model.CustomerLevelId + "'></TD>";
        //            }
        //            else
        //            {
        //                strTemp += "<TD align='center' style=\"border:1px solid #93B5D7;\" class='" + strClassName + "'><input name='PeoplePrice" + model.CustomerLevelId + "' size='7'  value='" + strName1 + "' type='text' class='Newinput' id='PeoplePrice" + model.CustomerLevelId + "'></TD>";
        //            }
        //            strTemp += "<TD align='center' style=\"border:1px solid #93B5D7;\" class='" + strClassName + "'><input name='ChildPrice" + model.CustomerLevelId + "' size='7' value='" + strName2 + "'   class='Newinput' type='text' id='ChildPrice" + model.CustomerLevelId + "'></TD>";
        //            index++;
        //        }
        //        strTemp += "<td align='center' style=\"border:1px solid #93B5D7;\"><a onclick='AjaxGetPriceDetail.addthis(this)' href='javascript:void(0)'>增加一行</a>&nbsp;<a onclick='AjaxGetPriceDetail.delthis(this)' href='javascript:void(0)'>删除</a></td>";
        //        strTemp += "</tr>";
        //    }

        //    list = null;
        //    bll = null;

        //    return strTemp;
        //}

        //protected Dictionary<string, Dictionary<string, string[]>> GetDic(IList<TourUnion.Model.LocalAgency.RoutePriceDetailInfo> RoutePriceDetail)
        //{
        //    Dictionary<string, Dictionary<string, string[]>> dic = new Dictionary<string, Dictionary<string, string[]>>();
        //    if (RoutePriceDetail != null && RoutePriceDetail.Count > 0)
        //    {
        //        Dictionary<string, string[]> dicTemp = new Dictionary<string, string[]>();
        //        foreach (TourUnion.Model.LocalAgency.RoutePriceDetailInfo model in RoutePriceDetail)
        //        {
        //            if (!dic.ContainsKey(model.PriceStandId))
        //            {
        //                Dictionary<string, string[]> dic1 = new Dictionary<string, string[]>();
        //                if (!dic1.ContainsKey(model.CustomerLevelId))
        //                {
        //                    string[] str = new string[] { model.AdultPrice.ToString("f0"), model.ChildrenPrice.ToString("f0"), model.PriceDetailId };
        //                    dic1.Add(model.CustomerLevelId, str);
        //                }
        //                dic.Add(model.PriceStandId, dic1);
        //            }
        //            else
        //            {
        //                dicTemp = dic[model.PriceStandId];
        //                if (!dicTemp.ContainsKey(model.CustomerLevelId))
        //                {
        //                    string[] str = new string[] { model.AdultPrice.ToString("f0"), model.ChildrenPrice.ToString("f0"), model.PriceDetailId };
        //                    dicTemp.Add(model.CustomerLevelId, str);
        //                }
        //                dic.Remove(model.PriceStandId);
        //                dic.Add(model.PriceStandId, dicTemp);
        //            }
        //        }
        //    }
        //    return dic;
        //}

        /// <summary>
        /// 绑定客户等级
        /// </summary>
        //private void BindrptLevel(IList<TourUnion.Model.LocalAgency.CustomerLevelInfo> list)
        //{
        //    if (list != null && list.Count > 0)
        //    {
        //        rptLevel.DataSource = list;
        //        rptLevel.DataBind();
        //    }
        //    list = null;
        //}

    }
}
