using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.BLL;
using AlipayClass;
using System.Linq;
using tenpay;


namespace UserBackCenter.TicketsCenter.FreightManage
{
    /// <summary>
    /// 运价添加、修改 页面
    /// 创建人：戴银柱
    /// 创建时间： 2010-10-19  
    public partial class FreightAdd : EyouSoft.Common.Control.BackPage
    {
        //表格Id
        protected string tblID = "";
        //操作类型
        protected string type = "";
        //是否打开回程  0 = 不显示 1 = 显示
        protected string isOpenBack = "0";

        protected void Page_Load(object sender, EventArgs e)
        {
            type = Utils.GetQueryStringValue("type");
            if (!IsPostBack)
            {
                //判断是否签约
                if (!IsJoin())
                {
                    Response.ClearContent();
                    Response.Write("您尚未加入支付圈! <a href='javascript:void(0);' onclick='topTab.open(\"/ticketscenter/joinpartner/default.aspx\", \"加入支付圈\");return false;'>点此加入</a>");
                    Response.End();
                    return;
                }

                //使用GUID作为页面Table容器的id.
                tblID = Guid.NewGuid().ToString();

                string id = Utils.GetQueryStringValue("id");//运价ID

                //根据当前用户的公司ID初始化 公司的 当前购买运价数 列表
                this.FreightTop1.CompanyId = SiteUserInfo.CompanyID;

                //根据 type操作类型 ，id运价ID,判断当前页面是运价编辑还是运价新增
                if (id != "" && type == "")//运价编辑
                {
                    this.fre_hideId.Value = id;
                    this.fre_hideType.Value = "Update";
                    DataInit(id);
                    //修改时显示
                    this.fre_divToLb.Visible = false;
                    this.fre_ToLbByUpdate.Visible = true;
                }
                else//运价新增
                {
                    this.fre_hideType.Value = "Add";
                    //默认单程选中
                    this.fre_rdo_Single.Checked = true;
                    //默认产品类型为整团 
                    this.fre_rdo_Integer.Checked = true;
                    //默认启用运价
                    this.fre_rdoPriceEnable.Checked = true;
                    //默认不修改PNR
                    this.fre_rdoPnrnNo.Checked = true;
                    //不显示回程信息
                    isOpenBack = "0";
                    //航空公司初始化
                    AirCompanyInit("");
                    //起始地初始化
                    StartAddressInit("");
                    //目的地初始化
                    FromAddressInit("");

                    //隐藏【过期关闭】选项
                    this.fre_rdoPriceExpired.Visible = false;
                    //添加是显示
                    this.fre_divToLb.Visible = true;//目的地选择区域
                    this.fre_ToLbByUpdate.Visible = false;
                    this.fre_rdoPnrnNo.Checked = true;
                }
            }

            if (!String.IsNullOrEmpty(type))
            {
                Response.Clear();
                Response.Write(AddOrUpdateFreight(type, SiteUserInfo.CompanyID));
                Response.End();
            }
        }


