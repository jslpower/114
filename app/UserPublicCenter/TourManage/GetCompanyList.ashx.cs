using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Text;
namespace UserPublicCenter.TourManage
{
    /// <summary>
    /// 平台线路按批发商搜索
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class GetCompanyList : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int CityId = EyouSoft.Common.Utils.GetInt(context.Request.QueryString["CityId"]);
            int TourAreaId = EyouSoft.Common.Utils.GetInt(context.Request.QueryString["TourAreaId"]);
            string keys = EyouSoft.Common.Utils.InputText(context.Request.QueryString["SeachKey"]);
            context.Response.Write(this.GetAllCompanyList(CityId, TourAreaId,keys));
        }

        /// <summary>
        /// 获的销售城市/线路区域下的批发商列表
        /// </summary>
        /// <param name="CityId">销售城市ID</param>
        /// <param name="TourAreaId">线路区域ID</param>
        protected string  GetAllCompanyList( int CityId,int TourAreaId ,string keys)
        {
            int recordCount = 0;
            EyouSoft.Model.CompanyStructure.QueryParamsCompanyBase SearchModel = new EyouSoft.Model.CompanyStructure.QueryParamsCompanyBase();
            SearchModel.CityId = CityId;
            SearchModel.AreaId = TourAreaId;
            SearchModel.CompanyName = keys;
            IList<EyouSoft.Model.CompanyStructure.CompanyInfo>CompanyList= EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetListRouteAgency(SearchModel, 10, 1,ref recordCount);
            StringBuilder strAllCompany = new StringBuilder(); 
            if (CompanyList != null && CompanyList.Count > 0)
            {
                foreach(EyouSoft.Model.CompanyStructure.CompanyInfo item in CompanyList)
                {
                    strAllCompany.Append(item.CompanyName +"\n");
                }
            }
            SearchModel = null;
            CompanyList = null;
            return strAllCompany.ToString();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
