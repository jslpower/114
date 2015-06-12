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

namespace SiteOperationsCenter.AdManagement
{
    public partial class AjaxAdSort :EyouSoft.Common.Control.YunYingPage
    {
        /// <summary>
        /// 广告管理：Ajax页面
        /// 功能：执行删除和排序操作
        /// 创建人：袁惠
        /// 创建时间： 2010-7-22  
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int id = Utils.GetInt(HttpContext.Current.Request.QueryString["Id"],-1);
                string type =HttpContext.Current.Request.QueryString["Type"];             
                int result=0;
                if (id != -1)
                {
                    if (type == "Sort")
                    {
                        if (!CheckMasterGrant(YuYingPermission.同业114广告_管理该栏目))
                        {
                            Utils.ResponseNoPermit(YuYingPermission.同业114广告_修改, true);
                            return;
                        }
                        int sort = Utils.GetInt(HttpContext.Current.Request.QueryString["sort"], -1);
                        string postion = Request.QueryString["postion"];
                        int cityid = Utils.GetInt(HttpContext.Current.Request.QueryString["city"], -1);
                        int provinceid = Utils.GetInt(HttpContext.Current.Request.QueryString["province"], -1);
                        int relation = -1;
                        if (cityid != -1 && provinceid != -1)
                        {
                            relation = cityid;
                        }
                        else if (provinceid != -1 && cityid == -1)
                        {
                            relation = provinceid;
                        }
                        if (sort == -1 && postion == "" && cityid == -1 && provinceid == -1 && relation == -1)
                        {
                            result = 0;
                        }
                        else
                        {
                            EyouSoft.Model.AdvStructure.AdvInfo info = EyouSoft.BLL.AdvStructure.Adv.CreateInstance().GetAdvInfo(id);
                            if (info != null)
                            {
                                if (EyouSoft.BLL.AdvStructure.Adv.CreateInstance().SetAdvSort(id, (EyouSoft.Model.AdvStructure.AdvPosition)Enum.Parse(typeof(EyouSoft.Model.AdvStructure.AdvPosition),postion), relation, sort))
                                {
                                    result = 2;  //成功
                                }
                                else
                                {
                                    result = 1;  //失败
                                }
                            }
                            else
                            {
                                result = -1;
                            }
                        }
                    }
                    else
                    {
                        if (!CheckMasterGrant(YuYingPermission.同业114广告_管理该栏目))
                        {
                            Utils.ResponseNoPermit(YuYingPermission.同业114广告_删除, true);
                            return;
                        }
                        if (EyouSoft.BLL.AdvStructure.Adv.CreateInstance().DeleteAdv(id))
                            result = 2;
                        else
                            result = 1;
                    }
                }
                else
                {
                    result=-1;
                }
                HttpContext.Current.Response.Write(result.ToString());
                HttpContext.Current.Response.End();
            }
        }
    }
}