        #region 修改操作时页面数据初始化
        /// <summary>
        /// 根据ID初始化数据
        /// </summary>
        /// <param name="id"></param>
        protected void DataInit(string id)
        {
            EyouSoft.IBLL.TicketStructure.ITicketFreightInfo IBll = EyouSoft.BLL.TicketStructure.TicketFreightInfo.CreateInstance();
            EyouSoft.Model.TicketStructure.TicketFreightInfo model = IBll.GetModel(id);
            if (model != null)
            {
                if (Convert.ToInt32(model.BuyType) == 0 || Convert.ToInt32(model.BuyType) == 2 || Convert.ToInt32(model.BuyType) == 3)
                {
                    //禁止修改航空公司
                    this.fre_AirCompanyList.Enabled = false;
                    //禁止修改始发地
                    this.fre_StartDdl.Enabled = false;
                    //禁止修改目的地
                    this.fre_ToLbByUpdate.Enabled = false;
                }

                //运价类型
                if (model.FreightType == EyouSoft.Model.TicketStructure.FreightType.单程 || model.FreightType.ToString() == "0")
                {
                    //不显示回程信息
                    isOpenBack = "0";
                    this.fre_rdo_Single.Checked = true;
                }
                else
                {
                    //显示回程信息
                    isOpenBack = "1";
                    this.fre_rdo_Back.Checked = true;
                }
                //产品类型
                if (model.ProductType == EyouSoft.Model.TicketStructure.ProductType.整团)
                {
                    this.fre_rdo_Integer.Checked = true;
                }
                else
                {
                    this.fre_rdo_San.Checked = true;
                }
                //航空公司初始化
                AirCompanyInit(model.FlightId.ToString());


                //起始地
                StartAddressInit(model.NoGadHomeCityId.ToString());
                //目的地
                FromAddressInit(model.NoGadDestCityId.ToString());

                //是否启用运价
                if (model.IsExpired)
                {
                    this.fre_rdoPriceExpired.Checked = true;
                    this.fre_rdoPriceEnable.Visible = false;
                    this.fre_rdoPriceClose.Visible = false;
                }
                else
                {
                    if (model.IsEnabled)
                    {
                        this.fre_rdoPriceEnable.Checked = true;
                    }
                    else
                    {
                        this.fre_rdoPriceClose.Checked = true;
                    }
                    this.fre_rdoPriceExpired.Visible = false;
                }

                //是否需要更换PNR
                if (model.IsCheckPNR)
                {
                    this.fre_rdoPnrYes.Checked = true;
                }
                else
                {
                    this.fre_rdoPnrnNo.Checked = true;
                }

                //运价开始日期
                if (model.FreightStartDate != null)
                {
                    this.fre_txtPriceBegin.Text = Convert.ToDateTime(model.FreightStartDate).ToString("yyyy-MM-dd");
                }
                //运价结束日期
                if (model.FreightEndDate != null)
                {
                    this.fre_OfferEnd.Text = Convert.ToDateTime(model.FreightEndDate).ToString("yyyy-MM-dd");
                }
                //去程

                //去程适用
                if (model.FromForDay != null)
                {
                    string[] fromForDay = model.FromForDay.Split(',');
                    if (fromForDay.Length > 0)
                    {
                        for (int i = 0; i < fromForDay.Length; i++)
                        {
                            switch (fromForDay[i])
                            {
                                case "周一": this.fre_FromMonday.Checked = true; break;
                                case "周二": this.fre_FromTuesday.Checked = true; break;
                                case "周三": this.fre_FromWednesday.Checked = true; break;
                                case "周四": this.fre_FromThursday.Checked = true; break;
                                case "周五": this.fre_FromFriday.Checked = true; break;
                                case "周六": this.fre_FromSaturday.Checked = true; break;
                                case "周日": this.fre_FromSunday.Checked = true; break;
                            }
                        }
                    }
                }
                this.fre_FromForDay.Value = model.FromForDay;


                if (model.FromSelfDate != null && Convert.ToDateTime(model.FromSelfDate).ToString("yyyy-MM-dd") != "1900-01-01")
                {
                    this.fre_txtIndeDate.Text = Convert.ToDateTime(model.FromSelfDate).ToString("yyyy-MM-dd");
                }

                this.fre_txtRefePrice.Text = model.FromReferPrice.ToString("0.00");
                this.fre_txtDeduction.Text = model.FromReferRate.ToString("0.0");
                this.fre_txtSettPrice.Text = model.FromSetPrice.ToString("0.00");
                this.fre_txtFuelPrice.Text = model.FromFuelPrice.ToString("0.00");
                this.fre_txtMachPrice.Text = model.FromBuildPrice.ToString("0.00");

                #region 回程信息
                if (model.FreightType == EyouSoft.Model.TicketStructure.FreightType.来回程)
                {

                    //回程适用
                    if (model.ToForDay != null)
                    {
                        string[] backForDay = model.ToForDay.Split(',');
                        if (backForDay.Length > 0)
                        {
                            for (int i = 0; i < backForDay.Length; i++)
                            {
                                switch (backForDay[i])
                                {
                                    case "周一": this.fre_BackMonday.Checked = true; break;
                                    case "周二": this.fre_BackTuesday.Checked = true; break;
                                    case "周三": this.fre_BackWednesday.Checked = true; break;
                                    case "周四": this.fre_BackThursday.Checked = true; break;
                                    case "周五": this.fre_BackFriday.Checked = true; break;
                                    case "周六": this.fre_BackSaturday.Checked = true; break;
                                    case "周日": this.fre_BackSunday.Checked = true; break;
                                }
                            }
                        }
                    }


                    this.fre_hideToForDay.Value = model.ToForDay;

                    if (model.ToSelfDate != null && Convert.ToDateTime(model.ToSelfDate).ToString("yyyy-MM-dd") != "1900-01-01")
                    {
                        this.fre_txtIndeDateBack.Text = Convert.ToDateTime(model.ToSelfDate).ToString("yyyy-MM-dd");
                    }
                    this.fre_txtRefePriceBack.Text = model.ToReferPrice.ToString("0.00");
                    this.fre_txtDeductionBack.Text = model.ToReferRate.ToString("0");
                    this.fre_txtSettPriceBack.Text = model.ToSetPrice.ToString("0.00");
                    this.fre_txtFuelPriceBack.Text = model.ToFuelPrice.ToString("0.00");
                    this.fre_txtMachPriceBack.Text = model.ToBuildPrice.ToString("0.00");

                }
                #endregion

                this.fre_txtPeopleCount.Text = model.MaxPCount.ToString();
                this.fre_txtToWork.Text = model.WorkStartTime;
                this.fre_txtBackHome.Text = model.WorkEndTime;
                this.fre_txtRemark.Text = model.SupplierRemark;
            }

        }

