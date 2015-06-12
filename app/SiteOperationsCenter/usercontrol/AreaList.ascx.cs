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
using EyouSoft.Common.Function;
using System.Collections.Generic;
using System.Text;

namespace SiteOperationsCenter.usercontrol
{
    /// <summary>
    /// 页面功能：国内长线和国内短线下的线路区域列表
    /// 开发人：杜桂云      开发时间：2010-07-07
    /// </summary>
    public partial class AreaList : System.Web.UI.UserControl
    {
        private int _typeid = 0;
        /// <summary>
        /// 公司类别编号
        /// </summary>
        public int TypeID
        {
            get { return _typeid; }
            set { _typeid = value; }
        }

        private string[] _getcheckvalues = null;
        /// <summary>
        /// 返回长线选中的线路区域编号
        /// </summary>
        public string[] GetCheckValues
        {
            get { return Utils.GetFormValues("cbkName"); }
            set { _getcheckvalues = value; }
        }

        private string[] _getShortckvalues = null;
        /// <summary>
        /// 返回短线选中的线路区域编号
        /// </summary>
        public string[] GetShortckvalues
        {
            get { return Utils.GetFormValues("cbkShortName"); }
            set { _getShortckvalues = value; }
        }

        #region 页面加载
        protected void Page_Load(object sender, EventArgs e)
        {
            InitData();
        }
        #endregion

        #region 绑定数据
        private void InitData()
        {
            IList<EyouSoft.Model.SystemStructure.SysArea> areaList = null;
            //取省份或城市编号
            //int EditeID = int.Parse(Utils.InputText(Request.QueryString["EditeID"]));

            if (TypeID == 0)
            {
                areaList = EyouSoft.BLL.SystemStructure.SysArea.CreateInstance().GetSysAreaList(EyouSoft.Model.SystemStructure.AreaType.国内长线);
                if (areaList != null && areaList.Count > 0)
                {
                    this.dalLongList.DataSource = areaList;
                    this.dalLongList.DataBind();
                }
            }
            else
            {
                areaList = EyouSoft.BLL.SystemStructure.SysArea.CreateInstance().GetSysAreaList(EyouSoft.Model.SystemStructure.AreaType.国内短线);
                if (areaList != null && areaList.Count > 0)
                {
                    this.dalShortList.DataSource = areaList;
                    this.dalShortList.DataBind();
                }
            }
            //释放资源
            areaList = null;
        }
        #endregion
    }
}