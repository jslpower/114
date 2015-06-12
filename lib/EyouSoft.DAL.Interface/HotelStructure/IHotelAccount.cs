using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.HotelStructure
{
    /// <summary>
    /// 酒店系统-结算帐号IDAL
    /// </summary>
    /// 创建人：luofx 2010-12-1
    public interface IHotelAccount
    {
        /// <summary>
        /// 添加结算帐号信息
        /// </summary>
        /// <param name="model">结算帐号信息实体</param>
        /// <returns></returns>
        bool Add(EyouSoft.Model.HotelStructure.HotelAccount model);
        /// <summary>
        /// 修改结算帐号信息
        /// </summary>
        /// <param name="model">结算帐号信息实体</param>
        /// <returns></returns>
        bool Update(EyouSoft.Model.HotelStructure.HotelAccount model);
        /// <summary>
        /// 获取结算帐号信息实体
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <returns></returns>
        EyouSoft.Model.HotelStructure.HotelAccount GetModel(string CompanyId);
        /// <summary>
        /// 判断是否存在账户
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <returns></returns>
        bool IsExist(string CompanyId);
    }
}
