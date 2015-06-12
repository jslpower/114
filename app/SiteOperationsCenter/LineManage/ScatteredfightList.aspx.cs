using System;
using System.Collections;
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
using EyouSoft.Common;
using EyouSoft.Model.SystemStructure;
using System.Collections.Generic;
using System.Text;
using EyouSoft.IBLL.SystemStructure;
using EyouSoft.Model.CompanyStructure;
using EyouSoft.Model.NewTourStructure;

namespace SiteOperationsCenter.LineManage
{
    /// <summary>
    /// 运营后台 散拼计划管理 散拼列表的查询删除
    /// 蔡永辉 2011-12-20
    /// </summary>
    public partial class ScatteredfightList : EyouSoft.Common.Control.YunYingPage
    {
        /// <summary>
        ///  分页
        /// </summary>
        protected int currentPage = 0;
        /// <summary>
        /// 该页面权限
        /// </summary>
        protected bool IsGrantUpdate = true,IsGrantDelete = true;
        protected void Page_Load(object sender, EventArgs e)
        {
            currentPage = Utils.GetInt(Request.QueryString["Page"]);
            if (!IsPostBack)
            {
                BindThemId();
                BindDropDownList();
                if (Request.QueryString["action"] != null && !string.IsNullOrEmpty(Request.QueryString["action"]))
                {
                    switch (Utils.GetQueryStringValue("action"))
                    {
                        case "changeState"://状态设置：客满，停收，正常
                            ChangeState();
                            break;
                        case "setTourMarkerNote"://团队类型设置
                            SetTourMarkerNote();
                            break;
                        case "GetLineByType"://获取线路根据类型
                            GetLineByType();
                            break;
                        case "GetCompanyByLine"://获取专线商根据线路
                            GetCompanyByLine();
                            break;
                        case "BatchUpdate":
                            BatchUpdate();
                            break;
                    }
                    return;
                }
            }
        }


        #region 获取样式
        protected string GetClass(string statue)
        {
            string strclass = "";
            switch (statue)
            {
                case "成团":
                    strclass = "chengtuan";
                    break;

                case "客满":
                    strclass = "keman";
                    break;

                case "收客":
                    strclass = "zhengc";
                    break;

                case "停收":
                    strclass = "tings";
                    break;
            }
            return strclass;

        }

        #endregion

        #region 初始化收客状态
        protected string GetTourMarkerNote()
        {
            StringBuilder str = new StringBuilder();
            List<EnumObj> listenum = EnumObj.GetList(typeof(PowderTourStatus));
            foreach (EnumObj model in listenum)
            {
                str.AppendFormat("<a href=\"#\" onclick=\"ScatteredfightManage.changeState({0})\" class=\"{1}\">{2}</a> &nbsp;", Convert.ToInt32(model.Value), GetClass(model.Text), model.Text);
            }
            return str.ToString();
        }
        #endregion

        #region 初始化推荐类型
        protected string GetTeamType()
        {
            StringBuilder str = new StringBuilder();
            List<EnumObj> listenum = EnumObj.GetList(typeof(RecommendType));
            foreach (EnumObj model in listenum)
            {
                if (model.Value == "1")

                    str.Append("<a class=\"nostate\" href=\"javascript:void(0)\" onclick=\"ScatteredfightManage.saveTourMarkerNote(0)\">取消设置</a>&nbsp;");
                else
                    str.AppendFormat("<a href=\"#\" class=\"state{0}\" onclick=\"ScatteredfightManage.saveTourMarkerNote({0})\">{1}</a>&nbsp;", Convert.ToInt32(model.Value) - 1, model.Text);
            }
            return str.ToString();
        }
        #endregion

        #region  国内长线
        protected string Bindstrgc()
        {
            StringBuilder strgc = new StringBuilder();
            IList<SysArea> listgc = EyouSoft.BLL.SystemStructure.SysArea.CreateInstance().GetSysAreaList(AreaType.国内长线);
            foreach (SysArea modelgn in listgc)
            {
                strgc.AppendFormat(" <a href=\"javascript:void(0)\" class=\"classLine\" onclick=\"ScatteredfightManage.OnSearchByAreaId(this,{0})\" title=\"{2}\">{1}</a>", modelgn.AreaId, Utils.GetText2(modelgn.AreaName, 6, false), modelgn.AreaName);
            }
            return strgc.ToString();
        }