        #endregion

        #region 获得航空公司列表
        /// <summary>
        /// 初始化所有航空公司
        /// </summary>
        /// <param name="selectVal">设置选中项，为空则不设置</param>
        protected void AirCompanyInit(string selectVal)
        {
            IList<EyouSoft.Model.TicketStructure.TicketFlightCompany> list = EyouSoft.BLL.TicketStructure.TicketFlightCompany.CreateInstance().GetTicketFlightCompanyList(null);
            this.fre_AirCompanyList.Items.Clear();
            if (list != null && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    ListItem item = new ListItem();
                    item.Value = list[i].Id.ToString();
                    item.Text = list[i].AirportCode + "-" + list[i].AirportName;
                    this.fre_AirCompanyList.Items.Add(item);
                }
                if (selectVal != "")
                {
                    this.fre_AirCompanyList.SelectedValue = selectVal;
                    //修改初始化
                }
            }
            list = null;
        }
        #endregion

        #region 起始地初始化
        /// <summary>
        /// 初始化起始地
        /// </summary>
        /// <param name="selectVal">设置选中项，为空则不设置</param>
        protected void StartAddressInit(string selectVal)
        {
            IList<EyouSoft.Model.TicketStructure.TicketSeattle> list = EyouSoft.BLL.TicketStructure.TicketSeattle.CreateInstance().GetTicketSeattle(null);
            list.OrderBy(p => p.ShortName);
            this.fre_StartDdl.Items.Clear();
            if (list != null && list.Count > 0)
            {
                list = list.OrderBy(p => p.ShortName).ToList();
                for (int i = 0; i < list.Count; i++)
                {
                    ListItem item = new ListItem();
                    item.Value = list[i].SeattleId.ToString();
                    item.Text = list[i].ShortName.Substring(0, 1).ToUpper() + "-" + list[i].Seattle;
                    this.fre_StartDdl.Items.Add(item);
                }
                if (selectVal != "")
                {
                    this.fre_StartDdl.SelectedValue = selectVal;
                }
            }
            list = null;

        }
        #endregion

