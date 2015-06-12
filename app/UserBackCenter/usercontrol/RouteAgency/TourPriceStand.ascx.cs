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
using System.Text;

namespace UserBackCenter.usercontrol.RouteAgency
{
    /// <summary>
    /// 价格等级
    /// 罗丽娥   2010-06-28
    /// </summary>
    public partial class TourPriceStand : System.Web.UI.UserControl
    {
        #region 自定义变量
        /// <summary>
        /// 页面最外层的tableid
        /// </summary>
        private string _containerid;
        public string ContainerID
        {
            set { _containerid = value; }
            get { return _containerid; }
        }
        /// <summary>
        /// 判断是团队还是线路的发布类型
        /// </summary>
        private string _releasetype;
        public string ReleaseType
        {
            set { _releasetype = value; }
            get { return _releasetype; }
        }

        private string _moduletype;
        /// <summary>
        /// 判断是线路还是团队 团队:tour， 线路: route
        /// </summary>
        public string ModuleType
        {
            set { _moduletype = value; }
            get { return _moduletype; }
        }
        public IList<EyouSoft.Model.TourStructure.RoutePriceDetail> _routepricedetails = null;
        /// <summary>
        /// 线路报价明细
        /// </summary>
        public IList<EyouSoft.Model.TourStructure.RoutePriceDetail> RoutePriceDetails
        {
            set { _routepricedetails = value; }
            get { return _routepricedetails; }
        }

        public IList<EyouSoft.Model.TourStructure.TourPriceDetail> _tourpricedetails = null;
        /// <summary>
        /// 团队报价明细
        /// </summary>
        public IList<EyouSoft.Model.TourStructure.TourPriceDetail> TourPriceDetails
        {
            set { _tourpricedetails = value; }
            get { return _tourpricedetails; }
        }

        private EyouSoft.SSOComponent.Entity.UserInfo _userinfomodel = null;
        /// <summary>
        /// 用户信息
        /// </summary>
        public EyouSoft.SSOComponent.Entity.UserInfo UserInfoModel
        {
            set { _userinfomodel = value; }
            get { return _userinfomodel; }
        }

        private int SysLevelCount = 0;

        protected string ImageServerPath = EyouSoft.Common.ImageManage.GetImagerServerUrl(1);

        protected string CompanyId = "0";

        protected string strTourPriceDetail = string.Empty;

        private IList<EyouSoft.Model.SystemStructure.SysFieldBase> CustomerLevelList = null;

        ArrayList arrLevelId = new ArrayList();
        #endregion 

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (UserInfoModel != null)
                {
                    CompanyId = UserInfoModel.CompanyID;
                }
                InitPriceStand();