        #endregion

        #region 国内短线
        protected string Bindstrgd()
        {
            StringBuilder strgd = new StringBuilder();
            IList<SysArea> listgd = EyouSoft.BLL.SystemStructure.SysArea.CreateInstance().GetSysAreaList(AreaType.国内短线);
            foreach (SysArea modelgd in listgd)
            {
                strgd.AppendFormat(" <a href=\"javascript:void(0)\" class=\"classLine\" onclick=\"ScatteredfightManage.OnSearchByAreaId(this,{0})\" title=\"{2}\">{1}</a>", modelgd.AreaId, Utils.GetText2(modelgd.AreaName, 6, false), modelgd.AreaName);
            }
            return strgd.ToString();
        }

        #endregion

        #region 国际线
        protected string Bindstrgj()
        {
            StringBuilder strgj = new StringBuilder();
            IList<SysArea> listgj = EyouSoft.BLL.SystemStructure.SysArea.CreateInstance().GetSysAreaList(AreaType.国际线);
            foreach (SysArea modelgj in listgj)
            {
                strgj.AppendFormat(" <a href=\"javascript:void(0)\" class=\"classLine\" onclick=\"ScatteredfightManage.OnSearchByAreaId(this,{0})\" title=\"{2}\">{1}</a>", modelgj.AreaId, Utils.GetText2(modelgj.AreaName, 6, false), modelgj.AreaName);
            }
            return strgj.ToString();
        }

        #endregion

        #region 批量修改团队行程
        protected void BatchUpdate()
        {
        }

        #endregion

        #region 绑定下来列表
        protected void BindDropDownList()
        {
            //国内国际周边
            foreach (EnumObj item in EnumObj.GetList(typeof(AreaType)))
            {
                if (Utils.GetInt(item.Value) < 3)
                {
                    this.dropLine.Items.Add(new ListItem(item.Text, item.Value));
                }
            }
            this.dropLine.Items.Insert(0, new ListItem("请选择", "-1"));

            this.dropLine1.Items.Insert(0, new ListItem("请选择", "0"));


            //线路类型
            foreach (EnumObj item in EnumObj.GetList(typeof(AreaType)))
            {
                if (Utils.GetInt(item.Value) < 3)
                {
                    this.DropSearchLine.Items.Add(new ListItem(item.Text, item.Value));
                }
            }
            this.DropSearchLine.Items.Insert(0, new ListItem("请选择", "-1"));
        }
        #endregion

        #region 设置收客状态
        /// <summary>
        /// 设置收客状态
        /// </summary>
        private void ChangeState()
        {
            if (!IsGrantUpdate)
            {
                Response.Clear();
                Response.Write("[{isSuccess:false,ErrorMessage:'对不起，你当前登录的帐号没有权限执行该操作！'}]");
                Response.End();
            }
            PowderTourStatus TourState = new PowderTourStatus();
            switch (Utils.GetQueryStringValue("TourState"))
            {
                case "1":
                    TourState = PowderTourStatus.客满;
                    break;
                case "2":
                    TourState = PowderTourStatus.停收;
                    break;
                case "3":
                    TourState = PowderTourStatus.收客;
                    break;
                case "4":
                    TourState = PowderTourStatus.成团;
                    break;
            }
            string[] TourIDs = Utils.GetQueryStringValue("TourID").Trim('$').Split('$');
            bool isTrue = EyouSoft.BLL.NewTourStructure.BPowderList.CreateInstance().UpdateStatus(TourState, TourIDs);
            if (isTrue)
            {
                Response.Clear();
                Response.Write("1");
                Response.End();
            }
            else
            {
                Response.Clear();
                Response.Write("-1");
                Response.End();
            }
        }
        #endregion