        #region 目的地初始化
        /// <summary>
        /// 初始化目的地
        /// </summary>
        protected void FromAddressInit(string selectVal)
        {
            IList<EyouSoft.Model.TicketStructure.TicketSeattle> list = EyouSoft.BLL.TicketStructure.TicketSeattle.CreateInstance().GetTicketSeattle(null);
            this.fre_FromLb.Items.Clear();
            this.fre_ToLbByUpdate.Items.Clear();
            if (list != null && list.Count > 0)
            {
                list = list.OrderBy(p => p.ShortName).ToList();
                for (int i = 0; i < list.Count; i++)
                {
                    ListItem item = new ListItem();
                    item.Value = list[i].SeattleId.ToString();
                    item.Text = list[i].ShortName.Substring(0, 1).ToUpper() + "-" + list[i].Seattle;
                    this.fre_FromLb.Items.Add(item);
                    this.fre_ToLbByUpdate.Items.Add(item);
                }
                if (selectVal != "")
                {
                    this.fre_FromLb.SelectedValue = selectVal;
                    this.fre_ToLbByUpdate.SelectedValue = selectVal;
                }
            }
            list = null;

        }
        #endregion

        #region 添加或修改运价信息
        /// <summary>
        /// 添加或修改运价信息
        /// </summary>
        /// <param name="type">操作类型</param>
        /// <param name="InfoId">修改的运价ID</param>
        /// <returns></returns>
        protected string AddOrUpdateFreight(string type, string InfoId)
        {
            EyouSoft.IBLL.TicketStructure.ITicketFreightInfo IBll = EyouSoft.BLL.TicketStructure.TicketFreightInfo.CreateInstance();

            #region 获得表单数据
            //运价类型
            string rdoSingle = Utils.GetFormValue("ctl00$ContentPlaceHolder1$freightType");
            //产品类型
            string rdoInteger = Utils.GetFormValue("ctl00$ContentPlaceHolder1$proType");
            //航空公司ID
            string companyId = Utils.GetFormValue(this.fre_AirCompanyList.UniqueID);
            //航空公司名称
            string companyName = "";
            if (!string.IsNullOrEmpty(companyId.Trim()))
            {
                if (EyouSoft.BLL.TicketStructure.TicketFlightCompany.CreateInstance().GetTicketFlightCompany(Convert.ToInt32(companyId)) != null)
                {
                    companyName = EyouSoft.BLL.TicketStructure.TicketFlightCompany.CreateInstance().GetTicketFlightCompany(Convert.ToInt32(companyId)).AirportName;
                }
            }
            //起始地Id
            int StartId = Utils.GetInt(Utils.GetFormValue(this.fre_StartDdl.UniqueID), 0);
            //起始地名称
            string startCityName = "";

            EyouSoft.Model.TicketStructure.TicketSeattle StartCityModel = EyouSoft.BLL.TicketStructure.TicketSeattle.CreateInstance().GetTicketSeattleById(StartId);
            if (StartCityModel != null)
                startCityName = StartCityModel.Seattle;
            //是否启用运价
            string riceEnable = Utils.GetFormValue("ctl00$ContentPlaceHolder1$fre_Price");

            //是否需要更换PNR
            string PnrYes = Utils.GetFormValue("ctl00$ContentPlaceHolder1$fre_pnr");
            //运价开始日
            DateTime? PriceBeginTime = Utils.GetDateTimeNullable(Utils.GetFormValue(this.fre_txtPriceBegin.UniqueID));
            //运价结束日
            DateTime? PriceEndTime = Utils.GetDateTimeNullable(Utils.GetFormValue(this.fre_OfferEnd.UniqueID));

            //去程适用
            string fromForDay = Utils.GetFormValue(this.fre_FromForDay.UniqueID).TrimEnd(',');
            //去程独立日期
            DateTime? indeDateTo = Utils.GetDateTimeNullable(Utils.GetFormValue(this.fre_txtIndeDate.UniqueID));
            //去程参考面价
            decimal refePriceTo = Utils.GetDecimal(Utils.GetFormValue(this.fre_txtRefePrice.UniqueID));
            //去程参考扣率
            decimal deductionTo = Utils.GetDecimal(Utils.GetFormValue(this.fre_txtDeduction.UniqueID));
            //去程燃油费
            decimal fuelPriceTo = Utils.GetDecimal(Utils.GetFormValue(this.fre_txtFuelPrice.UniqueID), -1);
            //机建费
            decimal machPriceTo = Utils.GetDecimal(Utils.GetFormValue(this.fre_txtMachPrice.UniqueID), -1);

            //回程适用
            string toForDay = Utils.GetFormValue(this.fre_hideToForDay.UniqueID).TrimEnd(',');
            //回程独立日期
            DateTime? toSelfDate = Utils.GetDateTimeNullable(Utils.GetFormValue(this.fre_txtIndeDateBack.UniqueID));
            //回程参考面价
            decimal toReferPrice = Utils.GetDecimal(Utils.GetFormValue(this.fre_txtRefePriceBack.UniqueID));
            //回程参考扣率
            decimal toReferRate = Utils.GetDecimal(Utils.GetFormValue(this.fre_txtDeductionBack.UniqueID));
            //回程燃油费
            decimal toFuelPrice = Utils.GetDecimal(Utils.GetFormValue(this.fre_txtFuelPriceBack.UniqueID), -1);
            //回程机建费
            decimal toBuildPrice = Utils.GetDecimal(Utils.GetFormValue(this.fre_txtMachPriceBack.UniqueID), -1);

            //人数上限
            int peopleCount = Utils.GetInt(Utils.GetFormValue(this.fre_txtPeopleCount.UniqueID));
            //上班时间
            string toWorkDate = Utils.GetFormValue(this.fre_txtToWork.UniqueID);
            //下班时间
            string backHomeDate = Utils.GetFormValue(this.fre_txtBackHome.UniqueID);
            //备注
            string remark = Utils.GetFormValue(this.fre_txtRemark.UniqueID, 250);
            #endregion

            #region 数据验证
            if (PriceBeginTime == null || PriceEndTime == null)
            {
                if (String.IsNullOrEmpty(fromForDay) && indeDateTo == null)
                {
                    return "请选择一个去程日期";
                }
            }

            if (refePriceTo == 0)
            {
                return "请正确输入去程参考面价!";
            }
            if (deductionTo == 0)
            {
                return "请正确输入去程参考扣率!";
            }
            if (deductionTo >= 100 || deductionTo <= 0)
            {
                return "去程参考扣率必须在0-100之间!";
            }
            if (fuelPriceTo < 0)
            {
                return "请输入去程燃油费!";
            }
            if (machPriceTo < 0)
            {
                return "请输入去程机建费!";
            }



            if (rdoSingle != "1")
            {
                if (PriceBeginTime == null || PriceEndTime == null)
                {
                    if (String.IsNullOrEmpty(toForDay) && toSelfDate == null)
                    {
                        return "请选择一个回程日期";
                    }
                }
                if (toReferPrice == 0)
                {
                    return "请正确输入回程参考面价!";
                }
                if (toReferRate == 0)
                {
                    return "请正确输入回程参考扣率!";
                }
                if (toReferRate >= 100 || toReferRate<=0)
                {
                    return "回程参考扣率必须在0-100之间!";
                }
                if (toFuelPrice < 0)
                {
                    return "请输入回程燃油费!";
                }
                if (toBuildPrice < 0)
                {
                    return "请输入回程机建费!";
                }
            }




            #endregion

            int count = EyouSoft.BLL.TicketStructure.FreightBuyLog.CreateInstance().GetAvailableCount(SiteUserInfo.CompanyID, EyouSoft.Model.TicketStructure.RateType.团队散拼);
            #region 添加新的运价
            if (type == "Add")
            {
                string hideToLb = Utils.GetFormValue(this.fre_hideToLb.UniqueID);
                if (String.IsNullOrEmpty(hideToLb))
                {
                    return "请选择至少一个目的地";
                }

                string[] toList = hideToLb.TrimEnd(',').Split(',');



                //如果新增运价 状态是关闭的  那么不算入启用数
                if (riceEnable == "1")
                {
                    if (toList.Length > count)
                    {
                        //判断添加条数是否大于启用数
                        return "Count";
                    }
                }

                if (toList.Length > 0)
                {
                    IList<EyouSoft.Model.TicketStructure.TicketFreightInfo> list = new List<EyouSoft.Model.TicketStructure.TicketFreightInfo>();
                    for (int i = 0; i < toList.Length; i++)
                    {
                        string cityId = toList[i].Split('|')[0];
                        string cityName = toList[i].Split('|')[1].Split('-')[1];
                        EyouSoft.Model.TicketStructure.TicketFreightInfo model = new EyouSoft.Model.TicketStructure.TicketFreightInfo();
                        model.Company.ID = SiteUserInfo.CompanyID;

                        model.Company.CompanyName = SiteUserInfo.CompanyName;

                        model.OperatorId = SiteUserInfo.ID;

                        if (rdoSingle == "1")
                        {
                            model.FreightType = EyouSoft.Model.TicketStructure.FreightType.单程;
                        }
                        else
                        {
                            model.FreightType = EyouSoft.Model.TicketStructure.FreightType.来回程;
                        }

                        if (rdoInteger == "1")
                        {
                            model.ProductType = EyouSoft.Model.TicketStructure.ProductType.整团;

                        }
                        else
                        {
                            model.ProductType = EyouSoft.Model.TicketStructure.ProductType.散拼;
                        }

                        model.FlightId = Convert.ToInt32(companyId);
                        model.FlightName = companyName;

                        model.NoGadHomeCityId = Convert.ToInt32(StartId);
                        model.NoGadHomeCityIdName = startCityName;

                        model.NoGadDestCityId = Convert.ToInt32(cityId);
                        model.NoGadDestCityName = cityName;

                        if (riceEnable == "1")
                        {
                            model.IsEnabled = true;
                        }
                        else if (riceEnable == "0")
                        {
                            model.IsEnabled = false;
                        }
                        else if (riceEnable == "2")
                        {
                            model.IsEnabled = false;
                            model.IsExpired = true;
                        }

                        if (PnrYes == "1")
                        {
                            model.IsCheckPNR = true;
                        }
                        else
                        {
                            model.IsCheckPNR = false;
                        }

                        model.FreightStartDate = PriceBeginTime;
                        model.FreightEndDate = PriceEndTime;

                        //去程开始
                        model.FromForDay = fromForDay;
                        model.FromSelfDate = indeDateTo;
                        model.FromReferPrice = Convert.ToDecimal(refePriceTo.ToString("0.00"));
                        model.FromReferRate = Convert.ToDecimal(deductionTo.ToString("0.00"));
                        //计算去程结算价格
                        model.FromSetPrice = Convert.ToDecimal((model.FromReferPrice * (1 - model.FromReferRate / 100)).ToString("0.00"));
                        model.FromFuelPrice = Convert.ToDecimal(fuelPriceTo.ToString("0.00"));
                        model.FromBuildPrice = Convert.ToDecimal(machPriceTo.ToString("0.00"));

                        //回程开始
                        if (rdoSingle == "0")
                        {
                            model.ToForDay = toForDay;
                            model.ToSelfDate = toSelfDate;
                            model.ToReferPrice = toReferPrice;
                            model.ToReferRate = toReferRate;
                            //计算回程结算价格
                            model.ToSetPrice = Convert.ToDecimal((toReferPrice * (1 - model.ToReferRate / 100)).ToString("0.00"));
                            model.ToFuelPrice = toFuelPrice;
                            model.ToBuildPrice = toBuildPrice;
                        }

                        model.MaxPCount = peopleCount;
                        model.WorkStartTime = toWorkDate;
                        model.WorkEndTime = backHomeDate;
                        model.SupplierRemark = remark;
                        //添加运价日期
                        model.IssueTime = DateTime.Now;

                        //最后修改日期
                        model.LastUpdateDate = DateTime.Now;

                        model.RateType = EyouSoft.Model.TicketStructure.RateType.团队散拼;

                        model.BuyType = EyouSoft.Model.TicketStructure.PackageTypes.常规;

                        //添加到集合
                        list.Add(model);

                    }
                    bool result = IBll.Add(list);
                    if (result)
                    {
                        return "AddOk";
                    }
                }
            }
            #endregion

            #region 修改原来运价
            if (type == "Update")
            {
                string id = Utils.GetFormValue(this.fre_hideId.UniqueID);
                EyouSoft.Model.TicketStructure.TicketFreightInfo model = EyouSoft.BLL.TicketStructure.TicketFreightInfo.CreateInstance().GetModel(id);
                if (model != null)
                {

                    ////如果新增运价 状态是关闭的  那么不算入启用数
                    //bool isGq = true;
                    //if (riceEnable == "1" && model.IsEnabled == false)
                    //{
                    //    if (1 > count && Convert.ToInt32(model.BuyType) == 1)
                    //    {
                    //        //判断添加条数是否大于启用数
                    //        return "Count";
                    //    }
                    //    else
                    //    {
                    //        //是否过期
                    //        if (model.FreightBuyId.Trim() != "")
                    //        {
                    //            isGq = EyouSoft.BLL.TicketStructure.FreightBuyLog.CreateInstance().SetAvailableByFreightState(model.FreightBuyId, true);

                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    if (model.FreightBuyId.Trim() != "")
                    //    {
                    //        isGq = EyouSoft.BLL.TicketStructure.FreightBuyLog.CreateInstance().SetAvailableByFreightState(model.FreightBuyId, false);
                    //    }
                    //}
                    //if (!isGq)
                    //{
                    //    return "该运价已过期!";
                    //}

                    if (rdoSingle == "1")
                    {
                        model.FreightType = EyouSoft.Model.TicketStructure.FreightType.单程;
                    }
                    else
                    {
                        model.FreightType = EyouSoft.Model.TicketStructure.FreightType.来回程;
                    }

                    if (rdoInteger == "1")
                    {
                        model.ProductType = EyouSoft.Model.TicketStructure.ProductType.整团;

                    }
                    else
                    {
                        model.ProductType = EyouSoft.Model.TicketStructure.ProductType.散拼;
                    }

                    if (Convert.ToInt32(model.BuyType) == 1)
                    {
                        model.FlightId = Convert.ToInt32(companyId);
                        model.FlightName = companyName;

                        model.NoGadHomeCityId = Convert.ToInt32(StartId);
                        model.NoGadHomeCityIdName = startCityName;

                        model.NoGadDestCityId = Utils.GetInt(Utils.GetFormValue(this.fre_ToLbByUpdate.UniqueID));
                        //获得城市名称
                        EyouSoft.Model.TicketStructure.TicketSeattle DestCityModel = EyouSoft.BLL.TicketStructure.TicketSeattle.CreateInstance().GetTicketSeattleById(model.NoGadDestCityId);
                        if (DestCityModel != null)
                            model.NoGadDestCityName = DestCityModel.Seattle;

                    }
                    if (riceEnable == "1")
                    {
                        model.IsEnabled = true;
                    }
                    else if (riceEnable == "0")
                    {
                        model.IsEnabled = false;
                    }
                    else if (riceEnable == "2")
                    {
                        model.IsEnabled = false;
                        model.IsExpired = true;
                    }

                    if (PnrYes == "1")
                    {
                        model.IsCheckPNR = true;
                    }
                    else
                    {
                        model.IsCheckPNR = false;
                    }

                    model.FreightStartDate = PriceBeginTime;
                    model.FreightEndDate = PriceEndTime;

                    //去程开始
                    model.FromForDay = fromForDay;
                    model.FromSelfDate = indeDateTo;
                    model.FromReferPrice = refePriceTo;
                    model.FromReferRate = deductionTo;

                    //扣率价格
                    decimal fromRatePrice = Convert.ToDecimal((model.FromReferPrice * model.FromReferRate / 100).ToString("0.00"));
                    if (fromRatePrice < 0.01M)
                    {
                        fromRatePrice = 0;
                    }

                    //计算去程结算价格
                    model.FromSetPrice = Convert.ToDecimal((model.FromReferPrice - fromRatePrice).ToString("0.00"));
                    model.FromFuelPrice = fuelPriceTo;
                    model.FromBuildPrice = machPriceTo;

                    //回程开始
                    if (rdoSingle == "0")
                    {
                        model.ToForDay = toForDay;
                        model.ToSelfDate = toSelfDate;
                        model.ToReferPrice = toReferPrice;
                        model.ToReferRate = toReferRate;

                        //扣率价格
                        decimal toRatePrice = Convert.ToDecimal((toReferPrice * toReferRate / 100).ToString("0.00"));
                        if (toRatePrice < 0.01M)
                        {
                            toRatePrice = 0;
                        }
                        //计算回程结算价格
                        model.ToSetPrice = Convert.ToDecimal((toReferPrice - toRatePrice).ToString("0.00"));
                        model.ToFuelPrice = toFuelPrice;
                        model.ToBuildPrice = toBuildPrice;
                    }

                    model.MaxPCount = peopleCount;
                    model.WorkStartTime = toWorkDate;
                    model.WorkEndTime = backHomeDate;
                    model.SupplierRemark = remark;
                    //最后修改日期
                    model.LastUpdateDate = DateTime.Now;

                    bool result = EyouSoft.BLL.TicketStructure.TicketFreightInfo.CreateInstance().Update(model);
                    if (result)
                    {
                        return "UpdateOk";
                    }
                }

            }
            #endregion
            return "error";
        }
        #endregion

