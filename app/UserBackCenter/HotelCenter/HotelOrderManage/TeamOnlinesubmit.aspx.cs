using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Function;

namespace UserBackCenter.HotelCenter.HotelOrderManage
{
    /// <summary>
    /// 团队房在线申请
    /// create：lixh datetime：2010-12-07
    /// 修改人：徐从栎
    /// 修改时间：2011-12-20
    /// 修改说明：改界面
    /// </summary>    
    public partial class TeamOnlinesubmit : EyouSoft.Common.Control.BackPage
    {
        #region   变量
        /// <summary>
        /// 采购商编号
        /// </summary>
        protected string CompanyID = "0";
        #endregion

        #region 页面加载        
        protected void Page_Load(object sender, EventArgs e)
        {
            CompanyID = this.SiteUserInfo.CompanyID;
            
            if (Utils.GetInt(Request.QueryString["issave"]) == 1)   //保存提交信息
            { 
                this.Save();
            }
            else
            {
                if (!this.Page.IsPostBack) //页面第一次加载
                {
                    //权限判断
                    if (this.SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.其他采购商) && this.SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.组团))
                    {
                        Response.Clear();
                        Response.Write("对不起，您没有权限");
                        Response.End();
                        return;
                    }
                    this.Initdestination();
                    this.InitHotelCity();
                }
            }
        }
        #endregion

        #region 酒店所在目的地        
        /// <summary>
        /// 绑定酒店所在目的地
        /// </summary>
        protected void InitHotelCity()
        {
            var s = "TeamOnlinesubmitAC.CityList={0}";
            IList<EyouSoft.Model.HotelStructure.HotelCity> Model_HotelCity = EyouSoft.BLL.HotelStructure.HotelCity.CreateInstance().GetList(); //获取所有的城市信息列表

            this.tos_destination.Items.Clear();  //清空下拉框项
            if (Model_HotelCity != null && Model_HotelCity.Count > 0) //城市列表不为空
            {
                this.tos_destination.Items.Add(new ListItem("--请选择城市--", "0"));
                for (int i = 0; i < Model_HotelCity.Count; i++)
                {
                    ListItem list = new ListItem();
                    list.Value = Model_HotelCity[i].CityCode;
                    list.Text = Model_HotelCity[i].Spelling + Model_HotelCity[i].CityName + Model_HotelCity[i].CityCode;
                    this.tos_destination.Items.Add(list);
                }
            }

            this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), string.Format(s, Newtonsoft.Json.JsonConvert.SerializeObject(Model_HotelCity)), true);
        }
        #endregion

        #region 绑定酒店星级 酒店房型 旅客类型 团队类型        
        /// <summary>
        /// 绑定团队预算提交信息
        /// </summary>
        protected void Initdestination()
        {
            //清空下拉框项
            this.tos_starrequirement.Items.Clear();
            List<EnumObj> Model_HotelRankEnum = EnumObj.GetList(typeof(EyouSoft.HotelBI.HotelRankEnum)); //绑定酒店星级
            
            if (Model_HotelRankEnum != null && Model_HotelRankEnum.Count > 0)
            {
                for (int i = 0; i < Model_HotelRankEnum.Count; i++)
                {
                    ListItem list_HotelRank = new ListItem();
                    switch (Model_HotelRankEnum[i].Value)  //类型转换 将相应的枚举值转换为文字
                    {
                        case "0": list_HotelRank.Text = "未选择星级"; break;
                        case "1": list_HotelRank.Text = "一星"; break;
                        case "2": list_HotelRank.Text = "二星"; break;
                        case "3": list_HotelRank.Text = "三星"; break;
                        case "4": list_HotelRank.Text = "四星"; break;
                        case "5": list_HotelRank.Text = "五星"; break;
                        case "6": list_HotelRank.Text = "准一星"; break;
                        case "7": list_HotelRank.Text = "准二星"; break;
                        case "8": list_HotelRank.Text = "准三星"; break;
                        case "9": list_HotelRank.Text = "准四星"; break;
                        default: list_HotelRank.Text = "准五星"; break;
                    }
                    list_HotelRank.Value = Model_HotelRankEnum[i].Value;
                    this.tos_starrequirement.Items.Add(list_HotelRank);
                }
            }


            //绑定旅客类型
            this.tos_visitorstype.Items.Clear();
            List<EnumObj> Model_visitorstype = EnumObj.GetList(typeof(EyouSoft.HotelBI.HBEGuestTypeIndicator));
            if (Model_visitorstype != null && Model_visitorstype.Count > 0)
            {
                for (int i = 0; i < Model_visitorstype.Count; i++)
                {
                    ListItem ListVisitorstype = new ListItem();
                    switch (Model_visitorstype[i].Value)
                    {
                        case "0": ListVisitorstype.Text = "内宾"; break;
                        default: ListVisitorstype.Text = "外宾"; break;
                    }
                    ListVisitorstype.Value = Model_visitorstype[i].Value;
                    this.tos_visitorstype.Items.Add(ListVisitorstype);
                }
            }

            //绑定团队类型
            this.tos_teamtype.Items.Clear();
            this.tos_teamtype.DataSource = EnumObj.GetList(typeof(EyouSoft.Model.HotelStructure.TourTypeList));
            this.tos_teamtype.DataTextField = "Text";
            this.tos_teamtype.DataValueField = "Value";
            this.tos_teamtype.DataBind();
        }
        #endregion

        #region 团队预算信息提交
        /// <summary>
        /// 保存提交信息
        /// </summary>
        protected void Save()
        {
            bool result = false; //验证是否添加成功
            EyouSoft.Model.HotelStructure.HotelTourCustoms Model_TotelTourCustoms = new EyouSoft.Model.HotelStructure.HotelTourCustoms();
            EyouSoft.BLL.HotelStructure.HotelTourCustoms Bll_TotelTourCustoms = new EyouSoft.BLL.HotelStructure.HotelTourCustoms();
            Model_TotelTourCustoms.CompanyId = CompanyID;//公司编号

            if (string.IsNullOrEmpty(Utils.GetFormValue("startDateTime")))
            {
                SetErrorMsg(false, "请填写入住时间");
                return;
            }

            if (string.IsNullOrEmpty(Utils.GetFormValue("endDateTime")))
            {
                SetErrorMsg(false, "请填写离店时间");
                return;
            }

            if (Utils.GetFormValue(this.tos_destination.UniqueID) == "0")
            {
                SetErrorMsg(false, "请填写城市");
                return;
            }

            Model_TotelTourCustoms.LiveStartDate = Utils.GetDateTime(Utils.GetFormValue("startDateTime"));
            Model_TotelTourCustoms.LiveEndDate = Utils.GetDateTime(Utils.GetFormValue("endDateTime"));
            Model_TotelTourCustoms.CityCode = Utils.GetFormValue(this.tos_destination.UniqueID);
            Model_TotelTourCustoms.LocationAsk = Utils.GetString(Utils.GetFormValue("tos_statusrequirement"), ""); //地理位置要求            
            Model_TotelTourCustoms.HotelStar = (EyouSoft.HotelBI.HotelRankEnum)Utils.GetInt(Utils.GetFormValue(tos_starrequirement.UniqueID).Trim());  //星级要求                                
            Model_TotelTourCustoms.RoomAsk = Utils.GetString(Utils.GetFormValue("tos_roomtyperequirements"), "");    //房型要求 
            Model_TotelTourCustoms.GuestType = (EyouSoft.HotelBI.HBEGuestTypeIndicator)Utils.GetInt(Utils.GetFormValue(this.tos_visitorstype.UniqueID).Trim());    //宾客类型
            Model_TotelTourCustoms.TourType = (EyouSoft.Model.HotelStructure.TourTypeList)Utils.GetInt(Utils.GetFormValue(this.tos_teamtype.UniqueID).Trim());         //团队类型
            Model_TotelTourCustoms.OtherRemark = Utils.GetString(Utils.GetFormValue("textarea"), "");   //其它要求           

            if (Utils.GetInt(Utils.GetFormValue("tos_result")) == 1) //有指定酒店
            {
                if (Utils.GetFormValue("tos_HotelName") == "请填写酒店官方名称")
                {
                    SetErrorMsg(false, "请填写酒店官方名称");
                    return;
                }
                else
                {
                    Model_TotelTourCustoms.HotelName = Utils.GetString(Utils.GetFormValue("tos_HotelName"), "");  //酒店名称
                }
            }
            else
                Model_TotelTourCustoms.HotelName = "";         //酒店名称为空  

            if (Utils.GetInt(Utils.GetFormValue("tos_roomnumber")) == 0) //团队预订的房间数量是0
            {
                SetErrorMsg(false, "请填写预订的房间数量!");
                return;
            }

            if (Utils.GetInt(Utils.GetFormValue("tos_roomnumber")) < 7)  //预订的房间数量小于7
            {
                SetErrorMsg(false, "至少要预订7间房间!");
                return;
            }
            Model_TotelTourCustoms.RoomCount = Utils.GetInt(Utils.GetFormValue("tos_roomnumber"));  //房间数量


            if (Utils.GetInt(Utils.GetFormValue("tos_Number")) == 0) //入住的人数是0
            {
                SetErrorMsg(false, "请填写入住的人数!");
                return;
            }
            Model_TotelTourCustoms.PeopleCount = Utils.GetInt(Utils.GetFormValue("tos_Number"));  //入住人数

            if (Utils.GetDecimal(Utils.GetFormValue("tos_budget1"))==0)
            {
                SetErrorMsg(false, "请填写团队房预算最小数!");
                return;
            }
            Model_TotelTourCustoms.BudgetMin = Utils.GetDecimal(Utils.GetFormValue("tos_budget1")); //团房预算最小值

            if (Utils.GetDecimal(Utils.GetFormValue("tos_budget2")) == 0)
            {
                SetErrorMsg(false, "请填写团队房预算最大数!");
                return;
            }

            if (Utils.GetDecimal(Utils.GetFormValue("tos_budget1")) > Utils.GetDecimal(Utils.GetFormValue("tos_budget2")))
            {
                SetErrorMsg(false, "团队房预算最大数不能小于最小数!");
                return;
            }
           
            Model_TotelTourCustoms.BudgetMax = Utils.GetDecimal(Utils.GetFormValue("tos_budget2")); //团房预算最大值             

            result = Bll_TotelTourCustoms.Add(Model_TotelTourCustoms);  //执行添加的方法
            if (result)
                SetErrorMsg(true, "提交成功!");
            else
                SetErrorMsg(false, "提交失败");

            Model_TotelTourCustoms = null; //释放资源
            Bll_TotelTourCustoms = null;
        }
        #endregion

        #region 提示操作信息方法       
        /// <summary>
        /// 提示信息
        /// </summary>
        /// <param name="isSuccess">true 执行成功 flase 执行失败</param>
        /// <param name="msg">提示信息</param>
        private void SetErrorMsg(bool isSuccess, string msg)
        {
            var s = "{{isSuccess:{0},errMsg:'{1}'}}";
            Response.Clear();
            Response.Write(string.Format(s, isSuccess.ToString().ToLower(), msg));
            Response.End();
        }
        #endregion
    }
}
