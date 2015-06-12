using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Common.URLREWRITE
{
    /// <summary>
    /// 分站
    /// </summary>
    public class SubStation
    {
        /// <summary>
        /// 分站URL改写
        /// </summary>
        public static string CityUrlRewrite(int cityId)
        {
            if (cityId == 362)
            {
                return Domain.UserPublicCenter + "/hangzhou";
            }
            EyouSoft.IBLL.SystemStructure.ISysCity Citybll = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance();

            EyouSoft.Model.SystemStructure.SysCity model = Citybll.GetSysCityModel(cityId);

            if (model != null)
            {
                
                return Domain.UserPublicCenter + "/" + model.RewriteCode;
            }
            else{
                return Domain.UserPublicCenter + "/";
            }
        }

        public static string CityUrlRewrite(EyouSoft.Model.SystemStructure.SysCity cityModel)
        {
            string url = string.Empty;

            if (cityModel.CityId == 362)
            {
                url= Domain.UserPublicCenter + "/hangzhou";
                return url;
            }

            if (cityModel != null)
            {
                return Domain.UserPublicCenter + "/" + cityModel.RewriteCode;
            }
            else
            {
                return Domain.UserPublicCenter + "/";
            }

            return url;
        }
    }
}
