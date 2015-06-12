using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace SiteOperationsCenter.LineManage
{
    
    public partial class AreaSelect : System.Web.UI.Page
    {
        protected int count;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataInit();
            }
        }

        /// <summary>
        /// 页面初始化方法
        /// </summary>
        private void DataInit()
        {
            int lineType = Utils.GetInt(Utils.GetQueryStringValue("typeID"));

            EyouSoft.Model.SystemStructure.AreaType areaType = (EyouSoft.Model.SystemStructure.AreaType)lineType;
            IList<EyouSoft.Model.SystemStructure.SysArea> list = EyouSoft.BLL.SystemStructure.SysArea.CreateInstance().GetSysAreaList(areaType);
            if (list != null)
            {
                this.rptQueryTour.DataSource = list;
                this.rptQueryTour.DataBind();
                count = list.Count;
            }
            list = null;

        }
    }
}