        #region 设置类型
        /// <summary>
        /// 设置线路类型,即推广状态
        /// </summary>
        private void SetTourMarkerNote()
        {
            if (!IsGrantUpdate)
            {
                Response.Clear();
                Response.Write("[{isSuccess:false,ErrorMessage:'对不起，你当前登录的帐号没有权限执行该操作！'}]");
                Response.End();
            }
            RecommendType SpreadState = RecommendType.无;
            switch (Utils.GetQueryStringValue("TourMarkerNote"))
            {

                case "0"://无 
                    SpreadState = RecommendType.无;
                    break;
                case "1"://推荐 
                    SpreadState = RecommendType.推荐;
                    break;
                case "2"://特价 
                    SpreadState = RecommendType.特价;
                    break;
                case "3"://豪华 
                    SpreadState = RecommendType.豪华;
                    break;
                case "4"://热门  
                    SpreadState = RecommendType.热门;
                    break;
                case "5"://新品  
                    SpreadState = RecommendType.新品;
                    break;
                case "6"://纯玩  
                    SpreadState = RecommendType.纯玩;
                    break;
                case "7"://经典  
                    SpreadState = RecommendType.经典;
                    break;
            }
            string TemplateTourID = Utils.GetQueryStringValue("TemplateTourID");
            EyouSoft.IBLL.NewTourStructure.IPowderList Ibll = EyouSoft.BLL.NewTourStructure.BPowderList.CreateInstance();
            if (TemplateTourID.EndsWith("$"))
                TemplateTourID = TemplateTourID.TrimEnd('$');
            bool isTrue = Ibll.UpdatePowderRecommend(SpreadState, TemplateTourID.Split('$'));
            Ibll = null;
            if (isTrue)
            {
                Response.Clear();
                Response.Write("1");
                Response.End();
            }
        }

        #endregion

        #region 根据专线类型获取专线
        protected void GetLineByType()
        {
            string argument = Utils.GetQueryStringValue("argument");
            ISysArea bll = EyouSoft.BLL.SystemStructure.SysArea.CreateInstance();
            int type = Utils.GetInt(argument, 0);
            IList<SysArea> list = bll.GetSysAreaList((AreaType)type);
            StringBuilder strb1 = new StringBuilder("{tolist:[");
            foreach (SysArea item in list)
            {
                strb1.Append("{\"AreaId\":\"" + item.AreaId + "\",\"AreaName\":\"" + item.AreaName + "\"},");
            }
            Response.Clear();
            Response.Write(strb1.ToString().TrimEnd(',')+"]}");
            Response.End();
        }

        #endregion

        #region 获取专线商根据线路
        protected void GetCompanyByLine()
        {
            int areaid = Utils.GetInt(Utils.GetQueryStringValue("argument"));
            QueryNewCompany Searchmodel = new QueryNewCompany();
            if (areaid != 0)
                Searchmodel.AreaId = areaid;
            IList<CompanyDetailInfo> listCompany = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetAllCompany(0, Searchmodel);
            StringBuilder strb = new StringBuilder("{tolist:[");
            foreach (Company item in listCompany)
            {
                strb.Append("{\"ID\":\"" + item.ID + "\",\"CompanyName\":\"" + item.CompanyName + "\"},");
            }
            Response.Clear();
            Response.Write(strb.ToString().TrimEnd(',') + "]}");
            Response.End();
        }

        #endregion

        #region 绑定主题
        protected string BindThemId()
        {
            StringBuilder strchb = new StringBuilder("");
            foreach (SysField item in EyouSoft.BLL.SystemStructure.SysField.CreateInstance().GetSysFieldList(SysFieldType.线路主题))
            {
                if (item != null)
                {
                    strchb.Append(string.Format("<a href=\"javascript:void(0)\" onclick=\"ScatteredfightManage.OnSearchByThemeid(this,{0})\">{1}</a>&nbsp;&nbsp;", item.FieldId, item.FieldName));
                }
            }
            return strchb.ToString();
        }
        #endregion
    }
}
