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
namespace SiteOperationsCenter.HotelManagement
{
    /// <summary>
    /// 添加团队订单回复
    /// 2010-12-10
    /// 袁惠
    /// </summary>
    public partial class AddRever : EyouSoft.Common.Control.YunYingPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if(!CheckMasterGrant(YuYingPermission.酒店后台管理_团队订单管理))
                {
                    Utils.ResponseNoPermit(YuYingPermission.酒店后台管理_团队订单管理, true);
                    return;
                }
                string tid=Utils.GetQueryStringValue("tid");
                if (Utils.GetQueryStringValue("method") == "save")//保存回复
                {
                    EyouSoft.Model.HotelStructure.HotelTourCustomsAsk askModel = new EyouSoft.Model.HotelStructure.HotelTourCustomsAsk();
                    askModel.AskContent =Server.HtmlDecode(Utils.GetQueryStringValue("content"));
                    askModel.AskName = MasterUserInfo.UserName;
                    askModel.AskTime = DateTime.Now.ToString();
                    askModel.TourOrderID =Utils.GetInt(tid);
                    if (EyouSoft.BLL.HotelStructure.HotelTourCustoms.CreateInstance().AddAsk(askModel)) //添加回复
                    {
                        Utils.ResponseMeg(true, "操作成功！");
                    }
                    else
                    {
                        Utils.ResponseMeg(false, "操作失败！");
                    }
                }
                else
                {
                    txtDate.Value = DateTime.Now.ToString("yyyy-MM-dd");  //初始化显示值
                    txtLinkPerson.Value = MasterUserInfo.ContactName;
                    hdTid.Value = tid;
                }
            }
        }
    }
}