                if (ModuleType == "tour")
                {
                    strTourPriceDetail = InitTourPriceDetail();
                }
                else
                { 
                    strTourPriceDetail = InitRoutePriceDetail();
                }
            }
        }

        #region 初始化客户等级
        /// <summary>
        /// 初始化客户等级
        /// </summary>
        private void InitPriceStand()
        {
            EyouSoft.IBLL.SystemStructure.ISysField bll = EyouSoft.BLL.SystemStructure.SysField.CreateInstance();
            CustomerLevelList = bll.GetSysFieldBaseList(EyouSoft.Model.SystemStructure.SysFieldType.客户等级);

            if (CustomerLevelList != null && CustomerLevelList.Count > 0)
            {
                SysLevelCount = CustomerLevelList.Count;
                this.rptPriceStand.DataSource = CustomerLevelList;
                this.rptPriceStand.DataBind();

                foreach (EyouSoft.Model.SystemStructure.SysFieldBase model in CustomerLevelList)
                {
                    if (!arrLevelId.Contains(model.FieldId) && model.FieldId != 0 && model.FieldId != 1 && model.FieldId != 2)
                    {
                        arrLevelId.Add(model.FieldId);
                    }
                }
            }
            bll = null;
        }
        #endregion

        #region 初始线路价格列表
        /// <summary>
        /// 初始线路价格列表
        /// </summary>
        private string InitRoutePriceDetail()
        {
            StringBuilder str = new StringBuilder();
            if (RoutePriceDetails != null && RoutePriceDetails.Count > 0)
            {
                foreach (EyouSoft.Model.TourStructure.RoutePriceDetail priceModel in RoutePriceDetails)
                {
                    str.Append("<tr>");
                    str.Append("<td align=\"right\" valign=\"bottom\" style=\"border: 1px solid #93B5D7;\">");
                    EyouSoft.IBLL.CompanyStructure.ICompanyPriceStand bll = EyouSoft.BLL.CompanyStructure.CompanyPriceStand.CreateInstance();
                    IList<EyouSoft.Model.CompanyStructure.CompanyPriceStand> priceStandList = bll.GetList(CompanyId);
                    str.Append("<select name=\"drpPriceRank\" onchange=\"TourPriceStand.isSamePrice(this,'"+ ContainerID +"');\" valid=\"required\" errmsg=\"请选择报价等级\">");
                    if (priceStandList != null && priceStandList.Count > 0)
                    {
                        foreach (EyouSoft.Model.CompanyStructure.CompanyPriceStand priceStandModel in priceStandList)
                        {
                            if (priceStandModel.ID == priceModel.PriceStandId)
                            {
                                str.AppendFormat("<option value=\"{0}\" selected>{1}</option>", priceStandModel.ID, priceStandModel.PriceStandName);
                            }
                            else
                            {
                                str.AppendFormat("<option value=\"{0}\">{1}</option>", priceStandModel.ID, priceStandModel.PriceStandName);
                            }
                        }
                    }
                    else
                    {
                        str.Append("<option value=\"\">请选择</option>");
                    }
                    str.Append("</select></td>");
                    priceStandList = null;
                    bll = null;

                    IList<EyouSoft.Model.TourStructure.RoutePriceCustomerLeaveDetail> detailList = priceModel.PriceDetail;
                    if (detailList != null && detailList.Count > 0)
                    {
                        ArrayList arrCustomerLevelID = new ArrayList();
                        StringBuilder strDF = new StringBuilder();
                        foreach (EyouSoft.Model.TourStructure.RoutePriceCustomerLeaveDetail detailModel in detailList)
                        {
                            arrCustomerLevelID.Add(detailModel.CustomerLevelId);
                            if (detailModel.CustomerLevelType == EyouSoft.Model.CompanyStructure.CustomerLevelType.门市 || detailModel.CustomerLevelType == EyouSoft.Model.CompanyStructure.CustomerLevelType.同行)
                            {
                                str.AppendFormat("<TD align=\"center\" style=\"border:1px solid #93B5D7;\" class=\"adultorchildren\"><nobr><input name=\"PeoplePrice{0}\" size=\"7\" class=\"bitiansm\"  type=\"text\" id=\"PeoplePrice{0}\" value=\"{1}\">", detailModel.CustomerLevelId, detailModel.AdultPrice > 0 ? detailModel.AdultPrice.ToString("F0") : "0");

                                str.AppendFormat("&nbsp;<input name=\"ChildPrice{0}\" size=\"7\" class=\"shurukuang\"  type=\"text\" id=\"ChildPrice{0}\" value=\"{1}\"></nobr></TD>", detailModel.CustomerLevelId, detailModel.ChildrenPrice > 0 ? detailModel.ChildrenPrice.ToString("F0") : "0");
                            }
                            else if (detailModel.CustomerLevelType == EyouSoft.Model.CompanyStructure.CustomerLevelType.单房差)
                            {
                                strDF.AppendFormat("<TD align=\"center\" style=\"border:1px solid #93B5D7;\" class=\"room\"><nobr><input name=\"PeoplePrice{0}\" size=\"7\" class=\"shurukuang\"  type=\"text\" id=\"PeoplePrice{0}\" value=\"{1}\">", detailModel.CustomerLevelId, detailModel.AdultPrice > 0 ? detailModel.AdultPrice.ToString("F0") : "0");

                                strDF.AppendFormat("&nbsp;<input name=\"ChildPrice{0}\" size=\"7\" class=\"shurukuang\"  type=\"text\" id=\"ChildPrice{0}\" value=\"{1}\"></nobr></TD>", detailModel.CustomerLevelId, detailModel.ChildrenPrice > 0 ? detailModel.ChildrenPrice.ToString("F0") : "0");
                            }
                            else
                            {
                                if (arrLevelId.Contains(detailModel.CustomerLevelId))
                                {
                                    str.AppendFormat("<TD align=\"center\" style=\"border:1px solid #93B5D7;\" class=\"adultorchildren\"><nobr><input name=\"PeoplePrice{0}\" size=\"7\" class=\"shurukuang\"  type=\"text\" id=\"PeoplePrice{0}\" value=\"{1}\">", detailModel.CustomerLevelId, detailModel.AdultPrice > 0 ? detailModel.AdultPrice.ToString("F0") : "0");

                                    str.AppendFormat("&nbsp;<input name=\"ChildPrice{0}\" size=\"7\" class=\"shurukuang\"  type=\"text\" id=\"ChildPrice{0}\" value=\"{1}\"></nobr></TD>", detailModel.CustomerLevelId, detailModel.ChildrenPrice > 0 ? detailModel.ChildrenPrice.ToString("F0") : "0");
                                }
                            }
                        }

                        StringBuilder strTMP = new StringBuilder();
                        if (arrLevelId.Count > 0)
                        {
                            for(int a = 0; a < arrLevelId.Count; a ++)
                            {
                                if (!arrCustomerLevelID.Contains(arrLevelId[a]))
                                {
                                    strTMP.AppendFormat("<TD align=\"center\" style=\"border:1px solid #93B5D7;\" class=\"adultorchildren\"><nobr><input name=\"PeoplePrice{0}\" size=\"7\" class=\"shurukuang\"  type=\"text\" id=\"PeoplePrice{0}\" value=\"成人价\">", arrLevelId[a]);
                                    strTMP.AppendFormat("&nbsp;<input name=\"ChildPrice{0}\" size=\"7\" class=\"shurukuang\"  type=\"text\" id=\"ChildPrice{0}\" value=\"儿童价\"></nobr></TD>", arrLevelId[a]);
                                }
                            }
                            str.Append(strTMP);
                        }

                        str.Append(strDF);
                    }
                    detailList = null;
                    str.Append("<td style=\"border: 1px solid #93B5D7;\"><a onclick=\"TourPriceStand.addthis(this,null,'" + ContainerID + "');return false;\" href=\"javascript:void(0);\">增加一行</a> <a onclick=\"TourPriceStand.delthis(this);return false;\" href=\"javascript:void(0);\">删除</a></td>");
                }
            }
            else
            {
                str.Append("<tr>");
                str.Append("<td align=\"right\" valign=\"bottom\" style=\"border: 1px solid #93B5D7;\">");
                EyouSoft.IBLL.CompanyStructure.ICompanyPriceStand bll = EyouSoft.BLL.CompanyStructure.CompanyPriceStand.CreateInstance();
                IList<EyouSoft.Model.CompanyStructure.CompanyPriceStand> priceStandList = bll.GetList(CompanyId);
                str.Append("<select name=\"drpPriceRank\" onchange=\"TourPriceStand.isSamePrice(this,'" + ContainerID + "');\" valid=\"required\" errmsg=\"请选择报价等级\">");
                if (priceStandList != null && priceStandList.Count > 0)
                {
                    foreach (EyouSoft.Model.CompanyStructure.CompanyPriceStand priceStandModel in priceStandList)
                    {
                        str.AppendFormat("<option value=\"{0}\">{1}</option>", priceStandModel.ID, priceStandModel.PriceStandName);
                    }
                }
                else
                {
                    str.Append("<option value=\"\">请选择</option>");
                }
                str.Append("</select></td>");
                priceStandList = null;
                bll = null;
                foreach (EyouSoft.Model.SystemStructure.SysFieldBase customerLevelModel in CustomerLevelList)
                {
                    if (customerLevelModel.FieldId == 0 || customerLevelModel.FieldId == 1)
                    {
                        str.AppendFormat("<TD align=\"center\" style=\"border:1px solid #93B5D7;\" class=\"adultorchildren\"><nobr><input name=\"PeoplePrice{0}\" size=\"7\" class=\"bitiansm\"  type=\"text\" id=\"PeoplePrice{0}\" value=\"成人价\">", customerLevelModel.FieldId);
                        str.AppendFormat("&nbsp;<input name=\"ChildPrice{0}\" size=\"7\" class=\"shurukuang\"  type=\"text\" id=\"ChildPrice{0}\" value=\"儿童价\"></nobr></TD>", customerLevelModel.FieldId);
                    }
                    else if (customerLevelModel.FieldId == 2)
                    {
                        str.AppendFormat("<TD align=\"center\" style=\"border:1px solid #93B5D7;\" class=\"room\"><nobr><input name=\"PeoplePrice{0}\" size=\"7\" class=\"shurukuang\"  type=\"text\" id=\"PeoplePrice{0}\" value=\"结算价\">", customerLevelModel.FieldId);
                        str.AppendFormat("&nbsp;<input name=\"ChildPrice{0}\" size=\"7\" class=\"shurukuang\"  type=\"text\" id=\"ChildPrice{0}\" value=\"门市价\"></nobr></TD>", customerLevelModel.FieldId);
                    }
                    else {
                        str.AppendFormat("<TD align=\"center\" style=\"border:1px solid #93B5D7;\" class=\"adultorchildren\"><nobr><input name=\"PeoplePrice{0}\" size=\"7\" class=\"shurukuang\"  type=\"text\" id=\"PeoplePrice{0}\" value=\"成人价\">", customerLevelModel.FieldId);
                        str.AppendFormat("&nbsp;<input name=\"ChildPrice{0}\" size=\"7\" class=\"shurukuang\"  type=\"text\" id=\"ChildPrice{0}\" value=\"儿童价\"></nobr></TD>", customerLevelModel.FieldId);
                    }
                }
                str.Append("<td style=\"border: 1px solid #93B5D7;\"><a onclick=\"TourPriceStand.addthis(this,null,'" + ContainerID + "');return false;\" href=\"javascript:void(0);\">增加一行</a> <a onclick=\"TourPriceStand.delthis(this);return false;\" href=\"javascript:void(0);\">删除</a></td>");
            }
            RoutePriceDetails = null;
            return str.ToString();
        }
        #endregion

        #region 初始化团队价格列表
        /// <summary>
        /// 初始化团队价格列表
        /// </summary>
        private string InitTourPriceDetail()
        {
            StringBuilder str = new StringBuilder();
            if (TourPriceDetails != null && TourPriceDetails.Count > 0)
            {
                foreach (EyouSoft.Model.TourStructure.TourPriceDetail priceModel in TourPriceDetails)
                {
                    str.Append("<tr>");
                    str.Append("<td align=\"right\" valign=\"bottom\" style=\"border: 1px solid #93B5D7;\">");
                    EyouSoft.IBLL.CompanyStructure.ICompanyPriceStand bll = EyouSoft.BLL.CompanyStructure.CompanyPriceStand.CreateInstance();
                    IList<EyouSoft.Model.CompanyStructure.CompanyPriceStand> priceStandList = bll.GetList(CompanyId);
                    str.Append("<select name=\"drpPriceRank\" onchange=\"TourPriceStand.isSamePrice(this,'" + ContainerID + "');\">");
                    if (priceStandList != null && priceStandList.Count > 0)
                    {
                        foreach (EyouSoft.Model.CompanyStructure.CompanyPriceStand priceStandModel in priceStandList)
                        {
                            if (priceStandModel.ID == priceModel.PriceStandId)
                            {
                                str.AppendFormat("<option value=\"{0}\" selected>{1}</option>", priceStandModel.ID, priceStandModel.PriceStandName);
                            }
                            else
                            {
                                str.AppendFormat("<option value=\"{0}\">{1}</option>", priceStandModel.ID, priceStandModel.PriceStandName);
                            }
                        }
                    }
                    else {
                        str.Append("<option value=\"\">请选择</option>");
                    }
                    str.Append("</select></td>");
                    priceStandList = null;

                    IList<EyouSoft.Model.TourStructure.TourPriceCustomerLeaveDetail> detailList = priceModel.PriceDetail;
                    if (detailList != null && detailList.Count > 0)
                    {
                        ArrayList arrCustomerLevelID = new ArrayList();
                        StringBuilder strDF = new StringBuilder();
                        foreach (EyouSoft.Model.TourStructure.TourPriceCustomerLeaveDetail detailModel in detailList)
                        {
                            arrCustomerLevelID.Add(detailModel.CustomerLevelId);
                            if (detailModel.CustomerLevelType == EyouSoft.Model.CompanyStructure.CustomerLevelType.门市 || detailModel.CustomerLevelType == EyouSoft.Model.CompanyStructure.CustomerLevelType.同行)
                            {
                                str.AppendFormat("<TD align=\"center\" style=\"border:1px solid #93B5D7;\" class=\"adultorchildren\"><nobr><input name=\"PeoplePrice{0}\" size=\"7\" class=\"bitiansm\"  type=\"text\" id=\"PeoplePrice{0}\" value=\"{1}\">", detailModel.CustomerLevelId, detailModel.AdultPrice > 0 ? detailModel.AdultPrice.ToString("F0") : "0");

                                str.AppendFormat("&nbsp;<input name=\"ChildPrice{0}\" size=\"7\" class=\"shurukuang\"  type=\"text\" id=\"ChildPrice{0}\" value=\"{1}\"></nobr></TD>", detailModel.CustomerLevelId, detailModel.ChildrenPrice > 0 ? detailModel.ChildrenPrice.ToString("F0") : "0");
                            }
                            else if (detailModel.CustomerLevelType == EyouSoft.Model.CompanyStructure.CustomerLevelType.单房差)
                            {
                                strDF.AppendFormat("<TD align=\"center\" style=\"border:1px solid #93B5D7;\" class=\"room\"><nobr><input name=\"PeoplePrice{0}\" size=\"7\" class=\"shurukuang\"  type=\"text\" id=\"PeoplePrice{0}\" value=\"{1}\">", detailModel.CustomerLevelId, detailModel.AdultPrice > 0 ? detailModel.AdultPrice.ToString("F0") : "0");

                                strDF.AppendFormat("&nbsp;<input name=\"ChildPrice{0}\" size=\"7\" class=\"shurukuang\"  type=\"text\" id=\"ChildPrice{0}\" value=\"{1}\"></nobr></TD>", detailModel.CustomerLevelId, detailModel.ChildrenPrice > 0 ? detailModel.ChildrenPrice.ToString("F0") : "0");
                            }
                            else
                            {
                                if (arrLevelId.Contains(detailModel.CustomerLevelId))
                                {
                                    str.AppendFormat("<TD align=\"center\" style=\"border:1px solid #93B5D7;\" class=\"adultorchildren\"><nobr><input name=\"PeoplePrice{0}\" size=\"7\" class=\"shurukuang\"  type=\"text\" id=\"PeoplePrice{0}\" value=\"{1}\">", detailModel.CustomerLevelId, detailModel.AdultPrice > 0 ? detailModel.AdultPrice.ToString("F0") : "0");

                                    str.AppendFormat("&nbsp;<input name=\"ChildPrice{0}\" size=\"7\" class=\"shurukuang\"  type=\"text\" id=\"ChildPrice{0}\" value=\"{1}\"></nobr></TD>", detailModel.CustomerLevelId, detailModel.ChildrenPrice > 0 ? detailModel.ChildrenPrice.ToString("F0") : "0");
                                }
                            }
                        }

                        StringBuilder strTMP = new StringBuilder();
                        if (arrLevelId.Count > 0)
                        {
                            for(int a = 0; a < arrLevelId.Count; a ++)
                            {
                                if (!arrCustomerLevelID.Contains(arrLevelId[a]))
                                {
                                    strTMP.AppendFormat("<TD align=\"center\" style=\"border:1px solid #93B5D7;\" class=\"adultorchildren\"><nobr><input name=\"PeoplePrice{0}\" size=\"7\" class=\"shurukuang\"  type=\"text\" id=\"PeoplePrice{0}\" value=\"成人价\">", arrLevelId[a]);
                                    strTMP.AppendFormat("&nbsp;<input name=\"ChildPrice{0}\" size=\"7\" class=\"shurukuang\"  type=\"text\" id=\"ChildPrice{0}\" value=\"儿童价\"></nobr></TD>", arrLevelId[a]);
                                }
                            }
                            str.Append(strTMP);
                        }

                        str.Append(strDF);
                    }

                    detailList = null;
                    str.Append("<td style=\"border: 1px solid #93B5D7;\"><a onclick=\"TourPriceStand.addthis(this,null,'" + ContainerID + "');return false;\" href=\"javascript:void(0);\">增加一行</a> <a onclick=\"TourPriceStand.delthis(this);return false;\" href=\"javascript:void(0);\">删除</a></td>");
                }
            }
            else
            {
                str.Append("<tr>");
                str.Append("<td align=\"right\" valign=\"bottom\" style=\"border: 1px solid #93B5D7;\">");
                EyouSoft.IBLL.CompanyStructure.ICompanyPriceStand bll = EyouSoft.BLL.CompanyStructure.CompanyPriceStand.CreateInstance();
                IList<EyouSoft.Model.CompanyStructure.CompanyPriceStand> priceStandList = bll.GetList(CompanyId);
                str.Append("<select name=\"drpPriceRank\" onchange=\"TourPriceStand.isSamePrice(this,'" + ContainerID + "');\" valid=\"required\" errmsg=\"请选择报价等级\">");
                if (priceStandList != null && priceStandList.Count > 0)
                {
                    foreach (EyouSoft.Model.CompanyStructure.CompanyPriceStand priceStandModel in priceStandList)
                    {
                        str.AppendFormat("<option value=\"{0}\">{1}</option>", priceStandModel.ID, priceStandModel.PriceStandName);
                    }
                }
                else
                {
                    str.Append("<option value=\"\">请选择</option>");
                }
                str.Append("</select></td>");
                priceStandList = null;
                bll = null;
                foreach (EyouSoft.Model.SystemStructure.SysFieldBase customerLevelModel in CustomerLevelList)
                {
                    if (customerLevelModel.FieldId == 0 || customerLevelModel.FieldId == 1)
                    {
                        str.AppendFormat("<TD align=\"center\" style=\"border:1px solid #93B5D7;\" class=\"adultorchildren\"><nobr><input name=\"PeoplePrice{0}\" size=\"7\" class=\"bitiansm\"  type=\"text\" id=\"PeoplePrice{0}\" value=\"成人价\">", customerLevelModel.FieldId);
                        str.AppendFormat("&nbsp;<input name=\"ChildPrice{0}\" size=\"7\" class=\"shurukuang\"  type=\"text\" id=\"ChildPrice{0}\" value=\"儿童价\"></nobr></TD>", customerLevelModel.FieldId);
                    }
                    else if (customerLevelModel.FieldId == 2)
                    {
                        str.AppendFormat("<TD align=\"center\" style=\"border:1px solid #93B5D7;\" class=\"room\"><nobr><input name=\"PeoplePrice{0}\" size=\"7\" class=\"shurukuang\"  type=\"text\" id=\"PeoplePrice{0}\" value=\"结算价\">", customerLevelModel.FieldId);
                        str.AppendFormat("&nbsp;<input name=\"ChildPrice{0}\" size=\"7\" class=\"shurukuang\"  type=\"text\" id=\"ChildPrice{0}\" value=\"门市价\"></nobr></TD>", customerLevelModel.FieldId);
                    }
                    else {
                        str.AppendFormat("<TD align=\"center\" style=\"border:1px solid #93B5D7;\" class=\"adultorchildren\"><nobr><input name=\"PeoplePrice{0}\" size=\"7\" class=\"shurukuang\"  type=\"text\" id=\"PeoplePrice{0}\" value=\"成人价\">", customerLevelModel.FieldId);
                        str.AppendFormat("&nbsp;<input name=\"ChildPrice{0}\" size=\"7\" class=\"shurukuang\"  type=\"text\" id=\"ChildPrice{0}\" value=\"儿童价\"></nobr></TD>", customerLevelModel.FieldId);
                    }
                }
                str.Append("<td style=\"border: 1px solid #93B5D7;\"><a onclick=\"TourPriceStand.addthis(this,null,'" + ContainerID + "');return false;\" href=\"javascript:void(0);\">增加一行</a> <a onclick=\"TourPriceStand.delthis(this);return false;\" href=\"javascript:void(0);\">删除</a></td>");
            }
            TourPriceDetails = null;
            return str.ToString();
        }
        #endregion
    }
}