        #region 是否签约
        /// <summary>
        /// 判断登录用户是否签约
        /// </summary>
        /// <returns></returns>
        protected bool IsJoin()
        {
            bool isJoin = false;
            //判断用户绑定的支付帐户信息 是否已经成功加入支付圈
            EyouSoft.IBLL.TicketStructure.ITicketCompanyAccount bll =
                EyouSoft.BLL.TicketStructure.TicketCompanyAccount.CreateInstance();

            //当前只能判断支付宝接口和财付通接口的帐户
            IList<EyouSoft.Model.TicketStructure.TicketCompanyAccount> accountList =
                bll.GetTicketCompanyAccountList(SiteUserInfo.CompanyID);
            if (accountList != null && accountList.Count > 0)
            {
                foreach (EyouSoft.Model.TicketStructure.TicketCompanyAccount account in accountList)
                {
                    if (account.InterfaceType == EyouSoft.Model.TicketStructure.TicketAccountType.支付宝)
                    {
                        if (account.IsSign)//已经签约
                        {
                            isJoin = true;
                            continue;
                        }
                    }
                    else if (account.InterfaceType == EyouSoft.Model.TicketStructure.TicketAccountType.财付通)
                    {
                        if (account.IsSign)//已经建立委托退款关系
                        {
                            isJoin = true;
                            continue;
                        }
                    }
                }
            }
            return isJoin;
        }
        #endregion
    }
}
